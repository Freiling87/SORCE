using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_Lighting;
using SORCE.Logging;
using System;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(SlimePuddle))]
	class P_SlimePuddle
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(SlimePuddle.SetVars), argumentTypes: new Type[0] { })]
		public static void SetVars_Postfix(SlimePuddle __instance)
		{
			if (GC.challenges.Contains(nameof(ObjectsRelit)))
				__instance.noLight = false;
		}
	}
}
