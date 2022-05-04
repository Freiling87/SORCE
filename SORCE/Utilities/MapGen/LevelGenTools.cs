using BepInEx.Logging;
using SORCE.Logging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SORCE.Utilities.MapGen
{
    class LevelGenTools
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static float BougienessFactor =>
			GC.levelTheme / 5f;
		public static float SlumminessFactor =>
			1f - (GC.levelTheme / 5f);

		private static bool IsCloseToWall(Vector2 pos, float circleRadius)
		{
			Collider2D[] tileInfo_hitsAlloc = new Collider2D[100];
			int num = Physics2D.OverlapCircleNonAlloc(pos, circleRadius, tileInfo_hitsAlloc);

			for (int i = 0; i < num; i++)
				if (tileInfo_hitsAlloc[i].CompareTag("Wall"))
					return true;

			return false;
		}
		internal static Vector2 RandomSpawnLocation(TileInfo tileInfo, float maxDistanceToWall)
		{
			float minInclusive = 1.28f;
			float maxInclusive = GC.loadLevel.levelSizePixels - minInclusive;

			for (int i = 0; i < 200; i++)
			{
				Vector2 vector = new Vector2(
					(Random.Range(minInclusive, maxInclusive)),
					(Random.Range(minInclusive, maxInclusive)));

				TileData tileData = tileInfo.GetTileData(vector);
				bool isPublic = tileData.owner == 0; // This IS buggy

				if (!(
						tileData.owner > 0 ||
						tileData.prison > 0 ||
						tileData.hole ||
						tileData.water ||
						tileData.ice ||
						tileData.conveyorBelt ||
						tileData.dangerousToWalk ||
						//tileData.solidObject ||
						tileData.wallColliderUnreachable ||
						!IsCloseToWall(vector, maxDistanceToWall)))
					return vector;
			}

			return Vector2.zero;
		}
		public static bool IsPublic(Vector3 spot) =>
			GC.tileInfo.GetTileData(spot).owner == 0;
	}
}
 