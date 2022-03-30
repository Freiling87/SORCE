using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges;
using SORCE.Localization;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(SpawnerObject))]
	public static class P_SpawnerObject
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerObject.spawn), argumentTypes: new[] { typeof(string) })]
		public static bool spawn_Prefix(ref string objectRealName)
		{
			//logger.LogDebug("SpawnerObject_spawn:");
			//logger.LogDebug("\tobjectRealName = '" + objectRealName + "'");

			if (ChallengeManager.IsChallengeFromListActive(NameLists.BuildingsFlammable) && objectRealName == vObject.FireSpewer)
				objectRealName = vObject.SecurityCam;

			return true;
		}
	}
}
