﻿using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using SORCE.Challenges.C_Features;
using SORCE.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(SawBlade))]
	class P_SawBlade
	{
		//private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;
	}

    [HarmonyPatch(typeof(SawBlade))]
    [HarmonyPatch("Start")]
    static class P_SawBlade_Start
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Start_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo allowSpawn = AccessTools.PropertyGetter(typeof(P_SawBlade_Start), nameof(AllowSpawn));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					//	Line 0010
					//	__instance.gc.levelTheme != 1

					new CodeInstruction(OpCodes.Ldarg_0),	//	this
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc.levelTheme
					new CodeInstruction(OpCodes.Ldc_I4_1),	//	this.gc.levelTheme, 1
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, allowSpawn),		//	int
					new CodeInstruction(OpCodes.Ldc_I4_1),				//	int, 1
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static int AllowSpawn =>
			GC.levelTheme == 1 ||
			GC.challenges.Contains(nameof(TrapsUnlimited))
				? 1
				: 0;
	}
}
