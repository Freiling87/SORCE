using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_AmbientLightLevel;
using SORCE.Logging;

namespace SORCE.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(ScrollingMenu))]
	class P_ScrollingMenu
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(ScrollingMenu.PushedButton), argumentTypes: new[] { typeof(ButtonHelper) })]
		public static void PushedButton_Postfix(ButtonHelper myButton, ScrollingMenu __instance)
		{
			logger.LogDebug("Button Name:\t" + myButton.name);
			logger.LogDebug("Button Text:\t:" + myButton.myText);
			
			// TODO: Eliminate this when you find a more elegant fix
			if (GC.challenges.Contains(nameof(NewMoon)))
				GC.cameraScript.lightingSystem.EnableAmbientLight = false;
			else
				GC.cameraScript.lightingSystem.EnableAmbientLight = true;

			// TODO: Check for Mutator Menu, or that the button regards ambient light mutators. For now, this'll work.
			GC.loadLevel.SetNormalLighting();
		}
	}
}
