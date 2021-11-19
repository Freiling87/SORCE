using BepInEx.Logging;
using HarmonyLib;
using Light2D;
using SORCE.Challenges;
using SORCE.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using System.Reflection.Emit;
using JetBrains.Annotations;

namespace SORCE.Patches
{
	public static class P_LoadLevel
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Level Size
		/// </summary>
		/// <param name="instructionsEnumerable"></param>
		/// <returns></returns>
		[HarmonyTranspiler, HarmonyPatch(methodName: "CreateInitialMap")]
		private static IEnumerable<CodeInstruction> CreateInitialMap_Transpiler(IEnumerable<CodeInstruction> instructionsEnumerable, ILGenerator generator)
		{
			List<CodeInstruction> instructions = instructionsEnumerable.ToList();
			FieldInfo loadLevel_levelSizeMax = AccessTools.Field(typeof(LoadLevel), nameof(LoadLevel.levelSizeMax));
			MethodInfo levelGenTools_SetLevelSizeModifier = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.SetLevelSizeModifier), new[] { typeof(LoadLevel) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 515
					new CodeInstruction(OpCodes.Ldstr, "LEVEL SIZE: "),
					new CodeInstruction(OpCodes.Ldarg_0)
				},
				insertInstructionSequence: new List<CodeInstruction>
				{ 
					// LevelGenTools.LevelSizeModifier(__instance);

					new CodeInstruction(OpCodes.Ldarg_0), // __instance
					new CodeInstruction(OpCodes.Call, levelGenTools_SetLevelSizeModifier), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Floor mods
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="__result"></param>
		/// <param name="___tilemapFloors2"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: "FillFloors")]
		public static bool FillFloors_Prefix(LoadLevel __instance, ref IEnumerator __result, ref tk2dTileMap ___tilemapFloors2)
		{
			if (ChallengeManager.IsChallengeFromListActive(cChallenge.FloorMutators))
			{
				__result = FillFloors_Enumerator(__instance, ___tilemapFloors2);

				return false;
			}

			return true;
		}
		public static IEnumerator FillFloors_Enumerator(LoadLevel __instance, tk2dTileMap ___tilemapFloors2)
		{
			// Attempt at FloorMod. No visible effect yet.

			float maxChunkTime = 0.02f;
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			int triesCount = 0;
			int num;

			for (int i2 = 0; i2 < __instance.levelSizeAxis; i2 = num + 1)
			{
				for (int j2 = 0; j2 < __instance.levelSizeAxis; j2 = num + 1)
				{
					num = triesCount;
					triesCount = num + 1;
					int num2 = i2 * 16;
					int num3 = i2 * 16 + 16;
					int num4 = 160 - j2 * 16;
					int num5 = 160 - j2 * 16 - 16;

					for (int k = num2; k < num3; k++)
					{
						for (int l = num4; l > num5; l--)
						{
							__instance.tileInfo.tileArray[k, l - 1].chunkID = __instance.mapChunkArray[i2, j2].chunkID;
							string floorTileGroup = vFloorTileGroup.Building; // Homebase is default

							if (ChallengeManager.IsChallengeFromListActive(cChallenge.FloorMutators))
								floorTileGroup = vFloorTileGroup.Industrial; // No effect?
							else if (GC.levelShape == 0 && GC.levelType != "HomeBase")
							{
								if (GC.levelTheme == 0)
									floorTileGroup = vFloorTileGroup.Slums;
								else if (GC.levelTheme == 1)
									floorTileGroup = vFloorTileGroup.Industrial;
								else if (GC.levelTheme == 2)
									floorTileGroup = vFloorTileGroup.Park;
								else if (GC.levelTheme == 3)
									floorTileGroup = vFloorTileGroup.Downtown;
								else if (GC.levelTheme == 4)
									floorTileGroup = vFloorTileGroup.Uptown;
								else if (GC.levelTheme == 5)
									floorTileGroup = vFloorTileGroup.MayorVillage;
							}

							int tile = int.Parse(GC.rnd.RandomSelect(floorTileGroup, "RandomFloorsWalls"));

							___tilemapFloors2.SetTile(k, l - 1, 0, tile);
						}
					}

					if (Time.realtimeSinceStartup - realtimeSinceStartup > maxChunkTime)
					{
						yield return null;
						realtimeSinceStartup = Time.realtimeSinceStartup;
					}

					Random.InitState(__instance.randomSeedNum + triesCount);
					num = j2;
				}

				num = i2;
			}

			__instance.allChunksFilled = true;

			yield break;
		}

		/// <summary>
		/// Floor Exteriors
		///		This works in two different places, which work on different levels.
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="__result"></param>
		/// <param name="___tilemapWalls"></param>
		/// <param name="___tilemapFloors2"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: "FillMapChunks")]
		public static bool FillMapChunks_Prefix(LoadLevel __instance, ref IEnumerator __result, ref tk2dTileMap ___tilemapWalls, ref tk2dTileMap ___tilemapFloors2)
		{
			__result = FillMapChunks_Replacement(__instance, ___tilemapWalls, ___tilemapFloors2);
			return false;
		}
		public static IEnumerator FillMapChunks_Replacement(LoadLevel __instance, tk2dTileMap ___tilemapWalls, tk2dTileMap ___tilemapFloors2)
		{
			int log = 0;
			logger.LogDebug("LoadLevel_FillMapChunks_Replacement");

			float maxChunkTime = 0.02f;
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			int triesCount = 0;
			int num;

			for (int i = 0; i < __instance.levelSizeAxis; i = num + 1)
			{
				for (int j = 0; j < __instance.levelSizeAxis; j = num + 1)
				{
					num = triesCount;
					triesCount = num + 1;

					if (__instance.mapChunkArray[i, j].chunkID != 0) // Slums, 
					{
						int num2 = i * 16;
						int num3 = i * 16 + 16;
						int num4 = 160 - j * 16;
						int num5 = 160 - j * 16 - 16;

						for (int k = num2; k < num3; k++)
						{
							for (int l = num4; l > num5; l--)
							{
								___tilemapWalls.ClearTile(k, l - 1, 0);
								__instance.tileInfo.tileArray[k, l - 1].chunkID = __instance.mapChunkArray[i, j].chunkID;
								string tilemapGroup = vFloorTileGroup.Building;

								if (ChallengeManager.IsChallengeFromListActive(cChallenge.FloorMutators))
									tilemapGroup = LevelGenTools.GetFloorTileGroup(); // Works on: Slums,
								else if (GC.levelShape == 0 && GC.levelType != "HomeBase")
								{
									if (GC.levelTheme == 0)
										tilemapGroup = vFloorTileGroup.Slums;
									else if (GC.levelTheme == 1)
										tilemapGroup = vFloorTileGroup.Industrial;
									else if (GC.levelTheme == 2)
										tilemapGroup = vFloorTileGroup.Park;
									else if (GC.levelTheme == 3)
										tilemapGroup = vFloorTileGroup.Downtown;
									else if (GC.levelTheme == 4)
										tilemapGroup = vFloorTileGroup.Uptown;
									else if (GC.levelTheme == 5)
										tilemapGroup = vFloorTileGroup.MayorVillage;
								}

								int tile = int.Parse(GC.rnd.RandomSelect(tilemapGroup, "RandomFloorsWalls"));
								___tilemapFloors2.SetTile(k, l - 1, 0, tile);
							}
						}
					}
					else if (!GC.holeLevel) // Park, 
					{
						__instance.mapChunkArray[i, j].filled = true;
						int num6 = i * 16;
						int num7 = i * 16 + 16;
						int num8 = 160 - j * 16;
						int num9 = 160 - j * 16 - 16;

						for (int m = num6; m < num7; m++)
						{
							for (int n = num8; n > num9; n--)
							{
								if (m != 0 && n != 160 && m != (__instance.levelSizeAxis - 1) * 16 + 16 - 1 &&
										n != 160 - (__instance.levelSizeAxis - 1) * 16 - 16 + 1)
								{
									int wallMaterialOffset = 0;
									int wallMaterialOffsetTop = 0;

									switch (GC.levelTheme)
									{
										case 0:
											wallMaterialOffset = 72;
											wallMaterialOffsetTop = 140;
											break;
										case 1:
											wallMaterialOffset = 244;
											wallMaterialOffsetTop = 1015;
											break;
										case 2:
											wallMaterialOffset = 220;
											wallMaterialOffsetTop = 1085;
											break;
										case 3:
											wallMaterialOffset = 228;
											wallMaterialOffsetTop = 1155;
											break;
										case 4:
											wallMaterialOffset = 236;
											wallMaterialOffsetTop = 1225;
											break;
										case 5:
											wallMaterialOffset = 300;
											wallMaterialOffsetTop = 1673;
											break;
									}

									___tilemapWalls.SetTile(m, n - 1, 0, 0);
									TileData tileData = __instance.tileInfo.tileArray[m, n - 1];
									tileData.wallMaterialOffset = wallMaterialOffset;
									tileData.wallMaterialOffsetTop = wallMaterialOffsetTop;
									tileData.wallFrontVariation = true;
									tileData.wallMaterial = wallMaterialType.Border;
									string tilemapGroup = vFloorTileGroup.Building;

									if (ChallengeManager.IsChallengeFromListActive(cChallenge.FloorMutators))
										tilemapGroup = vFloorTileGroup.MayorVillage; // Works on: Park, 
									else if (GC.levelShape == 0 && GC.levelType != "HomeBase")
									{
										if (GC.levelTheme == 0)
											tilemapGroup = vFloorTileGroup.Slums;
										else if (GC.levelTheme == 1)
											tilemapGroup = vFloorTileGroup.Industrial;
										else if (GC.levelTheme == 2)
											tilemapGroup = vFloorTileGroup.Park;
										else if (GC.levelTheme == 3)
											tilemapGroup = vFloorTileGroup.Downtown;
										else if (GC.levelTheme == 4)
											tilemapGroup = vFloorTileGroup.Uptown;
										else if (GC.levelTheme == 5)
											tilemapGroup = vFloorTileGroup.MayorVillage;
									}

									int tile2 = int.Parse(GC.rnd.RandomSelect(tilemapGroup, "RandomFloorsWalls"));
									___tilemapFloors2.SetTile(m, n - 1, 0, tile2);
									tileData.chunkID = __instance.mapChunkArray[i, j].chunkID;
								}
							}
						}
					}

					if (Time.realtimeSinceStartup - realtimeSinceStartup > maxChunkTime)
					{
						yield return null;

						realtimeSinceStartup = Time.realtimeSinceStartup;
					}

					Random.InitState(__instance.randomSeedNum + triesCount);
					num = j;
				}

				num = i;
			}

			if (GC.levelType == "Tutorial" || GC.levelType == "HomeBase")
				__instance.allChunksFilled = true;
			else
			{
				logger.LogDebug("\tA");
				MethodInfo FillMapChunks2_Private = AccessTools.Method(typeof(LoadLevel), "FillMapChunks2", new Type[0] { });
				IEnumerator FillMapChunks2_Private_IEnumerator = (IEnumerator)FillMapChunks2_Private.Invoke(__instance, new object[0]);
				__instance.StartCoroutine(FillMapChunks2_Private_IEnumerator);
				logger.LogDebug("\tB");
			}

			yield break;
		}

		/// <summary>
		/// Skyway District Holes
		/// </summary>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.loadStuff2))]
		public static bool loadStuff2_Prefix(LoadLevel __instance)
		{
			logger.LogDebug("LoadLevel_loadStuff2_Prefix");

			if (GC.challenges.Contains(cChallenge.SkywayDistrict))
				GC.canalHoles = true;
			else
				GC.canalHoles = false;

			return true;
		}

		/// <summary>
		/// WallMod Borders
		/// TODO: Transpiler
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="___tilemapWalls"></param>
		/// <param name="___tilemapFloors2"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.SetupBasicLevel))]
		public static bool SetupBasicLevel_Prefix(LoadLevel __instance, tk2dTileMap ___tilemapWalls, tk2dTileMap ___tilemapFloors2)
		{
			for (int i = 0; i < __instance.levelSizeAxis; i++)
				for (int j = 0; j < __instance.levelSizeAxis; j++)
					if (__instance.mapChunkArray[i, j].chunkID != 0)
						for (int k = i * 16; k < i * 16 + 16; k++)
							for (int l = 160 - j * 16; l > 160 - j * 16 - 16; l--)
							{
								___tilemapWalls.ClearTile(k, l - 1, 0);
								__instance.tileInfo.tileArray[k, l - 1].chunkID = __instance.mapChunkArray[i, j].chunkID;
								int tile = Random.Range(0, 0);
								___tilemapFloors2.SetTile(k, l - 1, 0, tile);
							}
					else
					{
						__instance.mapChunkArray[i, j].filled = true;
						int num = i * 16;
						int num2 = i * 16 + 16;
						int num3 = 160 - j * 16;
						int num4 = 160 - j * 16 - 16;

						for (int m = num; m < num2; m++)
							for (int n = num3; n > num4; n--)
								if (m != 0 && n != 160 && m != (__instance.levelSizeAxis - 1) * 16 + 16 - 1 && n != 160 - (__instance.levelSizeAxis - 1) * 16 - 16 + 1)
								{
									int wallMaterialOffset = 0;
									int wallMaterialOffsetTop = 0;

									switch (GC.levelTheme)
									{
										case 0:
											wallMaterialOffset = 72;
											wallMaterialOffsetTop = 140;
											break;
										case 1:
											wallMaterialOffset = 244;
											wallMaterialOffsetTop = 1015;
											break;
										case 2:
											wallMaterialOffset = 220;
											wallMaterialOffsetTop = 1085;
											break;
										case 3:
											wallMaterialOffset = 228;
											wallMaterialOffsetTop = 1155;
											break;
										case 4:
											wallMaterialOffset = 236;
											wallMaterialOffsetTop = 1225;
											break;
										case 5:
											wallMaterialOffset = 300;
											wallMaterialOffsetTop = 1673;
											break;
									}

									TileData tileData = __instance.tileInfo.tileArray[m, n - 1];
									___tilemapWalls.SetTile(m, n - 1, 0, 0);
									tileData.wallMaterialOffset = wallMaterialOffset;
									tileData.wallMaterialOffsetTop = wallMaterialOffsetTop;
									tileData.wallFrontVariation = true;
									tileData.wallMaterial = LevelGenTools.GetBorderWallMaterialFromMutator(); //
									int tile2 = Random.Range(0, 0);
									___tilemapFloors2.SetTile(m, n - 1, 0, tile2);
									tileData.chunkID = __instance.mapChunkArray[i, j].chunkID;
								}
					}

			return false;
		}

		/// <summary>
		/// Lake It or Leave It
		/// </summary>
		/// <param name="instructionsEnumerable"></param>
		/// <param name="generator"></param>
		/// <returns></returns>
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_Lakes(IEnumerable<CodeInstruction> instructionsEnumerable, ILGenerator generator)
		{
			List<CodeInstruction> instructions = instructionsEnumerable.ToList();
			FieldInfo loadLevel_hasLakes = AccessTools.Field(typeof(LoadLevel), nameof(LoadLevel.hasLakes));
			MethodInfo levelGenTools_SetHasLakes = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.SetHasLakes), new[] { typeof(LoadLevel) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 30

					new CodeInstruction(OpCodes.Ldfld, loadLevel_hasLakes),
					new CodeInstruction(OpCodes.Brfalse),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// Call SetupMore3_3_SetHasLakes(__instance);

					new CodeInstruction(OpCodes.Ldarg_0), // __instance
					new CodeInstruction(OpCodes.Call, levelGenTools_SetHasLakes), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}


		/// <summary>
		/// Level Features,
		/// Level Roamers,
		/// TODO: Transpiler
		///		- Break each feature/Roamer portion into its own method
		///		- Test that it still works
		///		- Only afterwards move it to a transpiler.
		///	TODO:
		///		- Set HasLakes from LakeItOrLeaveIt after @29
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="___tilemapFloors4"></param>
		/// <param name="___minimap"></param>
		/// <param name="__result"></param>
		/// <returns></returns>
		public static IEnumerator CHOPTHISUP(LoadLevel __instance, tk2dTileMap ___tilemapFloors4, Minimap ___minimap)
		{
			logger.LogDebug("LoadLevel_SetupMore3_3_Replacement");

			Random.InitState(__instance.randomSeedNum);
			float maxChunkTime = 0.02f;
			float chunkStartTime = Time.realtimeSinceStartup;
			int randomCount = 0;

			if (GC.serverPlayer && !__instance.memoryTest && GC.levelType != "Attract")
			{
				if (GC.sessionDataBig.curLevel > 0 && GC.staticChunk == "")
					__instance.levelSizeModifier = (float)__instance.levelSizeMax / 30f;

				if (GC.levelType != "HomeBase" && GC.levelType != "Tutorial" && GC.staticChunk == "")
					GC.levelFeelingsScript.StartLevelFeelings();

				if (GC.sessionDataBig.curLevel > 0 && GC.staticChunk == "" && GC.levelType != "Tutorial" && GC.levelType != "HomeBase")
				{
					#region Features
					#region Exploding & Slime Barrels

					if (GC.levelTheme == 1 ||
							(GC.customLevel && __instance.customLevel.levelFeatures.Contains("ExplodingSlimeBarrel")) ||
							GC.challenges.Contains(cChallenge.ThePollutionSolution) ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)))
					{
						Debug.Log("Loading Exploding and Slime Barrels");
						int numObjects = (int)((float)Random.Range(11, 16) * __instance.levelSizeModifier);
						int i2;

						for (int i = 0; i < numObjects; i = i2 + 1)
						{
							Vector2 vector16 = Vector2.zero;
							int num41 = 0;

							do
							{
								vector16 = GC.tileInfo.FindRandLocationGeneral(2f);
								num41++;
							} while ((vector16 == Vector2.zero || Vector2.Distance(vector16, GC.playerAgent.tr.position) < 5f) && num41 < 100);

							string objectType = GC.Choose<string>("ExplodingBarrel", "SlimeBarrel", new string[0]);

							if (vector16 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector16, null, objectType);

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + i);
							i2 = i;
						}
					}

					#endregion
					#region Flaming Barrels

					bool hasFlamingBarrels = false;

					if (GC.levelTheme < 3 ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
							(GC.customLevel && __instance.customLevel.levelFeatures.Contains("FlamingBarrel")) ||
							GC.challenges.Contains(cChallenge.AnCapistan))
						hasFlamingBarrels = true;

					if (GC.challenges.Contains(cChallenge.PoliceState) || GC.challenges.Contains(cChallenge.MACITS))
						hasFlamingBarrels = false;

					if (hasFlamingBarrels)
					{
						Debug.Log("Loading Flaming Barrels");
						int numObjects = (int)((float)Random.Range(6, 10) * __instance.levelSizeModifier);
						int i2;

						for (int i = 0; i < numObjects; i = i2 + 1)
						{
							Vector2 vector17 = Vector2.zero;
							int num42 = 0;

							do
							{
								vector17 = GC.tileInfo.FindRandLocationGeneral(2f);

								if (vector17 != Vector2.zero)
								{
									if (GC.tileInfo.WaterNearby(vector17))
										vector17 = Vector2.zero;

									if (GC.tileInfo.IceNearby(vector17))
										vector17 = Vector2.zero;

									if (GC.tileInfo.BridgeNearby(vector17))
										vector17 = Vector2.zero;
								}

								num42++;
							} while ((vector17 == Vector2.zero || Vector2.Distance(vector17, GC.playerAgent.tr.position) < 5f) && num42 < 100);

							if (vector17 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector17, null, vObject.FlamingBarrel);

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + i);
							i2 = i;
						}
					}

