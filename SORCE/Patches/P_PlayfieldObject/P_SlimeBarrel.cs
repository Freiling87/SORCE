using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(SlimeBarrel))]
	class P_SlimeBarrel
	{
		//private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

        public static object CCULogger { get; private set; }
	}

	[HarmonyPatch(typeof(SlimeBarrel))]
	[HarmonyPatch("Start")]
	static class P_SlimeBarrel_Start
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Start_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

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
					new CodeInstruction(OpCodes.Ldc_I4_1),	//	1
					new CodeInstruction(OpCodes.Ldc_I4_1),	//	1, 1
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}
