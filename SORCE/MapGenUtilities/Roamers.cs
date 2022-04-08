using BepInEx.Logging;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Challenges.C_Roamers;
using SORCE.Logging;
using static SORCE.Localization.NameLists;

namespace SORCE.MapGenUtilities
{
    internal class Roamers
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool HasRoamingGangbangers(bool vanilla) =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(YoungMenInTheNeighborhood)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasRoamingMafia(bool vanilla) =>
			!GC.challenges.Contains(nameof(PoliceState)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(UnionTown)) ||
			vanilla;

		public static int PopulationGang(int vanilla) =>
			GC.challenges.Contains(nameof(HoodlumsWonderland)) ? 12 :
			vanilla;
		public static int PopulationMultiplier() =>
			GC.challenges.Contains(nameof(GhostTown)) ? 0 :
			GC.challenges.Contains(nameof(HordeAlmighty)) ? 2 :
			GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 4 :
			GC.challenges.Contains(nameof(SwarmWelcome)) ? 8 :
			1;
		public static int PopulationMafia(int vanilla) =>
			vanilla;

		public static string RoamerAgentType(string agentType)
		{
			// TODO: Adjustments for overhauls, etc.

			if (agentType == VAgent.Thief)
			{
				int thiefReduction =
					GC.challenges.Contains(nameof(HordeAlmighty)) ? 50 :
					GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 75 :
					GC.challenges.Contains(nameof(SwarmWelcome)) ? 87 :
					0;

				if (thiefReduction != 0 && GC.percentChance(thiefReduction))
					agentType = GC.levelTheme == 4 || GC.levelTheme == 5
						? VAgent.UpperCruster
						: VAgent.SlumDweller;
			}

			return agentType;
		}
	}
}
