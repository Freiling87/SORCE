using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Challenges.C_Gangs;
using SORCE.Challenges.C_MapSize;
using SORCE.Logging;
using System.Linq;
using UnityEngine;

namespace SORCE.Utilities.MapGen
{
    public class LevelSize
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;


		public static int ChunkCountBase =>
			RogueFramework.Unlocks.OfType<MapSizeChallenge>().FirstOrDefault(c => c.IsEnabled)?.ChunkCount
					?? 30;

		public static int ChunkCountExtra =>
			(GC.challenges.Contains(nameof(ProtectAndServo)) ? 2 : 0) +
			(GC.levelTheme == 3 ? 20 : 0); // For Canals

		public static int ChunkCountTotal =>
			Mathf.Clamp(ChunkCountBase + ChunkCountExtra, 4, 64);

		/// <summary>
		/// Technically redundant to LoadLevel.levelSizeModifier, but accurate earlier.
		/// </summary>
		public static float ChunkCountRatio => 
			ChunkCountTotal / 30f;

		public static void SetChunkCount(LoadLevel loadLevel) =>
			GC.loadLevel.levelSizeMax = ChunkCountTotal;
	}
}
