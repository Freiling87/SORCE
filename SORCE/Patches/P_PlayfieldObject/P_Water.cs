using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_Features;
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

        /// <summary>
        /// ThePollutionSolution Poisoned Water Smoke particles
        /// </summary>
        /// <param name="item"></param>
        /// <param name="doClean"></param>
        /// <param name="myCauserAgent"></param>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Water.SpreadPoisonStart), argumentTypes: new[] { typeof(InvItem), typeof(bool), typeof(Agent) })]
        public static void SpreadPoisonStart_Postfix(InvItem item, bool doClean, Agent myCauserAgent, Water __instance)
        {
            if (GC.challenges.Contains(nameof(ThePollutionSolution)))
                foreach (Vector2 tile in __instance.waterTiles)
                    if (GC.percentChance(2))
                        GC.spawnerMain.SpawnParticleEffect(VParticleEffect.SmokePuffs, new Vector3(tile.x * 0.64f, tile.y * 0.64f, __instance.tr.position.z), 0f);
        }
    }
}
