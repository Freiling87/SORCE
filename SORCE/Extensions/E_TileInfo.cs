using BepInEx.Logging;
using SORCE.Logging;
using System;
using UnityEngine;

namespace SORCE.Extensions
{
    internal class E_TileInfo
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool HasWall(TileData tile) =>
			tile.wallMaterial != wallMaterialType.None ||
			GC.tileInfo.IsOverlapping(new Vector2(tile.posX, tile.posY), "Wall");

		public static Vector2 EastOf(Vector2 origin, float distance = 0.64f) =>
			new Vector2(origin.x + distance, origin.y);
		public static Vector2 NorthOf(Vector2 origin, float distance = 0.64f) =>
            new Vector2(origin.x, origin.y + distance);
		public static Vector2 SouthOf(Vector2 origin, float distance = 0.64f) =>
			new Vector2(origin.x, origin.y - distance);
		public static Vector2 WestOf(Vector2 origin, float distance = 0.64f) =>
            new Vector2(origin.x - distance, origin.y);

		public static bool IsWallDecorationPlaceable(Vector2 cell, string direction)
        {
			switch (direction)
            {
				case "N": return 
					HasWall(GC.tileInfo.GetTileData(NorthOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(SouthOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(SouthOf(EastOf(cell)))) &&
					!HasWall(GC.tileInfo.GetTileData(SouthOf(WestOf(cell))));
				case "E": return 
					HasWall(GC.tileInfo.GetTileData(EastOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(WestOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(WestOf(NorthOf(cell)))) &&
					!HasWall(GC.tileInfo.GetTileData(WestOf(SouthOf(cell))));
				case "S": return 
					HasWall(GC.tileInfo.GetTileData(SouthOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(NorthOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(NorthOf(EastOf(cell)))) &&
					!HasWall(GC.tileInfo.GetTileData(NorthOf(WestOf(cell))));
				case "W": return 
					HasWall(GC.tileInfo.GetTileData(WestOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(EastOf(cell))) &&
					!HasWall(GC.tileInfo.GetTileData(EastOf(NorthOf(cell)))) &&
					!HasWall(GC.tileInfo.GetTileData(EastOf(SouthOf(cell))));
			}

			return false;
        }
	}
}
