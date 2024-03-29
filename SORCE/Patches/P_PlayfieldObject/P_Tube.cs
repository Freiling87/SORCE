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
	[HarmonyPatch(declaringType: typeof(Tube))]
	class P_Tube
	{
		//private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;
	}

	[HarmonyPatch(typeof(Tube))]
	[HarmonyPatch("Start")]
	static class P_Tube_Start
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Start_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo allowSpawn = AccessTools.PropertyGetter(typeof(P_Tube_Start), nameof(AllowSpawn));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					//	Line 0006
					//	__instance.gc.levelTheme != 1

					new CodeInstruction(OpCodes.Ldarg_0),	//	this
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc.levelTheme
					new CodeInstruction(OpCodes.Ldc_I4_1),	//	this.gc.levelTheme, 1
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// This is a silly way to just bypass the if-block
					new CodeInstruction(OpCodes.Call, allowSpawn),		//	int
					new CodeInstruction(OpCodes.Ldc_I4_1),				//	int, 1
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static int AllowSpawn =>
			GC.levelTheme == 1 || 
			GC.levelTheme == 2 ||
			GC.challenges.Contains(nameof(MeatsOfRogue))
				? 1 
				: GC.levelTheme;
	}
}
