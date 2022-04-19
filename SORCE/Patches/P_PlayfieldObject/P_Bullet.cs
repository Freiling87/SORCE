using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Lighting;
using SORCE.Localization;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static bool NoBulletLights;
        public static bool RealisticBullets = Core.debugMode;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.BulletHitEffect), argumentTypes: new[] { typeof(GameObject) })]
        public static void BulletHitEffect_Postfix(GameObject hitObject, Bullet __instance)
        {
            if (bullets.Contains((int)__instance.bulletType) 
                && hitObject.CompareTag("Wall"))
            {
                SpawnWallDecal(hitObject.transform.position);
            }
        }

        /// <summary>
        /// No Bullet Lights
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.RealAwake), argumentTypes: new Type[0] { })]
        public static void RealAwake_Postfix(Bullet __instance)
        {
            if (NoBulletLights)
                __instance.lightTemp.fancyLightRenderer.enabled = false;
        }

        // TODO: Export to Gunplay mod
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.SetupBullet), argumentTypes: new Type[0] { })]
        public static void SetupBullet_Postfix(Bullet __instance)
        {
            if (bullets.Contains((int)__instance.bulletType)
                && RealisticBullets) 
            {
                __instance.tr.localScale *= 0.20f;
                __instance.speed = 27;

                // Highest good     27
                // Lowest bad       28
            }
        }
        private static List<int> bullets = new List<int>()
        {
            1, 2, 19
        };

        public static void SpawnWallDecal(Vector3 pos)
        {
            Vector3 newPos = new Vector3(pos.x, pos.y, 0f); // Might be redundant
            GameObject gameObject = GC.spawnerMain.floorDecalPrefab.Spawn(newPos);
            gameObject.layer = 23;
            gameObject.GetComponent<tk2dSprite>().SetSprite(CSprite.BulletHole);

            //GC.floorDecalsList.Add(gameObject);

            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
} 
  