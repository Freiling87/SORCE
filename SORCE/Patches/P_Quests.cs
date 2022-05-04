using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.BigQuests;
using SORCE.Logging;
using SORCE.Patches.P_PlayfieldObject;
using System;
using System.Collections.Generic;
using System.Reflection;

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

		// This is to be called within BigQuestUpdate.
		// Haven't written the transpiler yet, as BQs aren't ready in RL yet.
		public static void BigQuestUpdate_InsertSequence(Agent agent)
		{
			if (agent.bigQuest == nameof(ToiletTourist))
			{
				List<int> chunksScored = new List<int>();
				List<int> chunksTotal = new List<int>();

				for (int j = 0; j < GC.objectRealListWithDestroyed.Count; j++)
				{
					ObjectReal objectReal = GC.objectRealListWithDestroyed[j];

					if (objectReal is Toilet toilet)
					{
						int curChunk = toilet.curChunk;

						if (!chunksTotal.Contains(curChunk))
							chunksTotal.Add(curChunk);

						if (toilet.GetHook<P_Toilet_Hook>().bigQuestShidded &&
							!chunksScored.Contains(curChunk))
                        {
							chunksScored.Add(curChunk);
						}
					}
				}

				agent.bigQuestObjectCountTemp = chunksScored.Count;
				agent.bigQuestObjectCountTotalTemp = chunksTotal.Count;
			}
		}
    }
}