using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.InstructionSearch;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using SORCE.Logging;
using SORCE.MapGenUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;


namespace SORCE.Patches.P_MapGen
{
	[HarmonyPatch(declaringType: typeof(LoadLevel))]
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
				AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasPoliceBoxesAndAlarmButtons), new[] { typeof(bool) });

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
		private static IEnumerable<CodeInstruction> ChunkAmbientAudio(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_AmbientAudio = AccessTools.Method(typeof(AmbientAudio), nameof(AmbientAudio.SetChunkAmbientAudio), new[] { typeof(string), typeof(string) });

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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> BearTraps(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo HasBearTraps = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasBearTraps), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					// if (flag24) ...

					new CodeInstruction(OpCodes.Ldloc_S, 18),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldstr, "Loading Bear Traps"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// flag24 = MapFeatures.HasBearTraps(flag24);

					new CodeInstruction(OpCodes.Ldloc_S, 18),			// flag24
					new CodeInstruction(OpCodes.Call, HasBearTraps),	// bool
					new CodeInstruction(OpCodes.Stloc_S, 18),			// clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Boulders(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasBoulders = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasBoulders), new[] { typeof(bool) });

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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Bushes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasBushes = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasBushes), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 16),
					new CodeInstruction(OpCodes.Brfalse),
					},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//	flag16 = LevelGenTools.HasBushes(flag21);
					
					new CodeInstruction(OpCodes.Ldloc_S, 16), // flag21
					new CodeInstruction(OpCodes.Call, LevelGenTools_HasBushes), // bool
					new CodeInstruction(OpCodes.Stloc_S, 16), // Clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Cops

		// Cops, Extra

		// Cop Bots

		/// <summary>
		/// TODO: Doesn't work
		/// </summary>
		/// <param name="codeInstructions"></param>
		/// <returns></returns>
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> ExplodingAndSlimeBarrels(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasExplodingAndSlimeBarrels = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasExplodingAndSlimeBarrels), new[] { typeof(bool) });
			MethodInfo contains = AccessTools.Method(typeof(List<string>), nameof(List<string>.Contains), new[] { typeof(string) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					//	if (this.gc.customLevel)
					//		flag15 = this.customLevel.levelFeatures.Contains("ExplodingSlimeBarrel");

					new CodeInstruction(OpCodes.Ldstr, "ExplodingSlimeBarrel"),
					new CodeInstruction(OpCodes.Callvirt, contains),
					new CodeInstruction(OpCodes.Stloc_S, 11),										// clear
					new CodeInstruction(OpCodes.Ldloc_S, 11),										// flag15
					},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//	flag15 = LevelGenTools.HasExplodingAndSlimeBarrels(flag15);

					new CodeInstruction(OpCodes.Call, LevelGenTools_HasExplodingAndSlimeBarrels),	// bool
					new CodeInstruction(OpCodes.Stloc_S, 11),										// Clear
					new CodeInstruction(OpCodes.Ldloc_S, 11),										// flag15
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> FireHydrants(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo LevelGenTools_HasFireHydrants = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasFireHydrants), new[] { typeof(bool) });

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
			MethodInfo LevelGenTools_SetHasFlameGrates = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.SetHasFlameGrates), new[] { typeof(LoadLevel) });

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
			MethodInfo LevelGenTools_HasFlamingBarrels = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasFlamingBarrels), new[] { typeof(bool) });

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
			MethodInfo levelGenTools_SetHasLakes = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.SetHasLakes), new[] { typeof(LoadLevel) });

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

		// Manholes (Done with custom method, not sure vanilla will be needed)

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Mafia(IEnumerable<CodeInstruction> codeInstructions)
        {
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo HasMafia = AccessTools.Method(typeof(Roamers), nameof(Roamers.HasMafiaGangs), new[] { typeof(bool) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 229),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldstr, "HarmAtIntervals")
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 229),
					new CodeInstruction(OpCodes.Call, HasMafia),
					new CodeInstruction(OpCodes.Stloc_S, 229)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		// Musician

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Mines(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasLandMines = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasLandMines), new[] { typeof(bool) });

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
			MethodInfo levelGenTools_HasOilSpills = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasOilSpills), new[] { typeof(bool) });
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

			MethodInfo HasPoliceBoxesAndAlarmButtons = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasPoliceBoxesAndAlarmButtons), new[] { typeof(bool) });

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
			MethodInfo hasPowerBoxes = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasPowerBoxes), new[] { typeof(bool) });

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

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> RoamingGangs(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo HasGangs = AccessTools.Method(typeof(Roamers), nameof(Roamers.HasGangbangerGangs), new[] { typeof(bool) });
			
			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					//	Line 25820

					new CodeInstruction(OpCodes.Ldstr, "Gangbanger"),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Stloc_S, 228),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//	flag34 = Roamers.HasRoamingGangbangers(flag34)

					new CodeInstruction(OpCodes.Ldloc_S, 228),		//	flag34
					new CodeInstruction(OpCodes.Call, HasGangs),	//	bool
					new CodeInstruction(OpCodes.Stloc_S, 228),		//	clear
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		//[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> RoamingGangCount(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo GangCount = AccessTools.Method(typeof(Roamers), nameof(Roamers.PopulationGang), new[] { typeof(int) });

			// This is example code for accessing a field from an Enumerator.
			// It's not complete and I don't understand it "yet"
			// And those are scare quotes, not misused emphatic quotes
			List<SearchMask> masks = new List<SearchMask>
			{
				SearchMask.MatchOpCode(OpCodes.Ldarg_0, false),
				SearchMask.MatchAny(true)
			};
			InstructionSearcher searcher = new InstructionSearcher(masks, 1);
			List<List<CodeInstruction>> result = searcher.DoSearchSafe(instructions, null);
			object operand = result[0][0];
			//

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldc_I4_0),
					new CodeInstruction(OpCodes.Stfld),
					new CodeInstruction(OpCodes.Br),
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldstr, "Gangbanger"),
					new CodeInstruction(OpCodes.Ldstr, "GangbangerB")
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					//new CodeInstruction(OpCodes.Ldfld, BigTries),
					new CodeInstruction(OpCodes.Call, GangCount),
					//new CodeInstruction(OpCodes.Stfld, BigTries),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SlimeBarrels(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo levelGenTools_HasSlimeBarrels = AccessTools.Method(type: typeof(MapFeatures), nameof(MapFeatures.HasSlimeBarrels), new Type[] { });

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
			MethodInfo levelGenTools_HasTrashCans = AccessTools.Method(type: typeof(MapFeatures), nameof(MapFeatures.HasTrashCans), new[] { typeof(bool) });

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
			MethodInfo levelGenTools_HasTrees = AccessTools.Method(type: typeof(MapFeatures), nameof(MapFeatures.HasTrees), new[] { typeof(bool) });

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
			MethodInfo loadLevel_HasVendorCarts = AccessTools.Method(typeof(MapFeatures), nameof(MapFeatures.HasVendorCarts), new[] { typeof(bool) });

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
			MethodInfo levelGenTools_PopulationMultiplier = AccessTools.Method(typeof(Roamers), nameof(Roamers.PopulationMultiplier));

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
			MethodInfo RoamerAgentType = AccessTools.Method(typeof(Roamers), nameof(Roamers.RoamerAgentType), new[] { typeof(string) });

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
