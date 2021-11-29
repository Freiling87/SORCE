using SORCE.Challenges.C_Exteriors;
using SORCE.Challenges.C_MapSize;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Content.Challenges.C_Interiors;
using SORCE.Content.Challenges.C_Roamers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SORCE.Localization
{
	public static class cChallenge
	{
		public static List<string> AddsCriminals = new List<string>()
		{
			nameof(HoodlumsWonderland),
			nameof(MobTown),
			nameof(YoungMenInTheNeighborhood),
		};
		public static List<string> AffectsLights = new List<string>()
		{
			nameof(DiscoCityDanceoff),
			nameof(GreenLiving),
		};
		public static List<string> Exteriors = new List<string>()
		{
			nameof(Arcology),
			nameof(CanalCity),
			nameof(TransitExperiment),
		};
		public static List<string> Interiors = new List<string>()
		{
			nameof(CityOfSteel),
			nameof(GreenLiving),
			nameof(Panoptikopolis),
			nameof(ShantyTown),
			nameof(SpelunkyDory)
		};
		public static List<string> InteriorsFlammable = new List<string>()
		{
			nameof(GreenLiving),
			nameof(ShantyTown)
		};
		public static List<string> MapSize = new List<string>()
		{
			nameof(ACityForAnts),
			nameof(Claustropolis),
			nameof(Megalopolis),
			nameof(Ultrapolis)
		};
		public static List<string> Overhaul = new List<string>()
		{
			nameof(AnCapistan),
			nameof(MACITS),
			nameof(PoliceState)
		};
		public static List<string> Population = new List<string>()
		{
			nameof(GhostTown),
			nameof(HordeAlmighty),
			nameof(LetMeSeeThatThrong),
			nameof(SwarmWelcome),
		};
	}
	public static class cLevelFeature // Custom Level Features
	{
		public const string
				Benches = "Benches",
				BrokenWindows = "BrokenWindows",
				Cornstalks = "Cornstalks",
				Fountains = "Fountains",
				Litter = "Litter",
				PublicSecurityCams = "PublicSecurityCams",
				Statues = "Statues";
	}
	public static class vAgent // Vanilla Agent Classes
	{
		public const string
				Alien = "Alien",
				Assassin = "Assassin",
				Athlete = "Athlete",
				Bartender = "Bartender",
				Blahd = "Gangbanger",
				Bouncer = "Bouncer",
				Cannibal = "Cannibal",
				Clerk = "Clerk",
				Comedian = "Comedian",
				Cop = "Cop",
				CopBot = "CopBot",
				Courier = "Courier",
				Crepe = "GangbangerB",
				Demolitionist = "Demolitionist",
				Doctor = "Doctor",
				DrugDealer = "DrugDealer",
				Firefighter = "Firefighter",
				Ghost = "Ghost",
				Goon = "Guard",
				Gorilla = "Gorilla",
				Hacker = "Hacker",
				InvestmentBanker = "Businessman",
				Mayor = "Mayor",
				MechPilot = "MechPilot",
				Mobster = "Mafia",
				Musician = "Musician",
				OfficeDrone = "OfficeDrone",
				ResistanceLeader = "ResistanceLeader",
				Robot = "Robot",
				RobotPlayer = "RobotPlayer",
				Scientist = "Scientist",
				ShapeShifter = "ShapeShifter",
				Shopkeeper = "Shopkeeper",
				Slave = "Slave",
				Slavemaster = "Slavemaster",
				SlumDweller = "Hobo",
				Soldier = "Soldier",
				SuperCop = "Cop2",
				Supergoon = "Guard2",
				Thief = "Thief",
				UpperCruster = "UpperCruster",
				Vampire = "Vampire",
				Werewolf = "WerewolfB",
				Worker = "Worker",
				Wrestler = "Wrestler",
				Zombie = "Zombie";

		public static List<string> Criminal = new List<string>()
		{
				Blahd,
				Crepe,
				DrugDealer,
				Mobster,
				Thief,
		};

		public static List<string> Evil = new List<string>()
		{
				ShapeShifter,
				Vampire,
				Zombie
		};

		public static List<string> LawEnforcement = new List<string>()
		{
				Cop,
				CopBot,
				SuperCop,
		};

		public static List<string> Nonhuman = new List<string>()
		{
				Alien,
				CopBot,
				Gorilla,
				Ghost,
				Robot,
				RobotPlayer,
				ShapeShifter,
				Werewolf,
				Vampire,
				Zombie,
		};

		public static List<string> Supernatural = new List<string>()
		{
				Ghost,
				Werewolf,
				Vampire,
				Zombie,
		};

		public static List<string> Undead = new List<string>()
		{
				Ghost,
				Vampire,
				Zombie
		};
	}
	public static class vAmbience // Vanilla Ambient Audio Loops
	{
		public const string
				AirConditioner = "AirFiltrationAmbience",
				BathHouse = "SpaAmbience",
				Casino = "CasinoAmbience",
				Cave = "CavernAmbience",
				Computer = "ComputerAmbience",
				ConveyorBelt = "EscalatorAmbience",
				Generator = "GeneratorAmbience",
				GeneratorOverclocked = "OverclockedGeneratorAmbience",
				Graveyard = "GraveyardAmbience",
				LampPost = "LampPostAmbience",
				Laser = "LaserAmbience",
				Park = "ParkAmbience",
				Television = "TVAmbience";
	}
	public static class vChallenge // Vanilla Mutators
	{
		public const string
				AssassinsEveryLevel = "AssassinsEveryLevel",
				BigKnockback = "BigKnockbackForAll",
				CoolWithCannibals = "CannibalsDontAttack",
				DoctorsMoreImportant = "DoctorsMoreImportant",
				EveryoneHatesYou = "EveryoneHatesYou",
				ExplodingBodies = "ExplodingBodies",
				FullHealth = "FullHealth",
				GorillaTown = "GorillaTown",
				HalfHealth = "HalfHealth",
				HighCost = "HighCost",
				InfiniteAmmo = "InfiniteAmmo",
				InfiniteAmmoNormalWeapons = "InfiniteAmmoNormalWeapons",
				InfiniteMeleeDurability = "InfiniteMeleeDurability",
				LowHealth = "LowHealth",
				ManyWerewolf = "ManyWerewolf",
				MixedUpLevels = "MixedUpLevels",
				MoneyRewards = "MoneyRewards",
				NoCops = "NoCops",
				NoCowards = "NoCowards",
				NoGuns = "NoGuns",
				NoLimits = "NoLimits",
				NoMelee = "NoMelee",
				RocketLaunchers = "RocketLaunchers",
				RogueVision = "RogueVision",
				SlowDown = "SlowDown",
				SpeedUp = "SpeedUp",
				SupercopLand = "SupercopsReplaceCops",
				TimeLimit = "TimeLimit",
				TimeLimit2 = "TimeLimit2",
				TimeLimitQuestsGiveMoreTime = "TimeLimitQuestsGiveMoreTime",
				ZombieMutator = "ZombieMutator",
				ZombiesWelcome = "ZombiesWelcome";

		public static List<string> AddsLawEnforcement = new List<string>()
		{
				SupercopLand,
		};

		public static List<string> AddsNonhumans = new List<string>()
		{
				CoolWithCannibals,
				ZombieMutator,
				ZombiesWelcome,
		};

		public static List<string> RemovesLawEnforcement = new List<string>()
		{
				NoCops,
		};

		public static List<string> Zombies = new List<string>()
		{
				ZombieMutator,
				ZombiesWelcome,
		};
	}
	public static class vChunkType // Vanilla Chunks
	{
		#region All Chunk Types

		public const string
				Apartments = "Apartments",
				Arcade = "Arcade",
				Arena = "Arena",
				Armory = "Armory",
				Bank = "Bank",
				Bar = "Bar",
				Bathhouse = "Bathhouse",
				Bathroom = "Bathroom",
				Cabin = "Cabin",
				Casino = "Casino",
				Cave = "Cave",
				Church = "Church",
				CityPark = "CityPark",
				ConfiscationCenter = "ConfiscationCenter",
				DanceClub = "DanceClub",
				DeportationCenter = "DeportationCenter",
				DrugDen = "DrugDen",
				Farm = "Farm",
				FireStation = "FireStation",
				GatedCommunity = "GatedCommunity",
				Generic = "Generic",
				Graveyard = "Graveyard",
				Greenhouse = "Greenhouse",
				HedgeMaze = "HedgeMaze",
				Hideout = "Hideout",
				Hospital = "Hospital",
				Hotel = "Hotel",
				House = "House",
				HouseUptown = "HouseUptown",
				IceRink = "IceRink",
				Lab = "Lab",
				Mall = "Mall",
				Mansion = "Mansion",
				MayorHouse = "MayorHouse",
				MayorOffice = "MayorOffice",
				MilitaryOutpost = "MilitaryOutpost",
				MovieTheater = "MovieTheater",
				MusicHall = "MusicHall",
				None = "None",
				OfficeBuilding = "OfficeBuilding",
				Pit = "Pit",
				PodiumPark = "PodiumPark",
				PoliceOutpost = "PoliceOutpost",
				PoliceStation = "PoliceStation",
				Prison = "Prison",
				PrivateClub = "PrivateClub",
				Shack = "Shack",
				Shop = "Shop",
				SlaveShop = "SlaveShop",
				TVStation = "TVStation",
				Zoo = "Zoo";

		#endregion

		#region AnCapistan

		public static List<string> AnCapistanLimitedTo2 = new List<string>()
		{
				GatedCommunity,
				Prison,
				SlaveShop,
		};

		public static List<string> AnCapistanUnlimited = new List<string>()
		{
				DrugDen,
				Shack,
		};

		public static List<string> AnCapistanProhibited = new List<string>()
		{
				ConfiscationCenter,
				CityPark,
				DeportationCenter,
				PoliceOutpost,
				PoliceStation,
		};

		#endregion

		#region Downtown

		public static List<string> DowntownLimitedTo1 = new List<string>()
		{
				Arcade,
				Arena,
				// Bank, // [sic], but prohibited
				Bathroom,
				Church,
				CityPark,
				DanceClub,
				FireStation,
				Graveyard,
				Hideout,
				IceRink,
				Mall,
				MovieTheater,
				MusicHall,
				PoliceStation,
				Shop,
				SlaveShop,
		};

		public static List<string> DowntownLimitedTo2 = new List<string>()
		{
				Bar,
				Casino,
				Hotel,
		};

		public static List<string> DowntownProhibited = new List<string>()
		{
				Apartments,
				Armory,
				Bank,
				Bathhouse,
				Cabin,
				Cave,
				ConfiscationCenter,
				DeportationCenter,
				DrugDen,
				Farm,
				GatedCommunity,
				Greenhouse,
				HedgeMaze,
				Hospital,
				House,
				HouseUptown,
				Lab,
				Mansion,
				MayorHouse,
				MayorOffice,
				MilitaryOutpost,
				OfficeBuilding,
				Pit,
				PodiumPark,
				PoliceOutpost,
				Prison,
				PrivateClub,
				Shack,
				TVStation,
				Zoo,
		};

		public static List<string> DowntownUnlimited = new List<string>()
		{
				Generic,
				None,
		};

		#endregion

		#region Industrial

		public static List<string> IndustrialLimitedTo1 = new List<string>()
		{
				Bank,
				Bathroom,
				FireStation,
				Graveyard,
				Hospital,
				PoliceStation,
				Prison,
				Shop,
				SlaveShop,
		};

		public static List<string> IndustrialLimitedTo2 = new List<string>()
		{
				Armory,
				Bar,
				Casino,
				Hideout,
				"Factory",
				Lab,
		};

		public static List<string> IndustrialLimitedTo3 = new List<string>()
		{
				Apartments,
				DrugDen,
				House,
				OfficeBuilding,
				Shack,
		};

		public static List<string> IndustrialProhibited = new List<string>()
		{
				Arcade,
				Arena,
				Bathhouse,
				Cabin,
				Cave,
				Church,
				CityPark,
				ConfiscationCenter,
				DanceClub,
				DeportationCenter,
				Farm,
				GatedCommunity,
				Greenhouse,
				HedgeMaze,
				Hotel,
				HouseUptown,
				IceRink,
				Mall,
				Mansion,
				MayorHouse,
				MayorOffice,
				MilitaryOutpost,
				MovieTheater,
				MusicHall,
				Pit,
				PodiumPark,
				PoliceOutpost,
				PrivateClub,
				TVStation,
				Zoo,
		};

		public static List<string> IndustrialUnlimited = new List<string>()
		{
				Generic,
				None,
		};

		#endregion

		#region Mayor's Village

		public static List<string> MayorVillageLimitedTo1 = new List<string>()
		{
				Bank,
				Bar,
				Bathhouse,
				// Bathroom, // [sic]; also included in prohibited
				Church,
				DanceClub,
				FireStation,
				Hospital,
				MayorHouse,
				MayorOffice,
				MusicHall,
				Pit,
				PodiumPark,
				PoliceOutpost,
				// PoliceStation, // [sic]; also included in prohibited
				PrivateClub,
				Shop,
				SlaveShop,
				Zoo,
		};

		public static List<string> MayorVillageLimitedTo2 = new List<string>()
		{
				HouseUptown,
		};

		public static List<string> MayorVillageProhibited = new List<string>()
		{
				Apartments,
				Arcade,
				Arena,
				Armory,
				Bathroom,
				Cabin,
				Casino,
				Cave,
				CityPark,
				ConfiscationCenter,
				DeportationCenter,
				DrugDen,
				Farm,
				Graveyard,
				Greenhouse,
				HedgeMaze,
				Hideout,
				Hotel,
				House,
				IceRink,
				Lab,
				Mall,
				Mansion,
				MilitaryOutpost,
				MovieTheater,
				OfficeBuilding,
				PoliceStation,
				Prison,
				Shack,
				TVStation,
		};

		public static List<string> MayorVillageUnlimited = new List<string>()
		{
				GatedCommunity,
				Generic,
				None,
		};

		#endregion

		#region Park

		public static List<string> ParkLimitedTo1 = new List<string>()
		{
				Graveyard,
				HedgeMaze,
				Hideout,
		};

		public static List<string> ParkLimitedTo2 = new List<string>()
		{
				MilitaryOutpost,
		};

		public static List<string> ParkLimitedTo3 = new List<string>()
		{
				Cabin,
				Cave,
				Farm,
				Greenhouse,
		};

		public static List<string> ParkProhibited = new List<string>()
		{
				Arcade,
				Arena,
				Bathhouse,
				Church,
				CityPark,
				ConfiscationCenter,
				DanceClub,
				DeportationCenter,
				FireStation,
				GatedCommunity,
				Hotel,
				HouseUptown,
				IceRink,
				Mall,
				Mansion,
				MayorHouse,
				MayorOffice,
				MovieTheater,
				MusicHall,
				Pit,
				PodiumPark,
				PoliceOutpost,
				PrivateClub,
				TVStation,
				Zoo,
		};

		public static List<string> ParkUnlimited = new List<string>()
		{
				Bathroom,
				Generic,
				Shop,
		};

		#endregion

		#region Police State

		public static List<string> PoliceStateLimitedTo1 = new List<string>()
		{
				ConfiscationCenter,
				DeportationCenter,
				MilitaryOutpost,
				PoliceOutpost,
				PoliceStation,
				Prison,
		};

		public static List<string> PoliceStateLimitedTo2 = new List<string>()
		{
				PoliceOutpost,
				PoliceStation,
		};

		public static List<string> PoliceStateProhibited = new List<string>()
		{
				CityPark,
				DrugDen,
		};

		#endregion

		#region Slums

		public static List<string> SlumsLimitedTo1 = new List<string>()
		{
				Bank,
				Bathroom,
				Graveyard,
				Hideout,
				Hospital,
				PoliceStation,
				Prison,
				Shop,
				SlaveShop
		};

		public static List<string> SlumsLimitedTo2 = new List<string>()
		{
				Armory,
				Bar,
				Casino,
				Lab,
		};

		public static List<string> SlumsLimitedTo3 = new List<string>()
		{
				Apartments,
				DrugDen,
				House,
				OfficeBuilding,
				Shack,
		};

		public static List<string> SlumsProhibited = new List<string>()
		{
				Arcade,
				Arena,
				Bathhouse,
				Cabin,
				Cave,
				Church,
				CityPark,
				ConfiscationCenter,
				DanceClub,
				DeportationCenter,
				Farm,
				FireStation,
				GatedCommunity,
				Greenhouse,
				HedgeMaze,
				Hotel,
				HouseUptown,
				IceRink,
				Mall,
				Mansion,
				MayorHouse,
				MayorOffice,
				MilitaryOutpost,
				MovieTheater,
				MusicHall,
				Pit,
				PodiumPark,
				PoliceOutpost,
				PrivateClub,
				TVStation,
				Zoo,
		};

		public static List<string> SlumsUnlimited = new List<string>()
		{
				Generic,
				None,
		};

		#endregion

		#region Uptown

		public static List<string> UptownLimitedTo1 = new List<string>()
		{
				Bank,
				Bathhouse,
				Church,
				CityPark,
				ConfiscationCenter,
				DeportationCenter,
				FireStation,
				Graveyard,
				//Hideout, // [sic]
				IceRink,
				Mall,
				Mansion,
				Pit,
				PoliceStation,
				Shop,
				SlaveShop,
				TVStation,
				Zoo,
		};

		public static List<string> UptownLimitedTo2 = new List<string>()
		{
				Bathroom,
				GatedCommunity,
				MusicHall,
				PrivateClub,
		};

		public static List<string> UptownLimitedTo3 = new List<string>()
		{
				PoliceOutpost,
		};

		public static List<string> UptownLimitedTo5 = new List<string>()
		{
				HouseUptown,
		};

		public static List<string> UptownProhibited = new List<string>()
		{
				Apartments,
				Arcade,
				Arena,
				Armory,
				Bar,
				Cabin,
				Casino,
				Cave,
				DanceClub,
				DrugDen,
				Farm,
				HedgeMaze,
				Hideout,
				Hotel,
				House,
				MayorHouse,
				MayorOffice,
				MilitaryOutpost,
				MovieTheater,
				OfficeBuilding,
				PodiumPark,
				Prison,
				Shack,
		};

		public static List<string> UptownUnlimited = new List<string>()
		{
				Generic,
				Greenhouse,
				Hospital,
				Lab,
				None,
		};

		#endregion
	}
	public static class vColor // Vanilla Colors
	{
		// https://colordesigner.io/color-mixer
		public static Color32 arenaRingColor = new Color32(167, 76, 134, 200);
		public static Color32 blueColor = new Color32(62, 62, 255, 200);
		public static Color32 cyanColor = new Color32(0, 113, 159, 200);
		public static Color32 cyanGreenColor = new Color32(0, 180, 143, 200);
		public static Color32 defaultColor = new Color32(161, 161, 161, 105);
		public static Color32 discoBlueColor = new Color32(64, 64, 255, 200);
		public static Color32 discoGreenColor = new Color32(85, 170, 0, 200);
		public static Color32 discoOrangeColor = new Color32(255, 188, 64, 200);
		public static Color32 discoPurpleColor = new Color32(140, 52, 173, 200);
		public static Color32 discoRedColor = new Color32(255, 85, 85, 200);
		public static Color32 discoYellowColor = new Color32(255, 255, 85, 200);
		public static Color32 fireStationColor = new Color32(125, 87, 248, 111);
		public static Color32 greenColor = new Color32(0, 159, 60, 200);
		public static Color32 homeColor = new Color32(199, 174, 120, 160);
		public static Color32 homeColorMayorVillage = new Color32(212, 122, 244, 160);
		public static Color32 homeColorUptown = new Color32(205, 173, 219, 85);
		public static Color32 labColor = new Color32(64, 224, 255, 180);
		public static Color32 lakeColor = new Color32(0, 213, 255, 85);
		public static Color32 lightBlueColor = new Color32(124, 151, 189, 180);
		public static Color32 lightBlueColorMayorVillage = new Color32(44, 106, 193, 180);
		public static Color32 mallColor = new Color32(255, 255, 255, 80);
		public static Color32 pinkColor = new Color32(159, 0, 148, 200);
		public static Color32 pinkWhiteColor = new Color32(208, 163, 255, 120);
		public static Color32 poolColor = new Color32(0, 213, 255, 85);
		public static Color32 poolColorLighter = new Color32(144, 237, 255, 85);
		public static Color32 privateClubColor = new Color32(163, 178, 110, 160);
		public static Color32 purpleColor = new Color32(111, 0, 159, 200);
		public static Color32 redColor = new Color32(159, 0, 0, 200);
		public static Color32 whiteColor = new Color32(255, 255, 255, 120);
		public static Color32 zooColor = new Color32(0, 255, 181, 85);

		public static List<Color32> discoColors = new List<Color32>()
		{
				discoBlueColor,
				discoGreenColor,
				discoOrangeColor,
				discoPurpleColor,
				discoRedColor,
				discoYellowColor,
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
	public static class vLevelFeeling // Vanilla Disasters
	{
		public const string
				BountyOnYourHead = "Bounty",
				FallingBombs = "DropBombs",
				HiddenBombs = "FindBombs",
				KillerRobot = "Killer",
				Lockdown = "Lockdown",
				Ooze = "Ooze",
				RadiationBlasts = "HarmAtIntervals",
				Riot = "Riot",
				ShiftingStatusEffects = "StatusEffectChange",
				WarZone = "WarZone",
				Zombies = "Zombies";
	}
	public static class vObject // Vanilla Objects
	{
		public const string
				AirConditioner = "AirConditioner",
				AlarmButton = "AlarmButton",
				Altar = "Altar",
				AmmoDispenser = "AmmoDispenser",
				ArcadeGame = "ArcadeGame",
				ATMMachine = "ATMMachine",
				AugmentationBooth = "AugmentationBooth",
				Barbecue = "Barbecue",
				BarStool = "BarStool",
				Bathtub = "Bathtub",
				Bed = "Bed",
				Boulder = "Boulder",
				BoulderSmall = "BoulderSmall",
				Bush = "Bush",
				CapsuleMachine = "CapsuleMachine",
				Chair = "Chair",
				Chair2 = "Chair2",
				ChestBasic = "ChestBasic",
				CloneMachine = "CloneMachine",
				Computer = "Computer",
				Counter = "Counter",
				Crate = "Crate",
				Desk = "Desk",
				Door = "Door",
				Elevator = "Elevator",
				EventTriggerFloor = "EventTriggerFloor",
				ExplodingBarrel = "ExplodingBarrel",
				FireHydrant = "FireHydrant",
				Fireplace = "Fireplace",
				FireSpewer = "FireSpewer",
				FlameGrate = "FlameGrate",
				FlamingBarrel = "FlamingBarrel",
				GasVent = "GasVent",
				Generator = "Generator",
				Generator2 = "Generator2",
				Gravestone = "Gravestone",
				Jukebox = "Jukebox",
				KillerPlant = "KillerPlant",
				Lamp = "Lamp",
				LaserEmitter = "LaserEmitter",
				LoadoutMachine = "LoadoutMachine",
				Manhole = "Manhole",
				Mine = "Mine",
				MovieScreen = "MovieScreen",
				PawnShopMachine = "PawnShopMachine",
				Plant = "Plant",
				Podium = "Podium",
				PoliceBox = "PoliceBox",
				PoolTable = "PoolTable",
				PowerBox = "PowerBox",
				Refrigerator = "Refrigerator",
				Safe = "Safe",
				SatelliteDish = "SatelliteDish",
				SecurityCam = "SecurityCam",
				Shelf = "Shelf",
				Sign = "Sign",
				SlimeBarrel = "SlimeBarrel",
				SlimePuddle = "SlimePuddle",
				SlotMachine = "SlotMachine",
				Speaker = "Speaker",
				Stove = "Stove",
				SwitchBasic = "SwitchBasic",
				SwitchFloor = "SwitchFloor",
				Table = "Table",
				TableBig = "TableBig",
				Television = "Television",
				Toilet = "Toilet",
				TrashCan = "TrashCan",
				Tree = "Tree",
				Tube = "Tube",
				Turntables = "Turntables",
				Turret = "Turret",
				VendorCart = "VendorCart",
				WaterPump = "WaterPump",
				Well = "Well",
				Window = "Window";
	}
	public static class vWall // Vanilla Walls
	{
		public const string
				BarbedWire = "BarbedWire",
				Bars = "Bars",
				Border = "Border",
				Brick = "Normal",
				Cave = "Cave",
				Glass = "Glass",
				Hedge = "Hedge",
				Null = "",
				Steel = "Steel",
				Wood = "Wood";
	}
	public static class vWallGroup // Vanilla Wall Groups
	{
		public const string
				Hideout = "WallsHideout",
				Normal = "WallsNormal",
				Strong = "WallsStrong",
				Weak = "WallsWeak";
	}
}
