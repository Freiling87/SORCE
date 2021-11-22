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
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_AmbientAudio(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_AmbientAudio = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.AmbientAudio), new[] { typeof(string) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					//	Line 2001
					//		(If-block's structure is inverted in C#/CIL due to structure. In C# the line is actually 1984.)
					//	text4 = "ParkAmbience";

					new CodeInstruction(OpCodes.Ldstr, "ParkAmbience"),
					new CodeInstruction(OpCodes.Stloc_S, 165),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 165), // text4
					new CodeInstruction(OpCodes.Call, LevelGenTools_AmbientAudio), // string
					new CodeInstruction(OpCodes.Stloc_S, 165), // clear
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
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_Boulders(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasBoulders = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasBoulders), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 1589
					// if(flag20)

					new CodeInstruction(OpCodes.Stloc_S, 15),
					new CodeInstruction(OpCodes.Ldloc_S, 15),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Boulders"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag20 = LevelGenTools.HasBoulders(flag20);

					new CodeInstruction(OpCodes.Ldloc_S, 15), // flag20
					new CodeInstruction(OpCodes.Call, LevelGenTools_HasBoulders), // bool
					new CodeInstruction(OpCodes.Stloc_S, 15), // Clear
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
					//	if (this.gc.customLevel)
					//		flag15 = this.customLevel.levelFeatures.Contains("ExplodingSlimeBarrel");

					new CodeInstruction(OpCodes.Ldstr, "ExplodingSlimeBarrel"),
					new CodeInstruction(OpCodes.Callvirt, contains),
					new CodeInstruction(OpCodes.Stloc_S, 11),
					new CodeInstruction(OpCodes.Ldloc_S, 11),
					},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//	flag15 = LevelGenTools.HasExplodingAndSlimeBarrels(flag15);

					new CodeInstruction(OpCodes.Ldloc_S, 11), // flag15
					new CodeInstruction(OpCodes.Call, LevelGenTools_HasExplodingAndSlimeBarrels), // bool
					new CodeInstruction(OpCodes.Stloc_S, 11), // Clear
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
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_FlameGrates(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_SetHasFlameGrates = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.SetHasFlameGrates), new[] { typeof(LoadLevel) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					/*	Line 1410
						if (this.hasFlameGrates) {
							Debug.Log("Loading Flame Grates");
					*/

					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Flame Grates"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// LevelGenTools.SetHasFlameGrates(__instance);

					new CodeInstruction(OpCodes.Ldloc_1), // __instance
					new CodeInstruction(OpCodes.Call, LevelGenTools_SetHasFlameGrates),
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
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_FlamingBarrels(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasFlamingBarrels = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasFlamingBarrels), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					//	Line 1367
					//	if (flag16) {
					//		Debug.Log("Loading Flaming Barrels");

					new CodeInstruction(OpCodes.Ldloc_S, 12),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Flaming Barrels"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag16 = LevelGenTools.HasFlamingBarrels(flag16);

					new CodeInstruction(OpCodes.Ldloc_S, 12), // flag16
					new CodeInstruction(OpCodes.Call, LevelGenTools_HasFlamingBarrels), // bool
					new CodeInstruction(OpCodes.Stloc_S, 12), // Clear
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
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_RoamerAgents(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo RoamerAgentType = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.RoamerAgentType), new[] { typeof(string), typeof(int) });
			FieldInfo gameController = AccessTools.Field(typeof(LoadLevel), "gc");

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					//	Line 2632
					//		Agent agent14 = this.gc.spawnerMain.SpawnAgent(vector30, null, text5);

					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldloc_S, 230), // 230 = vector30
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Ldnull),
					new CodeInstruction(OpCodes.Ldloc_S, 232), // 232 = text5
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Stloc_S, 233) // 233 = agent14
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// text5 = LevelGenTools(text5);

					new CodeInstruction(OpCodes.Ldloc_S, 232), // text5
					new CodeInstruction(OpCodes.Call, RoamerAgentType), // string
					new CodeInstruction(OpCodes.Stloc_S, 232), // clear
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
		private static IEnumerable<CodeInstruction> SetupMore3_3_Transpiler_Trees(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasTrees = AccessTools.Method(type: typeof(LevelGenTools), nameof(LevelGenTools.HasTrees), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					//	Line 1548
					//	if(flag19)

					new CodeInstruction(OpCodes.Stloc_S, 14),
					new CodeInstruction(OpCodes.Ldloc_S, 14),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Trees"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//	flag19 = LevelGenTools.HasTrees(flag19);

					new CodeInstruction(OpCodes.Ldloc_S, 14), // flag19
					new CodeInstruction(OpCodes.Call, levelGenTools_HasTrees), // bool
					new CodeInstruction(OpCodes.Stloc_S, 14) // clear
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
