﻿using BepInEx.Logging;
using HarmonyLib;
using SORCE.Localization;
using SORCE.Logging;
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

        public static bool GunplayRelit = Core.debugMode;
        public static bool RealisticBullets = Core.debugMode;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.BulletHitEffect), argumentTypes: new[] { typeof(GameObject) })]
        public static void BulletHitEffect_Postfix(GameObject hitObject, Bullet __instance)
        {
            logger.LogDebug(hitObject.name);
            // Need name for NON-MODDED glass wall to see if they differ

            if (GunplayRelit
                && bullets.Contains((int)__instance.bulletType) 
                && hitObject.CompareTag("Wall")
                && hitObject.name.Contains("Front")
                && __instance.originalSpawnerPos.y < hitObject.transform.position.y)
            {
                Vector3 pos = new Vector3(
                    __instance.tr.position.x + Random.Range(-0.16f, 0.16f),
                    __instance.tr.position.y + Random.Range(-0.30f, -0.04f),
                    0);

                if (hitObject.name.Contains("Glass"))
                {
                    SpawnBulletHole(pos, wallMaterialType.Glass);
                    GC.audioHandler.Play(hitObject.GetComponent<PlayfieldObject>(), VAudioClip.WindowDamage);
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
            if (GunplayRelit)
                __instance.lightTemp.fancyLightRenderer.enabled = false;
        }

        // TODO: Export to Gunplay mod
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.SetupBullet), argumentTypes: new Type[0] { })]
        public static void SetupBullet_Postfix(Bullet __instance)
        {
            if (bullets.Contains((int)__instance.bulletType)
                && RealisticBullets) 
            {
                __instance.tr.localScale = Vector3.one * 0.20f;
                __instance.speed = 27;
                // TODO: Change Collision Detection algos for bullets and wall to prevent noclipping
            }
        }
        public static readonly List<int> bullets = new List<int>()
        {
            1, 2, 19
        };

        public static void SpawnBulletHole(Vector3 pos, wallMaterialType wmt)
        {
            pos.z = 0.01f;
            GameObject gameObject = GC.spawnerMain.floorDecalPrefab.Spawn(pos);
            gameObject.layer = 5;
            // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 
            // ~ ~ X X ~ X X X X  X  X                       X  X  X           X
            string sprite =
                wmt == wallMaterialType.Glass
                ? CSprite.BulletHoleGlass
                : CSprite.BulletHole;

            gameObject.GetComponent<tk2dSprite>().SetSprite(CSprite.BulletHole);

            // GC.floorDecalsList.Add(gameObject); // Hoping this will cause it to not stay over level // DW 

            int num = Random.Range(0, 360);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, num);
        } 
    }
} 
  