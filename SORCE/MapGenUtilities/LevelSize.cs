using BepInEx.Logging;
using SORCE.Challenges;
using SORCE.Challenges.C_MapSize;
using SORCE.Localization;
using SORCE.Logging;

namespace SORCE.MapGenUtilities
{
    internal class LevelSize
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static int LevelSizeModifier =>
			GC.challenges.Contains(nameof(Arthropolis)) ? 4 :
			GC.challenges.Contains(nameof(Claustropolis)) ? 12 :
			GC.challenges.Contains(nameof(Megapolis)) ? 48 :
			GC.challenges.Contains(nameof(Ultrapolis)) ? 64 :
			30;
		public static float LevelSizeRatio() =>
			LevelSizeModifier / 30;
		public static void SetLevelSizeMax(LoadLevel loadLevel)
		{
			int newVal = loadLevel.levelSizeMax;

			string active = ChallengeManager.GetActiveChallengeFromList(CChallenge.MapSize);

			if (active == nameof(Arthropolis))
				newVal = 4;
			else if (active == nameof(Claustropolis))
				newVal = 12;
			else if (active == nameof(Megapolis))
				newVal = 48;
			else if (active == nameof(Ultrapolis))
				newVal = 64;

			loadLevel.levelSizeMax = newVal;
		}
	}
}
