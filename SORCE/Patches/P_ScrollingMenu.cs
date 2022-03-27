using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;
using static SORCE.Localization.NameLists;
using SORCE.Challenges.C_AmbientLightColor;
using SORCE.Challenges.C_AmbientLightLevel;
using SORCE.Challenges;

namespace SORCE.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(ScrollingMenu))]
	class P_ScrollingMenu
	{
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(ScrollingMenu.PushedButton), argumentTypes: new[] { typeof(ButtonHelper) })]
		public static void PushedButton_Postfix(ButtonHelper myButton, ScrollingMenu __instance)
		{
			// TODO: Refactor this entirely

			switch (ChallengeManager.GetActiveChallengeFromList(AmbientLightLevel))
			{
				case nameof(Blinding):
					RenderSettings.ambientIntensity = 255;
					break;
				case nameof(NewMoon):
					RenderSettings.ambientIntensity = 0;
					break;
				default:
					RenderSettings.ambientIntensity = 100;
					break;
			}

			if (GC.challenges.Contains(nameof(NewMoon)))
				GC.cameraScript.lightingSystem.EnableAmbientLight = false;
			else
				GC.cameraScript.lightingSystem.EnableAmbientLight = true;

			if (GC.challenges.Contains(nameof(Sepia)))
			{
				GC.cameraScript.lightingSystem.EnableAmbientLight = true;
				RenderSettings.ambientLight = cColors.Sepia;
				RenderSettings.ambientIntensity = 255;
			}
			else if (GC.challenges.Contains(nameof(Hellscape)))
			{
				//GC.cameraScript.lightingSystem.EnableAmbientLight = true;
				//RenderSettings.ambientLight = cColors.Hellscape;
				//RenderSettings.ambientIntensity = 255;

				// Werewolf transformation code
				// I think it'll be better just to prefix the following two methods
				// Because this works in Home Base but does not carry over to the gameplay, obv (refs LoadLevel)
				GC.loadLevel.SetNewLightingRenderer(1f, 0f, 0f, 1f, 0.1f);
				GC.loadLevel.SetNewLightingAmbient(66, 0, 0, byte.MaxValue);

				for (int i = 0; i < GC.lightRealsAmbientList.Count; i++)
				{
					LightReal lightReal = GC.lightRealsAmbientList[i];

					lightReal.originalColor = lightReal.lightReal2Color;
					Color32 color = lightReal.fancyLight.Color;
					lightReal.lightReal2Color = new Color32(159, 0, 0, color.a);
					lightReal.fancyLight.Color = lightReal.lightReal2Color;
					lightReal.fancyLight.mustForceUpdate = true;
				}
			}
			else
			{
				RenderSettings.ambientLight = cColors.Vanilla;
				RenderSettings.ambientIntensity = 100;
			}
		}
	}
}
