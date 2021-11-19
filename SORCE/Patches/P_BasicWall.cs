using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using RogueLibsCore;
using Random = UnityEngine.Random;
using System.Collections;
using System.Reflection;
using System;
using SORCE;
using SORCE.Challenges;
using SORCE.Logging;

namespace BunnyMod.Content.Patches
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
		public static void Spawn_Prefix(SpawnerBasic spawner, ref string wallName, Vector2 myPos, Vector2 myScale, Chunk startingChunkReal)
		{
			if (LevelGenTools.IsWallModActive())
				wallName = LevelGenTools.GetWallTypeFromMutator();
		}
	}
}
