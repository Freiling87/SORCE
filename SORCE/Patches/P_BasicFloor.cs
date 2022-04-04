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
			logger.LogDebug("Spawn_Prefix: " + ChallengeManager.IsChallengeFromListActive(CChallenge.BuildingsNames));

			if (ChallengeManager.IsChallengeFromListActive(CChallenge.BuildingsNames))
			{
				BuildingsChallenge mutator = RogueFramework.Unlocks.OfType<BuildingsChallenge>().FirstOrDefault(m => m.IsEnabled);
				logger.LogDebug(mutator is null);

				if (VFloor.Natural.Contains(floorName))
				{
					if (!(mutator.NaturalFloorType is null))
						floorName = mutator.NaturalFloorType;
                }
				else if (VFloor.Constructed.Contains(floorName))
				{
					if (!(mutator.ConstructedFloorType is null))
						floorName = mutator.ConstructedFloorType;
				}
				else if (VFloor.Raised.Contains(floorName))
				{
					if (!(mutator.RaisedFloorType is null))
						floorName = mutator.RaisedFloorType;
				}
				else if (VFloor.Rugs.Contains(floorName))
				{
					if (!(mutator.RugFloorType is null))
						floorName = mutator.RugFloorType;
				}
				else if (VFloor.UnraisedTileTiles.Contains(floorName))
                {
					if (!(mutator.UnraisedTileTilesType is null))
						floorName= mutator.UnraisedTileTilesType;
                }
			}

			return true;
		}
	}
}
