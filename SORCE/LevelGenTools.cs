using BepInEx.Logging;
using SORCE.Challenges;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE
{
	class LevelGenTools
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public static string GetFloorTile()
		{
			string curMutator = "";

			foreach (string mutator in cChallenge.FloorMutators)
				if (GC.challenges.Contains(mutator))
					curMutator = mutator;

			switch (curMutator)
			{
				case cChallenge.ArcologyEcology:
					return vFloor.Grass;
				default:
					return null;
			}
		}

		public static string GetFloorTileGroup()
		{
			string curMutator = "";

			foreach (string mutator in cChallenge.FloorMutators)
				if (GC.challenges.Contains(mutator))
					curMutator = mutator;

			switch (curMutator)
			{
				case cChallenge.ArcologyEcology:
					return vFloorTileGroup.Park;
				default:
					return vFloorTileGroup.Building;
			}
		}

		public static string GetWallMutator()
		{
			foreach (string mutator in GC.challenges)
				if (cChallenge.WallMutators.Contains(mutator))
					return mutator;

			return null;
		}

		public static wallMaterialType GetBorderWallMaterialFromMutator()
		{
			logger.LogDebug("GetWallBorderTypeFromMutator: '" + GetWallMutator() + "'");

			switch (GetWallMutator())
			{
				case cChallenge.CityOfSteel:
					return wallMaterialType.Steel;

				case cChallenge.GreenLiving:
					return wallMaterialType.Wood;

				case cChallenge.Panoptikopolis:
					return wallMaterialType.Glass;

				case cChallenge.ShantyTown:
					return wallMaterialType.Wood;

				case cChallenge.SpelunkyDory:
					return wallMaterialType.Border;

				default:
					return wallMaterialType.Border;
			}
		}

		public static string GetActiveFloorMod()
		{
			foreach (string mutator in cChallenge.WallMutators)
				if (GC.challenges.Contains(mutator))
					return mutator;

			return null;
		}

		public static bool IsWallModActive()
		{
			foreach (string mutator in cChallenge.WallMutators)
				if (GC.challenges.Contains(mutator))
					return true;

			return false;
		}

		public static int LevelSizeModifier(int vanilla)
		{
			if (GC.challenges.Contains(cChallenge.ACityForAnts))
				vanilla = 4;
			else if (GC.challenges.Contains(cChallenge.Claustropolis))
				vanilla = 12;
			else if (GC.challenges.Contains(cChallenge.Megalopolis))
				vanilla = 48;
			else if (GC.challenges.Contains(cChallenge.Ultrapolis))
				vanilla = 64;

			return vanilla;
		}

		public static int LevelSizeRatio()
		{
			return LevelSizeModifier(30) / 30;
		}

		public static string GetWallTypeFromMutator()
		{
			logger.LogDebug("GetWallTypeFromMutator: '" + GetWallMutator() + "'");

			switch (GetWallMutator())
			{
				case cChallenge.CityOfSteel:
					return vWall.Steel;

				case cChallenge.GreenLiving:
					return vWall.Hedge;

				case cChallenge.Panoptikopolis:
					return vWall.Glass;

				case cChallenge.ShantyTown:
					return vWall.Wood;

				case cChallenge.SpelunkyDory:
					return vWall.Cave;
			}

			return null;
		}
	}
}
