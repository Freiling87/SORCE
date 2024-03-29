﻿using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using SORCE.Challenges;
using SORCE.Challenges.C_AmbientLightColor;
using SORCE.Challenges.C_Features;
using SORCE.Challenges.C_Gangs;
using SORCE.Challenges.C_Lighting;
using SORCE.Challenges.C_Preview;
using SORCE.Challenges.C_VFX;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Traits;
using SORCE.Utilities;
using SORCE.Utilities.MapGen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(LoadLevel))]
	public static class P_LoadLevel
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.CanUseChunk), argumentTypes: new[] { typeof(GameObject), typeof(ChunkData), typeof(bool), typeof(int), typeof(int), typeof(Vector3) })]
		public static bool CanUseChunk_Prefix(GameObject myChunkGo, ChunkData myChunkBasic, bool checkSessionData, int randChunkNum, int chunkShape, Vector3 myNewPos, ref bool __result)
        {
			if (myChunkBasic.description == VChunkType.ConfiscationCenter &&
				Chunks.HasConfiscationCenters)
			{
				__result = !GC.loadLevel.placedConfiscationCenter;
				return false;
			}
			else if (myChunkBasic.description == VChunkType.DeportationCenter &&
				Chunks.HasDeportationCenters)
			{
				__result = !GC.loadLevel.placedDeportationCenter;
				return false;
			}

			return true;
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
									tilemapGroup = Structures.PublicFloorTileGroup(); // Works on: Slums,
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
		/// Bool setters
		/// </summary>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.loadStuff), new Type[0] { })]
		public static bool LoadStuff_Prefix()
		{
			logger.LogDebug("loadStuff_Prefix");

			// Reduces performance to complex boolean calls
			Gunplay.ModBulletholes = GC.challenges.Contains(nameof(BuggierBulletholes));
			Gunplay.ModFastBullets = GC.challenges.Contains(nameof(ScaryGunsPreview));
			Gunplay.ModGunLighting = GC.challenges.Contains(nameof(GunplayRelit));
			Gunplay.ModGunParticles = GC.challenges.Contains(nameof(ShootierGuns));
			Underdank.UnderdankActive =
				TraitManager.IsPlayerTraitActive<UnderdankCitizen>() ||
				TraitManager.IsPlayerTraitActive<UnderdankVIP>();
			Wreckage.HasObjectExtraWreckage = GC.challenges.Contains(nameof(DestroyederDestroyage)) || DebugTools.debugMode;

			return true;
		}

		/// <summary>
		/// Skyway District Holes
		/// </summary>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(LoadLevel.loadStuff2), new Type[0] { })]
		public static bool LoadStuff2_Prefix()
		{
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
									tileData.wallMaterial = Structures.BorderWallMaterial(); //
									int tile2 = Random.Range(0, 0);
									___tilemapFloors2.SetTile(m, n - 1, 0, tile2);
									tileData.chunkID = __instance.mapChunkArray[i, j].chunkID;
								}
					}

			return false;
		}

		/// <summary>
		/// Call Spawn_Master
		/// </summary>
		[HarmonyPostfix, HarmonyPatch(methodName: "SetupMore3_3", new Type[] { })]
		public static void SetupMore3_3_Postfix()
		{
			if (GC.levelType == "HomeBase")
				return;
			
			MapFeatureSpawners.Spawn_Master();
			GangChallengeTools.Spawner_Main();
			Roamers.Spawner_Main();
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
				AmbientLightColorChallenge challenge = RogueFramework.Unlocks.OfType<AmbientLightColorChallenge>().FirstOrDefault(c => c.IsEnabled);
				Color32 color = challenge.FilterColor;
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

	[HarmonyPatch(declaringType: typeof(LoadLevel), methodName: "CreateInitialMap")]
	static class P_LoadLevel_CreateInitialMap
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Level Size
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> CreateInitialMap_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo setChunkCount = AccessTools.Method(typeof(LevelSize), nameof(LevelSize.SetChunkCount), new[] { typeof(LoadLevel) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					// Debug.Log("LEVEL SIZE: " + num.ToString());

					new CodeInstruction(OpCodes.Ldstr, "LEVEL SIZE: "),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldflda),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Call),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{ 
					// LevelSize.SetChunkCount(__instance);

					new CodeInstruction(OpCodes.Ldarg_0), // __instance
					new CodeInstruction(OpCodes.Call, setChunkCount), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}