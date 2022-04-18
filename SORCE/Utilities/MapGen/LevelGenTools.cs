using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Challenges;
using SORCE.Challenges.C_Audio;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Features;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Challenges.C_Gangs;
using SORCE.Challenges.C_Wreckage;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Traits;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.MapGenUtilities
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
			int num;
			Collider2D[] tileInfo_hitsAlloc = new Collider2D[100];

			if (circleRadius == 0f)
				num = Physics2D.OverlapPointNonAlloc(pos, tileInfo_hitsAlloc);
			else
				num = Physics2D.OverlapCircleNonAlloc(pos, circleRadius, tileInfo_hitsAlloc);

			for (int i = 0; i < num; i++)
			{
				Collider2D collider2D = tileInfo_hitsAlloc[i];

				if (collider2D.CompareTag("Wall"))
					return true;
			}

			return false;
		}
		internal static Vector2 RandomSpawnLocation(TileInfo tileInfo, float maxDistanceToWall, bool publicOnly = false, bool privateOnly = false)
		{
			float minInclusive = 1.28f;
			float maxInclusive = GC.loadLevel.levelSizePixels - 1.28f;
			float minInclusive2 = 1.28f;
			float maxInclusive2 = GC.loadLevel.levelSizePixels - 1.28f;

			for (int i = 0; i < 200; i++)
			{
				Vector2 vector = new Vector2(
					(Random.Range(minInclusive, maxInclusive)),
					(Random.Range(minInclusive2, maxInclusive2)));

				TileData tileData = tileInfo.GetTileData(vector);
				bool isPublic = tileData.owner == 0;

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
 