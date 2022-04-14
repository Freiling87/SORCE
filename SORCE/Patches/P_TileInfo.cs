using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(TileInfo))]
    public static class P_TileInfo
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction>WaterNearby_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldc_I4_2),
					new CodeInstruction(OpCodes.Blt)
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
        }
    }
}