					#endregion
					#region Flame Grates

					//if (GC.challenges.Contains(cChallenge.TransitExperiment))
					//	__instance.hasFlameGrates = false;

					if (__instance.hasFlameGrates)
					{
						Debug.Log("Loading Flame Grates");
						int numObjects = (int)((float)Random.Range(6, 10) * __instance.levelSizeModifier);
						int num2;

						for (int bigTries = 0; bigTries < numObjects; bigTries = num2 + 1)
						{
							Vector2 vector18 = Vector2.zero;
							int num43 = 0;
							bool flag17;

							do
							{
								vector18 = GC.tileInfo.FindRandLocationGeneral(1f);
								num43++;
								flag17 = false;

								if (vector18 != Vector2.zero && (GC.tileInfo.GetTileData(new Vector3(vector18.x, vector18.y, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x, vector18.y + 0.64f, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x, vector18.y - 0.64f, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x + 0.64f, vector18.y, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x - 0.64f, vector18.y, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x + 0.64f, vector18.y + 0.64f, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x + 0.64f, vector18.y - 0.64f, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x - 0.64f, vector18.y + 0.64f, 0f)).spillOil ||
										GC.tileInfo.GetTileData(new Vector3(vector18.x - 0.64f, vector18.y - 0.64f, 0f)).spillOil))
									flag17 = true;

								GC.tileInfo.GetTileData(vector18);
							} while ((vector18 == Vector2.zero || Vector2.Distance(vector18, GC.playerAgent.tr.position) < 7f || flag17) && num43 < 100);

							num43 = 0;

							if (vector18 == Vector2.zero)
							{
								Random.InitState(__instance.randomSeedNum + bigTries + ++randomCount);

								do
								{
									vector18 = GC.tileInfo.FindRandLocationGeneral();
									num43++;
								} while ((vector18 == Vector2.zero || Vector2.Distance(vector18, GC.playerAgent.tr.position) < 5f) && num43 < 50);
							}

							if (vector18 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector18, null, "FlameGrate");

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + bigTries);
							num2 = bigTries;
						}
					}

					#endregion
					#region Barbecues

					bool hasBarbecues = false;

					if (GC.levelTheme == 2 ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
							(GC.customLevel && __instance.customLevel.levelFeatures.Contains("Barbecue")))
						hasBarbecues = true;

					if (hasBarbecues)
					{
						Debug.Log("Loading Barbecues");
						int numObjects = (int)((float)Random.Range(3, 5) * __instance.levelSizeModifier);
						int num2;

						for (int bigTries = 0; bigTries < numObjects; bigTries = num2 + 1)
						{
							Vector2 vector19 = Vector2.zero;
							int num44 = 0;

							do
							{
								vector19 = GC.tileInfo.FindRandLocationGeneral(1f);

								for (int num45 = 0; num45 < GC.objectRealList.Count; num45++)
									if (GC.objectRealList[num45].objectName == "Barbecue" &&
											Vector2.Distance(GC.objectRealList[num45].tr.position, vector19) < 14f)
										vector19 = Vector2.zero;

								num44++;
							} while ((vector19 == Vector2.zero || Vector2.Distance(vector19, GC.playerAgent.tr.position) < 5f) && num44 < 100);

							if (vector19 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector19, null, "Barbecue");

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + bigTries);
							num2 = bigTries;
						}
					}

					#endregion
					#region Mod - BroughtBackFountain / Fountains

					if (__instance.customLevel.levelFeatures.Contains(cLevelFeature.Fountains) || GC.challenges.Contains(cChallenge.BroughtBackFountain))
					{
						Debug.Log("Loading Fountains");
						int numObjects = Mathf.Clamp(3 * LevelGenTools.LevelSizeRatio(), 1, 5);
						int i2;

						for (int i = 0; i < numObjects; i = i2 + 1)
						{
							Vector2 vector20 = Vector2.zero;
							int num46 = 0;

							do
							{
								vector20 = GC.tileInfo.FindRandLocationGeneral(2f);

								for (int num47 = 0; num47 < GC.objectRealList.Count; num47++)
									if (GC.objectRealList[num47].objectName == "Fountain" &&
											Vector2.Distance(GC.objectRealList[num47].tr.position, vector20) < (14f * LevelGenTools.LevelSizeRatio()))
										vector20 = Vector2.zero;

								num46++;
							} while ((vector20 == Vector2.zero || Vector2.Distance(vector20, GC.playerAgent.tr.position) < 5f) && num46 < 100);

							if (vector20 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector20, null, "Fountain");

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + i);
							i2 = i;
						}
					}

					#endregion
					#region Trees

					if (GC.levelTheme == 2 ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
							(GC.customLevel && __instance.customLevel.levelFeatures.Contains("Tree")) ||
							(GC.challenges.Contains(cChallenge.ArcologyEcology)))
					{
						Debug.Log("Loading Trees");

						int numObjects = (int)((float)Random.Range(30, 40) * __instance.levelSizeModifier);
						int num2;

						for (int bigTries = 0; bigTries < numObjects; bigTries = num2 + 1)
						{
							Vector2 vector21 = Vector2.zero;
							int num48 = 0;

							do
							{
								vector21 = GC.tileInfo.FindRandLocationGeneral(0.64f);
								num48++;
							} while ((vector21 == Vector2.zero || Vector2.Distance(vector21, GC.playerAgent.tr.position) < 5f) && num48 < 100);

							if (vector21 != Vector2.zero && (GC.tileInfo.GetTileData(new Vector2(vector21.x, vector21.y + 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector21.x + 0.64f, vector21.y + 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector21.x + 0.64f, vector21.y + 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector21.x + 0.64f, vector21.y)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector21.x, vector21.y - 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector21.x - 0.64f, vector21.y - 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector21.x - 0.64f, vector21.y - 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector21.x - 0.64f, vector21.y)).lake))
								vector21 = Vector2.zero;

							if (vector21 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector21, null, "Tree");

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + bigTries);
							num2 = bigTries;
						}
					}

					#endregion
					#region Boulders

					if (GC.levelTheme == 2 ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
							GC.customLevel && __instance.customLevel.levelFeatures.Contains("Boulder") ||
							GC.challenges.Contains(cChallenge.ArcologyEcology) || GC.challenges.Contains(cChallenge.SpelunkyDory))
					{
						Debug.Log("Loading Boulders");
						int numObjects = (int)((float)Random.Range(10, 20) * __instance.levelSizeModifier);
						int num2;

						for (int bigTries = 0; bigTries < numObjects; bigTries = num2 + 1)
						{
							Vector2 vector22 = Vector2.zero;
							int num49 = 0;

							do
							{
								vector22 = GC.tileInfo.FindRandLocationGeneral(1.28f);
								num49++;
							} while ((vector22 == Vector2.zero || Vector2.Distance(vector22, GC.playerAgent.tr.position) < 5f) && num49 < 100);

							if (vector22 != Vector2.zero && (GC.tileInfo.GetTileData(new Vector2(vector22.x, vector22.y + 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector22.x + 0.64f, vector22.y + 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector22.x + 0.64f, vector22.y + 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector22.x + 0.64f, vector22.y)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector22.x, vector22.y - 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector22.x - 0.64f, vector22.y - 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector22.x - 0.64f, vector22.y - 0.64f)).lake ||
									GC.tileInfo.GetTileData(new Vector2(vector22.x - 0.64f, vector22.y)).lake))
								vector22 = Vector2.zero;

							if (vector22 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector22, null, "Boulder");

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + bigTries);
							num2 = bigTries;
						}

						numObjects = (int)((float)Random.Range(10, 20) * __instance.levelSizeModifier);

						for (int bigTries = 0; bigTries < numObjects; bigTries = num2 + 1)
						{
							Vector2 vector23 = Vector2.zero;
							int num50 = 0;

							do
							{
								vector23 = GC.tileInfo.FindRandLocationGeneral(0.64f);
								num50++;
							} while ((vector23 == Vector2.zero || Vector2.Distance(vector23, GC.playerAgent.tr.position) < 5f) && num50 < 100);

							if (vector23 != Vector2.zero)
								GC.spawnerMain.spawnObjectReal(vector23, null, "BoulderSmall");

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + bigTries);
							num2 = bigTries;
						}
					}

					#endregion
					#region Bushes

					if (GC.levelTheme == 2 ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
							(GC.customLevel && __instance.customLevel.levelFeatures.Contains("Bush")) ||
							GC.challenges.Contains(cChallenge.ArcologyEcology))
					{
						Debug.Log("Loading Bushes");
						Random.InitState(__instance.randomSeedNum);
						int numObjects = (int)((float)Random.Range(20, 30) * __instance.levelSizeModifier);
						int num2;

						for (int i = 0; i < numObjects; i = num2 + 1)
						{
							Vector2 vector24 = Vector2.zero;
							int num51 = 0;

							do
							{
								vector24 = GC.tileInfo.FindRandLocationGeneral(0.64f);

								for (int num52 = 0; num52 < GC.objectRealList.Count; num52++)
									if (GC.objectRealList[num52].objectName == "VendorCart" &&
											Vector2.Distance(GC.objectRealList[num52].tr.position, vector24) < 4f)
										vector24 = Vector2.zero;

								num51++;
							} while ((vector24 == Vector2.zero || Vector2.Distance(vector24, GC.playerAgent.tr.position) < 5f) && num51 < 100);

							num51 = 0;

							if (vector24 == Vector2.zero)
							{
								Random.InitState(__instance.randomSeedNum + i + ++randomCount);

								do
								{
									vector24 = GC.tileInfo.FindRandLocationGeneral();
									num51++;
								} while ((vector24 == Vector2.zero || Vector2.Distance(vector24, GC.playerAgent.tr.position) < 5f) && num51 < 50);
							}

							if (vector24 != Vector2.zero)
								GC.tileInfo.CreateBushCluster(vector24, 0);

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + i);
							num2 = i;
						}

						int bigTries = (int)((float)Random.Range(4, 6) * __instance.levelSizeModifier);
						List<Bush> bushList = new List<Bush>();

						for (int num53 = 0; num53 < GC.objectRealList.Count; num53++)
							if (GC.objectRealList[num53].objectName == "Bush")
								bushList.Add((Bush)GC.objectRealList[num53]);

						for (int i = 0; i < bigTries; i = num2 + 1)
						{
							int num54 = 0;
							Random.InitState(__instance.randomSeedNum + i + ++randomCount);
							Bush bush;
							bool flag22;

							do
							{
								bush = bushList[Random.Range(0, bushList.Count)];
								flag22 = true;

								for (int num55 = 0; num55 < GC.agentList.Count; num55++)
									if (GC.agentList[num55].oma.hidden && Vector2.Distance(bush.tr.position, GC.agentList[num55].tr.position) < 10f)
									{
										num54++;
										flag22 = false;
									}

								if (Vector2.Distance(bush.tr.position, GC.playerAgent.tr.position) < 10f)
									flag22 = false;

								num54++;
							} while (num54 < 50 && !flag22);

							if (flag22 && !GC.challenges.Contains("CannibalsDontAttack") && GC.levelFeeling != "HarmAtIntervals")
							{
								Agent agent3 = GC.spawnerMain.SpawnAgent(bush.tr.position, bush, "Cannibal");
								agent3.SetDefaultGoal("Idle");
								agent3.statusEffects.BecomeHidden(null);
								agent3.hiddenInBush = bush;
								bush.TurnOnShake();
								agent3.noEnforcerAlert = true;
								agent3.oma.mustBeGuilty = true;
							}

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + i);
							num2 = i;
						}

						bushList = null;
					}

					#endregion
					#region Land Mines

					if (GC.levelTheme == 2 ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
							(GC.customLevel && __instance.customLevel.levelFeatures.Contains("Mine")))
					{
						Debug.Log("Loading Mines");

						if (GC.levelTheme == 2)
						{
							int num2;

							for (int bigTries = 0; bigTries < __instance.levelChunks.Count; bigTries = num2 + 1)
							{
								if (__instance.levelChunks[bigTries].description == "MilitaryOutpost")
								{
									int numObjects = Random.Range(5, 10);

									for (int i = 0; i < numObjects; i = num2 + 1)
									{
										Vector2 vector25 = Vector2.zero;
										int num56 = 0;

										do
										{
											vector25 = GC.tileInfo.FindRandLocationGeneral(0.64f);
											num56++;

											if
											(
													(
															(
																	(
																			vector25.x <= __instance.levelChunks[bigTries].chunkEdgeE ||
																			vector25.x >= __instance.levelChunks[bigTries].chunkEdgeE + __instance.chunkSize
																	)
																	&&
																	(
																			vector25.x >= __instance.levelChunks[bigTries].chunkEdgeW ||
																			vector25.x <= __instance.levelChunks[bigTries].chunkEdgeW - __instance.chunkSize
																	)
															) ||
															vector25.y <= __instance.levelChunks[bigTries].chunkEdgeS - __instance.chunkSize ||
															vector25.y >= __instance.levelChunks[bigTries].chunkEdgeN + __instance.chunkSize
													)
													&&
													(
															(
																	(
																			vector25.y <= __instance.levelChunks[bigTries].chunkEdgeN ||
																			vector25.y >= __instance.levelChunks[bigTries].chunkEdgeN + __instance.chunkSize
																	)
																	&&
																	(
																			vector25.y >= __instance.levelChunks[bigTries].chunkEdgeS ||
																			vector25.y <= __instance.levelChunks[bigTries].chunkEdgeS - __instance.chunkSize
																	)
															)
															||
															vector25.x <= __instance.levelChunks[bigTries].chunkEdgeW - __instance.chunkSize ||
															vector25.x >= __instance.levelChunks[bigTries].chunkEdgeE + __instance.chunkSize
													)
											)
												vector25 = Vector2.zero;

											for (int num57 = 0; num57 < GC.objectRealList.Count; num57++)
												if (GC.objectRealList[num57].objectName == "Mine" &&
														Vector2.Distance(GC.objectRealList[num57].tr.position, vector25) < 1.92f)
													vector25 = Vector2.zero;
										} while ((vector25 == Vector2.zero || Vector2.Distance(vector25, GC.playerAgent.tr.position) < 5f) && num56 < 100);

										if (vector25 != Vector2.zero)
											GC.spawnerMain.spawnObjectReal(vector25, null, "Mine");

										if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
										{
											yield return null;
											chunkStartTime = Time.realtimeSinceStartup;
										}

										Random.InitState(__instance.randomSeedNum + i);
										num2 = i;
									}
								}
								num2 = bigTries;
							}
						}
						else
						{
							int bigTries = (int)((float)Random.Range(5, 10) * __instance.levelSizeModifier);
							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Vector2 vector26 = Vector2.zero;
								int num58 = 0;

								do
								{
									vector26 = GC.tileInfo.FindRandLocationGeneral(0.64f);

									for (int num59 = 0; num59 < GC.objectRealList.Count; num59++)
										if (GC.objectRealList[num59].objectName == "Mine" &&
												Vector2.Distance(GC.objectRealList[num59].tr.position, vector26) < 1.92f)
											vector26 = Vector2.zero;

									num58++;
								} while ((vector26 == Vector2.zero || Vector2.Distance(vector26, GC.playerAgent.tr.position) < 5f) && num58 < 100);

								if (vector26 != Vector2.zero)
									GC.spawnerMain.spawnObjectReal(vector26, null, "Mine");

								if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
								{
									yield return null;
									chunkStartTime = Time.realtimeSinceStartup;
								}

								Random.InitState(__instance.randomSeedNum + numObjects);
								num2 = numObjects;
							}
						}
					}

					#endregion
					#region Bear Traps

					if (GC.levelTheme == 2 ||
							(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)) ||
							(GC.customLevel && __instance.customLevel.levelFeatures.Contains("BearTrap")))
					{
						Debug.Log("Loading Bear Traps");
						int bigTries = (int)((float)Random.Range(10, 20) * __instance.levelSizeModifier);
						int num2;

						for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
						{
							Vector2 vector27 = Vector2.zero;
							int num60 = 0;

							do
							{
								vector27 = GC.tileInfo.FindRandLocationGeneral(0.64f);

								for (int num61 = 0; num61 < GC.itemList.Count; num61++)
									if (GC.itemList[num61].objectName == "BearTrapPark" && Vector2.Distance(GC.itemList[num61].tr.position, vector27) < 4f)
										vector27 = Vector2.zero;

								num60++;
							} while ((vector27 == Vector2.zero || Vector2.Distance(vector27, GC.playerAgent.tr.position) < 5f) && num60 < 100);

							if (vector27 != Vector2.zero)
							{
								Item item = GC.spawnerMain.SpawnItem(vector27, "BearTrapPark");
								item.interactable = false;
								item.SetCantPickUp(true);
								item.dangerous = true;
								item.danger = GC.spawnerMain.SpawnDanger(item, "Major", "Normal");
								item.danger.noFlee = true;
								item.dangerPenaltyAmount = 100000;
								item.danger.avoidInCombat = false;
								GC.objectModifyEnvironmentList.Add(item);
								GC.tileInfo.GetTileData(vector27).dangerousToWalk = true;
							}

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + numObjects);
							num2 = numObjects;
						}
					}

					#endregion
					#region Lamps

					bool hasLamps = true;

					if (GC.customLevel)
						hasLamps = __instance.customLevel.levelFeatures.Contains("Lamp");

					if (GC.challenges.Contains(cChallenge.AnCapistan))
						hasLamps = false;

					if (hasLamps)
					{
						Debug.Log("Loading Lamps");
						int bigTries = (int)((float)Random.Range(18, 22) * __instance.levelSizeModifier);
						List<int> spawnedInChunks = new List<int>();
						int num2;

						for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
						{
							Vector2 vector28 = Vector2.zero;
							int num62 = 0;

							do
							{
								vector28 = GC.tileInfo.FindRandLocationGeneral(2f);

								if (vector28 != Vector2.zero)
								{
									TileData tileData6 = GC.tileInfo.GetTileData(vector28);
									int num63 = 0;

									for (int num64 = 0; num64 < spawnedInChunks.Count; num64++)
										if (spawnedInChunks[num64] == tileData6.chunkID)
											num63++;

									if (num63 >= 3)
										vector28 = Vector2.zero;
								}

								if (vector28 != Vector2.zero)
								{
									if (GC.tileInfo.WaterNearby(vector28))
										vector28 = Vector2.zero;

									if (GC.tileInfo.IceNearby(vector28))
										vector28 = Vector2.zero;

									if (GC.tileInfo.BridgeNearby(vector28))
										vector28 = Vector2.zero;
								}

								num62++;
							} while ((vector28 == Vector2.zero || Vector2.Distance(vector28, GC.playerAgent.tr.position) < 5f) && num62 < 100);

							if (vector28 != Vector2.zero)
							{
								GC.spawnerMain.spawnObjectReal(vector28, null, "Lamp");
								TileData tileData7 = GC.tileInfo.GetTileData(vector28);
								spawnedInChunks.Add(tileData7.chunkID);
							}

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;
								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + numObjects);
							num2 = numObjects;
						}

						spawnedInChunks = null;
					}

					#endregion
					#endregion
					#region Ambience

					Debug.Log("Loading Ambient Objects");

					for (int num65 = 0; num65 < __instance.levelChunks.Count; num65++)
					{
						Chunk chunk = __instance.levelChunks[num65];
						string ambience = "";
						string description = chunk.description;

						switch (description)
						{
							case vChunkType.Casino:
								ambience = vAmbience.Casino; // This was left out of vanilla.
								break;
							case vChunkType.Cave:
								ambience = vAmbience.Cave;
								break;
							case vChunkType.CityPark:
								ambience = vAmbience.Park;
								break;
							case vChunkType.Bathhouse:
								ambience = vAmbience.BathHouse;
								break;
							case vChunkType.Graveyard:
								ambience = vAmbience.Graveyard;
								break;
							default:
								if (GC.challenges.Contains(cChallenge.ArcologyEcology) || GC.challenges.Contains(cChallenge.GreenLiving))
									ambience = vAmbience.Park;
								else if (GC.challenges.Contains(cChallenge.SpelunkyDory))
									ambience = vAmbience.Cave;
								else if (GC.challenges.Contains(cChallenge.GhostTown))
									ambience = vAmbience.Graveyard;
								break;
						}

						if (ambience != "")
						{
							if (chunk.shape == 6)
							{
								Vector2 v2 = new Vector2(
										(chunk.chunkLeash1Tr.x + chunk.chunkLeash2Tr.x + chunk.chunkLeash3Tr.x + chunk.chunkLeash4Tr.x) / 4f,
										(chunk.chunkLeash1Tr.y + chunk.chunkLeash2Tr.y + chunk.chunkLeash3Tr.y + chunk.chunkLeash4Tr.y) / 4f);
								GC.spawnerMain.spawnObjectReal(v2, null, "AmbientObject").GetComponent<AmbientObject>().ambientAudioForObject =
										ambience + "_Huge";
							}
							else if (chunk.shape == 5)
								GC.spawnerMain.spawnObjectReal(chunk.chunkLeash1Tr, null, "AmbientObject").GetComponent<AmbientObject>().ambientAudioForObject =
										ambience + "_Long";
							else
								GC.spawnerMain.spawnObjectReal(chunk.chunkLeash1Tr, null, "AmbientObject").GetComponent<AmbientObject>().ambientAudioForObject =
										ambience;
						}
					}

					#endregion
					#region Mayor

					__instance.LevelContainsMayor();
					bool hasMayor = false;

					if (__instance.LevelContainsMayor())
						hasMayor = true;

					if (GC.customLevel)
						hasMayor = __instance.customLevel.levelFeatures.Contains("Mayor");

					if (hasMayor)
					{
						Debug.Log("Loading Mayor");
						bool findingFactoryLake = false;
						bool removeAgentsInBuilding = false;
						Agent mayorAgent = null;
						List<string> list4 = new List<string>();

						for (int num66 = 0; num66 < __instance.levelChunks.Count; num66++)
						{
							if (__instance.levelChunks[num66].description == "MayorHouse" && !list4.Contains("Sleeping"))
								list4.Add("Sleeping");

							if (__instance.levelChunks[num66].description == "DanceClub" && !list4.Contains("Dancing"))
								list4.Add("Dancing");

							if (__instance.levelChunks[num66].description == "Bar" && !list4.Contains("Drinking"))
								list4.Add("Drinking");

							if (__instance.levelChunks[num66].description == "MusicHall" && !list4.Contains("WatchingShow"))
								list4.Add("WatchingShow");

							if (__instance.levelChunks[num66].description == "MayorOffice" && !list4.Contains("WalkingInOffice"))
								list4.Add("WalkingInOffice");

							if (__instance.levelChunks[num66].description == "Bathhouse" && !list4.Contains("Swimming"))
								list4.Add("Swimming");
						}

						list4.Add("Wandering");

						for (int num67 = 0; num67 < list4.Count; num67++)
							Debug.Log("Potential Location: " + list4[num67]);

						string lakeType = list4[Random.Range(0, list4.Count)];
						Debug.Log("CHOSENLOCATION: " + lakeType);
						bool flag27 = false;

						if (lakeType == "Sleeping")
							for (int num68 = 0; num68 < GC.objectRealList.Count; num68++)
							{
								ObjectReal objectReal5 = GC.objectRealList[num68];

								if (objectReal5.objectName == "Bed" && objectReal5.startingChunkRealDescription == "MayorHouse")
								{
									Vector2 v3 = objectReal5.tr.position;
									string agentType2 = "Mayor";
									Agent agent4 = GC.spawnerMain.SpawnAgent(v3, null, agentType2);
									flag27 = true;
									mayorAgent = agent4;
									agent4.startingChunk = objectReal5.startingChunk;
									agent4.initialStartingChunk = agent4.startingChunk;
									agent4.startingChunkReal = objectReal5.startingChunkReal;
									agent4.startingChunkRealDescription = objectReal5.startingChunkRealDescription;
									agent4.SetDefaultGoal("Sleep");
									agent4.cantBump = true;
									agent4.ownerID = 1;
									findingFactoryLake = true;
									removeAgentsInBuilding = true;

									break;
								}
							}
						else if (lakeType == "Dancing")
						{
							List<EventTriggerFloor> list5 = new List<EventTriggerFloor>();

							for (int num69 = 0; num69 < GC.objectRealList.Count; num69++)
							{
								ObjectReal objectReal6 = GC.objectRealList[num69];

								if (objectReal6.objectName == "EventTriggerFloor")
								{
									EventTriggerFloor eventTriggerFloor = (EventTriggerFloor)objectReal6;

									if (eventTriggerFloor.triggerType == "MayorDancePosition")
									{
										list5.Add(eventTriggerFloor);
										Vector2 v4 = objectReal6.tr.position;
										string agentType3 = "Mayor";
										Agent agent5 = GC.spawnerMain.SpawnAgent(v4, null, agentType3);
										flag27 = true;
										mayorAgent = agent5;
										agent5.startingChunk = objectReal6.startingChunk;
										agent5.initialStartingChunk = agent5.startingChunk;
										agent5.startingChunkReal = objectReal6.startingChunkReal;
										agent5.startingChunkRealDescription = objectReal6.startingChunkRealDescription;
										agent5.SetDefaultGoal("Dance");
										agent5.cantBump = true;
										findingFactoryLake = true;

										break;
									}
								}
							}

							if (list5.Count > 0)
							{
								EventTriggerFloor eventTriggerFloor2 = list5[Random.Range(0, list5.Count)];
							}
						}
						else if (lakeType == "Drinking")
							for (int num70 = 0; num70 < GC.objectRealList.Count; num70++)
							{
								ObjectReal objectReal7 = GC.objectRealList[num70];

								if (objectReal7.objectName == "BarStool" && objectReal7.startingChunkRealDescription == "Bar")
								{
									Vector2 v5 = objectReal7.tr.position;
									string agentType4 = "Mayor";
									Agent agent6 = GC.spawnerMain.SpawnAgent(v5, null, agentType4);
									flag27 = true;
									mayorAgent = agent6;
									agent6.startingChunk = objectReal7.startingChunk;
									agent6.initialStartingChunk = agent6.startingChunk;
									agent6.startingChunkReal = objectReal7.startingChunkReal;
									agent6.startingChunkRealDescription = objectReal7.startingChunkRealDescription;
									agent6.cantBump = true;
									findingFactoryLake = true;

									break;
								}
							}
						else if (lakeType == "WatchingShow")
							for (int num71 = 0; num71 < GC.objectRealList.Count; num71++)
							{
								ObjectReal objectReal8 = GC.objectRealList[num71];

								if (objectReal8.objectName == "Chair2" && objectReal8.startingChunkRealDescription == "MusicHall")
								{
									Vector2 v6 = objectReal8.tr.position;
									string agentType5 = "Mayor";
									Agent agent7 = GC.spawnerMain.SpawnAgent(v6, null, agentType5);
									flag27 = true;
									mayorAgent = agent7;
									agent7.startingChunk = objectReal8.startingChunk;
									agent7.initialStartingChunk = agent7.startingChunk;
									agent7.startingChunkReal = objectReal8.startingChunkReal;
									agent7.startingChunkRealDescription = objectReal8.startingChunkRealDescription;
									agent7.SetDefaultGoal("ListenToJokeNPC");
									agent7.cantBump = true;
									findingFactoryLake = true;

									break;
								}
							}
						else if (lakeType == "Swimming")
						{
							List<EventTriggerFloor> list6 = new List<EventTriggerFloor>();

							for (int num72 = 0; num72 < GC.objectRealList.Count; num72++)
							{
								ObjectReal objectReal9 = GC.objectRealList[num72];

								if (objectReal9.objectName == "EventTriggerFloor")
								{
									EventTriggerFloor eventTriggerFloor3 = (EventTriggerFloor)objectReal9;

									if (eventTriggerFloor3.triggerType == "MayorSwimPosition")
									{
										list6.Add(eventTriggerFloor3);
										Vector2 v7 = objectReal9.tr.position;
										string agentType6 = "Mayor";
										Agent agent8 = GC.spawnerMain.SpawnAgent(v7, null, agentType6);
										flag27 = true;
										mayorAgent = agent8;
										agent8.startingChunk = objectReal9.startingChunk;
										agent8.initialStartingChunk = agent8.startingChunk;
										agent8.startingChunkReal = objectReal9.startingChunkReal;
										agent8.startingChunkRealDescription = objectReal9.startingChunkRealDescription;
										agent8.SetDefaultGoal("Swim");
										agent8.agentHitboxScript.SetupBodyStrings();
										agent8.cantBump = true;
										findingFactoryLake = true;

										break;
									}
								}
							}

							if (list6.Count > 0)
							{
								EventTriggerFloor eventTriggerFloor4 = list6[Random.Range(0, list6.Count)];
							}
						}
						else if (lakeType == "WalkingInOffice")
						{
							List<EventTriggerFloor> list7 = new List<EventTriggerFloor>();

							for (int num73 = 0; num73 < GC.objectRealList.Count; num73++)
							{
								ObjectReal objectReal10 = GC.objectRealList[num73];

								if (objectReal10.objectName == "EventTriggerFloor")
								{
									EventTriggerFloor eventTriggerFloor5 = (EventTriggerFloor)objectReal10;

									if (eventTriggerFloor5.triggerType == "MayorOfficePosition")
									{
										list7.Add(eventTriggerFloor5);
										Vector2 v8 = objectReal10.tr.position;
										string agentType7 = "Mayor";
										Agent agent9 = GC.spawnerMain.SpawnAgent(v8, null, agentType7);
										flag27 = true;
										mayorAgent = agent9;
										agent9.startingChunk = objectReal10.startingChunk;
										agent9.initialStartingChunk = agent9.startingChunk;
										agent9.startingChunkReal = objectReal10.startingChunkReal;
										agent9.startingChunkRealDescription = objectReal10.startingChunkRealDescription;
										agent9.SetDefaultGoal("CuriousObject");
										agent9.relationships.SetupInterests();
										agent9.cantBump = true;
										agent9.ownerID = 1;
										findingFactoryLake = true;

										break;
									}
								}
							}
						}

						if (lakeType == "Wandering" || !flag27)
						{
							Vector2 myLocation = Vector2.zero;
							int num74 = 0;

							do
							{
								myLocation = GC.tileInfo.FindRandLocationGeneral(0.32f);
								num74++;
							} while ((myLocation == Vector2.zero || Vector2.Distance(myLocation, GC.playerAgent.tr.position) < 10f) && num74 < 300);

							string agentType8 = "Mayor";
							Agent myAgent = GC.spawnerMain.SpawnAgent(myLocation, null, agentType8);
							mayorAgent = myAgent;
							myAgent.movement.RotateToAngleTransform((float)Random.Range(0, 360));
							myAgent.SetDefaultGoal("WanderFar");
							myLocation = Vector2.zero;
							num74 = 0;

							do
							{
								myLocation = GC.tileInfo.FindLocationNearLocation(myAgent.tr.position, null, 0.32f, 1.28f, true, true);
								num74++;
							} while (myLocation == Vector2.zero && num74 < 300);

							if (myLocation != Vector2.zero)
							{
								Agent.gangCount++;
								myAgent.gang = Agent.gangCount;
								myAgent.modLeashes = 0;
								Random.InitState(__instance.randomSeedNum + ++randomCount);
								int bigTries = Random.Range(4, 6);
								int num2;

								for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
								{
									agentType8 = "Cop2";

									if (__instance.replaceCopWithGangbanger)
										agentType8 = "Gangbanger";

									Agent agent10 = GC.spawnerMain.SpawnAgent(myLocation, null, agentType8);
									agent10.movement.RotateToAngleTransform((float)Random.Range(0, 360));
									agent10.SetDefaultGoal("WanderFar");
									agent10.gang = Agent.gangCount;
									agent10.modVigilant = 0;
									agent10.guardingMayor = true;
									agent10.modLeashes = 1;
									agent10.specialWalkSpeed = myAgent.speedMax;
									agent10.oma.mustBeGuilty = true;

									for (int num75 = 0; num75 < GC.agentList.Count; num75++)
									{
										Agent agent11 = GC.agentList[num75];

										if (agent11.gang == myAgent.gang)
										{
											agent10.relationships.SetRelInitial(agent11, "Aligned");
											agent11.relationships.SetRelInitial(agent10, "Aligned");

											if (!agent11.gangMembers.Contains(agent10))
												agent11.gangMembers.Add(agent10);

											if (!agent10.gangMembers.Contains(agent11))
												agent10.gangMembers.Add(agent11);
										}
									}

									if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
									{
										yield return null;
										chunkStartTime = Time.realtimeSinceStartup;
									}

									Random.InitState(__instance.randomSeedNum + numObjects);
									num2 = numObjects;
								}
							}

							myLocation = default(Vector2);
							myAgent = null;
						}

						if (removeAgentsInBuilding)
						{
							List<Agent> list8 = new List<Agent>();

							for (int num76 = 0; num76 < GC.agentList.Count; num76++)
								if (GC.agentList[num76].startingChunk == mayorAgent.startingChunk && !GC.agentList[num76].isMayor &&
										!list8.Contains(GC.agentList[num76]))
									list8.Add(GC.agentList[num76]);

							while (list8.Count > 0)
							{
								list8[0].DestroyMe();
								list8.Remove(list8[0]);
							}
						}

						if (findingFactoryLake)
							for (int num77 = 0; num77 < 5; num77++)
							{
								string agentType9 = "Cop2";

								if (__instance.replaceCopWithGangbanger)
									agentType9 = "Gangbanger";

								int num78 = 0;
								Vector2 vector29 = Vector2.zero;

								do
								{
									vector29 = GC.tileInfo.FindLocationNearLocation(mayorAgent.tr.position, null, 0.32f, 1.28f, true, false);

									if (!GC.tileInfo.IsIndoors(vector29))
										vector29 = Vector2.zero;

									num78++;
								} while (vector29 == Vector2.zero && num78 < 300);

								if (vector29 != Vector2.zero)
								{
									Agent agent12 = GC.spawnerMain.SpawnAgent(vector29, null, agentType9);
									agent12.movement.RotateToAngleTransform((float)Random.Range(0, 360));
									agent12.startingChunk = mayorAgent.startingChunk;
									agent12.initialStartingChunk = agent12.startingChunk;
									agent12.startingChunkReal = mayorAgent.startingChunkReal;
									agent12.startingChunkRealDescription = mayorAgent.startingChunkRealDescription;
									agent12.SetDefaultGoal("CuriousObject");
									agent12.relationships.SetupInterests();
									agent12.guardingMayor = true;
									agent12.modLeashes = 1;
									agent12.oma.mustBeGuilty = true;

									if (lakeType == "WalkingInOffice")
									{
										agent12.ownerID = 1;
										agent12.SetFollowing(mayorAgent);
									}
									else if (lakeType == "Sleeping")
									{
										agent12.ownerID = 1;
										agent12.oma.modProtectsProperty = 2;
									}
									else if (lakeType == "Swimming")
									{
										agent12.SetDefaultGoal("Swim");
										agent12.agentHitboxScript.SetupBodyStrings();
										agent12.SetFollowing(mayorAgent);
									}

									if (num77 < 3)
										agent12.modVigilant = 0;
								}
							}

						for (int num79 = 0; num79 < GC.agentList.Count; num79++)
						{
							Agent agent13 = GC.agentList[num79];

							if (agent13.startingChunkRealDescription == "MayorHouse" || agent13.startingChunkRealDescription == "MayorOffice")
								agent13.oma.mustBeGuilty = true;
						}

						mayorAgent = null;
						lakeType = null;
					}

					#endregion
					#region Roamers

					if (GC.levelFeeling != vLevelFeeling.RadiationBlasts)
					{
						#region Generic

						bool hasGenericRoamers = true;

						if (GC.sessionDataBig.curLevel >= 1)
							hasGenericRoamers = true;

						if (GC.customLevel)
							hasGenericRoamers = __instance.customLevel.levelFeatures.Contains(vAgent.SlumDweller);

						if (hasGenericRoamers && GC.levelFeeling != vLevelFeeling.Riot && GC.levelFeeling != vLevelFeeling.Lockdown &&
								GC.levelFeeling != vLevelFeeling.WarZone)
						{
							Debug.Log("Loading Roamers");

							int bigTries = (int)((float)Random.Range(16, 20) * __instance.levelSizeModifier);
							bigTries = LevelGenTools.GenPopCount(bigTries);
							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Vector2 trySpot = Vector2.zero;
								int num80 = 0;

								do
								{
									trySpot = GC.tileInfo.FindRandLocationGeneral(0.32f);
									num80++;
								} while ((trySpot == Vector2.zero || Vector2.Distance(trySpot, GC.playerAgent.tr.position) < 10f) && num80 < 300);

								if (trySpot != Vector2.zero)
								{
									string roamerAgent = "Hobo";
									Random.InitState(__instance.randomSeedNum + numObjects + ++randomCount);

									if (GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33))
									{
										roamerAgent = GC.Choose<string>("Hobo", "Worker", "ParkAgent", "Agents", "UpperCruster");

										if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(1))
											roamerAgent = "Thief";
										else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(3))
											roamerAgent = "Thief";
										else if (GC.percentChance(10))
											roamerAgent = "Thief";

										if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
											roamerAgent = "Zombie";

										if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
											roamerAgent = "Cannibal";

										if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
											roamerAgent = "Doctor";
									}
									else
									{
										if (GC.levelTheme == 0)
										{
											if (GC.challenges.Contains(cChallenge.AnCapistan))
											{
												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(5))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(5))
													roamerAgent = "Doctor";

												else if (GC.percentChance(10))
													roamerAgent = GC.Choose<string>(vAgent.DrugDealer, vAgent.Crepe, vAgent.Blahd, vAgent.Thief);
											}
											else if (GC.challenges.Contains(cChallenge.PoliceState))
											{
												if (GC.percentChance(10))
													roamerAgent = "Firefighter";
												else
													roamerAgent = "Hobo";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
											else
											{
												if (hasPoliceBoxes)
												{
													if (GC.percentChance(10))
														roamerAgent = "Firefighter";
													else
														roamerAgent = GC.Choose<string>("Hobo", "UpperCruster");
												}

												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(1))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
										}
										else if (GC.levelTheme == 1)
										{
											if (GC.challenges.Contains(cChallenge.AnCapistan))
											{
												roamerAgent = GC.Choose<string>(vAgent.SlumDweller, vAgent.SlumDweller, vAgent.Worker);

												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(1))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
											else if (GC.challenges.Contains(cChallenge.PoliceState))
											{
												if (GC.percentChance(10))
													roamerAgent = "Firefighter";
												else
													roamerAgent = GC.Choose<string>("Hobo", "Worker", "UpperCruster");

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
											else
											{
												if (GC.percentChance(10))
													roamerAgent = "Firefighter";
												else if (hasPoliceBoxes)
													roamerAgent = GC.Choose<string>("Hobo", "Worker", "UpperCruster");
												else
													roamerAgent = GC.Choose<string>("Hobo", "Worker");

												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(1))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
										}
										else if (GC.levelTheme == 2)
										{
											if (GC.challenges.Contains(cChallenge.AnCapistan))
											{
												roamerAgent = GC.Choose<string>(GC.rnd.RandomSelect("ParkAgent", "Agents"), vAgent.SlumDweller);

												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(5))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
											else if (GC.challenges.Contains(cChallenge.PoliceState))
											{
												roamerAgent = GC.Choose<string>(GC.rnd.RandomSelect("ParkAgent", "Agents"), "UpperCruster");

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
											else
											{
												if (hasPoliceBoxes)
													GC.Choose<string>(GC.rnd.RandomSelect("ParkAgent", "Agents"), "UpperCruster");
												else
													roamerAgent = GC.rnd.RandomSelect("ParkAgent", "Agents");

												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(1))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
										}
										else if (GC.levelTheme == 3)
										{
											if (GC.challenges.Contains(cChallenge.AnCapistan))
											{
												roamerAgent = GC.Choose<string>("Hobo", "Hobo", "UpperCruster", "UpperCruster");

												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(5))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";

												if (GC.percentChance(3))
													roamerAgent = "Vampire";
											}
											else if (GC.challenges.Contains(cChallenge.PoliceState))
											{
												if (GC.percentChance(8))
													roamerAgent = "Firefighter";
												else
													roamerAgent = GC.Choose<string>("Hobo", "UpperCruster", "UpperCruster");

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";

												if (GC.percentChance(3))
													roamerAgent = "Vampire";
											}
											else
											{
												if (GC.percentChance(8))
													roamerAgent = "Firefighter";
												else
													roamerAgent = GC.Choose<string>("Hobo", "UpperCruster", "UpperCruster");

												if (GC.challenges.Contains(cChallenge.LetMeSeeThatThrong) && GC.percentChance(1))
													roamerAgent = "Thief";
												else if (GC.challenges.Contains(cChallenge.SwarmWelcome) && GC.percentChance(3))
													roamerAgent = "Thief";
												else if (GC.percentChance(10))
													roamerAgent = "Thief";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";

												if (GC.percentChance(3))
													roamerAgent = "Vampire";
											}
										}
										else if (GC.levelTheme == 4)
										{
											if (GC.challenges.Contains(cChallenge.AnCapistan))
											{
												roamerAgent = GC.Choose<string>("UpperCruster", vAgent.SlumDweller);

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";

												if (GC.percentChance(3))
													roamerAgent = "Vampire";
											}
											else if (GC.challenges.Contains(cChallenge.PoliceState))
											{
												if (GC.percentChance(8))
													roamerAgent = "Firefighter";
												else
													roamerAgent = GC.Choose<string>("UpperCruster", "UpperCruster");

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";

												if (GC.percentChance(3))
													roamerAgent = "Vampire";
											}
											else
											{
												if (GC.percentChance(8))
													roamerAgent = "Firefighter";
												else
													roamerAgent = GC.Choose<string>("UpperCruster", "UpperCruster", new string[0]);

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";

												if (GC.percentChance(3))
													roamerAgent = "Vampire";
											}
										}
										else if (GC.levelTheme == 5)
										{
											if (GC.challenges.Contains(cChallenge.AnCapistan))
											{
												roamerAgent = vAgent.UpperCruster;

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
											else if (GC.challenges.Contains(cChallenge.PoliceState))
											{
												if (GC.percentChance(10))
													roamerAgent = "Firefighter";
												else
													roamerAgent = GC.Choose<string>("UpperCruster", "UpperCruster");

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
											else
											{
												if (GC.percentChance(10))
													roamerAgent = "Firefighter";
												else
													roamerAgent = GC.Choose<string>("UpperCruster", "UpperCruster");

												if (GC.challenges.Contains("ZombiesWelcome") && GC.percentChance(10))
													roamerAgent = "Zombie";

												if (GC.challenges.Contains("CannibalsDontAttack") && GC.percentChance(10))
													roamerAgent = "Cannibal";

												if (GC.challenges.Contains("DoctorsMoreImportant") && GC.percentChance(10))
													roamerAgent = "Doctor";
											}
										}
									}

									Agent spawnedAgent = GC.spawnerMain.SpawnAgent(trySpot, null, roamerAgent);
									spawnedAgent.movement.RotateToAngleTransform((float)Random.Range(0, 360));

									if (roamerAgent == vAgent.UpperCruster)
									{
										if (GC.percentChance(20))
										{
											trySpot = Vector2.zero;
											num80 = 0;
											Random.InitState(__instance.randomSeedNum + numObjects + ++randomCount);

											do
											{
												trySpot = GC.tileInfo.FindLocationNearLocation(spawnedAgent.tr.position, null, 0.32f, 1.28f, true, true);
												num80++;
											} while (trySpot == Vector2.zero && num80 < 300);

											if (trySpot != Vector2.zero && num80 < 300)
											{
												roamerAgent = vAgent.Slave;
												Agent agent15 = GC.spawnerMain.SpawnAgent(trySpot, null, roamerAgent);
												agent15.movement.RotateToAngleTransform((float)Random.Range(0, 360));
												agent15.relationships.SetRelInitial(spawnedAgent, "Submissive");
												spawnedAgent.relationships.SetRelInitial(agent15, "Aligned");
												agent15.slaveOwners.Add(spawnedAgent);
												spawnedAgent.slavesOwned.Add(agent15);
												Agent.gangCount++;
												spawnedAgent.gang = Agent.gangCount;
												agent15.gang = Agent.gangCount;
												agent15.modLeashes = 0;
												spawnedAgent.gangMembers.Add(agent15);
												agent15.gangMembers.Add(spawnedAgent);
												agent15.SetDefaultGoal("WanderFar");
											}
										}
									}
									else if (roamerAgent == vAgent.Thief && GC.percentChance(50))
										spawnedAgent.losCheckAtIntervals = true;
								}

								if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
								{
									yield return null;
									chunkStartTime = Time.realtimeSinceStartup;
								}

								Random.InitState(__instance.randomSeedNum + numObjects);
								num2 = numObjects;
							}
						}

						#endregion

						#region Musician

						bool hasMusician = false;

						if ((GC.levelTheme == 3 || GC.levelTheme == 4 || GC.levelTheme == 5 ||
								(GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33))) && GC.percentChance(33))
							hasMusician = true;

						if (GC.customLevel)
							hasMusician = __instance.customLevel.levelFeatures.Contains("Musician");

						if (GC.challenges.Contains(cChallenge.DiscoCityDanceoff))
							hasMusician = true;

						if (hasMusician && GC.levelFeeling != "Riot" && GC.levelFeeling != "HarmAtIntervals" && GC.levelFeeling != "Lockdown" &&
								GC.levelFeeling != "WarZone")
						{
							Debug.Log("Loading Musician");
							int bigTries = 1;

							if (GC.challenges.Contains(cChallenge.DiscoCityDanceoff))
								bigTries = 4 * LevelGenTools.LevelSizeRatio();

							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Vector2 myLocation = Vector2.zero;
								int num81 = 0;

								do
								{
									myLocation = GC.tileInfo.FindRandLocationGeneral(0.32f);
									num81++;
								} while ((myLocation == Vector2.zero || Vector2.Distance(myLocation, GC.playerAgent.tr.position) < 10f) && num81 < 300);

								if (myLocation != Vector2.zero && num81 < 300)
								{
									string agentType10 = "Musician";
									Agent mayorAgent = GC.spawnerMain.SpawnAgent(myLocation, null, agentType10);
									mayorAgent.movement.RotateToAngleTransform((float)Random.Range(0, 360));
									mayorAgent.SetDefaultGoal("WanderFar");
									myLocation = Vector2.zero;
									mayorAgent.mustSpillMoney = true;
									num81 = 0;
									Random.InitState(__instance.randomSeedNum + numObjects + ++randomCount);

									do
									{
										myLocation = GC.tileInfo.FindLocationNearLocation(mayorAgent.tr.position, null, 0.32f, 1.28f, true, true);
										num81++;
									} while (myLocation == Vector2.zero && num81 < 300);

									if (myLocation != Vector2.zero)
									{
										Agent.gangCount++;
										mayorAgent.gang = Agent.gangCount;
										mayorAgent.modLeashes = 0;
										Random.InitState(__instance.randomSeedNum + numObjects + ++randomCount);
										int i = Random.Range(2, 4);

										for (int j = 0; j < i; j = num2 + 1)
										{
											if (GC.challenges.Contains(cChallenge.DiscoCityDanceoff))
												agentType10 = vAgent.Musician;
											else if ((!GC.challenges.Contains("QuickGame") && GC.sessionDataBig.curLevelEndless > 9) ||
													(GC.challenges.Contains("QuickGame") && GC.sessionDataBig.curLevelEndless > 6))
												agentType10 = vAgent.Goon;
											else
												agentType10 = vAgent.Supergoon;

											Agent agent16 = GC.spawnerMain.SpawnAgent(myLocation, null, agentType10);
											agent16.movement.RotateToAngleTransform((float)Random.Range(0, 360));
											agent16.SetDefaultGoal("WanderFar");
											agent16.gang = Agent.gangCount;
											agent16.modLeashes = 0;
											agent16.modVigilant = 0;

											for (int num82 = 0; num82 < GC.agentList.Count; num82++)
											{
												Agent agent17 = GC.agentList[num82];

												if (agent17.gang == mayorAgent.gang)
												{
													agent16.relationships.SetRelInitial(agent17, "Aligned");
													agent17.relationships.SetRelInitial(agent16, "Aligned");

													if (!agent17.gangMembers.Contains(agent16))
														agent17.gangMembers.Add(agent16);

													if (!agent16.gangMembers.Contains(agent17))
														agent16.gangMembers.Add(agent17);
												}
											}

											if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
											{
												yield return null;
												chunkStartTime = Time.realtimeSinceStartup;
											}

											Random.InitState(__instance.randomSeedNum + j);
											num2 = j;
										}
									}

									mayorAgent = null;
								}

								myLocation = default(Vector2);
								num2 = numObjects;
							}
						}

						#endregion

						#region Cops

						bool hasCops = false;

						if ((GC.sessionDataBig.curLevel >= 2 || GC.levelTheme != 0) && GC.levelTheme != 2 && !GC.challenges.Contains("NoCops"))
							hasCops = true;

						if (GC.challenges.Contains(cChallenge.PoliceState) || (GC.levelTheme == 3 || GC.levelTheme == 4 || GC.levelTheme == 5) &&
								!GC.challenges.Contains("NoCops") && GC.debugMode)
							hasCops = true;

						if (GC.customLevel)
							hasCops = __instance.customLevel.levelFeatures.Contains("Cop");

						if (GC.challenges.Contains(cChallenge.AnCapistan))
							hasCops = false;

						if (hasCops)
						{
							Debug.Log("Loading Cops");
							int bigTries = (int)((float)Random.Range(6, 10) * __instance.levelSizeModifier);
							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Vector2 spot = Vector2.zero;
								int num83 = 0;

								do
								{
									spot = GC.tileInfo.FindRandLocationGeneral(0.32f);
									num83++;
								} while ((spot == Vector2.zero || Vector2.Distance(spot, GC.playerAgent.tr.position) < 20f) && num83 < 300);

								if (spot != Vector2.zero && num83 < 300)
								{
									string agentName = vAgent.Cop;

									if (GC.levelTheme == 4 || GC.levelTheme == 5 || GC.challenges.Contains(vChallenge.SupercopLand) ||
											GC.challenges.Contains(cChallenge.PoliceState))
										agentName = vAgent.SuperCop;

									if (__instance.replaceCopWithGangbanger)
										agentName = "Gangbanger";

									Agent agent = GC.spawnerMain.SpawnAgent(spot, null, agentName);
									agent.movement.RotateToAngleTransform((float)Random.Range(0, 360));

									if ((agentName == "Cop" || agentName == "Cop2") && GC.levelFeeling == "Lockdown")
										agent.oma.modProtectsProperty = 1;

									if (GC.challenges.Contains(cChallenge.PoliceState))
									{
										foreach (Agent otherAgent in GC.agentList)
											if (!vAgent.LawEnforcement.Contains(otherAgent.agentName))
											{
												agent.relationships.SetRelInitial(otherAgent, nameof(relStatus.Annoyed));

												agent.relationships.SetStrikes(otherAgent, 2);
											}
									}

									if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
									{
										yield return null;
										chunkStartTime = Time.realtimeSinceStartup;
									}

									Random.InitState(__instance.randomSeedNum + numObjects);
								}

								num2 = numObjects;
							}
						}

						#endregion

						#region Extra Cops

						bool extraCops = false;

						if (GC.sessionData.nextLevelExtraCops)
						{
							extraCops = true;
							GC.sessionData.nextLevelExtraCops = false;
						}

						if (GC.challenges.Contains(cChallenge.AnCapistan))
							extraCops = false;

						if (extraCops)
						{
							Debug.Log("Loading Extra Cops");
							int bigTries = (int)((float)Random.Range(6, 10) * __instance.levelSizeModifier);
							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Vector2 vector32 = Vector2.zero;
								int num84 = 0;

								do
								{
									vector32 = GC.tileInfo.FindRandLocationGeneral(0.32f);
									num84++;
								} while ((vector32 == Vector2.zero || Vector2.Distance(vector32, GC.playerAgent.tr.position) < 20f) && num84 < 300);

								if (vector32 != Vector2.zero && num84 < 300)
								{
									string agentType11 = "Cop2";
									Agent agent19 = GC.spawnerMain.SpawnAgent(vector32, null, agentType11);
									agent19.movement.RotateToAngleTransform((float)Random.Range(0, 360));

									if (GC.levelFeeling == "Lockdown")
										agent19.oma.modProtectsProperty = 1;

									if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
									{
										yield return null;
										chunkStartTime = Time.realtimeSinceStartup;
									}

									Random.InitState(__instance.randomSeedNum + numObjects);
								}

								num2 = numObjects;
							}
						}

						for (int num85 = 0; num85 < GC.objectRealList.Count; num85++)
						{
							ObjectReal objectReal11 = GC.objectRealList[num85];

							if (objectReal11.objectName == "PowerBox")
							{
								if (hasExtraPowerBoxes)
									__instance.SpawnCopNearLocation(objectReal11.tr.position, 1.28f, 2.56f, GC.Choose<int>(2, 3, new int[0]));
								else
									__instance.SpawnCopNearLocation(objectReal11.tr.position, 1.28f, 2.56f);
							}
							else if (objectReal11.objectName == "ATMMachine")
								__instance.SpawnCopNearLocation(objectReal11.tr.position, 0.64f, 2.56f);
						}

						#endregion

						#region Cop Bots

						bool hasCopBots = false;

						if (GC.challenges.Contains(cChallenge.PoliceState) || GC.levelTheme == 4 && !GC.challenges.Contains(vChallenge.NoCops))
							hasCopBots = true;

						if (GC.customLevel)
							hasCopBots = __instance.customLevel.levelFeatures.Contains(vAgent.CopBot);

						if (GC.challenges.Contains(cChallenge.AnCapistan))
							hasCops = false;

						if (hasCopBots)
						{
							Debug.Log("Loading CopBots");
							int bigTries = (int)((float)Random.Range(4, 6) * __instance.levelSizeModifier);
							string lakeType = "Normal";
							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Vector2 vector33 = Vector2.zero;
								int num86 = 0;

								do
								{
									vector33 = GC.tileInfo.FindRandLocationGeneral(0.32f);

									for (int num87 = 0; num87 < GC.agentList.Count; num87++)
										if (GC.agentList[num87].agentName == "CopBot" && Vector2.Distance(GC.agentList[num87].tr.position, vector33) < 14f)
											vector33 = Vector2.zero;

									num86++;
								} while ((vector33 == Vector2.zero || Vector2.Distance(vector33, GC.playerAgent.tr.position) < 20f) && num86 < 500);

								if (vector33 != Vector2.zero && num86 < 500)
								{
									string agentType12 = "CopBot";
									Agent agent20 = GC.spawnerMain.SpawnAgent(vector33, null, agentType12);
									agent20.movement.RotateToAngleTransform((float)Random.Range(0, 360));
									agent20.oma.securityType = agent20.oma.convertSecurityTypeToInt(lakeType);

									if (lakeType == "Normal")
										lakeType = "ID";
									else if (lakeType == "ID")
										lakeType = "Weapons";
									else if (lakeType == "Weapons")
										lakeType = "Normal";

									if (GC.levelFeeling == "Lockdown")
										agent20.oma.modProtectsProperty = 1;

									if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
									{
										yield return null;
										chunkStartTime = Time.realtimeSinceStartup;
									}

									Random.InitState(__instance.randomSeedNum + numObjects);
								}

								num2 = numObjects;
							}

							lakeType = null;
						}

						#endregion

						#region Zombies

						bool hasZombies = false;

						if (GC.challenges.Contains(vChallenge.ZombieMutator))
							hasZombies = true;

						if (GC.customLevel)
							hasZombies = __instance.customLevel.levelFeatures.Contains("Zombie");

						if (GC.challenges.Contains(cChallenge.PoliceState))
							hasZombies = false;

						if (hasZombies)
						{
							Debug.Log("Loading Zombies");
							int bigTries = (int)((float)Random.Range(25, 36) * __instance.levelSizeModifier);
							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Vector2 vector34 = Vector2.zero;
								int num88 = 0;

								do
								{
									vector34 = GC.tileInfo.FindRandLocationGeneral(0.32f);
									num88++;
								} while ((vector34 == Vector2.zero || Vector2.Distance(vector34, GC.playerAgent.tr.position) < 20f) && num88 < 200);

								if (vector34 != Vector2.zero && num88 < 200)
								{
									string agentType13 = "Zombie";
									Agent agent21 = GC.spawnerMain.SpawnAgent(vector34, null, agentType13);
									agent21.movement.RotateToAngleTransform((float)Random.Range(0, 360));
									agent21.lowerItemChance = true;
									Object.Destroy(agent21.nonQuestObjectMarker);

									if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
									{
										yield return null;
										chunkStartTime = Time.realtimeSinceStartup;
									}

									Random.InitState(__instance.randomSeedNum + numObjects);
								}

								num2 = numObjects;
							}
						}

						#endregion

						#region Gangbangers

						bool hasGangbangers = false;
						bool removeAgentsInBuilding = false;
						bool findingFactoryLake = false;

						if ((GC.sessionDataBig.curLevel >= 2 && (GC.levelTheme == 0 || GC.levelTheme == 1 || GC.levelTheme == 2 || GC.levelTheme == 3)) ||
								(GC.sessionDataBig.curLevel >= 2 && GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)))
							hasGangbangers = true;

						if (GC.levelTheme == 2 || GC.levelTheme == 4)
						{
							for (int num89 = 0; num89 < GC.playerAgentList.Count; num89++)
							{
								if (GC.playerAgentList[num89].bigQuest == "Gangbanger")
									findingFactoryLake = true;
								else if (GC.playerAgentList[num89].bigQuest == "GangbangerB")
									removeAgentsInBuilding = true;
							}

							if (removeAgentsInBuilding || findingFactoryLake)
								hasGangbangers = true;
						}

						if (GC.customLevel)
							hasGangbangers = __instance.customLevel.levelFeatures.Contains("Gangbanger");

						if (GC.challenges.Contains(cChallenge.YoungMenInTheNeighborhood))
							hasGangbangers = true;

						if (GC.challenges.Contains(cChallenge.PoliceState))
							hasGangbangers = false;

						if (GC.challenges.Contains(cChallenge.AnCapistan))
							hasGangbangers = true;

						if (hasGangbangers && GC.levelFeeling != "HarmAtIntervals" && GC.levelFeeling != "Lockdown" && GC.levelFeeling != "WarZone" &&
								GC.levelFeeling != "Riot")
						{
							Debug.Log("Loading Roving Gangs");

							int bigTries = GC.Choose<int>(0, 0, 0, 0, 1, 1, 2);
							bigTries = (int)(LevelGenTools.GangCount(bigTries) * (float)__instance.levelSizeMax / 30f);

							bool placedGangbangers = false;
							bool placedGangbangersB = false;

							if (removeAgentsInBuilding && findingFactoryLake)
								bigTries = 2;
							else if ((removeAgentsInBuilding || findingFactoryLake) && (bigTries == 0 || bigTries == 1))
								bigTries = GC.Choose<int>(1, 2, new int[0]);
							else if (GC.levelTheme == 2)
								bigTries = GC.Choose<int>(1, 1, 1, 1, 1, 2);

							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								string agentType14 = GC.Choose<string>("Gangbanger", "GangbangerB", new string[0]);

								if (removeAgentsInBuilding && numObjects == 0 && !placedGangbangers)
								{
									agentType14 = "Gangbanger";
									placedGangbangers = true;
								}
								else if (findingFactoryLake && numObjects == 0 && !placedGangbangersB)
								{
									agentType14 = "GangbangerB";
									placedGangbangersB = true;
								}
								else if ((placedGangbangers || placedGangbangersB) && numObjects == 1)
								{
									if (placedGangbangers)
										agentType14 = "GangbangerB";
									else
										agentType14 = "Gangbanger";
								}

								Agent.gangCount++;
								int num90 = Random.Range(3, 5);

								if (GC.levelTheme == 2)
									num90 = Random.Range(2, 4);

								Vector2 pos2 = Vector2.zero;

								for (int num91 = 0; num91 < num90; num91++)
								{
									Vector2 vector35 = Vector2.zero;
									int num92 = 0;

									if (num91 == 0)
									{
										do
										{
											vector35 = GC.tileInfo.FindRandLocationGeneral(0.32f);
											num92++;
										} while ((vector35 == Vector2.zero || Vector2.Distance(vector35, GC.playerAgent.tr.position) < 20f) && num92 < 300);

										pos2 = vector35;
									}
									else
										vector35 = GC.tileInfo.FindLocationNearLocation(pos2, null, 0.32f, 1.28f, true, true);

									if (vector35 != Vector2.zero && num92 < 300)
									{
										Agent agent22 = GC.spawnerMain.SpawnAgent(vector35, null, agentType14);
										agent22.movement.RotateToAngleTransform((float)Random.Range(0, 360));
										agent22.gang = Agent.gangCount;
										agent22.modLeashes = 0;

										for (int num93 = 0; num93 < GC.agentList.Count; num93++)
											if (GC.agentList[num93].gang == agent22.gang)
											{
												GC.agentList[num93].gangMembers.Add(agent22);

												if (!agent22.gangMembers.Contains(GC.agentList[num93]))
													agent22.gangMembers.Add(GC.agentList[num93]);
											}
									}
								}

								if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
								{
									yield return null;
									chunkStartTime = Time.realtimeSinceStartup;
								}

								Random.InitState(__instance.randomSeedNum + numObjects);
								num2 = numObjects;
							}
						}

						#endregion

						#region Mafia

						bool hasMafia = false;

						if (GC.levelTheme == 3 || (GC.sessionDataBig.curLevel >= 2 && GC.challenges.Contains("MixedUpLevels") && GC.percentChance(33)))
							hasMafia = true;

						if (GC.customLevel)
							hasMafia = __instance.customLevel.levelFeatures.Contains("Mafia");

						if (GC.challenges.Contains(cChallenge.MobTown) || GC.challenges.Contains(cChallenge.AnCapistan))
							hasMafia = true;

						if (GC.challenges.Contains(cChallenge.PoliceState))
							hasMafia = false;

						if (hasMafia && GC.levelFeeling != "HarmAtIntervals" && GC.levelFeeling != "Lockdown" && GC.levelFeeling != "WarZone")
						{
							Debug.Log("Loading Mafia");
							int bigTries = Random.Range(3, 5);
							bigTries = LevelGenTools.MafiaCount(bigTries);
							int num2;

							for (int numObjects = 0; numObjects < bigTries; numObjects = num2 + 1)
							{
								Agent.gangCount++;
								int num94 = Random.Range(3, 5);
								Vector2 pos3 = Vector2.zero;

								for (int num95 = 0; num95 < num94; num95++)
								{
									Vector2 vector36 = Vector2.zero;
									int num96 = 0;

									if (num95 == 0)
									{
										do
										{
											vector36 = GC.tileInfo.FindRandLocationGeneral(0.32f);
											num96++;
										} while ((vector36 == Vector2.zero || Vector2.Distance(vector36, GC.playerAgent.tr.position) < 20f) && num96 < 300);

										pos3 = vector36;
									}
									else
										vector36 = GC.tileInfo.FindLocationNearLocation(pos3, null, 0.32f, 1.28f, true, true);

									if (vector36 != Vector2.zero && num96 < 300)
									{
										Agent agent23 = GC.spawnerMain.SpawnAgent(vector36, null, "Mafia");
										agent23.movement.RotateToAngleTransform((float)Random.Range(0, 360));
										agent23.gang = Agent.gangCount;
										agent23.modLeashes = 0;

										if (num95 == 0 && numObjects == 0)
											agent23.gangLeader = true;

										for (int num97 = 0; num97 < GC.agentList.Count; num97++)
											if (GC.agentList[num97].gang == agent23.gang)
											{
												GC.agentList[num97].gangMembers.Add(agent23);

												if (!agent23.gangMembers.Contains(GC.agentList[num97]))
													agent23.gangMembers.Add(GC.agentList[num97]);
											}
									}
								}

								if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
								{
									yield return null;
									chunkStartTime = Time.realtimeSinceStartup;
								}

								Random.InitState(__instance.randomSeedNum + numObjects);
								num2 = numObjects;
							}
						}

						#endregion
					}

					#region Assassins

					for (int num98 = 0; num98 < GC.agentList.Count; num98++)
						if (GC.agentList[num98].isPlayer > 0 && (GC.agentList[num98].statusEffects.hasStatusEffect("InDebt3") ||
								(GC.agentList[num98].isPlayer == 1 && GC.challenges.Contains("AssassinsEveryLevel"))))
							__instance.SpawnAssassins(GC.agentList[num98], GC.agentList[num98].CalculateDebt());

					#endregion

					#endregion
				}

				if (!__instance.loadedObjectAgents)
				{
					Debug.Log("Loading Object Agents");
					List<Agent> objectAgents = new List<Agent>();
					int num2;

					for (int earlyCops = 0; earlyCops < 50; earlyCops = num2 + 1)
					{
						Agent item2 = GC.spawnerMain.SpawnAgent(Vector3.zero, null, "ObjectAgent");
						objectAgents.Add(item2);

						if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
						{
							yield return null;

							chunkStartTime = Time.realtimeSinceStartup;
						}

						Random.InitState(__instance.randomSeedNum + earlyCops);
						num2 = earlyCops;
					}

					yield return null;

					for (int earlyCops = 0; earlyCops < objectAgents.Count; earlyCops = num2 + 1)
					{
						objectAgents[earlyCops].DestroyMe();

						if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
						{
							yield return null;

							chunkStartTime = Time.realtimeSinceStartup;
						}

						Random.InitState(__instance.randomSeedNum + earlyCops);
						num2 = earlyCops;
					}

					__instance.loadedObjectAgents = true;
					objectAgents = null;
				}

				if (!GC.multiplayerMode)
				{
					List<Agent> objectAgents = new List<Agent>();
					int num99 = 10;

					if (GC.levelFeeling == "WarZone")
						num99 = 40;

					int num100 = GC.poolsScene.agentCount - GC.poolsScene.agentIterator;
					int earlyCops = num99 - num100;

					if (earlyCops > 0)
					{
						Debug.Log("Loading Recycled Agents: " + earlyCops);
						int num2;

						for (int bigTries = 0; bigTries < earlyCops; bigTries = num2 + 1)
						{
							Agent item3 = GC.spawnerMain.SpawnAgent(Vector3.zero, null, "Hobo");
							objectAgents.Add(item3);

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;

								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + bigTries);
							num2 = bigTries;
						}

						yield return null;

						for (int bigTries = 0; bigTries < objectAgents.Count; bigTries = num2 + 1)
						{
							objectAgents[bigTries].DestroyMe();

							if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
							{
								yield return null;

								chunkStartTime = Time.realtimeSinceStartup;
							}

							Random.InitState(__instance.randomSeedNum + bigTries);
							num2 = bigTries;
						}
					}

					objectAgents = null;
				}
			}
			else if (!__instance.memoryTest && !GC.streamingWorld)
			{
				GC.levelFeeling = GC.playerAgent.oma.convertIntToLevelFeeling(GC.playerAgent.oma.levelFeeling);
				int num2;

				for (int earlyCops = 0; earlyCops < GC.tileInfo.simpleObjects.Count; earlyCops = num2 + 1)
				{
					GC.spawnerMain.spawnObjectReal(GC.tileInfo.simpleObjectPositions[earlyCops], null, GC.tileInfo.simpleObjects[earlyCops]);

					if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
					{
						yield return null;

						chunkStartTime = Time.realtimeSinceStartup;
					}

					Random.InitState(__instance.randomSeedNum + earlyCops);
					num2 = earlyCops;
				}
			}

			if (!__instance.loadedPoolAgents && GC.streamingWorld && GC.streamingWorldController.usePools)
			{
				Debug.Log("Loading Pool Agents");
				__instance.spawningAgentPool = true;
				List<Agent> objectAgents = new List<Agent>();
				int earlyCopCount;

				for (int earlyCops = 0; earlyCops < 149; earlyCops = earlyCopCount + 1)
				{
					Agent earlyCop = GC.spawnerMain.SpawnAgent(Vector3.zero, null, "");

					if (!GC.serverPlayer)
						earlyCop.tr.SetParent(GC.agentsNest.transform);

					objectAgents.Add(earlyCop);

					if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
					{
						yield return null;

						chunkStartTime = Time.realtimeSinceStartup;
					}

					Random.InitState(__instance.randomSeedNum + earlyCops);
					earlyCopCount = earlyCops;
				}

				yield return null;

				for (int earlyCops = 0; earlyCops < objectAgents.Count; earlyCops = earlyCopCount + 1)
				{
					objectAgents[earlyCops].DestroyMe();

					if (Time.realtimeSinceStartup - chunkStartTime > maxChunkTime)
					{
						yield return null;

						chunkStartTime = Time.realtimeSinceStartup;
					}

					Random.InitState(__instance.randomSeedNum + earlyCops);
					earlyCopCount = earlyCops;
				}

				__instance.loadedPoolAgents = true;
				__instance.spawningAgentPool = false;
				objectAgents = null;
			}

			if (GC.streamingWorld)
				while (GC.streamingWorldController.settingUpPools)
					yield return null;

			for (int num101 = 0; num101 < __instance.levelChunks.Count; num101++)
				__instance.levelChunks[num101].chunkPos = __instance.levelChunks[num101].transform.position;

			if (!GC.serverPlayer)
				__instance.tileInfo.SetupFloorTiles(false, null);

			GC.tileInfo.SetupOrganicTiles(false);
			GC.tileInfo.SetupOrganicTiles2(false);
			___tilemapFloors4.Build();
			___minimap.UpdateMiniMap();
			__instance.Invoke("SetupMore4", 0.1f); // base.invoke

			yield break;
		}
	}
	
	[HarmonyPatch]
	static class SetupMore3_3_Patches
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "SetupMore3_3", new Type[] { }));

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_AlarmButtons(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasPoliceBoxesAndAlarmButtons = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPoliceBoxesAndAlarmButtons), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 1040
					// if(flag11)

					new CodeInstruction(OpCodes.Stloc_S, 8),
					new CodeInstruction(OpCodes.Ldloc_S, 8),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Alarm Buttons"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag11 = LevelGenTools(flag11);

					new CodeInstruction(OpCodes.Ldloc_S, 8), // flag11
					new CodeInstruction(OpCodes.Call, LevelGenTools_HasPoliceBoxesAndAlarmButtons), // bool
					new CodeInstruction(OpCodes.Stloc_S, 8), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_ExplodingAndSlimeBarrels(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasExplodingAndSlimeBarrels = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasFireHydrants), new[] { typeof(bool) });
			MethodInfo contains = AccessTools.Method(typeof(List<string>), nameof(List<string>.Contains), new[] { typeof(string) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					// if (this.gc.customLevel)
					//	flag15 = this.customLevel.levelFeatures.Contains("ExplodingSlimeBarrel");

					new CodeInstruction(OpCodes.Ldstr, "ExplodingSlimeBarrel"),
					new CodeInstruction(OpCodes.Callvirt, contains),
					new CodeInstruction(OpCodes.Stloc_S, 11),
					new CodeInstruction(OpCodes.Stloc_S, 11),

					},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag14 = LevelGenTools.HasFireHydrants(flag14);

					new CodeInstruction(OpCodes.Ldloc_S, 10), // flag14
					new CodeInstruction(OpCodes.Call, LevelGenTools_HasExplodingAndSlimeBarrels), // bool
					new CodeInstruction(OpCodes.Stloc_S, 10), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_FireHydrants(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasFireHydrants = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasFireHydrants), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 1270
					// if (flag14)

					new CodeInstruction(OpCodes.Ldloc_S, 10),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Fire Hydrants"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag14 = LevelGenTools.HasFireHydrants(flag14);

					new CodeInstruction(OpCodes.Ldloc_S, 10), // flag14
					new CodeInstruction(OpCodes.Call, LevelGenTools_HasFireHydrants), // bool
					new CodeInstruction(OpCodes.Stloc_S, 10), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_Lakes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo loadLevel_hasLakes = AccessTools.Field(typeof(LoadLevel), nameof(LoadLevel.hasLakes));
			MethodInfo levelGenTools_SetHasLakes = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.SetHasLakes), new[] { typeof(LoadLevel) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 30

					new CodeInstruction(OpCodes.Ldfld, loadLevel_hasLakes),
					new CodeInstruction(OpCodes.Brfalse),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// SetupMore3_3_SetHasLakes(__instance);

					new CodeInstruction(OpCodes.Ldloc_1), // LoadLevel (instance containing this class, created when MoveNext/IEnumerator is used)
					new CodeInstruction(OpCodes.Call, levelGenTools_SetHasLakes), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_Manholes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasManholes = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasManholesVanilla), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 1154
					// if (flag12)

					new CodeInstruction(OpCodes.Ldloc_S, 9),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Manholes"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag12 = LevelGenTools.HasManholes(flag12);
					
					new CodeInstruction(OpCodes.Ldloc_S, 9), // flag12
					new CodeInstruction(OpCodes.Call, levelGenTools_HasManholes), // bool
					new CodeInstruction(OpCodes.Stloc_S, 9), // Clear

				}); ;

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_OilSpills(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasPollutionFeatures = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPollutionFeatures), new[] { typeof(bool) });
			FieldInfo gc = AccessTools.Field(typeof(LoadLevel), "gc"); // private
			FieldInfo gc_serverPlayer = AccessTools.Field(typeof(GameController), nameof(GameController.serverPlayer));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 452
					// if (flag3 && this.gc.serverPlayer)

					new CodeInstruction(OpCodes.Ldloc_S, 4),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld, gc),
					new CodeInstruction(OpCodes.Ldfld, gc_serverPlayer), 
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Oil Spills"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag3 = SetupMore3_3_HasPollutionFeatures(flag3);

					new CodeInstruction(OpCodes.Ldloc_S, 4), // flag3
					new CodeInstruction(OpCodes.Call, levelGenTools_HasPollutionFeatures), // bool
					new CodeInstruction(OpCodes.Stloc_S, 4) // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_PoliceBoxes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo hasPoliceBoxes = AccessTools.Field(typeof(LoadLevel), "hasPoliceBoxes");
			MethodInfo loadLevel_HasPoliceBoxes = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPoliceBoxesAndAlarmButtons), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 926
					// if (hasPoliceBoxes)

					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, hasPoliceBoxes),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Police Boxes"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// __instance.hasPoliceBoxes = LevelGenTools.HasPoliceBoxes(__instance.hasPoliceBoxes);

					new CodeInstruction(OpCodes.Ldarg_0), // __instance
					new CodeInstruction(OpCodes.Ldfld, hasPoliceBoxes), // __instance.hasPoliceBoxes
					new CodeInstruction(OpCodes.Call, loadLevel_HasPoliceBoxes), // bool
					new CodeInstruction(OpCodes.Stfld, hasPoliceBoxes), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_PowerBoxes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasPowerBoxes = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPowerBoxes), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 340
					// if (flag)

					new CodeInstruction(OpCodes.Ldloc_2),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Power Boxes"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag = SetupMore3_3_HasPowerBoxes(flag);

					new CodeInstruction(OpCodes.Ldloc_2), // flag
					new CodeInstruction(OpCodes.Call, levelGenTools_HasPowerBoxes), // bool
					new CodeInstruction(OpCodes.Stloc_2) // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_SlimeBarrels(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasPollutionFeatures = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPollutionFeatures), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 403
					// if (flag2)

					new CodeInstruction(OpCodes.Ldloc_3),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Slime Barrels"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag2 = HasPollutionFeatures(flag2);

					new CodeInstruction(OpCodes.Ldloc_3), // flag2
					new CodeInstruction(OpCodes.Call, levelGenTools_HasPollutionFeatures), // bool
					new CodeInstruction(OpCodes.Stloc_3) // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_TrashCans(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasTrashCans = AccessTools.Method(type: typeof(LevelGenTools), nameof(LevelGenTools.HasTrashCans), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 694
					// if (flag7)

					new CodeInstruction(OpCodes.Ldloc_S, 6),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Trash Cans"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag7 = HasTrashCans(flag7);

					new CodeInstruction(OpCodes.Ldloc_S, 6), // flag7
					new CodeInstruction(OpCodes.Call, levelGenTools_HasTrashCans), // bool
					new CodeInstruction(OpCodes.Stloc_S, 6) // Clear

				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_VendorCarts(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo loadLevel_HasVendorCarts = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasVendorCarts), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 838
					// if (flag10)

					new CodeInstruction(OpCodes.Stloc_S, 7),
					new CodeInstruction(OpCodes.Ldloc_S, 7),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Vendor Carts"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag10 = HasVendorCarts(flag10);

					new CodeInstruction(OpCodes.Ldloc_S, 7), // flag10
					new CodeInstruction(OpCodes.Call, loadLevel_HasVendorCarts), // bool
					new CodeInstruction(OpCodes.Stloc_S, 7), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}
