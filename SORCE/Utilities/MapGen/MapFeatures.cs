using BepInEx.Logging;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Features;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Gangs;
using SORCE.Challenges.C_Wreckage;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SORCE.Localization.NameLists;

namespace SORCE.MapGenUtilities
{
    internal class MapFeatures
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		#region Custom
		public static bool HasBrokenWindows =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			(GC.challenges.Contains(VChallenge.MixedUpLevels) && GC.percentChance(33)) ||
			(GC.customLevel && GC.loadLevel.customLevel.levelFeatures.Contains(CLevelFeature.BrokenWindows)) ||
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(BadNeighborhoods));
		public static bool HasBushCannibals =>
			!GC.challenges.Contains(nameof(Arcology));
		public static bool HasWallOutcroppings =>
			GC.challenges.Contains(nameof(DUMP)) ||
			GC.challenges.Contains(nameof(SpelunkyDory));
		public static bool HasCoziness =>
			GC.challenges.Contains(nameof(DepartmentOfPublicComfiness)) ||
			Core.debugMode;
		public static bool HasFountains =>
			GC.challenges.Contains(nameof(BroughtbackFountain)) ||
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState));
		public static bool HasKillerPlants =>
			GC.challenges.Contains(nameof(Arcology)) ||
			GC.challenges.Contains(nameof(VerdantVistas));
		public static bool HasManholes_Underdank =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			TraitManager.IsPlayerTraitActive<UnderdankCitizen>();
		public static bool HasLakesColored =>
			false;
		public static bool HasLakesPolluted =>
			!GC.challenges.Contains(nameof(Arcology)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			GC.challenges.Contains(nameof(ThePollutionSolution)) ||
			Core.debugMode;
		public static bool HasRugs =>
			GC.challenges.Contains(nameof(GrandCityHotel)) ||
			GC.challenges.Contains(nameof(DepartmentOfPublicComfiness)) ||
			Core.debugMode;
		public static bool HasScreens =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(Technocracy));
		public static bool HasSecurityCamsAndTurrets =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(PoliceState)) ||
			GC.challenges.Contains(nameof(SurveillanceSociety));
		public static bool HasTurntablesAndSpeakers =>
			GC.challenges.Contains(nameof(DiscoCityDanceoff));

		public static string LakeColor()
        {
			return "Yellow";
			// TODO
        }
		#endregion
		#region Vanilla
		public static bool HasAssassins(bool vanilla) => // New
			vanilla;
		public static bool HasBarbecues(bool vanilla) =>
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(Arcology)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasBearTraps(bool vanilla) =>
			GC.challenges.Contains(nameof(WelcomeMats)) ||
			vanilla;
		public static bool HasBoulders(bool vanilla) =>
			GC.challenges.Contains(nameof(Arcology)) ||
			GC.challenges.Contains(nameof(DUMP)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasBushes(bool vanilla) =>
			GC.challenges.Contains(nameof(Arcology)) ||
			GC.challenges.Contains(nameof(VerdantVistas)) ||
			vanilla;
		public static bool HasCopBots(bool vanilla) =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(PoliceState)) ||
			GC.challenges.Contains(nameof(Technocracy)) ||
			vanilla;
		public static bool HasCops(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(Technocracy)) &&
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState)) ||
			vanilla;
		public static bool HasCopsExtra(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(Technocracy)) &&
			GC.challenges.Contains(nameof(PoliceState)) ||
			vanilla;
		public static bool HasExplodingAndSlimeBarrels(bool vanilla) =>
			!GC.challenges.Contains(nameof(Arcology)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(ThePollutionSolution)) ||
			vanilla;
		public static bool HasExplodingBarrels() =>
			Core.debugMode;
		public static bool HasFireHydrants(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) ||
			vanilla;
		public static bool HasFlamingBarrels(bool vanilla) =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			vanilla;
		public static bool HasLamps(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(MACITS)) ||
			vanilla;
		public static bool HasLandMines(bool vanilla) =>
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(ThisLandIsMineLand)) ||
			vanilla;
		public static bool HasOilSpills(bool vanilla) =>
			!GC.challenges.Contains(nameof(Arcology)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) && 
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(ThePollutionSolution)) ||
			vanilla;
		public static bool HasPoliceBoxesAndAlarmButtons(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState)) ||
			vanilla;
		public static bool HasPowerBoxes(bool vanilla) =>
			GC.challenges.Contains(nameof(PowerWhelming)) ||
			GC.challenges.Contains(nameof(Technocracy)) ||
			vanilla;
		public static bool HasSlimeBarrels() =>
			!GC.challenges.Contains(nameof(Arcology)) &&
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(ThePollutionSolution));
		public static bool HasTrashCans(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(Arcology)) ||
			GC.challenges.Contains(nameof(MACITS)) ||
			GC.challenges.Contains(nameof(PoliceState)) ||
			Core.debugMode ||
			vanilla; 
		public static bool HasTrees(bool vanilla) =>
			!GC.challenges.Contains(nameof(AnCapistan)) &&
			GC.challenges.Contains(nameof(Arcology)) ||
			GC.challenges.Contains(nameof(VerdantVistas)) ||
			Core.debugMode ||
            vanilla;
        public static bool HasVendorCarts(bool vanilla) =>
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(CartOfTheDeal)) ||
			GC.challenges.Contains(nameof(MACITS)) ||
			vanilla;

		public static void SetHasLakes(LoadLevel loadLevel) =>
			loadLevel.hasLakes = 
				GC.levelTheme != 3 && // TODO: Allow spawn, but check for Bridges or skin canal chunks entirely
				loadLevel.hasLakes ||
				GC.challenges.Contains(nameof(Arcology)) ||
				GC.challenges.Contains(nameof(LakeItOrLeaveIt));
		public static void SetHasFlameGrates(LoadLevel loadLevel) =>
			loadLevel.hasFlameGrates = loadLevel.hasFlameGrates ||
				!GC.challenges.Contains(nameof(LowTechLowLife)) &&
				GC.challenges.Contains(nameof(Technocracy));
		public static void SetHasLockdownWalls(LoadLevel loadLevel) =>
			loadLevel.hasTracks = loadLevel.hasTracks ||
				Core.debugMode;
		public static void SetHasTracks(LoadLevel loadLevel) =>
			loadLevel.hasTracks = loadLevel.hasTracks ||
				Core.debugMode;
        #endregion
    }
}
 