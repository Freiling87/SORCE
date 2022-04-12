using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches.P_PlayfieldObject
{
	[HarmonyPatch(declaringType: typeof(SwitchFloor))]
	class P_SwitchFloor
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: "FindPos", new Type[] { })]
		private static IEnumerable<CodeInstruction> FindPos(IEnumerable<CodeInstruction> instructionsEnumerable, ILGenerator generator)
		{
			List<CodeInstruction> instructions = instructionsEnumerable.ToList();
			MethodInfo trapType = AccessTools.Method(typeof(P_SwitchFloor), nameof(P_SwitchFloor.TrapType), new[] { typeof(SwitchFloor) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Brfalse),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldc_I4_S, 50)
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, trapType),
					new CodeInstruction(OpCodes.Stloc, 1),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// A copy of the original portion
		/// </summary>
		/// <param name="switchFloor"></param>
		/// <returns></returns>
		public static string TrapType(SwitchFloor switchFloor)
        {
			if (switchFloor.moveNS)
			{
				if (switchFloor.stepCountN + switchFloor.stepCountS <= 4)
					return GC.Choose("Crusher", "DartTrap", "TrapDoor");
				else
					return GC.Choose("Crusher", "SawBlade");
			}
			else if (switchFloor.stepCountE + switchFloor.stepCountW <= 4)
				return GC.Choose("Crusher", "DartTrap", "TrapDoor");
			else
				return GC.Choose("Crusher", "SawBlade");
		}
	}
}
