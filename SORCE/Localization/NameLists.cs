using RogueLibsCore;
using SORCE.Challenges.C_AmbientLightColor;
using SORCE.Challenges.C_AmbientLightLevel;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_MapSize;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Challenges.C_Roamers;
using System.Collections.Generic;
using UnityEngine;

namespace SORCE.Localization
{
    // TODO: Move this out of here
    public static class NameLists
	{
		#region Challenges
		public static List<string> AddsCriminals = new List<string>()
		{
			nameof(HoodlumsWonderland),
			nameof(UnionTown),
			nameof(YoungMenInTheNeighborhood),
		};
		public static List<string> AffectsLights = new List<string>()
		{
			nameof(DiscoCityDanceoff),
			nameof(GreenLiving),
		};
		public static List<string> Buildings = new List<string>()
		{
			nameof(CityOfSteel),
			nameof(GreenLiving),
			nameof(Panoptikopolis),
			nameof(ShantyTown),
			nameof(SpelunkyDory)
		};
		public static List<string> BuildingsFlammable = new List<string>()
		{
			nameof(GreenLiving),
			nameof(ShantyTown)
		};
		public static List<string> MapSize = new List<string>()
		{
			nameof(Arthropolis),
			nameof(Claustropolis),
			nameof(Megapolis),
			nameof(Ultrapolis)
		};
		public static List<string> Overhauls = new List<string>()
		{
			vChallenge.MixedUpLevels,
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
		#endregion
		#region Colors
		public static List<string> AmbientLightColor = new List<string>()
		{
			nameof(ShadowRealm),
			nameof(Goodsprings),
			nameof(Hellscape),
			nameof(NuclearWinter),
			nameof(Reactor),
			nameof(Sepia),
			nameof(ShadowRealm),
			nameof(Shinobi),
		};
		public static Dictionary<string, Color32> AmbientLightColorDict = new Dictionary<string, Color32>()
		{
			{ nameof(Goodsprings),		cColors.Goodsprings },
			{ nameof(Hellscape),		cColors.Hellscape },
			{ nameof(NuclearWinter),	cColors.NuclearWinter },
			{ nameof(Reactor),          cColors.Reactor },
			{ nameof(Sepia),			cColors.Sepia },
			{ nameof(ShadowRealm),      cColors.ShadowRealm },
			{ nameof(Shinobi),			cColors.Shinobi },
		};
		public static List<string> AmbientLightLevel = new List<string>()
		{
			nameof(Blinding),
			nameof(Daytime),
			nameof(Evening),
			nameof(FullMoon),
			nameof(HalfMoon),
			nameof(NewMoon),
		};
		public static Dictionary<string, int> AmbientLightLevelDict = new Dictionary<string, int>()
		{
			{ nameof(Blinding),	255 },
			{ nameof(Daytime),	200 },
			{ nameof(Evening),	150 },
			{ nameof(FullMoon), 100 },
			{ nameof(HalfMoon), 50 },
			{ nameof(NewMoon),	0 },
		};
		public static class cColors
		{
			public static Color32 Goodsprings = new Color32(200, 125, 25, 190);
			public static Color32 Hellscape = new Color32(200, 0, 0, 175);
			public static Color32 NuclearWinter = new Color32(255, 255, 255, 175);
			public static Color32 Reactor = new Color32(75, 200, 50, 125);
			public static Color32 Sepia = new Color32(150, 150, 50, 190);
			public static Color32 ShadowRealm = new Color32(75, 75, 75, 175);
			public static Color32 Shinobi = new Color32(75, 75, 150, 200);

			public static Color32 TestBlack = new Color32(0, 0, 0, 255);
			public static Color32 TestGreen = new Color32(0, 255, 0, 255);
			public static Color32 TestRed = new Color32(255, 0, 0, 255);
			public static Color32 TestBlue = new Color32(0, 0, 255, 255);
			public static Color32 TestPurple = new Color32(255, 0, 255, 255);
			public static Color32 TestWhite = new Color32(255, 255, 255, 255);
		}
		#endregion
		#region Custom Content
		public static class cButtonText // Custom Button Text
        {
			public const string
				CameraDetectEnforcer = "CameraDetectEnforcer",
				CameraDetectGuilty = "CameraDetectGuilty",
				CameraDetectWanted = "CameraDetectWanted",
				ElevatorBuy = "ElevatorBuyTicket",
				FountainSteal = "FountainSteal",
				FountainWishFabulousWealth = "FountainWishFabulousWealth",
				FountainWishFameAndGlory = "FountainWishFameAndGlory",
				FountainWishGoodHealth = "FountainWishGoodHealth",
				FountainWishTrueFriendship = "FountainWishTrueFriendship",
				FountainWishWorldPeace = "FountainWishWorldPeace",
				FireHydrantBuy = "HydrantBuy"
				;
		}
		public static class cDialogue // Custom Dialogue
		{
			public const string
				AlarmButtonBuyFail = "AlarmButtonCantAfford",
				ElevatorBuyFail = "CantAffordElevator",
				ElevatorBuySuccess = "PurchaseElevator",
				FireHydrantBuyFail = "FireHydrantBuyFail",
				FireHydrantBuySuccess = "FireHydrantBuySuccess",
				MachineBusy = "MachineBusy",
				ToiletBuyFail = "ToiletCantAfford"
				;
		}
		public static class cLevelFeature // Custom Level Features
		{
			public const string
					BrokenWindows = "BrokenWindows",
					Fountains = "Fountains",
					Litter = "Litter",
					PublicSecurityCams = "PublicSecurityCams";
		}
		public static class cObject
		{
			public static List<string> WreckageMisc = new List<string>()
			{
					vObject.BarStool,
					vObject.Lamp,
					vObject.Shelf,
			};

			public static List<string> WreckageOrganic = new List<string>() // All should have gibs with visible burn
			{
					vObject.Chair,
					vObject.Shelf,
					vObject.Table,
					vObject.TableBig,
			};
		}
		public static class cOperatingText // Custom Operating Bar Text
        {
			public const string
				FountainStealing = "BeingAPieceOfShit";
        }
		#endregion
		#region Vanilla Content
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
					ClubMusic = "ClubMusic", // If this works, try ClubMusic_Long, ClubMusic_Huge
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
		public static class vAnimation
        {
			public const string
				MachineOperate = "MachineOperate";
        }
		public static class vAudioClip // Vanilla Audio Clips
		{
			public const string
					AddTrait = "AddTrait",
					AgentAlert = "AgentAlert",
					AgentAnnoyed = "AgentAnnoyed",
					AgentArrest = "AgentArrest",
					AgentDie = "AgentDie",
					AgentDie1 = "AgentDie1",
					AgentDie2 = "AgentDie2",
					AgentDie3 = "AgentDie3",
					AgentEnslave = "AgentEnslave",
					AgentFlee = "AgentFlee",
					AgentGiant1 = "AgentGiant1",
					AgentGiant2 = "AgentGiant2",
					AgentGiant3 = "AgentGiant3",
					AgentGib = "AgentGib",
					AgentInvestigate = "AgentInvestigate",
					AgentInvestigate2 = "AgentInvestigate2",
					AgentInvestigate3 = "AgentInvestigate3",
					AgentJoin = "AgentJoin",
					AgentJoin2 = "AgentJoin2",
					AgentJoin3 = "AgentJoin3",
					AgentJoke = "AgentJoke",
					AgentKnockOut = "AgentKnockOut",
					AgentKnockout2 = "AgentKnockout2",
					AgentKnockout3 = "AgentKnockout3",
					AgentLaugh = "AgentLaugh",
					AgentOK = "AgentOK",
					AgentOK2 = "AgentOK2",
					AgentRevive = "AgentRevive",
					AgentReviveZombie = "AgentReviveZombie",
					AgentRuckus = "AgentRuckus",
					AgentRuckus2 = "AgentRuckus2",
					AgentRuckus3 = "AgentRuckus3",
					AgentShrink = "AgentShrink",
					AgentTalk = "AgentTalk",
					AgentTalk2 = "AgentTalk2",
					AgentTalk3 = "AgentTalk3",
					AgentTalk4 = "AgentTalk4",
					AgentTalk5 = "AgentTalk5",
					AirFiltrationAmbience = "AirFiltrationAmbience",
					AlarmButton = "AlarmButton",
					AmmoOut1 = "AmmoOut1",
					AmmoOut2 = "AmmoOut2",
					AmmoOutLaserGun = "AmmoOutLaserGun",
					ArmedMine = "ArmedMine",
					ArmorBreak = "ArmorBreak",
					ATMDeposit = "ATMDeposit",
					BearTrapSnap = "BearTrapSnap",
					BeginCombine = "BeginCombine",
					BigUnlock = "BigUnlock",
					BiteCannibal = "BiteCannibal",
					BiteCannibal2 = "BiteCannibal2",
					BiteCannibal3 = "BiteCannibal3",
					BloodSuck = "BloodSuck",
					Boombox1 = "Boombox1",
					Boombox2 = "Boombox2",
					BulletHitAgent = "BulletHitAgent",
					BulletHitIndestructibleObject = "BulletHitIndestructibleObject",
					BulletHitObject = "BulletHitObject",
					BulletHitWall = "BulletHitWall",
					BushDestroy = "BushDestroy",
					ButlerBotClean = "ButlerBotClean",
					BuyItem = "BuyItem",
					BuyUnlock = "BuyUnlock",
					CannibalFinish = "CannibalFinish",
					CantDo = "CantDo",
					CasinoAmbience = "CasinoAmbience",
					CavernAmbience = "CavernAmbience",
					ChainsawSwing = "ChainsawSwing",
					ChainsawSwing2 = "ChainsawSwing2",
					ChargeIntoSolidWall = "ChargeIntoSolidWall",
					ChargeLand = "ChargeLand",
					ChargeLaunch = "ChargeLaunch",
					ChargePrepare = "ChargePrepare",
					ChestOpen = "ChestOpen",
					ChloroformAgent = "ChloroformAgent",
					ChloroformAgent2 = "ChloroformAgent2",
					ChooseDisaster = "ChooseDisaster",
					ChooseReward = "ChooseReward",
					ClickButton = "ClickButton",
					CloseMenu = "CloseMenu",
					ClubMusic = "ClubMusic",
					CombineItem = "CombineItem",
					ComputerAmbience = "ComputerAmbience",
					ConveyorBelt = "ConveyorBelt",
					CopBotCam = "CopBotCam",
					CopBotDetect = "CopBotDetect",
					Countdown = "Countdown",
					CountdownEnd = "CountdownEnd",
					Credits = "Credits",
					DeliverPackage = "DeliverPackage",
					Depossess = "Depossess",
					DialogueTextCrawl1 = "DialogueTextCrawl1",
					DialogueTextCrawl2 = "DialogueTextCrawl2",
					DialogueTextCrawl3 = "DialogueTextCrawl3",
					DialogueTextCrawl4 = "DialogueTextCrawl4",
					DialogueTextCrawl5 = "DialogueTextCrawl5",
					DialogueTextCrawl6 = "DialogueTextCrawl6",
					DialogueTextCrawl7 = "DialogueTextCrawl7",
					DialogueTextCrawl8 = "DialogueTextCrawl8",
					Dizzy = "Dizzy",
					DoorClose = "DoorClose",
					DoorCloseAI = "DoorCloseAI",
					DoorKnock = "DoorKnock",
					DoorOpen = "DoorOpen",
					DoorOpenAI = "DoorOpenAI",
					DropItem = "DropItem",
					ElectroZap = "ElectroZap",
					ElectroZap2 = "ElectroZap2",
					EnterMech = "EnterMech",
					EquipArmor = "EquipArmor",
					EquipWeapon = "EquipWeapon",
					EscalatorAmbience = "EscalatorAmbience",
					ExitMech = "ExitMech",
					ExplodeDizzy = "ExplodeDizzy",
					ExplodeGiant = "ExplodeGiant",
					ExplodeMindControl = "ExplodeMindControl",
					ExplodeMindControl2 = "ExplodeMindControl2",
					ExplodeMindControl3 = "ExplodeMindControl3",
					ExplodeMonkeyBarrel = "ExplodeMonkeyBarrel",
					ExplodeSlime = "ExplodeSlime",
					ExplodeWarp = "ExplodeWarp",
					ExplodeWater = "ExplodeWater",
					Explosion = "Explosion",
					ExplosionEMP = "ExplosionEMP",
					Fail = "Fail",
					FallInHole = "FallInHole",
					FireballHitAgent = "FireballHitAgent",
					FireConstant = "FireConstant",
					FireExtinguisherEnd = "FireExtinguisherEnd",
					FireExtinguisherFire = "FireExtinguisherFire",
					FireExtinguisherLoop = "FireExtinguisherLoop",
					FireHit = "FireHit",
					FireHitShort = "FireHitShort",
					FireHydrantBreak = "FireHydrantBreak",
					FirePersist = "FirePersist",
					FireSpewerFire = "FireSpewerFire",
					FireworksFire1 = "FireworksFire1",
					FireworksFire2 = "FireworksFire2",
					FireworksFire3 = "FireworksFire3",
					FireworksLoop = "FireworksLoop",
					FlameThrowerEnd = "FlameThrowerEnd",
					FlamethrowerFireOld = "FlamethrowerFire-Old",
					FlamethrowerFire = "FlamethrowerFire",
					FlameThrowerLoop = "FlameThrowerLoop",
					FlamingBarrelCrackle = "FlamingBarrelCrackle",
					FloorClear = "FloorClear",
					FloorTrap = "FloorTrap",
					Freeze = "Freeze",
					FreezeRayFire = "FreezeRayFire",
					FreezeRayFireOld = "FreezeRayFireOld",
					GasConstant = "GasConstant",
					GasSpawn = "GasSpawn",
					GeneratorAmbience = "GeneratorAmbience",
					GeneratorHiss = "GeneratorHiss",
					GhostGibberEnd = "GhostGibberEnd",
					GhostGibberFire = "GhostGibberFire",
					GhostGibberLoop = "GhostGibberLoop",
					GoInvisible = "GoInvisible",
					GraveyardAmbience = "GraveyardAmbience",
					Grill = "Grill",
					GrillOperate = "GrillOperate",
					Hack = "Hack",
					Hack2 = "Hack2",
					Heal = "Heal",
					Hide = "Hide",
					HideInterface = "HideInterface",
					HighVolume = "HighVolume",
					Hoist = "Hoist",
					Home_Base_v2 = "Home_Base_v2",
					Hypnotize = "Hypnotize",
					IceBreak = "IceBreak",
					InstallMalware = "InstallMalware",
					Intro_Hit = "Intro_Hit",
					Intro_Loop = "Intro_Loop",
					Intro_Whoosh = "Intro_Whoosh",
					ItemFallInHole = "ItemFallInHole",
					ItemFallInHole2 = "ItemFallInHole2",
					ItemFallInHoleSmall = "ItemFallInHoleSmall",
					ItemHitItem = "ItemHitItem",
					Jukebox = "Jukebox",
					Jump = "Jump",
					JumpIntoWater = "JumpIntoWater",
					JumpIntoWater2 = "JumpIntoWater2",
					JumpIntoWater3 = "JumpIntoWater3",
					JumpOutWater = "JumpOutWater",
					KillerPlantBite = "KillerPlantBite",
					KillerPlantSnap = "KillerPlantSnap",
					KillerPlantSnap2 = "KillerPlantSnap2",
					KillerPlantSnapOther = "KillerPlantSnapOther",
					LampDestroy = "LampDestroy",
					LamppostAmbience = "LamppostAmbience",
					Land = "Land",
					LaserAmbience = "LaserAmbience",
					LaserGunFire = "LaserGunFire",
					LaserGunFire2 = "LaserGunFire2",
					LaserGunFire3 = "LaserGunFire3",
					LaserGunFireEmpty = "LaserGunFireEmpty",
					LeafBlowerEnd = "LeafBlowerEnd",
					LeafBlowerFire = "LeafBlowerFire",
					LeafBlowerLoop = "LeafBlowerLoop",
					Level1_1 = "Level1_1",
					Level1_2 = "Level1_2",
					Level1_3 = "Level1_3",
					Level2_1 = "Level2_1",
					Level2_2 = "Level2_2",
					Level2_3 = "Level2_3",
					Level3_1 = "Level3_1",
					Level3_2 = "Level3_2",
					Level3_3 = "Level3_3",
					Level4_1 = "Level4_1",
					Level4_2 = "Level4_2",
					Level4_3 = "Level4_3",
					Level5_1 = "Level5_1",
					Level5_2 = "Level5_2",
					Level5_3 = "Level5_3",
					Level6 = "Level6",
					LevelIntro = "LevelIntro",
					LevelUp = "LevelUp",
					LevelWarning = "LevelWarning",
					LockdownWallDown = "LockdownWallDown",
					LockdownWallUp = "LockdownWallUp",
					LoseStatusEffect = "LoseStatusEffect",
					LungePrepare = "LungePrepare",
					LungeSwing = "LungeSwing",
					LungeSwing2 = "LungeSwing2",
					MachineGunFire = "MachineGunFire",
					MakeOffering = "MakeOffering",
					ManholeOpen = "ManholeOpen",
					MeleeHitAgentCutLarge = "MeleeHitAgentCutLarge",
					MeleeHitAgentCutSmall = "MeleeHitAgentCutSmall",
					MeleeHitAgentCutSmall2 = "MeleeHitAgentCutSmall2",
					MeleeHitAgentLarge = "MeleeHitAgentLarge",
					MeleeHitAgentSmall = "MeleeHitAgentSmall",
					MeleeHitAgentSmall2 = "MeleeHitAgentSmall2",
					MeleeHitAgentSmall3 = "MeleeHitAgentSmall3",
					MeleeHitMelee = "MeleeHitMelee",
					MeleeHitMelee2Old = "MeleeHitMelee2-Old",
					MeleeHitMelee2 = "MeleeHitMelee2",
					MeleeHitMelee3Old = "MeleeHitMelee3-Old",
					MeleeHitMelee3 = "MeleeHitMelee3",
					MeleeHitMelee4 = "MeleeHitMelee4",
					MeleeHitMelee5 = "MeleeHitMelee5",
					MeleeHitMeleeBlade = "MeleeHitMeleeBlade",
					MeleeHitMeleeBlade2 = "MeleeHitMeleeBlade2",
					MeleeHitMeleeBlade3 = "MeleeHitMeleeBlade3",
					MeleeHitMeleeBlade4 = "MeleeHitMeleeBlade4",
					MeleeWeaponBreak = "MeleeWeaponBreak",
					MenuMove = "MenuMove",
					MindControlEnd = "MindControlEnd",
					MindControlFire = "MindControlFire",
					MindControlFire2 = "MindControlFire2",
					MindControlFire3 = "MindControlFire3",
					MindControlSuccess = "MindControlSuccess",
					MindControlSuccess2 = "MindControlSuccess2",
					MindControlSuccess3 = "MindControlSuccess3",
					MineCart = "MineCart",
					MolotovCocktailBreak = "MolotovCocktailBreak",
					MovieScreen = "MovieScreen",
					ObjectDestroy = "ObjectDestroy",
					ObjectOnFire = "ObjectOnFire",
					OpenMenu = "OpenMenu",
					OperateTrapDoor = "OperateTrapDoor",
					Operating = "Operating",
					OperatingArrest = "OperatingArrest",
					OperatingElectronicsSuccess = "OperatingElectronicsSuccess",
					OverclockedGeneratorAmbience = "OverclockedGeneratorAmbience",
					Paralyzer = "Paralyzer",
					ParkAmbience = "ParkAmbience",
					PistolFire = "PistolFire",
					PlayerDeath = "PlayerDeath",
					PoisonDamage = "PoisonDamage",
					Poisoned = "Poisoned",
					PolluteWater = "PolluteWater",
					Possess = "Possess",
					PourOil = "PourOil",
					PourOil2 = "PourOil2",
					PourOil3 = "PourOil3",
					PourOil4 = "PourOil4",
					PourOil5 = "PourOil5",
					PowerBox = "PowerBox",
					PushTrap = "PushTrap",
					PushTrapHit = "PushTrapHit",
					PushTrapHitWall = "PushTrapHitWall",
					PushTrapRetract = "PushTrapRetract",
					QuestAccept = "QuestAccept",
					QuestComplete = "QuestComplete",
					QuestCompleteBig = "QuestCompleteBig",
					QuestFail = "QuestFail",
					RadiationBlast = "RadiationBlast",
					RapOnWindow = "RapOnWindow",
					Recharge = "Recharge",
					RefillWaterCannon = "RefillWaterCannon",
					ResearchGunEnd = "ResearchGunEnd",
					ResearchGunFire = "ResearchGunFire",
					ResearchGunLoop = "ResearchGunLoop",
					RevolverFire1 = "RevolverFire1",
					RevolverFire2 = "RevolverFire2",
					RevolverFire3 = "RevolverFire3",
					Rimshot = "Rimshot",
					RobotDeath = "RobotDeath",
					RobotWalk = "RobotWalk",
					RobotWalk2 = "RobotWalk2",
					RobotWalk3 = "RobotWalk3",
					RobotWalk4 = "RobotWalk4",
					RobotWalkLoop = "RobotWalkLoop",
					RocketLauncherFire = "RocketLauncherFire",
					RocketLauncherHitIndestructible = "RocketLauncherHitIndestructible",
					SatelliteAdjust = "SatelliteAdjust",
					SatelliteBroadcast = "SatelliteBroadcast",
					SawBladeHit = "SawBladeHit",
					SawBladeHit2 = "SawBladeHit2",
					SawBladeRun = "SawBladeRun",
					SecurityCamSpot = "SecurityCamSpot",
					SecurityCamTurn = "SecurityCamTurn",
					SecurityShutdown = "SecurityShutdown",
					SelectCharacter = "SelectCharacter",
					SelectCharacter2 = "SelectCharacter2",
					SelectCharacter3 = "SelectCharacter3",
					SelectCharacter4 = "SelectCharacter4",
					SelectItem = "SelectItem",
					ShotgunFire = "ShotgunFire",
					ShowInterface = "ShowInterface",
					ShrinkRayFire = "ShrinkRayFire",
					SilencedBulletHitObject = "SilencedBulletHitObject",
					SilencedGun = "SilencedGun",
					SilencedGun2 = "SilencedGun2",
					SilencedGun3 = "SilencedGun3",
					Singe = "Singe",
					SlideWhistle = "SlideWhistle",
					Slip = "Slip",
					SlipLand = "SlipLand",
					SpaAmbience = "SpaAmbience",
					SpeechEnding = "SpeechEnding",
					SpeechMain = "SpeechMain",
					SpinRecordFail = "SpinRecordFail",
					SpinRecordSuccess = "SpinRecordSuccess",
					SpitFireball = "SpitFireball",
					SpitMeat = "SpitMeat",
					SplitScreenSwitch = "SplitScreenSwitch",
					StatusEffectShift = "StatusEffectShift",
					Stomp = "Stomp",
					Stomp2 = "Stomp2",
					Stomp3 = "Stomp3",
					Success = "Success",
					SwingWeaponFist = "SwingWeaponFist",
					SwingWeaponLarge = "SwingWeaponLarge",
					SwingWeaponSmall = "SwingWeaponSmall",
					TakeMoney = "TakeMoney",
					TakePicture = "TakePicture",
					TaserFire = "TaserFire",
					TaserFireFail = "TaserFireFail",
					TaserHitAgent = "TaserHitAgent",
					Teleport = "Teleport",
					ThrowItem = "ThrowItem",
					ThrowMoneyInWell = "ThrowMoneyInWell",
					TitleScreen = "TitleScreen",
					TitleScreenLogo = "TitleScreenLogo",
					ToiletPurge = "ToiletPurge",
					ToiletTeleportIn = "ToiletTeleportIn",
					ToiletTeleportOut = "ToiletTeleportOut",
					Track_A_Tutorial_Start = "Track_A_Tutorial_Start",
					Track_Hype_v4 = "Track_Hype_v4",
					Train = "Train",
					TranquilizerFire = "TranquilizerFire",
					TranquilizerHitAgent = "TranquilizerHitAgent",
					TrapDoorClose = "TrapDoorClose",
					TreeDestroy = "TreeDestroy",
					TreeHit = "TreeHit",
					TreeHit2 = "TreeHit2",
					TripLaser = "TripLaser",
					TripLaser2 = "TripLaser2",
					TVAmbience = "TVAmbience",
					UnHide = "UnHide",
					UseBlindenizer = "UseBlindenizer",
					UseBooUrn = "UseBooUrn",
					UseCigaretteLighter = "UseCigaretteLighter",
					UseCigarettes = "UseCigarettes",
					UseCocaine = "UseCocaine",
					UseCologne = "UseCologne",
					UseDeliveryApp = "UseDeliveryApp",
					UseDrink = "UseDrink",
					UseEarwarpWhistle = "UseEarwarpWhistle",
					UseExplosiveStimulator = "UseExplosiveStimulator",
					UseFireworks = "UseFireworks",
					UseFood = "UseFood",
					UseItemTeleporter = "UseItemTeleporter",
					UseItemTeleporter2 = "UseItemTeleporter2",
					UseItemTeleporter3 = "UseItemTeleporter3",
					UseMemoryEraser = "UseMemoryEraser",
					UseNecronomicon = "UseNecronomicon",
					UseSyringe = "UseSyringe",
					UseWalkieTalkie = "UseWalkieTalkie",
					WallDestroy = "WallDestroy",
					WallDestroyGlass = "WallDestroyGlass",
					WallDestroyGlass1 = "WallDestroyGlass1",
					WallDestroyGlass2 = "WallDestroyGlass2",
					WallDestroyGlass3 = "WallDestroyGlass3",
					WaterBlastEnd = "WaterBlastEnd",
					WaterBlastFire = "WaterBlastFire",
					WaterBlastLoop = "WaterBlastLoop",
					WaterCannonEnd = "WaterCannonEnd",
					WaterCannonFire = "WaterCannonFire",
					WaterCannonLoop = "WaterCannonLoop",
					WaterFire = "WaterFire",
					WaterHit = "WaterHit",
					WaterPistolFire = "WaterPistolFire",
					WaterPistolHitAgent = "WaterPistolHitAgent",
					WerewolfSlash1Old = "WerewolfSlash1-Old",
					WerewolfSlash1 = "WerewolfSlash1",
					WerewolfSlash2Old = "WerewolfSlash2-Old",
					WerewolfSlash2 = "WerewolfSlash2",
					WerewolfTransform = "WerewolfTransform",
					WerewolfTransformBack = "WerewolfTransformBack",
					Whoosh2 = "Whoosh2",
					Win = "Win",
					WindowDamage = "WindowDamage",
					WithdrawalDamage = "WithdrawalDamage",
					WithdrawalStart = "WithdrawalStart",
					ZombieSpitCharge = "ZombieSpitCharge",
					ZombieSpitFire = "ZombieSpitFire";
		}
		public static class vButtonText // Vanilla Button Text
		{
			public const string
				AlarmButtonAncapistan = "AlarmButtonAncapistan",
				ElevatorGoUp = "ElevatorGoUp",
				FlushYourself = "FlushYourself",
				RefillWaterCannon = "RefillWaterCannon",
				TossCoin = "TossCoin"
				;
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
		public static class vExplosion // Vanilla Explosion Types
		{
			public const string
					Big = "Big",
					Dizzy = "Dizzy",
					EMP = "EMP",
					Molotov = "Firebomb",
					Huge = "Huge",
					NoiseOnly = "Knocker",
					MindControl = "MindControl",
					Normal = "Normal",
					Ooze = "Ooze",
					PowerSap = "PowerSap",
					Ridiculous = "Ridiculous",
					Slime = "Slime",
					Stomp = "Stomp",
					Warp = "Warp",
					Water = "Water";
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
		public static class vItem // Vanilla Items
		{
			public const string
						AccuracyMod = "AccuracyMod",
						AmmoCapacityMod = "AmmoCapacityMod",
						AmmoProcessor = "AmmoProcessor",
						AmmoStealer = "AmmoStealer",
						Antidote = "Antidote",
						ArmorDurabilitySpray = "ArmorDurabilityDoubler",
						Axe = "Axe",
						BaconCheeseburger = "BaconCheeseburger",
						BalletShoes = "BalletShoes",
						Banana = "Banana",
						BananaPeel = "BananaPeel",
						BaseballBat = "BaseballBat",
						Beartrap = "BearTrap",
						BeartrapfromPark = "BearTrapPark",
						Beer = "Beer",
						BFG = "BFG",
						BigBomb = "BigBomb",
						Blindenizer = "Blindenizer",
						BloodBag = "BloodBag",
						Blowtorch = "Blowtorch",
						Blueprints = "Blueprints",
						BodySwapper = "BodySwapper",
						BodyVanisher = "BodyVanisher",
						BombProcessor = "BombMaker",
						BoomBox = "Boombox",
						BooUrn = "BooUrn",
						BraceletofStrength = "BraceletStrength",
						Briefcase = "Briefcase",
						BrokenCourierPackage = "CourierPackageBroken",
						BulletproofVest = "BulletproofVest",
						CardboardBox = "CardboardBox",
						Chainsaw = "Chainsaw",
						ChloroformHankie = "ChloroformHankie",
						CigaretteLighter = "CigaretteLighter",
						Cigarettes = "Cigarettes",
						CircuitBoard = "CircuitBoard",
						Cocktail = "Cocktail",
						Codpiece = "Codpiece",
						Cologne = "Cologne",
						CourierPackage = "CourierPackage",
						CritterUpper = "CritterUpper",
						Crowbar = "Crowbar",
						CubeOfLampey = "CubeOfLampey",
						CyanidePill = "CyanidePill",
						DeliveryApp = "DeliveryApp",
						DizzyGrenade = "GrenadeDizzy",
						DoorDetonator = "DoorDetonator",
						DrinkMixer = "DrinkMixer",
						EarWarpWhistle = "HearingBlocker",
						ElectroPill = "ElectroPill",
						ElectroTetherVest = "BodyguardTether",
						EMPGrenade = "GrenadeEMP",
						Evidence = "Evidence",
						Explodevice = "ExplosiveStimulator",
						FireExtinguisher = "FireExtinguisher",
						FireproofSuit = "FireproofSuit",
						Fireworks = "Fireworks",
						FirstAidKit = "FirstAidKit",
						Fist = "Fist",
						FiveLeafClover = "FiveLeafClover",
						Flamethrower = "Flamethrower",
						FlamingSword = "FlamingSword",
						Flask = "Flask",
						FoodProcessor = "FoodProcessor",
						Forcefield = "ForceField",
						FourLeafClover = "FourLeafClover",
						FreeItemVoucher = "FreeItemVoucher",
						FreezeRay = "FreezeRay",
						FriendPhone = "FriendPhone",
						Fud = "Fud",
						GasMask = "GasMask",
						GhostGibber = "GhostBlaster",
						Giantizer = "Giantizer",
						GrapplingHook = "GrapplingHook",
						Grenade = "Grenade",
						GuidedMissileLauncher = "GuidedMissileLauncher",
						HackingTool = "HackingTool",
						HamSandwich = "HamSandwich",
						HardDrive = "HardDrive",
						HardHat = "HardHat",
						Haterator = "Haterator",
						HiringVoucher = "HiringVoucher",
						HotFud = "HotFud",
						Hypnotizer = "Hypnotizer",
						HypnotizerII = "Hypnotizer2",
						IdentifyWand = "IdentifyWand",
						IncriminatingPhoto = "IncriminatingPhoto",
						ItemTeleporter = "ItemTeleporter",
						Jackhammer = "Jackhammer",
						Key = "Key",
						KeyCard = "KeyCard",
						KillAmmunizer = "KillProfiterAmmo",
						KillerThrower = "KillerThrower",
						KillHealthenizer = "KillProfiterHealth",
						KillProfiter = "KillProfiter",
						Knife = "Knife",
						KnockerGrenade = "GrenadeKnocker",
						KnockerMelee = "KnockerMelee",
						LandMine = "LandMine",
						Laptop = "Laptop",
						LaserBlazer = "LaserBlazer",
						LaserGun = "LaserGun",
						Leafblower = "LeafBlower",
						Lockpick = "Lockpick",
						MacGuffinMuffin = "MacguffinMuffin",
						MachineGun = "MachineGun",
						MagicLamp = "MagicLamp",
						MayorHat = "MayorHat",
						MayorsMansionGuestBadge = "MayorBadge",
						MechKey = "MechTransformItem",
						MeleeDurabilitySpray = "MeleeDurabilityDoubler",
						MemoryMutilator = "MemoryEraser",
						MindReaderDevice = "MindReaderDevice",
						MiniFridge = "MiniFridge",
						MolotovCocktail = "MolotovCocktail",
						Money = "Money",
						MonkeyBarrel = "MonkeyBarrel",
						MoodRing = "MoodRing",
						MusclyPill = "Steroids",
						Necronomicon = "Necronomicon",
						OilContainer = "OilContainer",
						ParalyzerTrap = "ParalyzerTrap",
						Pistol = "Pistol",
						PlasmaSword = "PlasmaSword",
						PoliceBaton = "PoliceBaton",
						PortableSellOMatic = "PortableSellOMatic",
						PossessionStone = "Depossessor",
						PowerDrill = "PowerDrill",
						PropertyDeed = "PropertyDeed",
						QuickEscapeTeleporter = "QuickEscapeTeleporter",
						RagePoison = "RagePoison",
						RateofFireMod = "RateOfFireMod",
						RecordofEvidence = "MayorEvidence",
						RemoteBomb = "RemoteBomb",
						RemoteBombTrigger = "BombTrigger",
						ResearchGun = "ResearchGun",
						ResurrectionShampoo = "ResurrectionShampoo",
						Revolver = "Revolver",
						Rock = "Rock",
						RocketLauncher = "RocketLauncher",
						RubberBulletsMod = "RubberBulletsMod",
						SafeBuster = "SafeBuster",
						SafeCombination = "SafeCombination",
						SafeCrackingTool = "SafeCrackingTool",
						Shotgun = "Shotgun",
						Shovel = "Shovel",
						Shrinker = "Shrinker",
						ShrinkRay = "ShrinkRay",
						Shuriken = "Shuriken",
						SignedBaseball = "SignedBaseball",
						Silencer = "Silencer",
						SixLeafClover = "SixLeafClover",
						SkeletonKey = "SkeletonKey",
						SlaveHelmet = "SlaveHelmet",
						SlaveHelmetRemote = "SlaveHelmetRemote",
						SlaveHelmetRemover = "SlaveHelmetRemover",
						Sledgehammer = "Sledgehammer",
						SniperRifle = "SniperRifle",
						SoldierHelmet = "SoldierHelmet",
						StickyGlove = "StealingGlove",
						StickyMine = "StickyMine",
						Sugar = "Cocaine",
						Sword = "Sword",
						Syringe = "Syringe",
						Taser = "Taser",
						Teleporter = "Teleporter",
						TimeBomb = "TimeBomb",
						Tooth = "Tooth",
						TranquilizerGun = "TranquilizerGun",
						Translator = "Translator",
						TripMine = "TripMine",
						VoodooDoll = "VoodooDoll",
						WalkieTalkie = "WalkieTalkie",
						WallBypasser = "WallBypasser",
						WarpGrenade = "GrenadeWarp",
						WarpZoner = "WarpZoner",
						WaterCannon = "WaterCannon",
						WaterPistol = "WaterPistol",
						Whiskey = "Whiskey",
						Will = "Will",
						WindowCutter = "WindowCutter",
						Wrench = "Wrench";

				public static List<string> alcohol = new List<string>()
			{
					Beer,
					Cocktail,
					Whiskey
			};

				public static List<string> drugs = new List<string>()
			{
					Antidote,
					Cigarettes,
					Sugar,
					CritterUpper,
					CyanidePill,
					ElectroPill,
					Giantizer,
					KillerThrower,
					RagePoison,
					Shrinker,
					MusclyPill,
					Syringe
			};

				public static List<string> nonVegetarian = new List<string>()
			{
					BaconCheeseburger,
					HamSandwich
			};

				public static List<string> vegetarian = new List<string>()
			{
					Beer,
					Banana,
					Cocktail,
					Fud,
					HotFud,
					Whiskey
			};

			public static List<string> blunt = new List<string>()
			{ };

			public static List<string> explosive = new List<string>()
			{ };

			public static List<string> heavy = new List<string>()
			{
					Axe,
					BaseballBat,
					Beartrap,
					BulletproofVest,
					Crowbar,
					FireExtinguisher,
					FireproofSuit,
					Flamethrower,
					GhostGibber,
					LandMine,
					MachineGun,
					Revolver,
					RocketLauncher,
					Shotgun,
					Sledgehammer,
					Wrench
			};

			public static List<string> loud = new List<string>()
			{
					BoomBox,
					DizzyGrenade,
					DoorDetonator,
					EMPGrenade,
					Explodevice,
					FireExtinguisher,
					Fireworks,
					GhostGibber,
					Grenade,
					EarWarpWhistle,
					Leafblower,
					LandMine,
					MachineGun,
					MolotovCocktail,
					Pistol,
					RemoteBomb,
					Revolver,
					RocketLauncher,
					Shotgun,
					TimeBomb,
					WarpGrenade
			};

			public static List<string> piercing = new List<string>()
			{
					Axe,
					Beartrap,
					Grenade,
					Knife,
					LandMine,
					MachineGun,
					Pistol,
					Revolver,
					RocketLauncher,
					Shotgun,
					Shuriken,
					Sword
			};

			public static List<string> tools = new List<string>()
			{
					Crowbar,
					Wrench,
			};
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
		public static class vNameType // Vanilla Name Types
		{
			public const string
					Agent = "Agent",
					Dialogue = "Dialogue",
					Description = "Description",
					Interface = "Interface",
					Item = "Item",
					Object = "Object",
					StatusEffect = "StatusEffect",
					Unlock = "Unlock";
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
					Fountain = "Fountain",
					GasVent = "GasVent",
					Generator = "Generator",
					Generator2 = "Generator2",
					Gravestone = "Gravestone",
					Hole = "Hole",
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
		public static class vStatusEffect // Vanilla Status Effects
		{
			public const string
					AbovetheLaw = "AboveTheLaw",
					AccuracyBoosted = "Accurate",
					Acid = "Acid",
					AllStatsDecreased = "DecreaseAllStats",
					AlwaysCrit = "AlwaysCrit",
					AmmoProcessor = "AmmoProcessor",
					BadVision = "BadVision",
					BoomBox = "Boombox",
					ChargeLevel1 = "ChargeLevel1",
					ChargeLevel2 = "ChargeLevel2",
					ChargeLevel3 = "ChargeLevel3",
					ChargeLevel4 = "ChargeLevel4",
					Confused = "Confused",
					CopDebt1 = "OweCops1",
					CopDebt2 = "OweCops2",
					CritterUpper = "CritterUpper",
					Cyanide = "Cyanide",
					DeliveringPackage = "DeliverPackage",
					Dizzy = "Dizzy",
					DNAConnection = "ZombieSpirit",
					Drunk = "Drunk",
					Electrocuted = "Electrocuted",
					ElectroTouch = "ElectroTouch",
					Fast = "Fast",
					FeelingGood = "FeelingGood",
					FeelingLucky = "FeelingLucky",
					FeelingUnlucky = "FeelingUnlucky",
					Frozen = "Frozen",
					Giant = "Giant",
					HearingBlocked = "HearingBlocked",
					IgnoreLasers = "IgnoreLasers",
					IncreaseAllStats = "IncreaseAllStats",
					InDebt1 = "InDebt",
					InDebt2 = "InDebt2",
					InDebt3 = "InDebt3",
					Invincible = "Invincible",
					Invisible = "Invisible",
					InvisiblePermanent = "InvisiblePermanent",
					InvisibleTemporary = "InvisibleLimited",
					KillerThrower = "KillerThrower",
					Loud = "Loud",
					MindControlling = "MindControlling",
					NiceSmelling = "NiceSmelling",
					Nicotine = "Nicotine",
					NumbtoPain = "NumbToPain",
					OnFire = "OnFire",
					Paralyzed = "Paralyzed",
					Poisoned = "Poisoned",
					Rage = "Enraged",
					RegenerateHealth = "RegenerateHealth",
					RegenerateHealthFaster = "RegenerateHealthFaster",
					ResistBulletsSmall = "ResistBulletsSmall",
					ResistDamageLarge = "ResistDamageLarge",
					ResistDamageMedium = "ResistDamageMed",
					ResistDamageSmall = "ResistDamageSmall",
					ResistFire = "ResistFire",
					Resurrection = "Resurrection",
					RevenueExtortion = "Revenue",
					Shrunk = "Shrunk",
					SlaveHelmetRemover = "SlaveHelmetRemover",
					Slow = "Slow",
					StableSystem = "BlockDebuffs",
					Strength = "Strength",
					SuperDizzy = "DizzyB",
					Tranquilized = "Tranquilized",
					WallBypasser = "WallBypasser",
					Weak = "Weak",
					Werewolf = "WerewolfEffect",
					Withdrawal = "Withdrawal";
		}
		public static class vTrait // Vanilla Traits
		{
			public const string
					AbovetheLaw = "AboveTheLaw",
					Accurate = "Accurate",
					Addict = "Addict",
					Aftershocked = "StompDamagesAgents",
					AmmoScavenger = "MoreAmmoInDroppedWeapons",
					Antisocial = "NoFollowers",
					ArmyofFive = "ZombieArmy",
					ArtoftheDeal = "ArtOfTheDeal",
					Backstabber = "Backstabber",
					BananaLover = "BananaLover",
					BenevolentOwner = "SlavesDontMutiny",
					BigBang = "BigRemoteBombExplosions",
					BigBullets = "BigBullets",
					BlahdBasher = "HatesBlahds",
					BlasterMaster = "ExplosionsBreakEverything",
					BlasterSurvivor = "ExplosionsDontDamageCauser",
					BlazingLasers = "LaserGunChargesFaster",
					BlendsInNicely = "HardToSeeFromDistance",
					BlockBreaker = "BlocksSometimesHit",
					BlockBullets = "MeleeHoldDeflectsBullets",
					BloodofSteel = "MusicianTakesLessHealth",
					Bloodlust = "Bloodlust",
					BloodyMess = "BloodyMess",
					BodySwapper = "PossessShorterCooldown",
					Bodyguard = "Bodyguard",
					BombBaker = "LowerCostRemoteBombs",
					BottomlessStomach = "BiteFullHealth",
					Bulky = "BigCollider",
					BulletBreaker = "BulletsDestroyOtherBullets",
					BulletSponge = "ResistBulletsTrait",
					BurningBullets = "BulletsCauseFire",
					Butterfingerer = "ChanceToKnockWeapons",
					CameraShy = "InvisibleToCameras",
					CardboardBoxLike = "StandingStillCausesCamouflage",
					Charismatic = "Likeable",
					ChipmunkTeeth = "BiteFaster",
					ClassSolidarity = "DontHitOwnKind",
					ClumsinessForgiven = "NoOwnCheckOnBreak",
					ComputerIlliterate = "NoTechSkill",
					ConfidentinCrowds = "MoreFollowersCauseMoreDamage",
					Confused = "Confused",
					CoolwithCannibals = "CannibalsNeutral",
					CopsDontCare = "CopsDontCare",
					CorruptionCosts = "MustPayCops",
					CovertCrook = "AgentsDontSeePickpocket",
					CrepeCrusher = "HatesCrepes",
					Crooked = "LessArrestXPLoss",
					Crooked2 = "NoArrestXPLoss",
					CyberNuke = "HacksBlowUpObjects",
					DeeperPockets = "PickpocketMoreMoney",
					DestructiveDeaths = "BiggerSlaveHelmetExplosions",
					Diminutive = "Diminutive",
					DisturbingFacialExpressions = "ScareEnemiesEasier",
					Dizzy = "Dizzy",
					DontMakeMeAngry = "MoreDamageWhenHealthLow",
					Drugalug = "IdentifyScience",
					Durabilitacious = "MeleeLastLonger",
					EggshellWalker = "JokesNeverCauseHate",
					Electronic = "Electronic",
					Extortionist = "Shakedowner",
					Extortionist_2 = "Shakedowner2",
					FairGame = "EveryoneHatesZombie",
					FastFood = "CannibalizeFaster",
					FeatureAct = "JokesMoreSuccessful/JokesAlwaysSuccessful", // TODO actually two traits
					FireproofSkin = "FireproofSkin",
					FireproofSkin2 = "FireproofSkin2",
					FleshFeast = "FleshFeast",
					FloatsLikeButterfly = "MeleeMobility",
					FoolproofHacks = "HackingMakesNoSound",
					FriendoftheCommonFolk = "GenericAgentsAligned",
					FriendoftheFamily = "MafiaAligned",
					Frozen = "Frozen",
					GPYesss = "MapFilled",
					GoodVibrations = "BiggerStompRadius",
					Graceful = "DontTriggerFloorHazards",
					Groupies = "BiggerMindControlRadius",
					Harmless = "CantAttack",
					HeartStopper = "EnemiesDieWhenFleeing",
					HomesicknessKiller = "AgentsFollowToNextLevel",
					HonorAmongThieves = "HonorAmongThieves",
					HonorableChallenges = "ChallengeAnyoneToFight",
					ImOuttie = "FastWhenHealthLow",
					ImOuttie_2 = "FastWhenHealthLow2",
					IdeologicalClash = "RandomPeopleSecretHate",
					ImpatientLunge = "FasterLunge",
					IncreasedCritChance = "IncreasedCritChance",
					InfectiousSpirit = "FollowersExtraHealth",
					InfernoAssailant = "FireExtinguishXP",
					InhumanStrength = "ZombiesStronger",
					IntrusionArtist = "ThiefToolsMayNotSubtract",
					JackofExtraTrades = "MoreTraitChoices",
					Juggernaut = "ChargeNoHealthLoss",
					Jugularious = "BloodRestoresHealth",
					KillerThrower = "KillerThrower",
					KillingTime = "WerewolfLastLonger",
					Kneecapper = "ChanceToSlowEnemies",
					KnockbackKing = "CauseBiggerKnockback",
					Knuckley = "StrongFists",
					Knuckley_2 = "StrongFists2",
					LeisurelyRide = "MoreTimeForDeliveries",
					LockandLoad = "ReloadWeaponsNewLevel",
					LonelinessKiller = "StartWithFollowers",
					LongLunge = "LongLunge",
					LongerStatusEffects = "StatusEffectsLonger",
					Loud = "Loud",
					LowCenterofGravity = "ChargeNoTrip",
					LowCostJobs = "LowcostJobs",
					Malodorous = "Unlikeable",
					MasterofDisaster = "DestructionXP",
					MedicalProfessional = "MedicalProfessional",
					ModernWarfarer = "RegenerateHealthWhenLow",
					Moocher = "CanBorrowMoney",
					Mugger = "Mugger",
					Naked = "Naked",
					NearHarmless = "AttacksOneDamage",
					NimbleFingers = "OperateQuickly",
					NoHarminDying = "NoDepossessHealthLoss",
					NoInFighting = "DontHitAligned",
					NoTeleports = "CantTeleport",
					OilLessEssential = "OilRestoresMoreHealth",
					OilReliant = "OilRestoresHealth",
					OntheHouse = "ChanceFreeShopItem",
					Pacifist = "CantUseWeapons",
					PeaBrained = "CantInteract",
					PenetratingBullets = "BulletsPassThroughObjects",
					PoorHandEyeCoordination = "OperateSlowly",
					PossessionsarePeachyKeen = "NoPossessHate",
					PotentialtoNotSuck = "IncreaseStatEvery2Levels",
					PowerLasers = "LaserMorePowerful",
					PowerWalkers = "MindControlledWalkThroughWalls",
					PricklySkin = "AttacksDamageAttacker",
					PromiseIllReturnIt = "NoStealPenalty",
					PuppetPower = "MindControlledDamageMore",
					QuickandDead = "ZombiesFaster",
					QuickTranq = "TranqDartsWorkFaster",
					Rampager = "BuffFromMultipleKills",
					RandomReverence = "RandomPeopleAligned",
					Rechargeable = "Rechargeable",
					ResearchGunRadiation = "ResearchGunStatusEffects",
					ResistBullets = "ResistBullets",
					ResistBulletsLarge = "ResistDamageLarge",
					ResistBulletsMedium = "ResistDamageMed",
					ResistBulletsSmall = "ResistBulletsSmall",
					ResistDamageSmall = "ResistDamageSmall",
					ResistFire = "ResistFire",
					ResistGas = "ResistGas",
					RestlessBeast = "WerewolfShorterCooldown",
					RiotCannon = "StrongerWaterCannon",
					RipandTear = "HigherWerewolfDamage",
					RollerSkates = "RollerSkates",
					RubberBullets = "BulletKnockouts",
					SafeinCrowds = "MoreFollowersLessDamageToPlayer",
					SafeStomp = "StompLessDamage",
					SapDamage = "MoreDamagePowerSap",
					SappyHealthy = "MoreHealthFromPowerSap",
					SausageFingers = "CantUseWeapons2",
					Savorer = "CannibalizeMoreHealth",
					ScientistSlayer = "HatesScientist",
					ScorchingSavior = "FightsFires",
					ScumbagSlaughterer = "MechHateTrait",
					SecretVandalizer = "HitObjectsNoNoise",
					ServeDrinks = "ServeDrinks",
					SharetheHealth = "HealthItemsGiveFollowersExtraHealth",
					ShopDrops = "VendorsDropShopItem",
					SkinnyNerdlinger = "KnockbackMore",
					SleepKiller = "SleepKiller",
					Slinky = "BumpsDontEndCamouflage",
					SlipperyTarget = "HardToShoot",
					SneakyBomber = "NPCsDontNoticeRemoteBombs",
					SneakyFingers = "OperateSecretly",
					Specist = "HatesGorilla",
					SpeedCoder = "HackImmediate",
					Sprinter = "FasterCharge",
					SteadfastSlaves = "SlavesFightForYou",
					SteadyHead = "WerewolfNoDizzy",
					StrictCannibal = "CannibalizeRestoresHealth",
					StubbyFingers = "CantUseGuns",
					Studious = "MoreSkillPoints",
					SuperStudious = "MoreSkillPoints2",
					SubduingSpree = "NoChloroformCooldown",
					Sucker = "BadTrader",
					SuperDizzy = "DizzyB",
					Surging = "MoreMoneyFromDeliveries",
					Suspicious = "Suspicious",
					SwiftSpitter = "FasterZombieSpitCharge",
					SwiftWolf = "HigherWerewolfSpeed",
					TableManners = "CannibalizeNoAnnoy",
					Tackler = "ChargeMorePowerful",
					TeamBuildingExpert = "MoreFollowers",
					TechExpert = "TechExpert",
					TeleportHappy = "TeleportAnytime",
					TheLaw = "TheLaw",
					TheyreJustKissing = "BiteNoAnnoy",
					ThickSkinnedPawn = "MindControlledResistDamage",
					ThroatofIron = "NoZombieSpitHealthLoss",
					TrustFunder = "MoneyAtLevelStart",
					UltimateButterfingerer = "KnockWeapons",
					UnCrits = "ChanceAttacksDoZeroDamage",
					UnCrits_2 = "ChanceAttacksDoZeroDamage2",
					UpperCrusty = "UpperCrusty",
					VeinTapper = "BiteGainMoreHealth",
					ViciousChameleon = "FailedAttacksDontEndCamouflage",
					VocallyChallenged = "CantSpeakEnglish",
					WallWalloper = "MeleeDestroysWalls",
					WallsWorstNightmare = "MoreKnockingThroughWalls",
					Wanted = "Naked",
					WerewolfAWereness = "WerewolfAwareness",
					WrongBuilding = "OwnersNotHostile",
					Zombiism = "Zombify";
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

			public static List<string> Fence = new List<string>()
			{
				BarbedWire,
				Bars
			};
			public static List<string> Structural = new List<string>()
			{
				Brick,
				Steel,
				Wood
			};
		}
		public static class vWallGroup // Vanilla Wall Groups
		{
			public const string
					Hideout = "WallsHideout",
					Normal = "WallsNormal",
					Strong = "WallsStrong",
					Weak = "WallsWeak";
		}
		#endregion
	}
}