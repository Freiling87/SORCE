using HarmonyLib;
using SORCE.Challenges.C_AmbientLightLevel;
using SORCE.Challenges.C_Lighting;
using SORCE.Patches.P_PlayfieldObject;

namespace SORCE.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(ScrollingMenu))]
	class P_ScrollingMenu
	{
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(ScrollingMenu.PushedButton), argumentTypes: new[] { typeof(ButtonHelper) })]
		public static void PushedButton_Postfix(ButtonHelper myButton, ScrollingMenu __instance)
		{
			// TODO: Eliminate this when you find a more elegant fix
			if (GC.challenges.Contains(nameof(NewMoon)))
				GC.cameraScript.lightingSystem.EnableAmbientLight = false;
			else
				GC.cameraScript.lightingSystem.EnableAmbientLight = true;

			// Hopefully helps performance
			P_Bullet.GunplayRelit = GC.challenges.Contains(nameof(GunplayRelit));

			// TODO: Check for Mutator Menu, or that the button regards ambient light mutators. For now, this'll work.
			GC.loadLevel.SetNormalLighting();
		}
	}
}
