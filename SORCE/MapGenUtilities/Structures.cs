using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Challenges;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Overhaul;
using SORCE.Localization;
using SORCE.Logging;
using System.Linq;
using static SORCE.Localization.NameLists;

namespace SORCE.MapGenUtilities
{
    internal class Structures
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static wallMaterialType BorderWallMaterial()
		{
			// TODO: When overhauls are scoped, model this after P_BasicFloor.Spawn_Prefix

			switch (ChallengeManager.GetActiveChallengeFromList(CChallenge.Overhauls))
			{
				case (nameof(Arcology)):
					return wallMaterialType.Wood;
				case (nameof(CanalCity)):
					return wallMaterialType.Normal;
				case (nameof(DiscoCityDanceoff)):
					return (wallMaterialType.Steel);
				//case (nameof(DUMP)):
				//	return wallMaterialType.Cave;
				// Not in enum
				case (nameof(Eisburg)):
					return wallMaterialType.Glass;
				case (nameof(GrandCityHotel)):
					return wallMaterialType.Wood;
				case (nameof(LowTechLowLife)):
					return wallMaterialType.Normal;
				case (nameof(Technocracy)):
					return wallMaterialType.Steel;
				case (nameof(TestTubeCity)):
					return wallMaterialType.Glass;
				case (nameof(Tindertown)):
					return wallMaterialType.Wood;
				default:
					return wallMaterialType.Border;
			}
		}
		public static string BorderWallType()
		{
			// TODO: When overhauls are scoped, model this after P_BasicFloor.Spawn_Prefix

			switch (ChallengeManager.GetActiveChallengeFromList(CChallenge.Overhauls))
			{
				case (nameof(Arcology)):
					return VWall.Hedge; // TODO: Could do this one Hedge and the solid blocks of Borderwall as Wood
				case (nameof(CanalCity)):
					return VWall.Brick;
				case (nameof(DiscoCityDanceoff)):
					return (VWall.Steel);
				case (nameof(DUMP)):
					return VWall.Cave;
				case (nameof(Eisburg)):
					return VWall.Glass;
				case (nameof(GrandCityHotel)):
					return VWall.Wood;
				case (nameof(LowTechLowLife)):
					return VWall.Brick;
				case (nameof(Technocracy)):
					return VWall.Steel;
				case (nameof(TestTubeCity)):
					return VWall.Glass;
				case (nameof(Tindertown)):
					return VWall.Wood;
				default:
					return VWall.Border;
			}
		}
		public static string RandomRugType()
		{
			var random = new System.Random();
			int index = random.Next(VFloor.Rugs.Count);

			return VFloor.Rugs[index];
		}
		public static string PublicFloorTile()
		{
			// TODO: When overhauls are scoped, model this after P_BasicFloor.Spawn_Prefix

			switch (ChallengeManager.GetActiveChallengeFromList(CChallenge.Overhauls))
			{
				case nameof(Arcology):
					return VFloor.Grass;
				case nameof(CanalCity):
					return VFloor.Water;
				case nameof(DUMP):
					return VFloor.CaveFloor;
				case nameof(GrandCityHotel):
					return RandomRugType();
				case nameof(TestTubeCity):
					return VFloor.CleanTiles;
				case nameof(Eisburg):
					return VFloor.IceRink;
				default:
					return null;
			}
		}
		public static string PublicFloorTileGroup()
		{
			// TODO: When overhauls are scoped, model this after P_BasicFloor.Spawn_Prefix

			switch (ChallengeManager.GetActiveChallengeFromList(CChallenge.Overhauls))
			{
				case nameof(Arcology):
					return VFloorTileGroup.Park;
				case nameof(CanalCity):
					return VFloorTileGroup.Water;
				case nameof(DUMP):
					return VFloorTileGroup.Industrial;
				case nameof(GrandCityHotel):
					return VFloorTileGroup.Rug;
				case nameof(TestTubeCity):
					return VFloorTileGroup.Building;
				case nameof(Eisburg):
					return VFloorTileGroup.Ice;
				default:
					return VFloorTileGroup.Building;
			}
		}
		public static string FenceWallType(string vanilla)
		{
			BuildingsChallenge mutator = (BuildingsChallenge)RogueFramework.Unlocks.OfType<MutatorUnlock>().FirstOrDefault(m => m is BuildingsChallenge && m.IsEnabled);

			if (!(mutator is null) && !(mutator.WallFence is null))
				return mutator.WallFence;

			return vanilla;
		}
		public static string BuildingWallType(string vanilla)
		{

			BuildingsChallenge mutator = (BuildingsChallenge)RogueFramework.Unlocks.OfType<MutatorUnlock>().FirstOrDefault(m => m is BuildingsChallenge && m.IsEnabled);

			if (!(mutator is null) && !(mutator.WallStructural is null))
				return mutator.WallStructural;

			return vanilla;
		}
	}
}
