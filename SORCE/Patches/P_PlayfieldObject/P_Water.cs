using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Water))]
    class P_Water
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Water.SpreadPoisonStart), argumentTypes: new[] { typeof(InvItem), typeof(bool), typeof(Agent) })]
        public static void SpreadPoisonStart_Postfix(InvItem item, bool doClean, Agent myCauserAgent, Water __instance)
        {
            foreach (Vector2 tile in __instance.waterTiles)
                if (GC.percentChance(2))
                    GC.spawnerMain.SpawnParticleEffect(VParticleEffect.SmokePuffs, new Vector3(tile.x * 0.64f, tile.y * 0.64f, __instance.tr.position.z), 0f);
        }
    }
}
