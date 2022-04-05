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
using SORCE.Challenges.C_Features;
using SORCE.Localization;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(LoadLevel))]
	public static class P_LoadLevel
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Level Size
		/// </summary>
		/// <param name="instructionsEnumerable"></param>
		/// <returns></returns>
		[HarmonyTranspiler, HarmonyPatch(methodName: "CreateInitialMap", new Type[] { })]
		private static IEnumerable<CodeInstruction> CreateInitialMap_Transpiler(IEnumerable<CodeInstruction> instructionsEnumerable, ILGenerator generator)
		{
			List<CodeInstruction> instructions = instructionsEnumerable.ToList();
			FieldInfo loadLevel_levelSizeMax = AccessTools.Field(typeof(LoadLevel), nameof(LoadLevel.levelSizeMax));
			MethodInfo levelGenTools_SetLevelSizeModifier = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.SetLevelSizeMax), new[] { typeof(LoadLevel) });

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
		/// Public floors
		///		This works in two different places, which work on different districts.
		///			Like, what the fuck.
		///	TODO: Transpiler
		///		There is a particular way to transpile into an IEnumerator, as modeled by SetupMore3_3
		/// </summary>
		/// <param name="__instance"></param>
		/// <param name="__result"></param>
		/// <param name="___tilemapWalls"></param>
		/// <param name="___tilemapFloors2"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: "FillMapChunks", new Type[] { })]
		public static bool FillMapChunks_Prefix(LoadLevel __instance, ref IEnumerator __result, ref tk2dTileMap ___tilemapWalls, ref tk2dTileMap ___tilemapFloors2)
		{
			__result = FillMapChunks_Replacement(__instance, ___tilemapWalls, ___tilemapFloors2);
			return false;
		}
		public static IEnumerator FillMapChunks_Replacement(LoadLevel __instance, tk2dTileMap ___tilemapWalls, tk2dTileMap ___tilemapFloors2)
		{
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
								string tilemapGroup = VFloorTileGroup.Building;

								if (ChallengeManager.IsChallengeFromListActive(CChallenge.Overhauls))
									tilemapGroup = LevelGenTools.PublicFloorTileGroup(); // Works on: Slums,
								else if (GC.levelShape == 0 && GC.levelType != "HomeBase")
								{
									if (GC.levelTheme == 0)
										tilemapGroup = VFloorTileGroup.Slums;
									else if (GC.levelTheme == 1)
										tilemapGroup = VFloorTileGroup.Industrial;
									else if (GC.levelTheme == 2)
										tilemapGroup = VFloorTileGroup.Park;
									else if (GC.levelTheme == 3)
										tilemapGroup = VFloorTileGroup.Downtown;
									else if (GC.levelTheme == 4)
										tilemapGroup = VFloorTileGroup.Uptown;
									else if (GC.levelTheme == 5)
										tilemapGroup = VFloorTileGroup.MayorVillage;
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
									string tilemapGroup = VFloorTileGroup.Building;

									if (ChallengeManager.IsChallengeFromListActive(CChallenge.Overhauls))
										tilemapGroup = VFloorTileGroup.MayorVillage; // Works on: Park, 
									else if (GC.levelShape == 0 && GC.levelType != "HomeBase")
									{
										if (GC.levelTheme == 0)
											tilemapGroup = VFloorTileGroup.Slums;
										else if (GC.levelTheme == 1)
											tilemapGroup = VFloorTileGroup.Industrial;
										else if (GC.levelTheme == 2)
											tilemapGroup = VFloorTileGroup.Park;
										else if (GC.levelTheme == 3)
											tilemapGroup = VFloorTileGroup.Downtown;
										else if (GC.levelTheme == 4)
											tilemapGroup = VFloorTileGroup.Uptown;
										else if (GC.levelTheme == 5)
											tilemapGroup = VFloorTileGroup.MayorVillage;
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
				MethodInfo FillMapChunks2_Private = AccessTools.Method(typeof(LoadLevel), "FillMapChunks2", new Type[0] { });
				IEnumerator FillMapChunks2_Private_IEnumerator = (IEnumerator)FillMapChunks2_Private.Invoke(__instance, new object[0]);
				__instance.StartCoroutine(FillMapChunks2_Private_IEnumerator);
			}

			yield break;
		}

		/// <summary>
		/// Skyway District Holes
		/// </summary>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.loadStuff2), new Type[] { })]
		public static bool LoadStuff2_Prefix()
		{
			logger.LogDebug("LoadLevel_loadStuff2_Prefix");

			if (GC.challenges.Contains(nameof(SkywayDistrict)))
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
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.SetupBasicLevel), new Type[] { })]
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
									tileData.wallMaterial = LevelGenTools.BorderWallMaterial(); //
									int tile2 = Random.Range(0, 0);
									___tilemapFloors2.SetTile(m, n - 1, 0, tile2);
									tileData.chunkID = __instance.mapChunkArray[i, j].chunkID;
								}
					}

			return false;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: "SetupMore3_3", new Type[] { })]
		public static void SetupMore3_3_Postfix(LoadLevel __instance)
		{
			LevelGenTools.Spawn_Master(__instance);
		}

		/// <summary>
		/// Ambient Light Color
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(LoadLevel.SetNormalLighting), new Type[] {})]
		public static void SetNormalLighting_Postfix(LoadLevel __instance)
		{
			if (ChallengeManager.IsChallengeFromListActive(CColor.AmbientLightColor))
			{
				string challenge = ChallengeManager.GetActiveChallengeFromList(CColor.AmbientLightColor);
				Color32 color = CColor.AmbientLightColorDict[challenge];
				int objectColorDivisor = 2;
				Color32 objectColor = new Color32(color.r, color.g, color.b, (byte)(color.a / objectColorDivisor));

				RenderSettings.ambientLight = objectColor; // Walls, Objects
				GameObject.Find("Ambient").GetComponent<SpriteRenderer>().color = color; // Floors

				MethodInfo SetRogueVisionLighting_Private = AccessTools.Method(typeof(LoadLevel), "SetRogueVisionLighting", new Type[0] { });
				IEnumerator SetRogueVisionLighting_Private_IEnumerator = (IEnumerator)SetRogueVisionLighting_Private.Invoke(__instance, new object[0]);
				__instance.StartCoroutine(SetRogueVisionLighting_Private_IEnumerator);
			}
		}
	}
	
	[HarmonyPatch]
	static class P_LoadLevel_SetupMore3_3
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "SetupMore3_3", new Type[] { }));

        #region Features
        [HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> AlarmButtons(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasPoliceBoxesAndAlarmButtons = 
				AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPoliceBoxesAndAlarmButtons), new[] { typeof(bool) });

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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> AmbientAudio(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_AmbientAudio = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.AmbientAudio), new[] { typeof(string), typeof(string) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{ 
					//	Line 2001
					//	text4 = "GraveyardAmbience";
					//		text4 (only added to stack)

					new CodeInstruction(OpCodes.Ldstr, "ParkAmbience"),
					new CodeInstruction(OpCodes.Stloc_S, 165),						// clear
					new CodeInstruction(OpCodes.Ldloc_S, 165),						// text4
					// Extra code is because insertion needs to be AFTER this major GOTO destination
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//		text4 = "GraveyardAmbience";
					// }
					// text4 = AmbientAudio(text4, description);
					// if (text4 != "")...
					
					new CodeInstruction(OpCodes.Ldloc_S, 166),						// text4, description
					new CodeInstruction(OpCodes.Call, LevelGenTools_AmbientAudio),	// audio track name
					new CodeInstruction(OpCodes.Stloc_S, 165),						// clear
					new CodeInstruction(OpCodes.Ldloc_S, 165),						// text4
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Assassins

		// Barbecues

		// Bear Traps

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Boulders(IEnumerable<CodeInstruction> codeInstructions)
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

		// Bushes

		// Cops

		// Cops, Extra

		// Cop Bots

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> ExplodingAndSlimeBarrels(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasExplodingAndSlimeBarrels = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasExplodingAndSlimeBarrels), new[] { typeof(bool) });
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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> FireHydrants(IEnumerable<CodeInstruction> codeInstructions)
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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> FlameGrates(IEnumerable<CodeInstruction> codeInstructions)
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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> FlamingBarrels(IEnumerable<CodeInstruction> codeInstructions)
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

		// Fountains

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Lakes(IEnumerable<CodeInstruction> codeInstructions)
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

		// Lamps

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Manholes(IEnumerable<CodeInstruction> codeInstructions)
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

		// Mafia

		// Musician

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Mines(IEnumerable<CodeInstruction> codeInstructions)
        {
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasLandMines = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasLandMines), new [] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					//	if (flag23)
					//		Debug.Log("Loading Mines");

					new CodeInstruction(OpCodes.Ldloc_S, 17),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Mines"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//	flag23 = HasLandMines(flag23);

					new CodeInstruction(OpCodes.Ldloc_S, 17), // flag23
					new CodeInstruction(OpCodes.Call, levelGenTools_HasLandMines), // bool
					new CodeInstruction(OpCodes.Stloc_S, 17), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
        }

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> OilSpills(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasOilSpills = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasOilSpills), new[] { typeof(bool) });
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
					// flag3 = SetupMore3_3_HasOilSpills(flag3);

					new CodeInstruction(OpCodes.Ldloc_S, 4), // flag3
					new CodeInstruction(OpCodes.Call, levelGenTools_HasOilSpills), // bool
					new CodeInstruction(OpCodes.Stloc_S, 4) // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> PoliceBoxes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			Type enumeratorType = PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "SetupMore3_3")).DeclaringType;
			FieldInfo hasPoliceBoxes = AccessTools.Field(enumeratorType, "<hasPoliceBoxes>5__7");

			MethodInfo HasPoliceBoxesAndAlarmButtons = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPoliceBoxesAndAlarmButtons), new[] { typeof(bool) });

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
					new CodeInstruction(OpCodes.Ldarg_0), // __instance, __instance
					new CodeInstruction(OpCodes.Ldfld, hasPoliceBoxes), // __instance, __instance.hasPoliceBoxes
					new CodeInstruction(OpCodes.Call, HasPoliceBoxesAndAlarmButtons), // __instance, bool
					new CodeInstruction(OpCodes.Stfld, hasPoliceBoxes), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> PowerBoxes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo hasPowerBoxes = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasPowerBoxes), new[] { typeof(bool) });

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
					// flag = LevelgenTools.HasPowerBoxes(flag);

					new CodeInstruction(OpCodes.Ldloc_2), // flag
					new CodeInstruction(OpCodes.Call, hasPowerBoxes), // bool
					new CodeInstruction(OpCodes.Stloc_2) // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Roving Gangs

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SlimeBarrels(IEnumerable<CodeInstruction> codeInstructions)
        {
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasSlimeBarrels = AccessTools.Method(type: typeof(LevelGenTools), nameof(LevelGenTools.HasSlimeBarrels), new Type[] { });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 13102
					// if (flag2)

					new CodeInstruction(OpCodes.Ldloc_S, 3),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Slime Barrels"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag2 = HasSlimeBarrels();

					new CodeInstruction(OpCodes.Call, levelGenTools_HasSlimeBarrels), // bool 
					new CodeInstruction(OpCodes.Stloc_S, 3) // Clear

				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> TrashCans(IEnumerable<CodeInstruction> codeInstructions)
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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Trees(IEnumerable<CodeInstruction> codeInstructions)
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

		// Vending Machines

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> VendorCarts(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo loadLevel_HasVendorCarts = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.HasVendorCarts), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// Line 838
					// if (flag10)

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

		// Zombies

		#endregion
        #region Roamers
        [HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> RoamerAgentNumber(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_PopulationMultiplier = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.PopulationMultiplier));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					//	Line 2414 
					//		Debug.Log("Loading Slum Dwellers");
					//		int bigTries = (int)((float)Random.Range(16, 20) * this.levelSizeModifier);

					new CodeInstruction(OpCodes.Ldstr, "Loading Slum Dwellers"),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldc_I4_S, 16),
					new CodeInstruction(OpCodes.Ldc_I4_S, 20),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Conv_R4),
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Mul),
					new CodeInstruction(OpCodes.Conv_I4),
					// Store field comes after insertion

				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//	* PopulationMultiplier;

					new CodeInstruction(OpCodes.Call, levelGenTools_PopulationMultiplier), // int
					new CodeInstruction(OpCodes.Mul) // clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Reduces excessive thieves in population mods
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> RoamerAgentType(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo RoamerAgentType = AccessTools.Method(typeof(LevelGenTools), nameof(LevelGenTools.RoamerAgentType), new[] { typeof(string) });

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
        #endregion
    }
}