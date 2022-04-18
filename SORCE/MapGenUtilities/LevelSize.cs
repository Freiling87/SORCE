using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Challenges;
using SORCE.Challenges.C_MapSize;
using SORCE.Localization;
using SORCE.Logging;
using System.Linq;

namespace SORCE.MapGenUtilities
{
    internal class LevelSize
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static int ChunkCount =>
			RogueFramework.Unlocks.OfType<MapSizeChallenge>().FirstOrDefault(c => c.IsEnabled)?.ChunkCount
			?? 30;

		/// <summary>
		/// Technically redundant to LoadLevel.levelSizeModifier, but accurate earlier.
		/// </summary>
		public static float ChunkCountRatio => 
			ChunkCount / 30f;

		public static void SetChunkCount(LoadLevel loadLevel) =>
			GC.loadLevel.levelSizeMax = ChunkCount;
	}
}
