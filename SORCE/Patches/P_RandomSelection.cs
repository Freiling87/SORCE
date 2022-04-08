using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Buildings;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(RandomSelection))]
	internal class P_RandomSelection
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(RandomSelection.RandomSelect), argumentTypes: new[] { typeof(string), typeof(string) })]
		public static bool RandomSelection_RandomSelect(string rName, string rCategory, ref string __result) // Prefix
		{
			if (rName.StartsWith("FireSpewerSpawnChance") && 
				RogueFramework.Unlocks.OfType<BuildingsChallenge>().Where(i => i.IsEnabled).Any(i => i.WallsFlammable))
			{
				__result = "No"; // Why would you even ask this
				return false;
			}

			return true;
		}
	}
}
