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
			if (!(hiddenInObject is null))
				UnderdankCitizen.Handle_StatusEffects_BecomeHidden(__instance, hiddenInObject);
		}
	}
}