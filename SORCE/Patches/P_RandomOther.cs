using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using RogueLibsCore;
using Random = UnityEngine.Random;
using System.Collections;
using System.Reflection;
using System;
using SORCE.Challenges;
using SORCE.Logging;
using SORCE.Challenges.C_Interiors;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(RandomOther))]
	public static class P_RandomOther
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Flammable Wall FireSpewer removal
		/// </summary>
		/// <param name="___component"></param>
		/// <param name="___rList"></param>
		public static void FillOther_Postfix(ref RandomSelection ___component, ref RandomList ___rList)
		{
			// Deleted the instruction for this one.
			// I think we're better off intercepting this elsewhere, perhaps in the method that makes random selections and passes that extremely unique string as an argument.

			logger.LogDebug("RandomOther_fillOther");
			// Pay special attention to this. If this is only called at Game Start, you need to find another place post-mutator to mod this.

			if (GC.challenges.Contains(nameof(ShantyTown)) || GC.challenges.Contains(nameof(GreenLiving)))
			{
				___rList = ___component.CreateRandomList("FireSpewerSpawnChance1", "Others", "Other");
				___component.CreateRandomElement(___rList, "No", 5);

				___rList = ___component.CreateRandomList("FireSpewerSpawnChance2", "Others", "Other");
				___component.CreateRandomElement(___rList, "No", 5);

				___rList = ___component.CreateRandomList("FireSpewerSpawnChance3", "Others", "Other");
				___component.CreateRandomElement(___rList, "No", 5);

				___rList = ___component.CreateRandomList("FireSpewerSpawnChance4", "Others", "Other");
				___component.CreateRandomElement(___rList, "No", 5);

				___rList = ___component.CreateRandomList("FireSpewerSpawnChance5", "Others", "Other");
				___component.CreateRandomElement(___rList, "No", 5);
			}
		}
	}
}
