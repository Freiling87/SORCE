using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using SORCE.Challenges.C_Features;
using SORCE.Challenges.C_Lighting;
using SORCE.Challenges.C_Overhaul;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(FlameGrate))]
    class P_FlameGrate
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(FlameGrate.SetVars), argumentTypes: new Type[0] { })]
		public static void SetVars_Postfix(FlameGrate __instance)
		{
			if (GC.challenges.Contains(nameof(ObjectsRelit)))
				__instance.noLight = false;
		}
	}

	[HarmonyPatch(typeof(FlameGrate))]
	[HarmonyPatch("Start")]
	static class P_FlameGrate_Start
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
        private static IEnumerable<CodeInstruction> Start_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo allowSpawn = AccessTools.PropertyGetter(typeof(P_FlameGrate_Start), nameof(AllowSpawn));

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
					new CodeInstruction(OpCodes.Call, allowSpawn),		//	int
					new CodeInstruction(OpCodes.Ldc_I4_1),				//	int, 1
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static int AllowSpawn =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.levelTheme == 1 ||
			GC.loadLevel.hasFlameGrates ||
			GC.challenges.Contains(nameof(TrapsUnlimited))
				? 1
				: 0;
	}
}
