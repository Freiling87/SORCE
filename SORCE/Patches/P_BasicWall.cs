using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_VFX;
using SORCE.Logging;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(BasicWall))]
	public static class P_BasicWall
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Walls
		/// </summary>
		/// <param name="spawner"></param>
		/// <param name="wallName"></param>
		/// <param name="myPos"></param>
		/// <param name="myScale"></param>
		/// <param name="startingChunkReal"></param>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(BasicWall.Spawn), argumentTypes: new[] { typeof(SpawnerBasic), typeof(string), typeof(Vector2), typeof(Vector2), typeof(Chunk) })]
		public static bool Spawn_Prefix(SpawnerBasic spawner, ref string wallName, Vector2 myPos, Vector2 myScale, Chunk startingChunkReal)
		{
			if (wallName == VWall.Border)
				wallName = Utilities.MapGen.Structures.BorderWallType();
			else if (VWall.Fence.Contains(wallName))
				wallName = Utilities.MapGen.Structures.FenceWallType(wallName);
			else if (VWall.Structural.Contains(wallName))
				wallName = Utilities.MapGen.Structures.BuildingWallType(wallName);

			return true;
		}

		/// <summary>
		/// FloralerFlora Hedge Wall leaves spawn
		/// </summary>
		/// <param name="spawner"></param>
		/// <param name="wallName"></param>
		/// <param name="myPos"></param>
		/// <param name="myScale"></param>
		/// <param name="startingChunkReal"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(BasicWall.Spawn), argumentTypes: new[] { typeof(SpawnerBasic), typeof(string), typeof(Vector2), typeof(Vector2), typeof(Chunk) })]
		public static void Spawn_Postfix(SpawnerBasic spawner, string wallName, Vector2 myPos, Vector2 myScale, Chunk startingChunkReal)
		{
			if (wallName == "Hedge" && 
				GC.challenges.Contains(nameof(FloralerFlora)))
			{
				int chance = 100;

				while (GC.percentChance(chance))
				{
					GC.spawnerMain.SpawnWreckagePileObject(
						new Vector2(
							myPos.x + Random.Range(-0.48f, 0.48f), 
							myPos.y + Random.Range(-0.48f, 0.48f)),
						VObject.Bush, false);
					chance -= 20;
				}
			}
		}

	}
}
