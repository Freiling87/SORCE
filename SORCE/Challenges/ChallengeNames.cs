using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges
{
	public static class cChallenge

	{
		public const string
				Buildings_Hide = "Buildings_Hide",
				Buildings_Show = "Buildings_Show",
				CityOfSteel = "CityOfSteel",
				GreenLiving = "GreenLiving",
				Panoptikopolis = "Panoptikopolis",
				ShantyTown = "ShantyTown",
				SpelunkyDory = "SpelunkyDory",

				Exteriors_Hide = "FloorExteriors_Hide",
				Exteriors_Show = "FloorExteriors_Show",
				ArcologyEcology = "ArcologyEcology",

				Features_Hide = "Features_Hide",
				Features_Show = "Features_Show",
				BadNeighborhoods = "BadNeighborhoods",
				BroughtBackFountain = "BroughtBackFountain",
				CartOfTheDeal = "CartOfTheDeal",
				LakeItOrLeaveIt = "LakeItOrLeaveIt",
				PowerWhelming = "PowerWhelming",
				SkywayDistrict = "SkywayDistrict",
				SurveillanceSociety = "SurveillanceSociety",
				ThePollutionSolution = "ThePollutionSolution",

				MapSize_Hide = "MapSize_Hide",
				MapSize_Show = "MapSize_Show",
				ACityForAnts = "ACityForAnts",
				Claustropolis = "Claustropolis",
				Megalopolis = "Megalopolis",
				Ultrapolis = "Ultrapolis",

				Lastentry = "";

		public static List<string> FloorMutators = new List<string>()
		{
				ArcologyEcology,
				SpelunkyDory
		};
		public static List<string> MapSizeMutators = new List<string>()
		{
				ACityForAnts,
				Claustropolis,
				Megalopolis,
				Ultrapolis
		};
		public static List<string> WallMutators = new List<string>()
		{
				CityOfSteel,
				GreenLiving,
				Panoptikopolis,
				ShantyTown,
				SpelunkyDory
		};
		public static List<string> WallMutatorsFlammable = new List<string>()
		{
				GreenLiving,
				ShantyTown
		};
	}

	public static class vFloor // Vanilla Floor Tiles
	{
		public const string
				ArenaFloor = "ArenaFloor",
				ArmoryFloor = "ArmoryFloor",
				BankFloor = "BankFloor",
				Bathhouse = "Bathhouse",
				BathroomTile = "BathroomTile",
				BrickIndoor = "BrickIndoor",
				Bridge_Unused = "Bridge",
				Canal = "Canal",
				CasinoFloor = "CasinoFloor",
				CaveFloor = "CaveFloor",
				Checkerboard = "Checkerboard",
				Checkerboard2 = "Checkerboard2",
				CleanTiles = "CleanTiles",
				CleanTilesRaised = "CleanTilesRaised",
				ClearFloor = "ClearFloor",
				ClearFloor2 = "ClearFloor2",
				ConveyorBelt = "ConveyorBelt",
				DanceFloor = "DanceFloor",
				DanceFloorRaised = "DanceFloorRaised",
				DirtFloor = "DirtFloor",
				DirtyTiles = "DirtyTiles",
				DrugDenFloor = "DrugDenFloor",
				ElectronicPlates = "ElectronicPlates",
				Facility = "Facility",
				FactoryFloor = "FactoryFloor",
				FlamePit = "FlamePit",
				Grass = "CityParkFloor",
				GreyTile_Unused = "GreyTile",
				Gym = "Gym",
				HideoutFloor = "HideoutFloor",
				Hole = "Hole",
				HospitalFloor = "HospitalFloor",
				Ice = "Ice",
				IceRink = "IceRink",
				Mall = "Mall",
				MetalFloor = "MetalFloor",
				MetalPlates = "MetalPlates",
				Muted = "Muted",
				Normal = "Normal",
				OfficeFloor = "OfficeFloor",
				PoliceStationFloor = "PoliceStationFloor",
				Pool = "Pool",
				Posh = "Posh",
				PrisonFloor = "PrisonFloor",
				RugBlue = "BlueRug",
				RugDarkBlue = "DarkBlueRug",
				RugGreen = "GreenRug",
				RugPurple = "PurpleRug",
				RugRed = "RedRug",
				SmallTiles = "SmallTiles",
				SolidPlates = "SolidPlates",
				Water = "Water",
				WoodClean = "WoodClean",
				WoodSlats = "WoodSlats";

		public static List<string> Constructed = new List<string>()
		{
				ArenaFloor,
				ArmoryFloor,
				BankFloor,
				Bathhouse,
				BathroomTile,
				BrickIndoor,
				Bridge_Unused,
				CasinoFloor,
				Checkerboard,
				Checkerboard2,
				CleanTiles,
				DanceFloor,
				DirtyTiles,
				DrugDenFloor,
				ElectronicPlates,
				Facility,
				FactoryFloor,
				GreyTile_Unused,
				Gym,
				HideoutFloor,
				HospitalFloor,
				Mall,
				MetalFloor,
				MetalPlates,
				Muted,
				Normal,
				OfficeFloor,
				PoliceStationFloor,
				PrisonFloor,
				SmallTiles,
				SolidPlates,
				WoodClean,
				WoodSlats,
		};

		public static List<string> Natural = new List<string>()
		{
				CaveFloor,
				DirtFloor,
				Grass,
		};

		public static List<string> Raised = new List<string>()
		{
				ArenaFloor,
				CleanTilesRaised,
				DanceFloorRaised
		};

		public static List<string> Rugs = new List<string>()
		{
				CasinoFloor,
				Posh,
				RugBlue,
				RugDarkBlue,
				RugGreen,
				RugPurple,
				RugRed,
		};
	}

	public static class vFloorTileGroup // Vanilla Floor Tile Groups
	{
		public const string
				Building = "FloorTilesBuilding",
				Disposal = "Disposal",
				Downtown = "FloorTilesDowntown",
				UnknownPossiblyGeneric = "FloorTiles",
				HoleTiles = "HoleTiles",
				Ice = "IceTiles",
				Industrial = "FloorTilesIndustrial",
				MayorVillage = "FloorTilesMayor",
				Park = "FloorTilesOutdoor",
				Rug = "Rug",
				Slums = "FloorTilesMain",
				Uptown = "FloorTilesWealthy",
				Wall = "WallTiles",
				Water = "WaterTiles";
	}

	public static class vLevelFeature // Vanilla Features
	{
		public const string
				AlarmButton = "AlarmButton",
				Barbecue = "Barbecue",
				BearTrap = "BearTrap",
				Boulder = "Boulder",
				Bush = "Bush",
				Cop = "Cop",
				CopBot = "CopBot",
				ExplodingSlimeBarrel = "ExplodingSlimeBarrel",
				FireHydrant = "FireHydrant",
				FlameGrate = "FlameGrate",
				FlamingBarrel = "FlamingBarrel",
				Gangbanger = "Gangbanger",
				Hobo = "Hobo",
				Lake = "Lake",
				Lamp = "Lamp",
				LockdownWall = "LockdownWall",
				Mafia = "Mafia",
				Manhole = "Manhole",
				Mayor = "Mayor",
				Mine = "Mine",
				Musician = "Musician",
				OilSpill = "OilSpill",
				PoliceBox = "PoliceBox",
				PowerBox = "PowerBox",
				SlimeBarrel = "SlimeBarrel",
				Tracks = "Tracks",
				Tracks1 = "Tracks1",
				Tracks2 = "Tracks2",
				Tracks3 = "Tracks3",
				Tracks4 = "Tracks4",
				Tracks5 = "Tracks5",
				Tracks6 = "Tracks6",
				Tracks7 = "Tracks7",
				Tracks8 = "Tracks8",
				Tracks9 = "Tracks9",
				TrashCan = "TrashCan",
				Tree = "Tree",
				VendingMachine = "VendingMachine",
				VendorCart = "VendorCart";
	}
}
