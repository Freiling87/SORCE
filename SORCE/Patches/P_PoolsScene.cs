using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using System;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(PoolsScene))]
    class P_PoolsScene
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(PoolsScene.SpawnObjectReal), argumentTypes: new Type[] {typeof(string), typeof(GameObject), typeof(Vector3)} )]
        public static void SpawnObjectReal_Postfix(string objectRealName, GameObject objectRealPrefab, Vector3 spawnPosition)
        {
            // THIS IS JUST A TEST
            LevelGenTools.SpawnWreckagePileObject_Granular(
                new Vector2(spawnPosition.x, spawnPosition.y),
                VObject.AmmoDispenser,
                false,
                1,
                0.64f, 0.64f);
        }
    }
}
