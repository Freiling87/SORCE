using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Wreckage;
using SORCE.Localization;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(SpawnerObject))]
	public static class P_SpawnerObject
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerObject.spawn), argumentTypes: new[] { typeof(string) })]
		public static bool Spawn_Prefix(ref string objectRealName)
		{
			//logger.LogDebug("SpawnerObject_spawn:");
			//logger.LogDebug("\tobjectRealName = '" + objectRealName + "'");

			if (RogueFramework.Unlocks.OfType<BuildingsChallenge>().Where(i => i.IsEnabled).Any(i => i.WallsFlammable) 
				&& objectRealName == VObject.FireSpewer)
				objectRealName = VObject.SecurityCam;

			return true;
		}
	}
}
