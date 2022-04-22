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

        //[HarmonyPostfix, HarmonyPatch(methodName: nameof(Bullet.BulletHitEffect), argumentTypes: new[] { typeof(GameObject) })]
        public static void BulletHitEffect_Postfix(GameObject hitObject, Bullet __instance)
        {
            if (bullets.Contains((int)__instance.bulletType) 
                && hitObject.CompareTag("Wall"))
            {
                SpawnBulletHole(__instance.transform.position);
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
                //__instance.speed = 27;

                // Highest good     27
                // Lowest bad       26
                // Suddenly, very low values are noclipping badly.
            }
        }
        private static readonly List<int> bullets = new List<int>()
        {
            1, 2, 19
        };

        public static void SpawnBulletHole(Vector3 pos)
        {
            pos.z = 0.01f;
            GameObject gameObject = GC.spawnerMain.floorDecalPrefab.Spawn(pos);
            gameObject.layer = 5;
            // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 
            // ~ ~ X X ~ X X X X  X  X                       X  X  X           X
            gameObject.GetComponent<tk2dSprite>().SetSprite(CSprite.BulletHole);

            // GC.floorDecalsList.Add(gameObject); // Hoping this will cause it to not stay over level // DW 

            int num = Random.Range(0, 360);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, num);
        } 
    }
} 
  