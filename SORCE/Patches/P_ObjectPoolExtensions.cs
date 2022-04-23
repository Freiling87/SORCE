using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using SORCE.Patches.P_PlayfieldObject;
using UnityEngine;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(ObjectPool))]
    public static class P_ObjectPoolExtensions
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(ObjectPool.Spawn), argumentTypes: new[] {typeof(GameObject), typeof(Transform), typeof(Vector3), typeof(Quaternion) })]
        public static bool Spawn_Prefix(ref GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            if (P_Gun.ShootierGuns &&
                prefab == GC.spawnerMain.particleBulletHitPrefab)
            {
                //prefab = null;
                //prefab = GC.spawnerMain.particleObjectDestroyedPrefab; 
                //prefab = GC.spawnerMain.particleObjectDestroyedSmokePrefab;
                prefab = GC.spawnerMain.particleWallDestroyedPrefab;
            }

            return true;
        }
    }
}
