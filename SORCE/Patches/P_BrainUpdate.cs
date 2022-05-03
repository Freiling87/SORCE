using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using SORCE.Traits;
using SORCE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(BrainUpdate))]
    public static class P_BrainUpdate
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Adapted copy of game code
		/// </summary>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(BrainUpdate.MyUpdate), argumentTypes: new Type[0] { })]
		public static bool MyUpdate_Prefix(BrainUpdate __instance, Agent ___agent)
		{
			// Don't log right here, this function is called constantly

			if (!___agent.dead && !___agent.fakeNotActiveStreaming && ___agent.brain.active && !GC.cinematic && // Outer vanilla gate conditions
				___agent.oma.hidden && Underdank.UnderdankActive && GC.serverPlayer && ___agent.isPlayer == 0 && ___agent.hiddenInObject is Manhole manhole) // Main vanilla gate conditions
			{
				logger.LogDebug("MyUpdate_Prefix");
				logger.LogDebug("Caught");

				foreach (Agent playerAgent in GC.playerAgentList)
				{
					if (___agent.agentName == VAgent.Thief)
					{
						if (playerAgent.statusEffects.hasTrait(nameof(UnderdankCitizen)))
							___agent.relationships.SetRel(playerAgent, VRelationship.Friendly);
						else if (playerAgent.statusEffects.hasTrait(nameof(UnderdankVIP)))
							___agent.relationships.SetRel(playerAgent, VRelationship.Loyal);
					}
					else if (___agent.agentName == VAgent.Cannibal)
                    {
						if (playerAgent.statusEffects.hasTrait(nameof(UnderdankCitizen)))
							___agent.relationships.SetRel(playerAgent, VRelationship.Neutral);
						else if (playerAgent.statusEffects.hasTrait(nameof(UnderdankVIP)))
							___agent.relationships.SetRel(playerAgent, VRelationship.Friendly);
					}
				}

				___agent.statusEffects.BecomeNotHidden();
				___agent.SetDefaultGoal(VGoal.WanderLevel);
				___agent.jumpDirection = Vector2.one;
				___agent.jumpSpeed = 8f;
				___agent.pathing = 1; 
				___agent.noEnforcerAlert = true;
				___agent.oma.mustBeGuilty = true;
				___agent.Jump();
				manhole.HoleAppear();

				return false;
			}

			return true;
		}
    }
}
