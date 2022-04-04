using System.Collections.Generic;

namespace SORCE.Localization
{
    public static partial class NameLists
	{
        public static class VAgent // Vanilla Agent Classes
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
	}
}