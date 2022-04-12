using BepInEx.Logging;
using SORCE.Challenges.C_Features;
using SORCE.Challenges.C_Overhaul;
using SORCE.Extensions;
using SORCE.Logging;
using SORCE.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.MapGenUtilities
{
    internal class MapFeatureSpawners
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// All custom feature spawning
		/// TODO: Refactor copied algorithms
		/// </summary>
		/// <param name="loadLevel"></param>
		public static void Spawn_Master()
		{
			BreakWindows();
			// ColorLakes();
			PoisonLakes();

			//SpawnWallOutcroppings();.
			SpawnCoziness();
			SpawnFountains();
			SpawnJukeboxesAndSpeakers();
			SpawnKillerPlants();
			SpawnManholes();
			SpawnRugs();
			SpawnScreens();
			SpawnSecurityCamsAndTurrets();
			SpawnSlimeBarrels();
			SpawnTurntablesAndSpeakers();

			Wreckage.SpawnPublicLitter();
		}
		private static void BreakWindows()
		{
			if (!MapFeatures.HasBrokenWindows)
				return;

			int chanceToBreak = (int)(LevelGenTools.SlumminessFactor * 15f);

			foreach (ObjectReal objectReal in GC.objectRealList)
				if (objectReal is Window window 
					&& GC.percentChance(chanceToBreak))
					window.DamagedObject(window, 0f);
		}
		private static void ColorLakes()
		{
			// TODO: Gate behind poisoning if not automatic
			// TODO: Gate behind mutators
			// TODO: Mutator interface for lake color

			foreach (Water body in GC.watersList)
			{
				Vector2[] array = new Vector2[body.waterTiles.Count];

				for (int i = 0; i < body.waterTiles.Count; i++)
					array[i] = body.waterTiles[i];

				string lakeColor = MapFeatures.LakeColor();

				body.objectMult.CallRpcChangeLakeColor(array, lakeColor);
			}
		}
		private static void PoisonLakes()
        {
			if (MapFeatures.HasPoisonedLakes)
				foreach (Water body in GC.watersList)
					body.SpreadPoisonWait(GC.playerAgent.statusEffects.ChooseRandomStatusEffectLake());
        }
		private static void SpawnKillerPlants()
		{
			if (!MapFeatures.HasKillerPlants)
				return;

			int numObjects = (int)(Random.Range(6, 12) * LevelSize.ChunkCountRatio());
			float objectBuffer = .64f;

			for (int i = 0; i < numObjects; i++)
			{
				Vector2 location = Vector2.zero;
				int attempts = 0;

				do
				{
					location = GC.tileInfo.FindRandLocationGeneral(objectBuffer);

					for (int j = 0; j < GC.objectRealList.Count; j++)
						if (GC.objectRealList[j].objectName == VObject.Fountain
							&& Vector2.Distance(GC.objectRealList[j].tr.position, location) < (objectBuffer * LevelSize.ChunkCountRatio()))
							location = Vector2.zero;

					attempts++;
				}
				while ((location == Vector2.zero ||
					Vector2.Distance(location, GC.playerAgent.tr.position) < 5f) && attempts < 100);

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, VObject.KillerPlant);
			}
		}
		private static void SpawnWallOutcroppings()
		{
			if (!MapFeatures.HasWallOutcroppings)
				return;

			int maxSpawns = (int)(Random.Range(48, 64) * LevelSize.ChunkCountRatio());
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

				itemCountIterator = i;
			}
		}
		private static void SpawnCoziness()
		{
			if (!MapFeatures.HasCoziness)
				return;
			// Fireplace flanked by Armchairs
			// Small table in front if there's room
			// Maybe some plants too?
		}
		private static void SpawnFountains()
		{
			if (!MapFeatures.HasFountains)
				return;

			int numObjects = (int)Mathf.Clamp(2f * LevelSize.ChunkCountRatio(), 1, 5);
			float objectBuffer = 14f;

			for (int i = 0; i < numObjects; i++)
			{
				Vector2 location = Vector2.zero;
				int attempts = 0;

				do
				{
					location = GC.tileInfo.FindRandLocationGeneral(2f);

					for (int j = 0; j < GC.objectRealList.Count; j++)
						if (GC.objectRealList[j].objectName == VObject.Fountain && Vector2.Distance(GC.objectRealList[j].tr.position, location) < (objectBuffer * LevelSize.ChunkCountRatio()))
							location = Vector2.zero;

					attempts++;
				}
				while ((location == Vector2.zero ||
					Vector2.Distance(location, GC.playerAgent.tr.position) < 5f) && attempts < 100);

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, VObject.Fountain);
			}
		}
		private static void SpawnJukeboxesAndSpeakers()
		{
			if (!GC.challenges.Contains(nameof(DiscoCityDanceoff)))
				return;

			Debug.Log("SORCE: Loading Disco Shit");
			int maxSpawns = (int)(Random.Range(6, 12) * LevelSize.ChunkCountRatio());
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
					GC.spawnerMain.spawnObjectReal(location, null, VObject.Speaker).ShiftTowardWalls();

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
							GC.spawnerMain.spawnObjectReal(zero4, null, VObject.Speaker).ShiftTowardWalls();
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
								GC.spawnerMain.spawnObjectReal(zero5, null, VObject.Jukebox).ShiftTowardWalls();
								i++;
							}
						}
					}
				}
			}
			spawnedInChunks = null;
		}
		private static void SpawnManholes()
		{
			if (!MapFeatures.HasManholes_Underdank)
				return;

			int maxSpawns = (int)(Random.Range(8, 12) * LevelSize.ChunkCountRatio()); 
			Manhole placedManhole;
			List<Manhole> manholeList = new List<Manhole>();

			for (int i = 0; i < maxSpawns; i++)
			{
				Vector2 spotCandidate = Vector2.zero;
				int attempts = 0;

				while (spotCandidate == Vector2.zero
					&& attempts < 50)
				{
					spotCandidate = GC.tileInfo.FindRandLocationGeneral(1.28f);

					if (GC.objectRealList.OfType<Manhole>().Any(m => Vector2.Distance(m.tr.position, spotCandidate) < 14f)
						|| GC.tileInfo.WaterNearby(spotCandidate)
						|| GC.tileInfo.IceNearby(spotCandidate)
						|| GC.tileInfo.BridgeNearby(spotCandidate))
						spotCandidate = Vector2.zero;

					attempts++;
				}

				if (attempts == 50 && spotCandidate == Vector2.zero)
					break;

				placedManhole = (Manhole)GC.spawnerMain.spawnObjectReal(spotCandidate, null, VObject.Manhole);
				manholeList.Add(placedManhole);
			}

			for (int i = 0; i < manholeList.Count / 3; i++)
			{
				string agentType = GC.Choose(VAgent.Thief, VAgent.Thief, VAgent.Thief, VAgent.Cannibal);
				Manhole hideManhole = manholeList[i];

				Agent agent = GC.spawnerMain.SpawnAgent(hideManhole.tr.position, hideManhole, agentType);
				agent.SetDefaultGoal(VGoal.Idle);
				agent.statusEffects.BecomeHidden(hideManhole);
				agent.oma.mustBeGuilty = true;

				// TODO: Transpile Brain.Update to disable hostility against UDC
				//(agentType != VAgent.Thief || !GC.challenges.Contains("ThiefNoSteal"))
				//	&& (agentType != VAgent.Cannibal || !GC.challenges.Contains(VanillaMutators.CoolWithCannibals)
			}
		}
		private static void SpawnRugs()
		{
			if (!MapFeatures.HasRugs)
				return;


		}
		private static void SpawnScreens()
		{
			if (!MapFeatures.HasScreens)
				return;

			int maxPlacements = (int)(Random.Range(12, 24) * LevelSize.ChunkCountRatio());
			List<int> spawnedInChunks = new List<int>();

			for (int i = 0; i < maxPlacements; i++)
			{
				Vector2 spot = Vector2.zero;
				int attempts = 0;

				// Spot-finding
				while (attempts < 100 && spot == Vector2.zero)
				{
					spot = GC.tileInfo.FindRandLocationNearWall(0.64f);

					if (spot != Vector2.zero)
					{
						if ((GC.tileInfo.GetTileData(E_TileInfo.NorthOf(spot)).owner == 0 &&
							GC.tileInfo.GetTileData(E_TileInfo.WestOf(spot)).owner == 0 &&
							GC.tileInfo.GetTileData(E_TileInfo.SouthOf(spot)).owner == 0 &&
							GC.tileInfo.GetTileData(E_TileInfo.EastOf(spot)).owner == 0) ||
							
							(!GC.tileInfo.IsOverlapping(E_TileInfo.NorthOf(spot), "Wall") &&
							!GC.tileInfo.IsOverlapping(E_TileInfo.SouthOf(spot), "Wall") &&
							!GC.tileInfo.IsOverlapping(E_TileInfo.EastOf(spot), "Wall") &&
							!GC.tileInfo.IsOverlapping(E_TileInfo.WestOf(spot), "Wall")) ||

							GC.tileInfo.IsOverlapping(spot, "ObjectRealSprite", 0.64f) ||
							spawnedInChunks.Contains(GC.tileInfo.GetTileData(spot).chunkID) ||
							GC.tileInfo.DestroyIfBetweenWalls(spot))
							spot = Vector2.zero;
					}

					attempts++;
				}

				// Spawning
				if (spot != Vector2.zero)
				{
					// Middle screen (not appearing)
					ObjectReal movieScreen1 = GC.spawnerMain.spawnObjectReal(spot, null, VObject.MovieScreen);
					movieScreen1.ShiftTowardWalls();
					movieScreen1.ambientAudio = VAmbience.Casino;
					TileData tileData = GC.tileInfo.GetTileData(spot);
					spawnedInChunks.Add(tileData.chunkID);

					// Determine Direction
					string wallDirection = "";
					Vector2 neighborCell1 = Vector2.zero;
					Vector2 neighborCell2 = Vector2.zero;
					Vector2 lightPosition = Vector2.zero;

					if (E_TileInfo.HasWall(GC.tileInfo.GetTileData(E_TileInfo.SouthOf(spot))))
					{
						neighborCell1 = E_TileInfo.EastOf(spot);
						neighborCell2 = E_TileInfo.WestOf(spot);
						lightPosition = E_TileInfo.NorthOf(spot);
						wallDirection = "S";
					}
					else if (E_TileInfo.HasWall(GC.tileInfo.GetTileData(E_TileInfo.EastOf(spot))))
					{
						neighborCell1 = E_TileInfo.NorthOf(spot);
						neighborCell2 = E_TileInfo.SouthOf(spot);
						lightPosition = E_TileInfo.WestOf(spot);
						wallDirection = "E";
					}
					else if (E_TileInfo.HasWall(GC.tileInfo.GetTileData(E_TileInfo.WestOf(spot))))
					{
						neighborCell1 = E_TileInfo.NorthOf(spot);
						neighborCell2 = E_TileInfo.SouthOf(spot);
						lightPosition = E_TileInfo.EastOf(spot);
						wallDirection = "W";
					}
					else if (E_TileInfo.HasWall(GC.tileInfo.GetTileData(E_TileInfo.NorthOf(spot)))) // North is last, to hide that screens are blank
					{
						neighborCell1 = E_TileInfo.EastOf(spot);
						neighborCell2 = E_TileInfo.WestOf(spot);
						lightPosition = E_TileInfo.SouthOf(spot);
						wallDirection = "N";
					}
					
					GameObject gameObject2 = UnityEngine.Object.Instantiate(GC.spawnerMain.lightReal2Prefab, lightPosition, Quaternion.Euler(0f, 0f, 0f));
					LightReal component3 = gameObject2.GetComponent<LightReal>();
					GC.spawnerMain.SetLightRealDetails(component3, gameObject2, movieScreen1.startingChunk, movieScreen1.tr.GetComponent<Chunk>(), 5, 5, 5, "PurpleLight");

					// Place sides
					// I think these always fire, because I never see only one.
					if (E_TileInfo.IsWallDecorationPlaceable(neighborCell1, wallDirection) &&
						neighborCell1 != Vector2.zero)
					{
						ObjectReal movieScreen2 = GC.spawnerMain.spawnObjectReal(neighborCell1, null, VObject.MovieScreen);
						movieScreen2.ShiftTowardWalls();
						movieScreen2.ambientAudio = VAmbience.Casino;
						
					}

					if (E_TileInfo.IsWallDecorationPlaceable(neighborCell2, wallDirection) &&
						neighborCell2 != Vector2.zero)
					{
						ObjectReal movieScreen3 = GC.spawnerMain.spawnObjectReal(neighborCell2, null, VObject.MovieScreen);
						movieScreen3.ShiftTowardWalls();
						movieScreen3.ambientAudio = VAmbience.Casino;
					}
				}
			}
		}
		private static void SpawnSecurityCamsAndTurrets()
		{
			if (!MapFeatures.HasSecurityCamsAndTurrets)
				return;

			logger.LogDebug("SORCE: Loading Public Security Cams");

			int bigTries = (int)(Random.Range(8, 12) * LevelSize.ChunkCountRatio() * LevelGenTools.SlumminessFactor);
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
				}
				while ((spotCandidate == Vector2.zero || Vector2.Distance(spotCandidate, GC.playerAgent.tr.position) < 5f)
					&& spotsTried < 100);

				if (spotCandidate != Vector2.zero)
				{
					ObjectReal securityCam = GC.spawnerMain.spawnObjectReal(spotCandidate, null, VObject.SecurityCam);
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
							ObjectReal turret1 = GC.spawnerMain.spawnObjectReal(leftOfSpot, null, VObject.Turret);
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
								ObjectReal turret2 = GC.spawnerMain.spawnObjectReal(rightOfSpot, null, VObject.Turret);
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

				num2 = numObjects;
			}
		}
		private static void SpawnSlimeBarrels()
		{
			if (!MapFeatures.HasSlimeBarrels())
				return;

			int numObjects = (int)(Random.Range(1, 3) * LevelSize.ChunkCountRatio() * LevelGenTools.SlumminessFactor);

			for (int i = 0; i < numObjects; i++)
			{
				Vector2 location = Vector2.zero;
				int attempts = 0;

				while ((location == Vector2.zero || Vector2.Distance(location, GC.playerAgent.tr.position) < 5f)
					&& attempts < 100)
				{
					location = GC.tileInfo.FindRandLocationGeneral(2f);
					attempts++;
				}

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, VObject.SlimeBarrel);
			}
		}
		private static void SpawnTurntablesAndSpeakers()
		{
			if (!MapFeatures.HasTurntablesAndSpeakers)
				return;

			int maximumLocations = 6;
			float distanceBetween = 28f;

			for (int i = 0; i < maximumLocations; i++)
			{
				Vector2 location = Vector2.zero;
				int j = 0;

				while (j < 100 &&
					(location == Vector2.zero ||
					Vector2.Distance(location, GC.playerAgent.tr.position) < 5f ||
					Vector2.Distance(location, GC.elevatorDown.tr.position) < 5f))
				{
					location = GC.tileInfo.FindRandLocationGeneral(2f);

					for (int k = 0; k < GC.objectRealList.Count; k++)
						if (GC.objectRealList[k].objectName == VObject.Turntables &&
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

				if (location != Vector2.zero)
					GC.spawnerMain.spawnObjectReal(location, null, VObject.Turntables);
			}
		}
	}
}
