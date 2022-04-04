using System.Collections.Generic;

namespace SORCE.Localization
{
    public static partial class NameLists
	{
        public static class VChunkType // Vanilla Chunks
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
	}
}