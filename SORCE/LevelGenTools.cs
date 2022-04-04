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
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Features;
using SORCE.Localization;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Roamers;
using static SORCE.Localization.NameLists;
using SORCE.Challenges.C_Wreckage;
using SORCE.Challenges.C_Population;
using HarmonyLib;
using System.Reflection;
using SORCE.Challenges.C_Audio;

namespace SORCE
{
	class LevelGenTools
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static wallMaterialType BorderWallMaterial()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(Overhauls))
			{
				case (nameof(Arcology)):
					return wallMaterialType.Wood;
				case (nameof(CanalCity)):
					return wallMaterialType.Normal;
				case (nameof(DiscoCityDanceoff)):
					return (wallMaterialType.Steel);
				//case (nameof(DUMP)):
				//	return wallMaterialType.Cave;
				// Not in enum
				case (nameof(Eisburg)):
					return wallMaterialType.Glass;
				case (nameof(GrandCityHotel)):
					return wallMaterialType.Wood;
				case (nameof(LowTechLowLife)):
					return wallMaterialType.Normal;
				case (nameof(Technocracy)):
					return wallMaterialType.Steel;
				case (nameof(TestTubeCity)):
					return wallMaterialType.Glass;
				case (nameof(Tindertown)):
					return wallMaterialType.Wood;
				default:
					return wallMaterialType.Border;
			}
		}
		public static string BorderWallType()
        {
			switch (ChallengeManager.GetActiveChallengeFromList(Overhauls))
			{
				case (nameof(Arcology)):
					return vWall.Hedge; // TODO: Could do this one Hedge and the solid blocks of Borderwall as Wood
				case (nameof(CanalCity)):
					return vWall.Brick;
				case (nameof(DiscoCityDanceoff)):
					return (vWall.Steel);
				case (nameof(DUMP)):
					return vWall.Cave;
				case (nameof(Eisburg)):
					return vWall.Glass;
				case (nameof(GrandCityHotel)):
					return vWall.Wood;
				case (nameof(LowTechLowLife)):
					return vWall.Brick;
				case (nameof(Technocracy)):
					return vWall.Steel;
				case (nameof(TestTubeCity)):
					return vWall.Glass;
				case (nameof(Tindertown)):
					return vWall.Wood;
				default:
					return vWall.Border;
			}
		}
		public static string RandomRugType()
		{
			var random = new System.Random();
			int index = random.Next(vFloor.Rugs.Count);

			return vFloor.Rugs[index];
		}
		public static string PublicFloorTile()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(Overhauls))
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
				case nameof(Eisburg):
					return vFloor.IceRink;
				default:
					return null; 
			}
		}
		public static string PublicFloorTileGroup()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(Overhauls))
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
				case nameof(Eisburg):
					return vFloorTileGroup.Ice;
				default:
					return vFloorTileGroup.Building;
			}
		}
		public static string FenceWallType()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(NameLists.BuildingsNames))
			{
				case nameof(CityOfSteel):
					return vWall.Bars;
				case nameof(GreenLiving):
					return vWall.BarbedWire;
				case nameof(Panoptikopolis):
					return vWall.Bars;
				case nameof(ShantyTown):
					return vWall.BarbedWire;
				default:
					return null;
			}
		}
		public static string BuildingWallType()
		{
			switch (ChallengeManager.GetActiveChallengeFromList(BuildingsNames))
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
				default:
					return null;
			}
		}
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
		public static int PopulationMultiplier() =>
			GC.challenges.Contains(nameof(GhostTown)) ? 0 :
			GC.challenges.Contains(nameof(HordeAlmighty)) ? 2 :
			GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 4 :
			GC.challenges.Contains(nameof(SwarmWelcome)) ? 8 :
			1;
		public static int PopulationMafia(int vanilla) => 
			vanilla;
		public static string AmbientAudio(string trackname, string chunkType)
		{
			logger.LogDebug("AmbientAudio");
			logger.LogDebug("trackName: " + trackname);
			logger.LogDebug("chunkType: " + chunkType);

			if (!Core.debugMode && !GC.challenges.Contains(nameof(AmbienterAmbience)))
				return trackname;

			if (chunkType == vChunkType.Casino)
				trackname = vAmbience.Casino;
			else if (
				chunkType != vChunkType.Bathhouse &&
				chunkType != vChunkType.Casino &&
				chunkType != vChunkType.Cave &&
				chunkType != vChunkType.CityPark &&
				chunkType != vChunkType.Graveyard)
			{
				if (GC.challenges.Contains(nameof(Arcology)))
					trackname = vAmbience.Park;

				if (GC.challenges.Contains(nameof(DiscoCityDanceoff)))
					trackname = vAmbience.ClubMusic; // ClubMusic, ClubMusic_Long, ClubMusic_Huge
				
				if (GC.challenges.Contains(nameof(DUMP)))
					trackname = vAmbience.Cave;

				if (GC.challenges.Contains(nameof(Eisburg)) ||
					GC.challenges.Contains(nameof(GhostTown)) ||
					GC.challenges.Contains(nameof(Hell)))
					trackname = vAmbience.Graveyard;
			}

			logger.LogDebug("\tResult: " + trackname);

			return trackname;
		}
		public static bool HasBarbecues(bool vanilla) =>
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(Arcology)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasBoulders(bool vanilla) => 
			GC.challenges.Contains(nameof(Arcology))  ||
			GC.challenges.Contains(nameof(DUMP)) ||
			GC.challenges.Contains(nameof(SpelunkyDory)) ||
			Core.debugMode ||
            vanilla;
		public static bool HasBrokenWindows =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
			(GC.customLevel && GC.loadLevel.customLevel.levelFeatures.Contains(cLevelFeature.BrokenWindows)) ||
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(BadNeighborhoods));
        public static bool HasBushes(bool vanilla) =>
			GC.challenges.Contains(nameof(Arcology)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasCaveWallOutcroppings =>
			GC.challenges.Contains(nameof(DUMP)) ||
			GC.challenges.Contains(nameof(SpelunkyDory));
		public static bool HasCopBots(bool vanilla) =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(PoliceState)) ||
			GC.challenges.Contains(nameof(Technocracy)) || 
			vanilla; 
		public static bool HasCops(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(Technocracy)) &&
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState)) ||
			vanilla;
		public static bool HasCopsExtra(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(Technocracy)) &&
			GC.challenges.Contains(nameof(PoliceState)) ||
			vanilla; 
		public static bool HasFireHydrants(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) && 
			!GC.challenges.Contains(nameof(LowTechLowLife)) ||
			vanilla; 
		public static bool HasFlamingBarrels(bool vanilla) =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			vanilla;
		public static bool HasFountains =>
			GC.challenges.Contains(nameof(BroughtbackFountain)) ||
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState));
		public static bool HasGangbangers(bool vanilla) =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(YoungMenInTheNeighborhood)) ||
			GC.challenges.Contains(nameof(AnCapistan)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasPublicLamps(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(MACITS)) ||
			vanilla;
		public static bool HasLandMines(bool vanilla) =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(ThisLandIsMineLand)) ||
			vanilla;
		public static bool HasLitter =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(Arcology)) || // Leaves
			GC.challenges.Contains(nameof(DirtierDistricts)) ||
			GC.challenges.Contains(nameof(DUMP)) || // Rock Debris (FlamingBarrel)
			GC.challenges.Contains(nameof(Eisburg)) || // Ice chunks
			GC.challenges.Contains(nameof(Tindertown)) || // Ashes
			Core.debugMode;
		public static bool HasManholesVanilla(bool vanilla) => 
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			// UnderdankCitizen uses a different method, SpawnManholes_Underdank.
			vanilla;
		public static bool HasManholesUnderdank =>
			TraitManager.IsPlayerTraitActive<UnderdankCitizen>();
		public static bool HasMobsters(bool vanilla) =>
			!GC.challenges.Contains(nameof(PoliceState)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(UnionTown)) ||
			vanilla;
		public static bool HasPollutionFeatures(bool vanilla) =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(ThePollutionSolution)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasPoliceBoxesAndAlarmButtons(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState)) || 
			vanilla;
		public static bool HasPowerBoxes(bool vanilla) =>
			GC.challenges.Contains(nameof(PowerWhelming)) || 
			GC.challenges.Contains(nameof(Technocracy)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasRugs =>
			GC.challenges.Contains(nameof(GrandCityHotel)) ||
			GC.challenges.Contains(nameof(DepartmentOfPublicComfiness)) ||
			Core.debugMode;
		public static bool HasScreens =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			Core.debugMode;
		public static bool HasSecurityCamsAndTurrets =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(PoliceState)) ||
			GC.challenges.Contains(nameof(SurveillanceSociety)) ||
			Core.debugMode;
		public static bool HasSlimeBarrels() =>
			GC.challenges.Contains(nameof(ThePollutionSolution)) ||
			GC.challenges.Contains(nameof(AnCapistan)) ||
			Core.debugMode;
		public static bool HasTrashCans(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(Arcology)) ||
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasTrees(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(Arcology)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasVendorCarts(bool vanilla) =>
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(CartOfTheDeal)) || 
			GC.challenges.Contains(nameof(MACITS)) ||
			Core.debugMode ||
			vanilla;
		public static string RoamerAgentType(string vanilla)
		{
			// TODO: Adjustments for MACITS, etc.

			if (vanilla == vAgent.Thief)
			{
				int thiefReduction =
					GC.challenges.Contains(nameof(HordeAlmighty)) ? 50 :
					GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 75 :
					GC.challenges.Contains(nameof(SwarmWelcome)) ? 87 :
					0;

				if (thiefReduction != 0 && GC.percentChance(thiefReduction))
					vanilla = 
						GC.levelTheme == 4 || GC.levelTheme == 5
					? vAgent.UpperCruster
					: vAgent.SlumDweller;
			}

			return vanilla;
		}
		public static void SetHasLakes(LoadLevel loadLevel) =>
			loadLevel.hasLakes =
				GC.challenges.Contains(nameof(Arcology)) ||
				GC.challenges.Contains(nameof(LakeItOrLeaveIt)) ||
				loadLevel.hasLakes;
		public static void SetHasFlameGrates(LoadLevel loadLevel) =>
			loadLevel.hasFlameGrates =
				!GC.challenges.Contains(nameof(LowTechLowLife)) &&
				GC.challenges.Contains(nameof(ThePollutionSolution)) ||
				GC.challenges.Contains(nameof(Technocracy)) ||
				Core.debugMode ||
				loadLevel.hasFlameGrates;
		public static void SetLevelSizeMax(LoadLevel loadLevel)
		{
			int newVal = loadLevel.levelSizeMax;

			string active = ChallengeManager.GetActiveChallengeFromList(MapSize);

			if (active == nameof(Arthropolis))
				newVal = 4;
			else if (active == nameof(Claustropolis))
				newVal = 12;
			else if (active == nameof(Megapolis))
				newVal = 48;
			else if (active == nameof(Ultrapolis))
				newVal = 64;

			loadLevel.levelSizeMax = newVal;
		}
		public static void Spawn_Master(LoadLevel loadLevel)
		{
			if (HasScreens)
				SpawnPublicScreens(loadLevel);

			if (HasBrokenWindows)
				BreakWindows();

			if (HasCaveWallOutcroppings)
				SpawnCaveWallOutcroppings(loadLevel);

			if (HasFountains)
				SpawnFountains();

			if (GC.challenges.Contains(nameof(DepartmentOfPublicComfiness)))
				SpawnCoziness();

			if (GC.challenges.Contains(nameof(DiscoCityDanceoff)))
			{
				SpawnJukeboxesAndSpeakers(loadLevel); 
				SpawnTurntables();
			}

			if (HasLitter)
				SpawnLitter(loadLevel);

			if (HasManholesUnderdank)
				SpawnManholes_Underdank(loadLevel);
			 
			if (HasRugs)
				SpawnRugs();

			if (HasSecurityCamsAndTurrets)
				SpawnSecurityCamsAndTurrets(loadLevel);

			if (HasSlimeBarrels())
				SpawnSlimeBarrels(loadLevel);
		}
		private static void BreakWindows()
		{
			int chanceToBreak = 10 - GC.levelTheme * 2;

			foreach (ObjectReal objectReal in GC.objectRealList)
				if (objectReal is Window window && GC.percentChance(chanceToBreak))
					window.DamagedObject(window, 0f);
		}
		private static void SpawnCaveWallOutcroppings(LoadLevel loadLevel)
		{
			Debug.Log("SORCE: Loading SpelunkyDory Cave Wall Outcroppings");

			int maxSpawns = (int)((float)Random.Range(48, 64) * LevelSizeRatio());
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
				} 
				while ((spotCandidate == Vector2.zero || Vector2.Distance(spotCandidate, GC.playerAgent.tr.position) < 5f) && spotsTried < 100);

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
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x, spotCandidate.y - 0.64f)).wallMaterial != wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x + 1.28f, spotCandidate.y);
							rightOfSpot = new Vector2(spotCandidate.x - 1.28f, spotCandidate.y);
							adjoiningWall = "S";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x + 0.64f, spotCandidate.y)).wallMaterial != wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x, spotCandidate.y + 1.28f);
							rightOfSpot = new Vector2(spotCandidate.x, spotCandidate.y - 1.28f);
							adjoiningWall = "E";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spotCandidate.x - 0.64f, spotCandidate.y)).wallMaterial != wallMaterialType.None)
						{
							leftOfSpot = new Vector2(spotCandidate.x, spotCandidate.y + 1.28f);
							rightOfSpot = new Vector2(spotCandidate.x, spotCandidate.y - 1.28f);
							adjoiningWall = "W";
						}

						GC.tileInfo.GetTileData(leftOfSpot);
						bool isSpotAcceptable = true;

						if ((GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y + 0.64f)).wallMaterial == wallMaterialType.None && adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "N") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y)).wallMaterial == wallMaterialType.None && adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y)).wallMaterial != wallMaterialType.None && adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "E") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y - 0.64f)).wallMaterial == wallMaterialType.None && adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x, leftOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "S") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x - 0.64f, leftOfSpot.y)).wallMaterial == wallMaterialType.None && adjoiningWall == "W") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y)).wallMaterial != wallMaterialType.None && adjoiningWall == "W") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "W") ||
							(GC.tileInfo.GetTileData(new Vector2(leftOfSpot.x + 0.64f, leftOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "W"))
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

							if ((GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y + 0.64f)).wallMaterial == wallMaterialType.None && adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "N") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y)).wallMaterial == wallMaterialType.None && adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y)).wallMaterial != wallMaterialType.None && adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "E") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y - 0.64f)).wallMaterial == wallMaterialType.None && adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x, rightOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "S") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x - 0.64f, rightOfSpot.y)).wallMaterial == wallMaterialType.None && adjoiningWall == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y)).wallMaterial != wallMaterialType.None && adjoiningWall == "W") ||
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y + 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "W") || 
								(GC.tileInfo.GetTileData(new Vector2(rightOfSpot.x + 0.64f, rightOfSpot.y - 0.64f)).wallMaterial != wallMaterialType.None && adjoiningWall == "W"))
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

				Random.InitState(loadLevel.randomSeedNum + i);
				itemCountIterator = i;
			}
		}
		private static void SpawnCoziness()
		{
			// TODO: Fireplace in middle, armchairs on sides
		}
		private static void SpawnFountains()
		{
			Debug.Log("SORCE: Loading Fountains");
			int numObjects = Mathf.Clamp(2 * LevelSizeRatio(), 1, 5);
			float objectBuffer = 14f;

			for (int i = 0; i < numObjects; i++)
			{
				Vector2 location = Vector2.zero;
				int attempts = 0;

				do
				{
					location = GC.tileInfo.FindRandLocationGeneral(2f);

					for (int j = 0; j < GC.objectRealList.Count; j++)
						if (GC.objectRealList[j].objectName == vObject.Fountain && Vector2.Distance(GC.objectRealList[j].tr.position, location) < (objectBuffer * LevelSizeRatio()))
							location = Vector2.zero;

					attempts++;
				} 
				while ((location == Vector2.zero || 
					Vector2.Distance(location, GC.playerAgent.tr.position) < 5f) && attempts < 100);

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, vObject.Fountain);
			}
		}
		private static void SpawnJukeboxesAndSpeakers(LoadLevel loadLevel)
		{
			if (GC.challenges.Contains(nameof(DiscoCityDanceoff)))
			{
				Debug.Log("SORCE: Loading Disco Shit");
				int maxSpawns = (int)((float)Random.Range(6, 12) * LevelSizeRatio());
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
					Random.InitState(loadLevel.randomSeedNum + i);
				}
				spawnedInChunks = null;
			}
		}
		private static void SpawnLitter(LoadLevel loadLevel)
		{
			int numObjects = (int)((5 - GC.levelTheme) * 100 * LevelSizeRatio());

			for (int i = 0; i < numObjects; i++)
			{
				// Vector2 location = GC.tileInfo.FindRandLocationGeneral(0f); // Vanilla 2f
				Vector2 location = FindRandLocationGeneral_NearWall(GC.tileInfo, 0.2f);

				GC.spawnerMain.SpawnWreckagePileObject(location, OverhaulWreckageType(), false);
			}
		}
		private static string OverhaulWreckageType()
        {
			// TODO: Call SpawnWreckagePileObject in here, because you need to determine whether trash is burnt or not
			if (ChallengeManager.IsChallengeFromListActive(Overhauls))
            {
				switch (ChallengeManager.GetActiveChallengeFromList(Overhauls)) 
				{
					case nameof(Arcology):
						return vObject.Bush;
					case nameof(DUMP): // Rock
						return vObject.FlamingBarrel;
					case nameof(Eisburg): // Ice chunks, but see notes to see if ice gibs are preferable
						return vObject.Refrigerator;
					case nameof(Hell): // Rock
						return vObject.FlamingBarrel;
					case nameof(Tindertown): // Ashes
						return vObject.Bush;
				}
            }

			// Regular trash
			return GC.Choose(vObject.Shelf, vObject.MovieScreen, vObject.Counter, vObject.VendorCart, vObject.Window);
		}
		public static void SpawnWreckagePileObject_Granular(Vector3 targetLoc, string objectType, bool burnt, int gibs, float radX, float radY)
        {
			for (int i = 0; i < gibs; i++)
			{
				string wreckageType = objectType + "Wreckage" + (Random.Range(1, 5)).ToString();
				targetLoc = new Vector3(
					targetLoc.x + Random.Range(radX * -1, radX), 
					targetLoc.y + Random.Range(radY * -1, radY), 0f);
				GC.spawnerMain.SpawnWreckage2(targetLoc, wreckageType, burnt);
			}
		}
		private static Vector2 FindRandLocationGeneral_NearWall(TileInfo tileInfo, float maxDistanceToWall)
        {
			for (int i = 0; i < 200; i++)
			{
				float minInclusive = 1.28f;
				float maxInclusive = GC.loadLevel.levelSizePixels - 1.28f;
				float minInclusive2 = 1.28f;
				float maxInclusive2 = GC.loadLevel.levelSizePixels - 1.28f;

				Vector2 vector = new Vector2(
					(Random.Range(minInclusive, maxInclusive)),
					(Random.Range(minInclusive2, maxInclusive2)));
				bool badSpot = false;
				TileData tileData = tileInfo.GetTileData(vector);

				if (tileData.owner > 0 ||
					tileData.prison > 0 ||
					tileData.hole ||
					tileData.water ||
					tileData.ice ||
					tileData.conveyorBelt ||
					tileData.dangerousToWalk ||
					//tileData.solidObject ||
					tileData.wallColliderUnreachable ||
					!IsCloseToWall(tileInfo, vector, maxDistanceToWall))
					badSpot = true;

				if (!badSpot)
					return vector;
			}

			return Vector2.zero;
		}
		private static bool IsCloseToWall(TileInfo tileInfo, Vector2 pos, float circleRadius)
        {
			int num;
			Collider2D[] tileInfo_hitsAlloc = new Collider2D[100];

			if (circleRadius == 0f)
				num = Physics2D.OverlapPointNonAlloc(pos, tileInfo_hitsAlloc);
			else
				num = Physics2D.OverlapCircleNonAlloc(pos, circleRadius, tileInfo_hitsAlloc);
			
			for (int i = 0; i < num; i++)
			{
				Collider2D collider2D = tileInfo_hitsAlloc[i];

				if (collider2D.CompareTag("Wall"))
					return true;
			}

			return false;
		}
		/// <summary>
		/// This was intentionally made separate from the vanilla algorithm
		/// TODO: Identify and comment the differences to verify that it's necessary.
		///		Possibly just a level gate?
		/// </summary>
		/// <param name="loadLevel"></param>
		private static void SpawnManholes_Underdank(LoadLevel loadLevel)
		{
			Debug.Log("SORCE: Loading Underdank Manholes");
			int bigTries = (int)((float)Random.Range(8, 12) * LevelSizeRatio());
			logger.LogDebug("\t\tSpawn Count: " + bigTries);

			for (int i = 0; i < bigTries; i++)
			{
				Vector2 spot = Vector2.zero;
				int spotsTried = 0;

				do
				{
					spot = GC.tileInfo.FindRandLocationGeneral(2f);

					for (int j = 0; j < GC.objectRealList.Count; j++)
						if (GC.objectRealList[j].objectName == vObject.Manhole && 
							Vector2.Distance(GC.objectRealList[j].tr.position, spot) < 2f) // Vanilla 14f
							spot = Vector2.zero;

					if (spot != Vector2.zero)
					{
						if (GC.tileInfo.WaterNearby(spot) ||
							GC.tileInfo.IceNearby(spot) ||
							GC.tileInfo.BridgeNearby(spot))
							spot = Vector2.zero;
					}

					spotsTried++;
				} 
				while ((spot == Vector2.zero || Vector2.Distance(spot, GC.playerAgent.tr.position) < 5f) && spotsTried < 100);

				if (spot != Vector2.zero && Vector2.Distance(spot, GC.playerAgent.tr.position) >= 5f)
					GC.spawnerMain.spawnObjectReal(spot, null, vObject.Manhole);
			}

			List<Manhole> manholeList = new List<Manhole>();

			for (int i = 0; i < GC.objectRealList.Count; i++)
				if (GC.objectRealList[i].objectName == vObject.Manhole)
					manholeList.Add((Manhole)GC.objectRealList[i]);

			logger.LogDebug("UDManhole List count: " + manholeList.Count());

			int hiddenAgents = (int)((float)Random.Range(2, 4) * LevelSizeRatio());
			
			if (manholeList.Count > 0)
				for (int i = 0; i < hiddenAgents; i++)
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
					} 
					while (attemptsToAddHiddenAgentToManhole < 50 && !NoHiddenAgentMatch);

					if (NoHiddenAgentMatch)
					{
						string agentType = vAgent.Thief;

						Agent agent2 = GC.spawnerMain.SpawnAgent(manhole.tr.position, manhole, agentType);
						agent2.SetDefaultGoal("Idle");
						agent2.statusEffects.BecomeHidden(manhole);
						agent2.oma.mustBeGuilty = true;
					}
				}
		}
		private static void SpawnRugs()
		{
			// Alternative: replace all public floors with rug, it doesn't matter that much

		}
		private static void SpawnPublicScreens(LoadLevel loadLevel)
        {
			Debug.Log("SORCE: Loading Public Screens");

			int bigTries = (int)((float)Random.Range(6, 12) * LevelSizeRatio());
			List<int> spawnedInChunks = new List<int>();
			int num2;

			for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
			{
				Vector2 spot = Vector2.zero;
				int attempts = 0;

				while (attempts < 100 &&
					(spot == Vector2.zero || Vector2.Distance(spot, GC.playerAgent.tr.position) < 5f))
				{
					spot = GC.tileInfo.FindRandLocationNearWall(0.64f);

					if (spot != Vector2.zero)
					{
						if (GC.tileInfo.GetTileData(new Vector2(spot.x, spot.y + 0.64f)).owner == 0 && 
							GC.tileInfo.GetTileData(new Vector2(spot.x + 0.64f, spot.y)).owner == 0 && 
							GC.tileInfo.GetTileData(new Vector2(spot.x, spot.y - 0.64f)).owner == 0 && 
							GC.tileInfo.GetTileData(new Vector2(spot.x - 0.64f, spot.y)).owner == 0)
							spot = Vector2.zero;
						
						if (!GC.tileInfo.IsOverlapping(new Vector2(spot.x, spot.y + 0.64f), "Wall") && 
							!GC.tileInfo.IsOverlapping(new Vector2(spot.x, spot.y - 0.64f), "Wall") && 
							!GC.tileInfo.IsOverlapping(new Vector2(spot.x + 0.64f, spot.y), "Wall") && 
							!GC.tileInfo.IsOverlapping(new Vector2(spot.x - 0.64f, spot.y), "Wall"))
							spot = Vector2.zero;
						
						if (GC.tileInfo.IsOverlapping(spot, "ObjectRealSprite", 0.64f))
							spot = Vector2.zero;
						
						if (spawnedInChunks.Contains(GC.tileInfo.GetTileData(spot).chunkID))
							spot = Vector2.zero;
						
						if (GC.tileInfo.DestroyIfBetweenWalls(spot))
							spot = Vector2.zero;
					}

					attempts++;
				}

				if (spot != Vector2.zero)
				{
					ObjectReal movieScreen = GC.spawnerMain.spawnObjectReal(spot, null, vObject.MovieScreen);
					movieScreen.ShiftTowardWalls();
					movieScreen.ambientAudio = vAmbience.Casino;
					TileData tileData = GC.tileInfo.GetTileData(spot);
					spawnedInChunks.Add(tileData.chunkID);

					if (true)
					{
						string direction = "";
						Vector2 neighborCell1 = Vector2.zero;
						Vector2 neighborCell2 = Vector2.zero;

						if (GC.tileInfo.GetTileData(new Vector2(spot.x, spot.y + 0.64f)).wallMaterial != wallMaterialType.None)
						{
							neighborCell1 = new Vector2(spot.x + 1.28f, spot.y);
							neighborCell2 = new Vector2(spot.x - 1.28f, spot.y);
							direction = "N";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spot.x, spot.y - 0.64f)).wallMaterial != wallMaterialType.None)
						{
							neighborCell1 = new Vector2(spot.x + 1.28f, spot.y);
							neighborCell2 = new Vector2(spot.x - 1.28f, spot.y);
							direction = "S";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spot.x + 0.64f, spot.y)).wallMaterial != wallMaterialType.None)
						{
							neighborCell1 = new Vector2(spot.x, spot.y + 1.28f);
							neighborCell2 = new Vector2(spot.x, spot.y - 1.28f);
							direction = "E";
						}
						else if (GC.tileInfo.GetTileData(new Vector2(spot.x - 0.64f, spot.y)).wallMaterial != wallMaterialType.None)
						{
							neighborCell1 = new Vector2(spot.x, spot.y + 1.28f);
							neighborCell2 = new Vector2(spot.x, spot.y - 1.28f);
							direction = "W";
						}

						bool spotUsable = true;

						if ((GC.tileInfo.GetTileData(new Vector2(neighborCell1.x, neighborCell1.y + 0.64f)).wallMaterial == wallMaterialType.None && direction == "N") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x, neighborCell1.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x + 0.64f, neighborCell1.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x - 0.64f, neighborCell1.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x + 0.64f, neighborCell1.y)).wallMaterial == wallMaterialType.None && direction == "E") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x - 0.64f, neighborCell1.y)).wallMaterial != wallMaterialType.None && direction == "E") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x - 0.64f, neighborCell1.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "E") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x - 0.64f, neighborCell1.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "E") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x, neighborCell1.y - 0.64f)).wallMaterial == wallMaterialType.None && direction == "S") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x, neighborCell1.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x + 0.64f, neighborCell1.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x - 0.64f, neighborCell1.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x - 0.64f, neighborCell1.y)).wallMaterial == wallMaterialType.None && direction == "W") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x + 0.64f, neighborCell1.y)).wallMaterial != wallMaterialType.None && direction == "W") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x + 0.64f, neighborCell1.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "W") || 
							(GC.tileInfo.GetTileData(new Vector2(neighborCell1.x + 0.64f, neighborCell1.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "W"))
							spotUsable = false;
						
						if (GC.tileInfo.IsOverlapping(neighborCell1, "Anything"))
							spotUsable = false;
						
						if (GC.tileInfo.IsOverlapping(neighborCell1, "ObjectRealSprite", 0.64f))
							spotUsable = false;
						
						if (spotUsable && neighborCell1 != Vector2.zero)
						{
							ObjectReal movieScreen2 = GC.spawnerMain.spawnObjectReal(neighborCell1, null, vObject.MovieScreen); 
							movieScreen2.ShiftTowardWalls();
							movieScreen2.ambientAudio = vAmbience.Casino;
							num2 = numObjects;
							numObjects = num2 + 1;
						}
						else
						{
							spotUsable = true;

							if ((GC.tileInfo.GetTileData(new Vector2(neighborCell2.x, neighborCell2.y + 0.64f)).wallMaterial == wallMaterialType.None && direction == "N") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x, neighborCell2.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x + 0.64f, neighborCell2.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x - 0.64f, neighborCell2.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "N") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x + 0.64f, neighborCell2.y)).wallMaterial == wallMaterialType.None && direction == "E") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x - 0.64f, neighborCell2.y)).wallMaterial != wallMaterialType.None && direction == "E") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x - 0.64f, neighborCell2.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "E") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x - 0.64f, neighborCell2.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "E") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x, neighborCell2.y - 0.64f)).wallMaterial == wallMaterialType.None && direction == "S") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x, neighborCell2.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x + 0.64f, neighborCell2.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x - 0.64f, neighborCell2.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "S") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x - 0.64f, neighborCell2.y)).wallMaterial == wallMaterialType.None && direction == "W") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x + 0.64f, neighborCell2.y)).wallMaterial != wallMaterialType.None && direction == "W") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x + 0.64f, neighborCell2.y + 0.64f)).wallMaterial != wallMaterialType.None && direction == "W") || 
								(GC.tileInfo.GetTileData(new Vector2(neighborCell2.x + 0.64f, neighborCell2.y - 0.64f)).wallMaterial != wallMaterialType.None && direction == "W"))
								spotUsable = false;
							
							if (GC.tileInfo.IsOverlapping(neighborCell2, "Anything"))
								spotUsable = false;
							
							if (GC.tileInfo.IsOverlapping(neighborCell2, "ObjectRealSprite", 0.64f))
								spotUsable = false;
							
							if (spotUsable && neighborCell2 != Vector2.zero)
							{
								ObjectReal movieScreen3 = GC.spawnerMain.spawnObjectReal(neighborCell2, null, vObject.MovieScreen);
								movieScreen3.ShiftTowardWalls();
								movieScreen3.ambientAudio = vAmbience.Casino;
								num2 = numObjects;
								numObjects = num2 + 1;
							}
						}
					}
				}
				num2 = numObjects;
			}
		}
		private static void SpawnSecurityCamsAndTurrets(LoadLevel loadLevel)
		{
			logger.LogDebug("SORCE: Loading Public Security Cams");

			int bigTries = (int)((float)Random.Range(8, 12) * LevelSizeRatio());
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

				Random.InitState(loadLevel.randomSeedNum + numObjects);
				num2 = numObjects;
			}
		}
		private static void SpawnSlimeBarrels(LoadLevel loadLevel)
        {
			logger.LogDebug("SORCE: Loading Slime Barrels");

			int numObjects = (int)(Random.Range(11, 16) * LevelSizeRatio());

			for (int i = 0; i < numObjects; i++)
			{
				Vector2 location = Vector2.zero;
				int attempts = 0;

				do
				{
					location = GC.tileInfo.FindRandLocationGeneral(2f);
					attempts++;
				}
				while ((location == Vector2.zero || Vector2.Distance(location, GC.playerAgent.tr.position) < 5f) && attempts < 100);

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, vObject.SlimeBarrel);
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
 