using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_Lighting;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(ObjectReal))]
	class P_ObjectReal
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPatch(methodName: nameof(ObjectReal.noLight), MethodType.Getter)]
		public static bool noLight_Prefix(ref bool ___result)
		{
			if (GC.challenges.Contains(nameof(NoObjectLights)))
			{
				___result = true;
				return false;
			}

			return true;
		}
	}
}
