using BepInEx.Logging;
using SORCE.Challenges;
using SORCE.Logging;
using SORCE.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using SORCE.Challenges.C_MapSize;
using SORCE.Challenges.C_Exteriors;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Features;
using SORCE.Localization;
using SORCE.Challenges.C_Interiors;
using SORCE.Challenges.C_Roamers;
using static SORCE.Localization.NameLists;
using SORCE.Challenges.C_Wreckage;

namespace SORCE
{
	class LevelGenTools
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static wallMaterialType BorderWallMaterial()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(NameLists.Exteriors))
			{
				case (nameof(Arcology)):
					return wallMaterialType.Border;
				case (nameof(CanalCity)):
					return wallMaterialType.Border;
				case (nameof(DUMP)):
					return wallMaterialType.Steel;
				case (nameof(GrandCityHotel)):
					return wallMaterialType.Wood;
				case (nameof(TestTubeCity)):
					return wallMaterialType.Glass;
				case (nameof(TransitExperiment)):
					return wallMaterialType.Border;
				default:
					return wallMaterialType.Border;
			}
		}
		public static string RandomRugType()
		{
			var random = new System.Random();
			int index = random.Next(vFloor.Rugs.Count);

			return vFloor.Rugs[index];
		}
		public static string ExteriorFloorTile()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(NameLists.Exteriors))
			{
				case nameof(Arcology):
					return vFloor.Grass;
				case nameof(CanalCity):
					return vFloor.Water;
				case nameof(DUMP):
					return vFloor.CaveFloor;
				case nameof(GrandCityHotel):
					return RandomRugType();
				case nameof(TestTubeCity):
					return vFloor.CleanTiles;
				case nameof(TransitExperiment):
					return vFloor.IceRink;
				default:
					return null; 
			}
		}
		public static string ExteriorFloorTileGroup()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(NameLists.Exteriors))
			{
				case nameof(Arcology):
					return vFloorTileGroup.Park;
				case nameof(CanalCity):
					return vFloorTileGroup.Water;
				case nameof(DUMP):
					return vFloorTileGroup.Industrial;
				case nameof(GrandCityHotel):
					return vFloorTileGroup.Rug;
				case nameof(TestTubeCity):
					return vFloorTileGroup.Building;
				case nameof(TransitExperiment):
					return vFloorTileGroup.Ice;
				default:
					return vFloorTileGroup.Building;
			}
		}
		public static string InteriorWallType()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(NameLists.Interiors))
			{
				case nameof(CityOfSteel):
					return vWall.Steel;
				case nameof(GreenLiving):
					return vWall.Hedge;
				case nameof(Panoptikopolis):
					return vWall.Glass;
				case nameof(ShantyTown):
					return vWall.Wood;
				case nameof(SpelunkyDory):
					return vWall.Cave;
			}

			return null;
		}
		public static bool IsNextToLake(Vector2 spot) =>
			GC.tileInfo.GetTileData(new Vector2(spot.x, spot.y + 0.64f)).lake ||
			GC.tileInfo.GetTileData(new Vector2(spot.x + 0.64f, spot.y + 0.64f)).lake ||
			GC.tileInfo.GetTileData(new Vector2(spot.x + 0.64f, spot.y + 0.64f)).lake ||
			GC.tileInfo.GetTileData(new Vector2(spot.x + 0.64f, spot.y)).lake ||
			GC.tileInfo.GetTileData(new Vector2(spot.x, spot.y - 0.64f)).lake ||
			GC.tileInfo.GetTileData(new Vector2(spot.x - 0.64f, spot.y - 0.64f)).lake ||
			GC.tileInfo.GetTileData(new Vector2(spot.x - 0.64f, spot.y - 0.64f)).lake ||
			GC.tileInfo.GetTileData(new Vector2(spot.x - 0.64f, spot.y)).lake;
		public static int LevelSizeModifier(int vanilla) =>
			GC.challenges.Contains(nameof(Arthropolis)) ? 4 :
			GC.challenges.Contains(nameof(Claustropolis)) ? 12 :
			GC.challenges.Contains(nameof(Megapolis)) ? 48 :
			GC.challenges.Contains(nameof(Ultrapolis)) ? 64 : 
			vanilla;
		public static int LevelSizeRatio() =>
			LevelSizeModifier(30) / 30;
		public static int PopulationGang(int vanilla) =>
			GC.challenges.Contains(nameof(HoodlumsWonderland)) ? 12 :
			vanilla;
		public static int RoamerAgentFactor(int vanilla) =>
			vanilla * (
			GC.challenges.Contains(nameof(GhostTown)) ? 0 :
			GC.challenges.Contains(nameof(HordeAlmighty)) ? 2 :
			GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 4 :
			GC.challenges.Contains(nameof(SwarmWelcome)) ? 8 : 
			1 );
		// TODO obv
		public static int PopulationMafia(int vanilla) => 
			vanilla;

		/// <summary>
		/// TODO: Call this somewhere
		/// </summary>
		/// <param name="chunkDescription"></param>
		/// <returns></returns>
		public static string AmbientAudio(string chunkDescription)
		{
			string ambientAudio = "";

			if (chunkDescription == vChunkType.Casino)
				ambientAudio = vAmbience.Casino;
			else if (
				chunkDescription != vChunkType.Bathhouse &&
				chunkDescription != vChunkType.Casino &&
				chunkDescription != vChunkType.Cave &&
				chunkDescription != vChunkType.CityPark &&
				chunkDescription != vChunkType.Graveyard)
			{
				if (GC.challenges.Contains(nameof(Arcology)))
					ambientAudio = vAmbience.Park;
				
				if (GC.challenges.Contains(nameof(SpelunkyDory)))
					ambientAudio = vAmbience.Cave;

				if (GC.challenges.Contains(nameof(GhostTown)))
					ambientAudio = vAmbience.Graveyard;
			}

			return ambientAudio;
		}
		public static bool HasBarbecues(bool vanilla) =>
			vanilla; // *
		public static bool HasBoulders(bool vanilla) =>
			GC.challenges.Contains(nameof(SpelunkyDory)) || 
			GC.challenges.Contains(nameof(Arcology))  ? true :
			vanilla;
		public static bool HasBushes(bool vanilla) =>
			GC.challenges.Contains(nameof(Arcology)) ? true :
			vanilla; // *
		public static bool HasCopBots(bool vanilla) =>
			GC.challenges.Contains(nameof(Technocracy)) ? true :
			vanilla; // *
		public static bool HasCops(bool vanilla) =>
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(Technocracy)) ? false :
			GC.challenges.Contains(nameof(PoliceState)) ? true :
			vanilla; // *
		public static bool HasCopsExtra(bool vanilla) =>
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(Technocracy)) ? false :
			GC.challenges.Contains(nameof(PoliceState)) ? true :
			vanilla; //* 
		public static bool HasFireHydrants(bool vanilla) =>
			GC.challenges.Contains(nameof(AnCapistan)) ? false : vanilla;
		public static bool HasFlamingBarrels(bool vanilla) =>
			GC.challenges.Contains(nameof(AnCapistan)) ? true :
			GC.challenges.Contains(nameof(PoliceState)) ||
			GC.challenges.Contains(nameof(MACITS)) ? false :
			vanilla;
		public static bool HasGangbangers(bool vanilla) =>
			GC.challenges.Contains(nameof(YoungMenInTheNeighborhood)) ||
			GC.challenges.Contains(nameof(AnCapistan)) ? true :
			GC.challenges.Contains(nameof(PoliceState)) ||
			GC.challenges.Contains(nameof(MACITS)) ? false :
			vanilla; // *
		public static bool HasLandMines(bool vanilla) =>
			GC.challenges.Contains(nameof(ThisLandIsMineLand)) ? true :
			vanilla; // *
		public static bool HasManholesVanilla(bool vanilla) =>
			//	Underdark Citizen uses a different Manhole algorithm that requires deactivation of vanilla.
			TraitManager.IsPlayerTraitActive("Underdark Citizen") || GC.challenges.Contains(nameof(AnCapistan)) ? false :
			vanilla;
		public static bool HasMobsters(bool vanilla) =>
			GC.challenges.Contains(nameof(MobTown)) ||
			GC.challenges.Contains(nameof(AnCapistan)) ? true :
			GC.challenges.Contains(nameof(PoliceState)) ||
			GC.challenges.Contains(nameof(MACITS)) ? false :
			vanilla; // *
		public static bool HasPollutionFeatures(bool vanilla) =>
			GC.challenges.Contains(nameof(ThePollutionSolution)) ? false : vanilla;
		public static bool HasPoliceBoxesAndAlarmButtons(bool vanilla) =>
			GC.challenges.Contains(nameof(PoliceState)) || GC.challenges.Contains(nameof(MACITS)) ? true :
			GC.challenges.Contains(nameof(AnCapistan)) ? false :
			vanilla;
		public static bool HasPowerBoxes(bool vanilla) =>
			GC.challenges.Contains(nameof(PowerWhelming)) ? true : vanilla;
		public static bool HasTrashCans(bool vanilla)
		{
			if (GC.challenges.Contains(nameof(AnCapistan)))
				vanilla = false;

			if (GC.challenges.Contains(nameof(Arcology)) ||
				GC.challenges.Contains(nameof(PoliceState)) ||
				GC.challenges.Contains(nameof(MACITS)))
				vanilla = true;

			return vanilla;
		}
		public static bool HasTrees(bool vanilla) =>
			GC.challenges.Contains(nameof(Arcology)) ? true :
			GC.challenges.Contains(nameof(AnCapistan)) ? false :
			vanilla;
		public static bool HasVendorCarts(bool vanilla) =>
			GC.challenges.Contains(nameof(CartOfTheDeal)) ? true : vanilla;
		public static string RoamerAgentType(string vanilla)
		{
			// TODO: Adjustments for MACITS, etc.

			if (vanilla == "Thief")
			{
				string generalRoamer = GC.levelTheme == 4 || GC.levelTheme == 5 ? vAgent.UpperCruster : vAgent.SlumDweller;

				int thiefReduction =
					GC.challenges.Contains(nameof(HordeAlmighty)) ? 50 :
					GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 75 :
					GC.challenges.Contains(nameof(SwarmWelcome)) ? 88 :
					0;

				if (thiefReduction != 0 && GC.percentChance(thiefReduction))
					vanilla = generalRoamer;
			}

			return vanilla;
		}
		public static void SetHasLakes(LoadLevel __instance)
		{
			if (GC.challenges.Contains(nameof(LakeItOrLeaveIt)))
				__instance.hasLakes = true;
		}
		public static void SetHasFlameGrates(LoadLevel __instance)
		{
			return;
		}
		public static void SetLevelSizeModifier(LoadLevel __instance)
		{
			int newVal = __instance.levelSizeMax;

			string active = ChallengeManager.GetActiveChallengeFromList(NameLists.MapSize);

			if (active == nameof(Arthropolis))
				newVal = 4;
			else if (active == nameof(Claustropolis))
				newVal = 12;
			else if (active == nameof(Megapolis))
				newVal = 48;
			else if (active == nameof(Ultrapolis))
				newVal = 64;

			__instance.levelSizeMax = newVal;
		}
		public static void Spawn_Master(LoadLevel __instance)
		{
			if ((GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
				(GC.customLevel && __instance.customLevel.levelFeatures.Contains(cLevelFeature.BrokenWindows)) ||
				GC.challenges.Contains(nameof(BadNeighborhoods)))
				BreakWindows();

			if (GC.challenges.Contains(nameof(SpelunkyDory)))
				SpawnCaveWallOutcroppings(__instance);

			if (GC.challenges.Contains(nameof(BroughtbackFountain)))
				SpawnFountains();

			if (GC.challenges.Contains(nameof(DepartmentOfPublicComfiness)))
				SpawnArmchairsFireplaces();

			if (GC.challenges.Contains(nameof(DiscoCityDanceoff)))
			{
				SpawnJukeboxesAndSpeakers(__instance);
				SpawnTurntables();
			}

			// TODO: Replace litter in Arcology with more leaves
			if (GC.challenges.Contains(nameof(DirtierDistricts)))
				SpawnLitter(__instance);

			if (TraitManager.IsPlayerTraitActive("UnderdarkCitizen"))
				SpawnManholes_Underdark(__instance);

			if (GC.challenges.Contains(nameof(GrandCityHotel)) ||
				GC.challenges.Contains(nameof(DepartmentOfPublicComfiness)))
				SpawnRugs();

			if (GC.challenges.Contains(nameof(PoliceState)) || GC.challenges.Contains(nameof(SurveillanceSociety)))
				SpawnSecurityCamsAndTurrets(__instance);
		}
		private static void BreakWindows()
		{
			logger.LogDebug("Breaking Windows");

			foreach (ObjectReal objectReal in GC.objectRealList)
				if (objectReal is Window window && GC.percentChance(2))
					window.DamagedObject(window, 0f);
		}
		private static void SpawnCaveWallOutcroppings(LoadLevel __instance)
		{
			Debug.Log("SORCE: Loading SpelunkyDory Cave Wall Outcroppings");

			int maxSpawns = (int)((float)Random.Range(48, 64) * __instance.levelSizeModifier);
			List<int> spawnedCount = new List<int>();
			int itemCountIterator;

			for (int i = 0; i < maxSpawns; i = itemCountIterator + 1)
			{
				Vector2 spotCandidate = Vector2.zero;
				int spotsTried = 0;

				do
				{
					spotCandidate = GC.tileInfo.FindRandLocationNearWall(0.64f);

					if (spotCandidate != Vector2.zero)
					{
						TileData spotTileData = GC.tileInfo.GetTileData(spotCandidate);

						//if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x, spotCandidate.y + 0.64f)).owner == 0 &&
						//	GC.tileInfo.GetTileData(new Vector2(spotCandidate.x + 0.64f, spotCandidate.y)).owner == 0 &&
						//	GC.tileInfo.GetTileData(new Vector2(spotCandidate.x, spotCandidate.y - 0.64f)).owner == 0 &&
						//	GC.tileInfo.GetTileData(new Vector2(spotCandidate.x - 0.64f, spotCandidate.y)).owner == 0)
						//	spotCandidate = Vector2.zero;

						if (!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x, spotCandidate.y + 0.64f), "Wall") &&
								!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x, spotCandidate.y - 0.64f), "Wall") &&
								!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x + 0.64f, spotCandidate.y), "Wall") &&
								!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x - 0.64f, spotCandidate.y), "Wall"))
							spotCandidate = Vector2.zero;

						if (GC.tileInfo.IsOverlapping(spotCandidate, "ObjectRealSprite", 0.64f))
							spotCandidate = Vector2.zero;

						if (spawnedCount.Contains(spotTileData.chunkID))
							spotCandidate = Vector2.zero;

						if (GC.tileInfo.DestroyIfBetweenWalls(spotCandidate))
							spotCandidate = Vector2.zero;
					}

					spotsTried++;
				} while ((spotCandidate == Vector2.zero || Vector2.Distance(spotCandidate, GC.playerAgent.tr.position) < 5f) && spotsTried < 100);

				if (spotCandidate != Vector2.zero)
				{
					GC.tileInfo.BuildWallTileAtPosition(spotCandidate.x, spotCandidate.y, wallMaterialType.Normal);
					TileData spotTileData = GC.tileInfo.GetTileData(spotCandidate);
					spawnedCount.Add(spotTileData.chunkID);

					if (i < maxSpawns - 1 && GC.percentChance(25))
					{
						string adjoiningWall = "";
						Vector2 leftOfSpot = Vector2.zero;
						Vector2 rightOfSpot = Vector2.zero;

						if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x, spotCandidate.y + 0.64f)).wallMaterial != wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x + 1.28f, spotCandidate.y);
							rightOfSpot = new Vector2(spotCandidate.x - 1.28f, spotCandidate.y);
							adjoiningWall = "N";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x, spotCandidate.y - 0.64f)).wallMaterial !=
								wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x + 1.28f, spotCandidate.y);
							rightOfSpot = new Vector2(spotCandidate.x - 1.28f, spotCandidate.y);
							adjoiningWall = "S";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x + 0.64f, spotCandidate.y)).wallMaterial !=
								wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x, spotCandidate.y + 1.28f);
							rightOfSpot = new Vector2(spotCandidate.x, spotCandidate.y - 1.28f);
							adjoiningWall = "E";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x - 0.64f, spotCandidate.y)).wallMaterial !=
								wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x, spotCandidate.y + 1.28f);
							rightOfSpot = new Vector2(spotCandidate.x, spotCandidate.y - 1.28f);
							adjoiningWall = "W";
						}

						GC.tileInfo.GetTileData(leftOfSpot);
						bool isSpotAcceptable = true;

						if ((GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y + 0.64f)).wallMaterial == wallMaterialType.None &&
									adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None &&
									adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y)).wallMaterial == wallMaterialType.None &&
									adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y)).wallMaterial != wallMaterialType.None &&
									adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y - 0.64f)).wallMaterial == wallMaterialType.None &&
									adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None &&
									adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y)).wallMaterial == wallMaterialType.None &&
									adjoiningWall == "W") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y)).wallMaterial != wallMaterialType.None &&
									adjoiningWall == "W") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "W") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
									wallMaterialType.None && adjoiningWall == "W"))
							isSpotAcceptable = false;

						if (GC.tileInfo.IsOverlapping(leftOfSpot, "Anything"))
							isSpotAcceptable = false;

						if (GC.tileInfo.IsOverlapping(leftOfSpot, "ObjectRealSprite", 0.64f))
							isSpotAcceptable = false;

						if (isSpotAcceptable && leftOfSpot != Vector2.zero)
						{
							GC.tileInfo.BuildWallTileAtPosition(spotCandidate.x, spotCandidate.y, wallMaterialType.Normal);
							itemCountIterator = i;
							i = itemCountIterator + 1;
						}
						else
						{
							GC.tileInfo.GetTileData(rightOfSpot);
							isSpotAcceptable = true;

							if ((GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y + 0.64f)).wallMaterial == wallMaterialType.None &&
										adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y - 0.64f)).wallMaterial
										!= wallMaterialType.None &&
										adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y)).wallMaterial
										== wallMaterialType.None &&
										adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y)).wallMaterial
										!= wallMaterialType.None &&
										adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y - 0.64f)).wallMaterial
										== wallMaterialType.None &&
										adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y + 0.64f)).wallMaterial
										!= wallMaterialType.None &&
										adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y)).wallMaterial
										== wallMaterialType.None &&
										adjoiningWall == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y)).wallMaterial
										!= wallMaterialType.None &&
										adjoiningWall == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && adjoiningWall == "W"))
								isSpotAcceptable = false;

							if (GC.tileInfo.IsOverlapping(rightOfSpot, "Anything"))
								isSpotAcceptable = false;

							if (GC.tileInfo.IsOverlapping(rightOfSpot, "ObjectRealSprite", 0.64f))
								isSpotAcceptable = false;

							if (isSpotAcceptable && rightOfSpot != Vector2.zero)
							{
								GC.spawnerMain.spawnObjectReal(rightOfSpot, null, "TrashCan").ShiftTowardWalls();
								itemCountIterator = i;
								i = itemCountIterator + 1;
							}
						}
					}
				}

				Random.InitState(__instance.randomSeedNum + i);
				itemCountIterator = i;
			}
		}
		private static void SpawnFountains()
		{
			Debug.Log("SORCE: Loading Fountains");
			int numObjects = Mathf.Clamp(3 * LevelSizeRatio(), 1, 5);
			float objectBuffer = 14f;

			for (int i = 0; i < numObjects; i++)
			{
				Vector2 location = Vector2.zero;
				int attempts = 0;

				do
				{
					location = GC.tileInfo.FindRandLocationGeneral(2f);

					for (int j = 0; j < GC.objectRealList.Count; j++)
						if (GC.objectRealList[j].objectName == "Fountain" && Vector2.Distance(GC.objectRealList[j].tr.position, location) < (objectBuffer * LevelSizeRatio()))
							location = Vector2.zero;

					attempts++;
				} 
				while ((location == Vector2.zero || Vector2.Distance(location, GC.playerAgent.tr.position) < 5f) && attempts < 100);

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, "Fountain");
			}
		}
		private static void SpawnArmchairsFireplaces()
		{
			// Fireplace in middle, armchairs on sides
		}
		private static void SpawnJukeboxesAndSpeakers(LoadLevel __instance)
		{
			if (GC.challenges.Contains(nameof(DiscoCityDanceoff)))
			{
				Debug.Log("SORCE: Loading Disco Shit");
				int maxSpawns = (int)((float)Random.Range(6, 12) * __instance.levelSizeModifier);
				List<int> spawnedInChunks = new List<int>();

				for (int i = 0; i < maxSpawns; i++)
				{
					Vector2 location = Vector2.zero;
					int attempts = 0;

					do
					{
						location = GC.tileInfo.FindRandLocationNearWall(0.64f);

						if (location != Vector2.zero)
						{
							TileData tileData4 = GC.tileInfo.GetTileData(location);

							if (GC.tileInfo.GetTileData(new Vector2(location.x, location.y + 0.64f)).owner == 0 &&
									GC.tileInfo.GetTileData(new Vector2(location.x + 0.64f, location.y)).owner == 0 &&
									GC.tileInfo.GetTileData(new Vector2(location.x, location.y - 0.64f)).owner == 0 &&
									GC.tileInfo.GetTileData(new Vector2(location.x - 0.64f, location.y)).owner == 0)
								location = Vector2.zero;

							if (!GC.tileInfo.IsOverlapping(new Vector2(location.x, location.y + 0.64f), "Wall") &&
									!GC.tileInfo.IsOverlapping(new Vector2(location.x, location.y - 0.64f), "Wall") &&
									!GC.tileInfo.IsOverlapping(new Vector2(location.x + 0.64f, location.y), "Wall") &&
									!GC.tileInfo.IsOverlapping(new Vector2(location.x - 0.64f, location.y), "Wall"))
								location = Vector2.zero;

							if (GC.tileInfo.IsOverlapping(location, "ObjectRealSprite", 0.64f))
								location = Vector2.zero;

							if (spawnedInChunks.Contains(tileData4.chunkID))
								location = Vector2.zero;

							if (GC.tileInfo.DestroyIfBetweenWalls(location))
								location = Vector2.zero;
						}

						attempts++;
					}
					while ((location == Vector2.zero || Vector2.Distance(location, GC.playerAgent.tr.position) < 5f) && attempts < 100);

					if (location != Vector2.zero)
					{
						GC.spawnerMain.spawnObjectReal(location, null, vObject.Speaker).ShiftTowardWalls();

						TileData tileData5 = GC.tileInfo.GetTileData(location);
						spawnedInChunks.Add(tileData5.chunkID);

						if (i < maxSpawns - 1 && GC.percentChance(25))
						{
							string direction = "";
							Vector2 zero4 = Vector2.zero;
							Vector2 zero5 = Vector2.zero;

							if (GC.tileInfo.GetTileData(new Vector2(location.x, location.y + 0.64f)).wallMaterial != wallMaterialType.None)
							{
								zero4 = new Vector2(location.x + 1.28f, location.y);
								zero5 = new Vector2(location.x - 1.28f, location.y);
								direction = "N";
							}
							else if (GC.tileInfo.GetTileData(new Vector2(location.x, location.y - 0.64f)).wallMaterial != wallMaterialType.None)
							{
								zero4 = new Vector2(location.x + 1.28f, location.y);
								zero5 = new Vector2(location.x - 1.28f, location.y);
								direction = "S";
							}
							else if (GC.tileInfo.GetTileData(new Vector2(location.x + 0.64f, location.y)).wallMaterial != wallMaterialType.None)
							{
								zero4 = new Vector2(location.x, location.y + 1.28f);
								zero5 = new Vector2(location.x, location.y - 1.28f);
								direction = "E";
							}
							else if (GC.tileInfo.GetTileData(new Vector2(location.x - 0.64f, location.y)).wallMaterial != wallMaterialType.None)
							{
								zero4 = new Vector2(location.x, location.y + 1.28f);
								zero5 = new Vector2(location.x, location.y - 1.28f);
								direction = "W";
							}

							GC.tileInfo.GetTileData(zero4);
							bool flag9 = true;

							if ((GC.tileInfo.GetTileData(new Vector2(zero4.x, zero4.y + 0.64f)).wallMaterial == wallMaterialType.None && direction == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x, zero4.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x + 0.64f, zero4.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x - 0.64f, zero4.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x + 0.64f, zero4.y)).wallMaterial == wallMaterialType.None && direction == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x - 0.64f, zero4.y)).wallMaterial != wallMaterialType.None && direction == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x - 0.64f, zero4.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x - 0.64f, zero4.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x, zero4.y - 0.64f)).wallMaterial == wallMaterialType.None && direction == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x, zero4.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x + 0.64f, zero4.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x - 0.64f, zero4.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x - 0.64f, zero4.y)).wallMaterial == wallMaterialType.None && direction == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x + 0.64f, zero4.y)).wallMaterial != wallMaterialType.None && direction == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x + 0.64f, zero4.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(zero4.x + 0.64f, zero4.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "W"))
								flag9 = false;

							if (GC.tileInfo.IsOverlapping(zero4, "Anything"))
								flag9 = false;

							if (GC.tileInfo.IsOverlapping(zero4, "ObjectRealSprite", 0.64f))
								flag9 = false;

							if (flag9 && zero4 != Vector2.zero)
							{
								GC.spawnerMain.spawnObjectReal(zero4, null, vObject.Speaker).ShiftTowardWalls();
								i++;
							}
							else
							{
								GC.tileInfo.GetTileData(zero5);
								flag9 = true;

								if ((GC.tileInfo.GetTileData(new Vector2(zero5.x, zero5.y + 0.64f)).wallMaterial == wallMaterialType.None &&
											direction == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x, zero5.y - 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x + 0.64f, zero5.y - 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x - 0.64f, zero5.y - 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x + 0.64f, zero5.y)).wallMaterial == wallMaterialType.None &&
											direction == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x - 0.64f, zero5.y)).wallMaterial != wallMaterialType.None &&
											direction == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x - 0.64f, zero5.y + 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x - 0.64f, zero5.y - 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x, zero5.y - 0.64f)).wallMaterial == wallMaterialType.None &&
											direction == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x, zero5.y + 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x + 0.64f, zero5.y + 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x - 0.64f, zero5.y + 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x - 0.64f, zero5.y)).wallMaterial == wallMaterialType.None &&
											direction == "W") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x + 0.64f, zero5.y)).wallMaterial != wallMaterialType.None &&
											direction == "W") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x + 0.64f, zero5.y + 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "W") ||
									(GC.tileInfo.GetTileData(new Vector2(zero5.x + 0.64f, zero5.y - 0.64f)).wallMaterial != wallMaterialType.None &&
											direction == "W"))
									flag9 = false;

								if (GC.tileInfo.IsOverlapping(zero5, "Anything"))
									flag9 = false;

								if (GC.tileInfo.IsOverlapping(zero5, "ObjectRealSprite", 0.64f))
									flag9 = false;

								if (flag9 && zero5 != Vector2.zero)
								{
									GC.spawnerMain.spawnObjectReal(zero5, null, vObject.Jukebox).ShiftTowardWalls();
									i++;
								}
							}
						}
					}
					Random.InitState(__instance.randomSeedNum + i);
				}
				spawnedInChunks = null;
			}
		}
		private static void SpawnLitter(LoadLevel __instance)
		{
			Debug.Log("SORCE: Loading Litter");

			int numObjects = (int)((5 - GC.levelTheme) * 20 * __instance.levelSizeModifier);

			for (int i = 0; i < numObjects; i++)
			{
				Vector2 location = Vector2.zero;
				int j = 0;

				do
				{
					location = GC.tileInfo.FindRandLocationGeneral(0f); // Vanilla 2f
					j++;
				} 
				while (location == Vector2.zero && j < 100);

				if (location != Vector2.zero)
					GC.spawnerMain.SpawnWreckagePileObject(location,
						GC.Choose<string>(vObject.Shelf, vObject.Lamp, vObject.Counter, vObject.VendorCart), false);

				//Random.InitState(__instance.randomSeedNum + i);
			}
		}
		private static void SpawnManholes_Underdark(LoadLevel __instance)
		{
			Debug.Log("SORCE: Loading Underdark Manholes");
			int bigTries = (int)((float)Random.Range(8, 12) * __instance.levelSizeModifier);

			for (int i = 0; i < bigTries; i++)
			{
				Vector2 spot = Vector2.zero;
				int spotsTried = 0;

				do
				{
					spot = GC.tileInfo.FindRandLocationGeneral(2f);

					for (int j = 0; j < GC.objectRealList.Count; j++)
						if (GC.objectRealList[j].objectName == vObject.Manhole && Vector2.Distance(GC.objectRealList[j].tr.position, spot) < 14f)
							spot = Vector2.zero;

					if (spot != Vector2.zero)
					{
						if (GC.tileInfo.WaterNearby(spot))
							spot = Vector2.zero;

						if (GC.tileInfo.IceNearby(spot))
							spot = Vector2.zero;

						if (GC.tileInfo.BridgeNearby(spot))
							spot = Vector2.zero;
					}

					spotsTried++;
				} while ((spot == Vector2.zero || Vector2.Distance(spot, GC.playerAgent.tr.position) < 5f) && spotsTried < 100);

				if (spot != Vector2.zero && Vector2.Distance(spot, GC.playerAgent.tr.position) >= 5f)
					GC.spawnerMain.spawnObjectReal(spot, null, vObject.Manhole);

				Random.InitState(__instance.randomSeedNum + i);
			}

			int numObjects = (int)((float)Random.Range(2, 4) * __instance.levelSizeModifier);
			List<Manhole> manholeList = new List<Manhole>();

			for (int i = 0; i < GC.objectRealList.Count; i++)
				if (GC.objectRealList[i].objectName == vObject.Manhole)
					manholeList.Add((Manhole)GC.objectRealList[i]);

			logger.LogDebug("UDManhole List count: " + manholeList.Count());

			// Hidden Agent Placement
			if (manholeList.Count > 0)
				for (int i = 0; i < numObjects; i++)
				{
					int attemptsToAddHiddenAgentToManhole = 0;
					Manhole manhole;
					bool NoHiddenAgentMatch;

					do
					{
						manhole = manholeList[Random.Range(0, manholeList.Count)];
						NoHiddenAgentMatch = true;

						for (int j = 0; j < GC.agentList.Count; j++)
							if (GC.agentList[j].oma.hidden && Vector2.Distance(manhole.tr.position, GC.agentList[j].tr.position) < 10f)
							{
								attemptsToAddHiddenAgentToManhole++;
								NoHiddenAgentMatch = false;
							}

						attemptsToAddHiddenAgentToManhole++;
					} while (attemptsToAddHiddenAgentToManhole < 50 && !NoHiddenAgentMatch);

					if (NoHiddenAgentMatch)
					{
						string agentType = vAgent.Thief;

						Agent agent2 = GC.spawnerMain.SpawnAgent(manhole.tr.position, manhole, agentType);
						agent2.SetDefaultGoal("Idle");
						agent2.statusEffects.BecomeHidden(manhole);
						agent2.oma.mustBeGuilty = true;
					}

					Random.InitState(__instance.randomSeedNum + i);
				}
		}
		private static void SpawnRugs()
		{
			// Alternative: replace all public floors with rug, it doesn't matter that much

		}
		private static void SpawnSecurityCamsAndTurrets(LoadLevel __instance)
		{
			logger.LogDebug("SORCE: Loading Public Security Cams");

			int bigTries = (int)((float)Random.Range(8, 12) * __instance.levelSizeModifier);
			List<int> spawnedInChunks = new List<int>();
			int num2;

			for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
			{
				Vector2 spotCandidate = Vector2.zero;
				int spotsTried = 0;

				do
				{
					spotCandidate = GC.tileInfo.FindRandLocationNearWall(0.64f);

					if (spotCandidate != Vector2.zero)
					{
						TileData spotTileData = GC.tileInfo.GetTileData(spotCandidate);

						//  We want these to generate in public.
						//if (GC.tileInfo.GetTileData(new Vector2(spotNearWall.x, spotNearWall.y + 0.64f)).owner == 0 &&
						//	GC.tileInfo.GetTileData(new Vector2(spotNearWall.x + 0.64f, spotNearWall.y)).owner == 0 &&
						//	GC.tileInfo.GetTileData(new Vector2(spotNearWall.x, spotNearWall.y - 0.64f)).owner == 0 &&
						//	GC.tileInfo.GetTileData(new Vector2(spotNearWall.x - 0.64f, spotNearWall.y)).owner == 0)
						//	spotNearWall = Vector2.zero;

						// What is the purpose of this one if we're using FindRandLocationNearWall(0.64f) above??
						if (!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x, spotCandidate.y + 0.64f), "Wall") &&
								!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x, spotCandidate.y - 0.64f), "Wall") &&
								!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x + 0.64f, spotCandidate.y), "Wall") &&
								!GC.tileInfo.IsOverlapping(new Vector2(spotCandidate.x - 0.64f, spotCandidate.y), "Wall"))
						{
							spotCandidate = Vector2.zero;
						}

						if (GC.tileInfo.IsOverlapping(spotCandidate, "ObjectRealSprite", 0.64f))
							spotCandidate = Vector2.zero;

						if (spawnedInChunks.Contains(spotTileData.chunkID))
							spotCandidate = Vector2.zero;

						if (GC.tileInfo.DestroyIfBetweenWalls(spotCandidate))
							spotCandidate = Vector2.zero;
					}

					spotsTried++;
				} while ((spotCandidate == Vector2.zero || Vector2.Distance(spotCandidate, GC.playerAgent.tr.position) < 5f) && spotsTried < 100);

				if (spotCandidate != Vector2.zero)
				{
					ObjectReal securityCam = GC.spawnerMain.spawnObjectReal(spotCandidate, null, vObject.SecurityCam);
					securityCam.ShiftTowardWalls();

					securityCam.owner = 85;
					SecurityCam securityCamCast = (SecurityCam)securityCam;
					securityCamCast.securityType = "Noise";
					securityCamCast.targets = "Wanted";
					TileData spotTileData = GC.tileInfo.GetTileData(spotCandidate);
					spawnedInChunks.Add(spotTileData.chunkID);

					if (numObjects < bigTries - 1 && GC.percentChance(25))
					{
						string wallEdge = "";
						Vector2 leftOfSpot = Vector2.zero;
						Vector2 rightOfSpot = Vector2.zero;

						if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x, spotCandidate.y + 0.64f)).wallMaterial != wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x + 1.28f, spotCandidate.y);
							rightOfSpot = new Vector2(spotCandidate.x - 1.28f, spotCandidate.y);
							wallEdge = "N";
							securityCam.direction = "S";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x, spotCandidate.y - 0.64f)).wallMaterial !=
								wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x + 1.28f, spotCandidate.y);
							rightOfSpot = new Vector2(spotCandidate.x - 1.28f, spotCandidate.y);
							wallEdge = "S";
							securityCam.direction = "N";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x + 0.64f, spotCandidate.y)).wallMaterial !=
								wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x, spotCandidate.y + 1.28f);
							rightOfSpot = new Vector2(spotCandidate.x, spotCandidate.y - 1.28f);
							wallEdge = "E";
							securityCam.direction = "W";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x - 0.64f, spotCandidate.y)).wallMaterial !=
								wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x, spotCandidate.y + 1.28f);
							rightOfSpot = new Vector2(spotCandidate.x, spotCandidate.y - 1.28f);
							wallEdge = "W";
							securityCam.direction = "E";
						}

						GC.tileInfo.GetTileData(leftOfSpot);
						bool isSpotAcceptable = true;

						#region Refactor - for readability, not performance

						//// Proceeding clockwise:
						//Vector2 neighborN = new Vector2(leftOfSpot.x, leftOfSpot.y + 0.64f);
						//Vector2 neighborNE = new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y + 0.64f);
						//Vector2 neighborE = new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y);
						//Vector2 neighborSE = new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y - 0.64f);
						//Vector2 neighborS = new Vector2(leftOfSpot.x, leftOfSpot.y - 0.64f);
						//Vector2 neibhorSW = new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y - 0.64f);
						//Vector2 neighborW = new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y);
						//Vector2 neighborNW = new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y + 0.64f);

						//switch (wallEdge)
						//{
						//	case "N":
						//		if ((GC.tileInfo.GetTileData(neighborN).wallMaterial == wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborS).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborSE).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neibhorSW).wallMaterial != wallMaterialType.None))
						//			isSpotAcceptable = false;

						//		break;

						//	case "E":
						//		if ((GC.tileInfo.GetTileData(neighborE).wallMaterial == wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborW).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborNW).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neibhorSW).wallMaterial != wallMaterialType.None))
						//			isSpotAcceptable = false;

						//		break;

						//	case "S":
						//		if ((GC.tileInfo.GetTileData(neighborS).wallMaterial == wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborN).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborNE).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborNW).wallMaterial != wallMaterialType.None))
						//			isSpotAcceptable = false;

						//		break;

						//	case "W":
						//		if ((GC.tileInfo.GetTileData(neighborW).wallMaterial == wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborE).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborNE).wallMaterial != wallMaterialType.None) ||
						//			(GC.tileInfo.GetTileData(neighborSE).wallMaterial != wallMaterialType.None))
						//			isSpotAcceptable = false;

						//		break;
						//}

						#endregion

						if ((GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y + 0.64f)).wallMaterial == wallMaterialType.None &&
										wallEdge == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None &&
										wallEdge == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y)).wallMaterial == wallMaterialType.None &&
										wallEdge == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y)).wallMaterial != wallMaterialType.None &&
										wallEdge == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y - 0.64f)).wallMaterial == wallMaterialType.None &&
										wallEdge == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None &&
										wallEdge == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y)).wallMaterial == wallMaterialType.None &&
										wallEdge == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y)).wallMaterial != wallMaterialType.None &&
										wallEdge == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y + 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y - 0.64f)).wallMaterial !=
										wallMaterialType.None && wallEdge == "W"))
							isSpotAcceptable = false;

						if (GC.tileInfo.IsOverlapping(leftOfSpot, "Anything"))
							isSpotAcceptable = false;

						if (GC.tileInfo.IsOverlapping(leftOfSpot, "ObjectRealSprite", 0.64f))
							isSpotAcceptable = false;

						if (isSpotAcceptable && leftOfSpot != Vector2.zero)
						{
							ObjectReal turret1 = GC.spawnerMain.spawnObjectReal(leftOfSpot, null, vObject.Turret);
							turret1.ShiftTowardWalls();

							turret1.direction = securityCam.direction;
							turret1.owner = 85;
							num2 = numObjects;
							numObjects = num2 + 1;
						}
						else
						{
							GC.tileInfo.GetTileData(rightOfSpot);
							isSpotAcceptable = true;

							if ((GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y + 0.64f)).wallMaterial == wallMaterialType.None &&
											wallEdge == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y - 0.64f)).wallMaterial
											!= wallMaterialType.None &&
											wallEdge == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "N") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y)).wallMaterial
											== wallMaterialType.None &&
											wallEdge == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y)).wallMaterial
											!= wallMaterialType.None &&
											wallEdge == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "E") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y - 0.64f)).wallMaterial
											== wallMaterialType.None &&
											wallEdge == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y + 0.64f)).wallMaterial
											!= wallMaterialType.None &&
											wallEdge == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "S") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y)).wallMaterial
											== wallMaterialType.None &&
											wallEdge == "W") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y)).wallMaterial
											!= wallMaterialType.None &&
											wallEdge == "W") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y + 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "W") ||
									(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y - 0.64f)).wallMaterial !=
											wallMaterialType.None && wallEdge == "W"))
								isSpotAcceptable = false;

							if (GC.tileInfo.IsOverlapping(rightOfSpot, "Anything"))
								isSpotAcceptable = false;

							if (GC.tileInfo.IsOverlapping(rightOfSpot, "ObjectRealSprite", 0.64f))
								isSpotAcceptable = false;

							if (isSpotAcceptable && rightOfSpot != Vector2.zero)
							{
								ObjectReal turret2 = GC.spawnerMain.spawnObjectReal(rightOfSpot, null, vObject.Turret);
								turret2.ShiftTowardWalls();

								turret2.direction = securityCam.direction;
								turret2.owner = 85;
								securityCamCast.turrets.Add((Turret)turret2);
								securityCamCast.securityType = "Turret";
								num2 = numObjects;
								numObjects = num2 + 1;
							}
						}
					}
				}

				Random.InitState(__instance.randomSeedNum + numObjects);
				num2 = numObjects;
			}
		}
		private static void SpawnTurntables()
		{
			int maximumLocations = 6;
			float distanceBetween = 28f;

			for (int i = 0; i < maximumLocations; i++)
			{
				Vector2 location = Vector2.zero;
				int j = 0;

				do
				{
					location = GC.tileInfo.FindRandLocationGeneral(2f);

					for (int k = 0; k < GC.objectRealList.Count; k++)
						if (GC.objectRealList[k].objectName == vObject.Turntables &&
								Vector2.Distance(GC.objectRealList[k].tr.position, location) < distanceBetween)
							location = Vector2.zero;

					if (location != Vector2.zero)
					{
						if (GC.tileInfo.WaterNearby(location))
							location = Vector2.zero;

						if (GC.tileInfo.IceNearby(location))
							location = Vector2.zero;

						if (GC.tileInfo.BridgeNearby(location))
							location = Vector2.zero;
					}

					j++;
				}
				while (j < 100 &&
					(location == Vector2.zero ||
					Vector2.Distance(location, GC.playerAgent.tr.position) < 5f ||
					Vector2.Distance(location, GC.elevatorDown.tr.position) < 5f));

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, vObject.Turntables);
			}
		}
	}
}
