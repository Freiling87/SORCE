using HarmonyLib;
using SORCE.Challenges.C_Overhaul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
    class P_Relationships
    {
		// TODO OwnCheck Prefix - PoliceState sets all Objects NoStrikesIfDestroyed to false

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Relationships.EnforcerAlert), argumentTypes: new[] { typeof(Agent), typeof(float), typeof(Vector2), typeof(int), typeof(Agent) })]
		private static void EnforcerAlert_Postfix(Agent criminal, float noiseDist, Vector2 noisePos, int numStrikes, Agent victim, Relationships __instance)
		{
			// TODO move logic to PoliceState challenge
			//		Unsure what this note is about... Just saw it on importing from BM to SORCE
			if (GameController.gameController.challenges.Contains(nameof(PoliceState)))
			{
				if (__instance.GetRel(criminal) == nameof(relStatus.Hostile))
				{
					criminal.statusEffects.AddTrait(VTrait.Wanted);
				}
			}
		}
	}
}
