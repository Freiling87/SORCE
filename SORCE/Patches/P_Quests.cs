using BepInEx.Logging;
using HarmonyLib;
using SORCE.BigQuests;
using SORCE.Logging;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(Quests))]
    public static class P_Quests
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName:nameof(Quests.AddBigQuestPoints), argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(InvItem), typeof(string) })]
        public static bool AddBigQuestPoints_Prefix(Agent myAgent, Agent otherAgent, InvItem myItem, string pointsType)
        {
            if (pointsType == nameof(ToiletTourist))
                myAgent.oma.bigQuestObjectCount++;

            return true;
        }
    }
}