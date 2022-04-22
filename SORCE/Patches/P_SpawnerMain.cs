using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Lighting;
using SORCE.Localization;
using SORCE.Logging;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
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
			string challenge = ChallengeManager.GetActiveChallengeFromList(CChallenge.AffectsLights);

			LightReal lightReal = new LightReal();

			if (challenge == null)
				return true;
			//else if (challenge == nameof(DiscoCityDanceoff))
			//	lightReal.lightReal2Color = vColor.discoColors.RandomElement<Color32>();
			else if (challenge == nameof(GreenLiving))
				lightReal.lightReal2Color = VColor.homeColor;
			else if (challenge == nameof(Panoptikopolis))
				lightReal.lightReal2Color = VColor.whiteColor;

			__result = lightReal.lightReal2Color;
			return false;

			#region Vanilla

#pragma warning disable CS0162 // Unreachable code detected
			if (lightRealName == "ArenaRingLight")
				lightReal.lightReal2Color = VColor.arenaRingColor;
			else if (lightRealName == "BankLight")
				lightReal.lightReal2Color = VColor.whiteColor;
			else if (lightRealName == "BlueLight")
				lightReal.lightReal2Color = VColor.blueColor;
			else if (lightRealName == "CyanGreenLight")
				lightReal.lightReal2Color = VColor.cyanGreenColor;
			else if (lightRealName == "CyanLight")
				lightReal.lightReal2Color = VColor.cyanColor;
			else if (lightRealName == "DefaultLight")
				lightReal.lightReal2Color = VColor.defaultColor;
			else if (lightRealName == "FarmLight")
				lightReal.lightReal2Color = VColor.homeColor;
			else if (lightRealName == "FireStationLight")
				lightReal.lightReal2Color = VColor.fireStationColor;
			else if (lightRealName == "GraveyardLight")
				lightReal.lightReal2Color = VColor.cyanColor;
#pragma warning restore CS0162 // Unreachable code detected
			if (lightRealName == "GreenLight")
				lightReal.lightReal2Color = VColor.greenColor;
			else if (lightRealName == "HomeLight")
			{
				if (GC.levelTheme == 4)
					lightReal.lightReal2Color = VColor.homeColorUptown;
				else if (GC.levelTheme == 5)
					lightReal.lightReal2Color = VColor.homeColorMayorVillage;
				else
					lightReal.lightReal2Color = VColor.homeColor;
			}
			else if (lightRealName == "HospitalLight")
			{
				if (GC.levelTheme == 5)
					lightReal.lightReal2Color = VColor.homeColorMayorVillage;
				else
					lightReal.lightReal2Color = VColor.whiteColor;
			}
			else if (lightRealName == "KitchenLight")
				lightReal.lightReal2Color = VColor.whiteColor;
			if (lightRealName == "LabLight")
				lightReal.lightReal2Color = VColor.labColor;
			else if (lightRealName == "LakeLight")
				lightReal.lightReal2Color = VColor.lakeColor;
			else if (lightRealName == "LightBlueLight")
			{
				if (GC.levelTheme == 5)
					lightReal.lightReal2Color = VColor.lightBlueColorMayorVillage;
				else
					lightReal.lightReal2Color = VColor.lightBlueColor;
			}
			else if (lightRealName == "MallLight")
				lightReal.lightReal2Color = VColor.mallColor;
			else if (lightRealName == "OfficeLight")
				lightReal.lightReal2Color = VColor.whiteColor;
			else if (lightRealName == "PinkLight")
				lightReal.lightReal2Color = VColor.pinkColor;
			if (lightRealName == "PinkWhiteLight")
				lightReal.lightReal2Color = VColor.pinkWhiteColor;
			else if (lightRealName == "PoolLight")
			{
				if (GC.levelTheme == 5)
					lightReal.lightReal2Color = VColor.poolColorLighter;
				else
					lightReal.lightReal2Color = VColor.poolColor;
			}
			else if (lightRealName == "PrivateClubLight")
				lightReal.lightReal2Color = VColor.privateClubColor;
			else if (lightRealName == "PurpleLight")
				lightReal.lightReal2Color = VColor.purpleColor;
			if (lightRealName == "RedLight")
				lightReal.lightReal2Color = VColor.redColor;
			else if (lightRealName == "TVStationLight")
			{
				lightReal.lightReal2Color = VColor.mallColor;
			}
			else if (lightRealName == "WhiteLight")
				lightReal.lightReal2Color = VColor.whiteColor;
			else if (lightRealName == "ZooLight")
				lightReal.lightReal2Color = VColor.zooColor;

			#endregion

			__result = lightReal.lightReal2Color;
			return false;
		}

		/// <summary>
		/// Light Sources
		/// </summary>
		/// <param name="myObject"></param>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerMain.SetLighting2), argumentTypes: new[] { typeof(PlayfieldObject) })]
		public static bool SetLighting2_Prefix(PlayfieldObject myObject, SpawnerMain __instance)
		{
			if (GC.challenges.Contains(nameof(NoObjectLights))
				&& !VObject.Electronics.Contains(myObject.objectName)
				&& myObject.CompareTag("ObjectReal"))
			{
				ObjectReal objectReal = (ObjectReal)myObject;
				objectReal.noLight = true;
			}

			if (GC.challenges.Contains(nameof(NoItemLights)) 
				&& myObject.CompareTag("Item") || myObject.CompareTag("Wreckage"))
            {
				Item item = (Item)myObject;

				myObject.defaultShader = GC.normalShader; // TODO: Test use LitShader with NewMoon to make them stand out subtly?
				return false;
			}

			return true;
		}
	}
} 