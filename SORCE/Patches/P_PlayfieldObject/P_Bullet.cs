using BepInEx.Logging;
using HarmonyLib;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Bullet))]
    public static class P_Bullet
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;


        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.BulletHitEffect), argumentTypes: new[] { typeof(GameObject) })]
        public static void BulletHitEffect_Postfix(GameObject hitObject, Bullet __instance)
        {
            if (Gunplay.ModBulletholes
                && Gunplay.bullets.Contains((int)__instance.bulletType) 
                //&& hitObject.CompareTag("Wall") // Might be redundant to "Front" in name 
                && hitObject.name.Contains("Front"))
            {
                Vector3 pos = new Vector3(
                    __instance.tr.position.x + Random.Range(-0.12f, 0.12f),
                    __instance.tr.position.y + Random.Range(-0.32f, -0.08f),
                    0);

                TileData tileData = GC.tileInfo.GetTileData(hitObject.transform.position);

                if (hitObject.name.Contains("Glass"))
                {
                    SpawnBulletHole(pos, wallMaterialType.Glass);
                    GC.audioHandler.Play(__instance, VAudioClip.WindowDamage);
                }
                else if (tileData.wallMaterialOffsetTop == 1365) // Is this seriosuly the only way to detect a Hedge wall??
                {
                    GC.spawnerMain.SpawnWreckage2(hitObject.transform.position, VObject.Bush, false);
                    GC.audioHandler.Play(__instance, VAudioClip.BushDestroy);
                }
                else
                    SpawnBulletHole(pos, wallMaterialType.Normal);
            }
        }

        /// <summary>
        /// No Bullet Lights
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.RealAwake), argumentTypes: new Type[0] { })]
        public static void RealAwake_Postfix(Bullet __instance)
        {
            if (Gunplay.ModGunLighting)
            {
                if (__instance.lightTemp != null)
                    __instance.lightTemp.fancyLightRenderer.enabled = false;
            }
        }

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.SetupBullet), argumentTypes: new Type[0] { })]
        public static void SetupBullet_Postfix(Bullet __instance) 
        {
            if (Gunplay.ModFastBullets &&
                Gunplay.bullets.Contains((int)__instance.bulletType))
            {
                __instance.tr.localScale = Vector3.one * 0.20f;
                __instance.speed = 27;
                // TODO: Change Collision Detection algos for bullets and wall to prevent noclipping
            }
        }

        public static void SpawnBulletHole(Vector3 pos, wallMaterialType wmt)
        {
            pos.z = 0.01f;
            GameObject gameObject = GC.spawnerMain.floorDecalPrefab.Spawn(pos);
            gameObject.layer = 5;
            // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
            // ~ ~ X X ~ X X X X  X  X                       X  X  X           X
            string sprite =
                wmt == wallMaterialType.Glass
                ? CSprite.BulletHoleGlass
                : CSprite.BulletHole; 

            gameObject.GetComponent<tk2dSprite>().SetSprite(sprite);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
            GC.floorDecalsList.Add(gameObject); // Hoping this will cause it to not stay over level // DW 
        } 
    }
} 
  