using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using SORCE.Traits;
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
		/// UnderdankVIP Poison resistance
		/// </summary>
		/// <param name="statusEffectName"></param>
		/// <param name="__instance"></param>
		/// <param name="__result"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(StatusEffects.GetStatusEffectTime), argumentTypes: new[] { typeof(string) })]
		private static void GetStatusEffectTime_Postfix(string statusEffectName, StatusEffects __instance, ref int __result)
        {
			if (statusEffectName == VStatusEffect.Poisoned)
			{
				if (__instance.agent.statusEffects.hasTrait(nameof(UnderdankCitizen)))
					__result = 10;
				if (__instance.agent.statusEffects.hasTrait(nameof(UnderdankVIP)))
					__result = 5;
			}
        }
	}
}