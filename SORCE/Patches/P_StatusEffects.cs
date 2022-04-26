using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using SORCE.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Ambient Light
		/// </summary>
		/// <param name="isDead"></param>
		[HarmonyPostfix, HarmonyPatch(methodName:nameof(StatusEffects.WerewolfTransformBack), argumentTypes: new[] { typeof(bool) })]
		public static void WerewolfTransformBack_Postfix(bool isDead)
		{
			GC.loadLevel.SetNormalLighting();
		}

		/// <summary>
		/// UnderdankCitizen
		/// </summary>
		/// <param name="hiddenInObject"></param>
		/// <param name="__instance"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(StatusEffects.BecomeHidden), argumentTypes: new[] { typeof(ObjectReal) })]
		private static void BecomeHidden_Postfix(ObjectReal hiddenInObject, StatusEffects __instance)
		{
			if (hiddenInObject is Manhole manhole
				&& (TraitManager.IsPlayerTraitActive<UnderdankCitizen>() || TraitManager.IsPlayerTraitActive<UnderdankVIP>()))
			{
				Agent agent = __instance.agent;

				__instance.BecomeNotHidden();
				agent.SetDefaultGoal(VGoal.WanderLevel);

				foreach(Agent player in GC.playerAgentList)
                {
					if (agent.agentName == VAgent.Thief)
                    {
						if (player.statusEffects.hasTrait(nameof(UnderdankCitizen)))
							agent.relationships.SetRel(player, VRelationship.Friendly);
						else if (player.statusEffects.hasTrait(nameof(UnderdankVIP)))
							agent.relationships.SetRel(player, VRelationship.Loyal);
					}
					else if (agent.agentName == VAgent.Cannibal)
                    {
						if (player.statusEffects.hasTrait(nameof(UnderdankVIP)))
							agent.relationships.SetRel(player, VRelationship.Friendly);
					}
				}

				manhole.HoleAppear();
			}
		}

		/// <summary>
		/// UnderdankVIP Poison resistance
		/// </summary>
		/// <param name="statusEffectName"></param>
		/// <param name="__instance"></param>
		/// <param name="__result"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(StatusEffects.GetStatusEffectTime), argumentTypes: new[] { typeof(string) })]
		private static void GetStatusEffectTime_Postfix(string statusEffectName, StatusEffects __instance, ref int __result)
        {
			if (statusEffectName == VStatusEffect.Poisoned
				&& __instance.agent.statusEffects.hasTrait(nameof(UnderdankVIP)))
				__result = 7;
        }
	}
}