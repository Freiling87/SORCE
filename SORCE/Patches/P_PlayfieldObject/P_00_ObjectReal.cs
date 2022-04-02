using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_Lighting;
using SORCE.Logging;
using System;
using System.Reflection;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(ObjectReal))]
	class P_00_ObjectReal
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static MethodInfo DamagedObject_base = AccessTools.DeclaredMethod(typeof(ObjectReal), "DamagedObject", new Type[2] { typeof(PlayfieldObject), typeof(float) });
		public static MethodInfo DetermineButtons_base = AccessTools.DeclaredMethod(typeof(ObjectReal), "DetermineButtons", new Type[0] { });
		public static MethodInfo Interact_base = AccessTools.DeclaredMethod(typeof(ObjectReal), "Interact", new Type[1] { typeof(Agent) });
		public static MethodInfo ObjectAction_base = AccessTools.DeclaredMethod(typeof(ObjectReal), "ObjectAction", new Type[5] { typeof(string), typeof(string), typeof(float), typeof(Agent), typeof(PlayfieldObject) });
		public static MethodInfo PressedButton_base = AccessTools.DeclaredMethod(typeof(ObjectReal), "PressedButton", new Type[2] { typeof(string), typeof(int) });
		public static MethodInfo Start_base = AccessTools.DeclaredMethod(typeof(ObjectReal), "Start", new Type[0] { });

		/// <summary>
		/// WARNING: Original was a replacement. This may need to be a transpiler.
		/// Manhole
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(ObjectReal.FinishedOperating), argumentTypes: new Type[0] { })]
		public static void FinishedOperating_Postfix(ObjectReal __instance)
		{
			if (!__instance.interactingAgent.interactionHelper.interactingFar)
			{
				if (__instance is Manhole)
					P_Manhole.PryOpen((Manhole)__instance);
				else if (__instance is Fountain)
					P_Fountain.Loot((Fountain)__instance);
			}
		}
	}
}
