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
using SORCE.Localization;
using SORCE.Challenges.C_Interiors;
using static SORCE.Localization.NameLists;
using SORCE.Challenges.C_Wreckage;

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
			string challenge = ChallengeManager.GetActiveChallengeFromList(NameLists.AffectsLights);

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

		/// <summary>
		/// Wreckage
		/// </summary>
		/// <param name="objectPos"></param>
		/// <param name="objectSource"></param>
		/// <param name="objectType"></param>
		/// <param name="myDir"></param>
		/// <param name="worldDataObjects"></param>
		/// <param name="worldDataElementPosition"></param>
		/// <param name="__instance"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(SpawnerMain.spawnObjectReal), argumentTypes: new[] 
			{ typeof(Vector3), typeof(PlayfieldObject), typeof(string), typeof(string), typeof(WorldDataObject), typeof(int) })]
		public static void SpawnerMain_spawnObjectReal(Vector3 objectPos, PlayfieldObject objectSource, string objectType, string myDir, WorldDataObject worldDataObjects, int worldDataElementPosition, SpawnerMain __instance)
		{
			float offsetSize = 9999f;
			string particleType = null;
			int iteratorChance = 0;
			int trashLevelInverse = GC.levelTheme; // 0 = Home Base, 5 = Mayor Village 
			Vector2 loc = objectPos;
			int chance = 100;

			if (GC.challenges.Contains(nameof(FloralerFlora)) || Core.debugMode)
				switch (objectType)
				{
					case vObject.Bush:
						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.64f, 0.64f), 
								loc.y + Random.Range(-0.64f, 0.64f)),
								vObject.Bush, false);
							chance -= 20;
						}

						break;

					case vObject.KillerPlant:
						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.64f, 0.64f), 
								loc.y + Random.Range(-0.64f, 0.64f)),
								vObject.Bush, false);
							chance -= 20;
						}

						break;

					case vObject.Plant:
						// TODO: Not working
						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.32f, 0.32f), 
								loc.y + Random.Range(-0.32f, 0.32f)),
								vObject.Bush, false);
							chance -= 66;
						}

						break;

					case vObject.Tree:
						while (GC.percentChance(chance + 40))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.64f, 0.64f), 
								loc.y + Random.Range(-0.64f, 0.64f)),
								vObject.Bush, false);
							chance -= 30;
						}

						break;
				}

			chance = 100;

			if (GC.challenges.Contains(nameof(DirtierDistricts)) || Core.debugMode)
				switch (objectType)
				{
					case vObject.ATMMachine:
						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.48f, 0.48f), 
								loc.y + Random.Range(-0.48f, 0.48f)),
								vObject.MovieScreen, false);
							chance -= 10;
						}

						break;

					case vObject.Barbecue:
						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.20f, 0.20f), 
								loc.y + Random.Range(-0.18f, 0.06f)),
								vObject.Bush, true);
							chance -= 25;
						}

						break;

					case vObject.Boulder:
						while (GC.percentChance(1))
							GC.spawnerMain.SpawnItem(new Vector2(
								loc.x + Random.Range(-0.48f, 0.48f), 
								loc.y + Random.Range(-0.48f, 0.48f)), 
								vItem.Rock);

						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.48f, 0.48f), 
								loc.y + Random.Range(-0.48f, 0.24f)),
								vObject.FlamingBarrel, false);
							chance -= 20;
						}

						break;

					case vObject.BoulderSmall:
						while (GC.percentChance(1))
							GC.spawnerMain.SpawnItem(new Vector2(
								loc.x + Random.Range(-0.16f, 0.16f), 
								loc.y + Random.Range(-0.16f, 0.08f)), 
								vItem.Rock);

						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.16f, 0.16f), 
								loc.y + Random.Range(-0.16f, 0.00f)),
								vObject.FlamingBarrel, false);
							chance -= 50;
						}

						break;

					case vObject.FlamingBarrel:
						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.06f, 0.06f), 
								loc.y + Random.Range(-0.08f, 0.00f)), 
								vObject.Bush, true);
							chance -= 50;
						}

						break;

					case vObject.Toilet:
						// TODO: Not working
						while (GC.percentChance(chance))
						{
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.14f, 0.14f), 
								loc.y + Random.Range(-0.24f, 0.24f)),
								vObject.MovieScreen, false); // Toilet Paperww
							chance -= 100;
						}

						break;

					case vObject.TrashCan:
						// TODO: See if you can just spawn their contents outside, instead
						while (GC.percentChance(1))
							GC.spawnerMain.SpawnItem(new Vector2(
								loc.x + Random.Range(-0.32f, 0.32f), 
								loc.y + Random.Range(-0.32f, 0.32f)), 
								vItem.BananaPeel);

						while (GC.percentChance(chance))
						{
							// TODO: Find a way to ensure it doesn't pass walls
							GC.spawnerMain.SpawnWreckagePileObject(new Vector2(
								loc.x + Random.Range(-0.48f, 0.48f), 
								loc.y + Random.Range(-0.48f, 0.48f)),
								cObject.WreckageMisc.RandomElement(), 
								GC.percentChance(25));
							chance -= 15;
						}

						break;
				}
		}

	}
}