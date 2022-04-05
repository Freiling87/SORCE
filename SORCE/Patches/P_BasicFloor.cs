using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using RogueLibsCore;
using Random = UnityEngine.Random;
using System.Collections;
using System.Reflection;
using System;
using SORCE.Logging;
using SORCE.Challenges;
using SORCE.Localization;
using SORCE.Challenges.C_Buildings;
using static SORCE.Localization.NameLists;
using System.Linq;

namespace SORCE.Patches
{
	[HarmonyPatch(declaringType: typeof(BasicFloor))]
	public static class P_BasicFloor
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		///	Building mutators, interior floors
		/// </summary>
		/// <param name="spawner"></param>
		/// <param name="floorName"></param>
		/// <param name="myPos"></param>
		/// <param name="myScale"></param>
		/// <param name="startingChunkReal"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(nameof(BasicFloor.Spawn), new[] { typeof(SpawnerBasic), typeof(string), typeof(Vector2), typeof(Vector2), typeof(Chunk) })]
		public static bool Spawn_Prefix(SpawnerBasic spawner, ref string floorName, Vector2 myPos, Vector2 myScale, Chunk startingChunkReal)
		{
			BuildingsChallenge mutator = (RogueFramework.Unlocks.OfType<BuildingsChallenge>().FirstOrDefault(m => m.IsEnabled));

			if (!(mutator is null))
			{
				if (VFloor.Natural.Contains(floorName))
				{
					if (!(mutator.FloorNatural is null))
						floorName = mutator.FloorNatural;
				}
				else if (VFloor.Constructed.Contains(floorName))
				{
					if (!(mutator.FloorConstructed is null))
						floorName = mutator.FloorConstructed;
				}
				else if (VFloor.Raised.Contains(floorName))
				{
					if (!(mutator.FloorRaised is null))
						floorName = mutator.FloorRaised;
				}
				else if (VFloor.Rugs.Contains(floorName))
				{
					if (!(mutator.FloorRug is null))
						floorName = mutator.FloorRug;
				}
				else if (VFloor.UnraisedTileTiles.Contains(floorName))
				{
					if (!(mutator.FloorUnraisedTile is null))
						floorName = mutator.FloorUnraisedTile;
				}
			}

			return true;
		}
	}
}
