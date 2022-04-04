using System.Collections.Generic;

namespace SORCE.Localization
{
    public static partial class NameLists
	{
        public static class VFloor // Vanilla Floor Tiles
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
				ArmoryFloor,
				BankFloor,
				Bathhouse,
				// Bridge_Unused, // Omit
				Checkerboard,
				Checkerboard2,
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
			public static List<string> UnraisedTileTiles = new List<string>()
			{
				BathroomTile,
				BrickIndoor,
				CleanTiles,
				DanceFloor,
				DirtyTiles,
				SmallTiles,
			};
		}
	}
}