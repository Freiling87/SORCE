using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using SORCE.Challenges.C_Gangs;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SORCE.Patches.P_MapGen
{
    [HarmonyPatch(declaringType: typeof(LoadLevel))]
	static class P_LoadLevel_FillMapChunks2
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "FillMapChunks2", new Type[] { }));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> EnableCopBotCenters(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo HasCopBotCenters = AccessTools.PropertyGetter(typeof(P_LoadLevel_FillMapChunks2), nameof(P_LoadLevel_FillMapChunks2.HasCopBotCenters));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
                {
					new CodeInstruction(OpCodes.Stloc_S, 51)
                },
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),				//	This, for IEnumerators
					new CodeInstruction(OpCodes.Ldfld),					//	GC
					new CodeInstruction(OpCodes.Ldfld),					//	GC.levelTheme
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, HasCopBotCenters)	//	int
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}


		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> ChunkPlaced(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo HasCopBotCenters = AccessTools.PropertyGetter(typeof(P_LoadLevel_FillMapChunks2), nameof(P_LoadLevel_FillMapChunks2.HasCopBotCenters));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_R4),
					new CodeInstruction(OpCodes.Sub),
					new CodeInstruction(OpCodes.Stfld),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),				//	this
					new CodeInstruction(OpCodes.Ldfld),					//	this.GC
					new CodeInstruction(OpCodes.Ldfld),					//	this.GC.levelTheme
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, HasCopBotCenters)	//	int
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static int HasCopBotCenters =>
			GC.levelTheme == 4
			|| GC.challenges.Contains(nameof(ProtectAndServo))
				? 4
				: 0;
	}
}
