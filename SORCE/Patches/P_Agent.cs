using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_Lighting;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(Agent))]
	class P_Agent
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: "Awake")]
		public static void Awake_Postfix(Agent __instance)
		{
			if (GC.challenges.Contains(nameof(NoAgentLights)))
				__instance.hasLight = false;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Agent.RecycleAwake))]
		public static void RecycleAwake_Postfix(Agent __instance)
		{
			if (GC.challenges.Contains(nameof(NoAgentLights)))
				__instance.hasLight = false;
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.SetLightBrightness), argumentTypes: new[] { typeof(bool) })]
		public static bool SetLightBrightness_Prefix(bool isDead)
		{
			return !GC.challenges.Contains(nameof(NoAgentLights));
		}
	}
}
