using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using RogueLibsCore;
using Random = UnityEngine.Random;
using System.Collections;
using System.Reflection;
using System;
using Light2D;
using SORCE.Challenges;
using SORCE.Logging;
using SORCE.Content.Challenges.C_Interiors;

namespace SORCE.Content.Patches.P_LevelGen
{
	[HarmonyPatch(declaringType: typeof(SpawnerMain))]
	public static class P_SpawnerMain
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Light Colors
		/// TODO: Transpiler
		/// </summary>
		/// <param name="lightRealName"></param>
		/// <param name="__instance"></param>
		/// <param name="__result"></param>
		/// <param name="___defaultColor"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerMain.GetLightColor), argumentTypes: new[] { typeof(string) })]
		public static bool GetLightColor_Prefix(string lightRealName, ref Color __result)
		{
			string challenge = ChallengeManager.GetActiveChallengeFromList(cChallenge.AffectsLights);

			LightReal lightReal = new LightReal();

			if (challenge == null)
				return true;
			//else if (challenge == nameof(DiscoCityDanceoff))
			//	lightReal.lightReal2Color = vColor.discoColors.RandomElement<Color32>();
			else if (challenge == nameof(GreenLiving))
				lightReal.lightReal2Color = vColor.homeColor;
			else if (challenge == nameof(Panoptikopolis))
				lightReal.lightReal2Color = vColor.whiteColor;

			__result = lightReal.lightReal2Color;
			return false;

			#region Vanilla

			if (lightRealName == "ArenaRingLight")
				lightReal.lightReal2Color = vColor.arenaRingColor;
			else if (lightRealName == "BankLight")
				lightReal.lightReal2Color = vColor.whiteColor;
			else if (lightRealName == "BlueLight")
				lightReal.lightReal2Color = vColor.blueColor;
			else if (lightRealName == "CyanGreenLight")
				lightReal.lightReal2Color = vColor.cyanGreenColor;
			else if (lightRealName == "CyanLight")
				lightReal.lightReal2Color = vColor.cyanColor;
			else if (lightRealName == "DefaultLight")
				lightReal.lightReal2Color = vColor.defaultColor;
			else if (lightRealName == "FarmLight")
				lightReal.lightReal2Color = vColor.homeColor;
			else if (lightRealName == "FireStationLight")
				lightReal.lightReal2Color = vColor.fireStationColor;
			else if (lightRealName == "GraveyardLight")
				lightReal.lightReal2Color = vColor.cyanColor;
			if (lightRealName == "GreenLight")
				lightReal.lightReal2Color = vColor.greenColor;
			else if (lightRealName == "HomeLight")
			{
				if (GC.levelTheme == 4)
					lightReal.lightReal2Color = vColor.homeColorUptown;
				else if (GC.levelTheme == 5)
					lightReal.lightReal2Color = vColor.homeColorMayorVillage;
				else
					lightReal.lightReal2Color = vColor.homeColor;
			}
			else if (lightRealName == "HospitalLight")
			{
				if (GC.levelTheme == 5)
					lightReal.lightReal2Color = vColor.homeColorMayorVillage;
				else
					lightReal.lightReal2Color = vColor.whiteColor;
			}
			else if (lightRealName == "KitchenLight")
				lightReal.lightReal2Color = vColor.whiteColor;
			if (lightRealName == "LabLight")
				lightReal.lightReal2Color = vColor.labColor;
			else if (lightRealName == "LakeLight")
				lightReal.lightReal2Color = vColor.lakeColor;
			else if (lightRealName == "LightBlueLight")
			{
				if (GC.levelTheme == 5)
					lightReal.lightReal2Color = vColor.lightBlueColorMayorVillage;
				else
					lightReal.lightReal2Color = vColor.lightBlueColor;
			}
			else if (lightRealName == "MallLight")
				lightReal.lightReal2Color = vColor.mallColor;
			else if (lightRealName == "OfficeLight")
				lightReal.lightReal2Color = vColor.whiteColor;
			else if (lightRealName == "PinkLight")
				lightReal.lightReal2Color = vColor.pinkColor;
			if (lightRealName == "PinkWhiteLight")
				lightReal.lightReal2Color = vColor.pinkWhiteColor;
			else if (lightRealName == "PoolLight")
			{
				if (GC.levelTheme == 5)
					lightReal.lightReal2Color = vColor.poolColorLighter;
				else
					lightReal.lightReal2Color = vColor.poolColor;
			}
			else if (lightRealName == "PrivateClubLight")
				lightReal.lightReal2Color = vColor.privateClubColor;
			else if (lightRealName == "PurpleLight")
				lightReal.lightReal2Color = vColor.purpleColor;
			if (lightRealName == "RedLight")
				lightReal.lightReal2Color = vColor.redColor;
			else if (lightRealName == "TVStationLight")
			{
				lightReal.lightReal2Color = vColor.mallColor;
			}
			else if (lightRealName == "WhiteLight")
				lightReal.lightReal2Color = vColor.whiteColor;
			else if (lightRealName == "ZooLight")
				lightReal.lightReal2Color = vColor.zooColor;

			#endregion

			__result = lightReal.lightReal2Color;
			return false;
		}
	}
}