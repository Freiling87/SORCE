using SORCE.Challenges.C_AmbientLightColor;
using SORCE.Challenges.C_AmbientLightLevel;
using System.Collections.Generic;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Localization
{
    public static class CColor
	{
		public static Color32 TestBlack = new Color32(0, 0, 0, 255);
		public static Color32 TestGreen = new Color32(0, 255, 0, 255);
		public static Color32 TestRed = new Color32(255, 0, 0, 255);
		public static Color32 TestBlue = new Color32(0, 0, 255, 255);
		public static Color32 TestPurple = new Color32(255, 0, 255, 255);
		public static Color32 TestWhite = new Color32(255, 255, 255, 255);



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
		public static List<string> AmbientLightLevel = new List<string>()
		{
			nameof(Blinding),
			nameof(Daytime),
			nameof(Evening),
			nameof(FullMoon),
			nameof(HalfMoon),
			nameof(NewMoon),
		};

		public static Color32 Red = new Color32(byte.MaxValue, byte.MinValue, byte.MinValue, 100);
		public static Color32 Orange = new Color32(byte.MaxValue, byte.MaxValue / 2, byte.MinValue, 100);
		public static Color32 Yellow = new Color32(byte.MaxValue, byte.MinValue, byte.MaxValue, 100);
		public static Color32 Green = new Color32(byte.MinValue, byte.MaxValue, byte.MinValue, 100);
		public static Color32 Blue = new Color32(byte.MinValue, byte.MinValue, byte.MaxValue, 100);
		public static Color32 Purple = new Color32(byte.MaxValue, byte.MinValue, byte.MaxValue, 100);

		public static Dictionary<string, Color32> RelitObjectColors = new Dictionary<string, Color32>
		{
			{ VObject.AlarmButton, Blue },
			{ VObject.Altar, Yellow },
			{ VObject.AmmoDispenser, Yellow },
			{ VObject.ArcadeGame, Red },
			{ VObject.ATMMachine, Yellow },
			{ VObject.AugmentationBooth, Green },
			{ VObject.CapsuleMachine, Red },
			{ VObject.CloneMachine, Red },
			{ VObject.Computer, Green },
			{ VObject.FlameGrate, Red },
			{ VObject.FlamingBarrel, Orange },
			{ VObject.Generator, Red },
			{ VObject.Generator2, Red },
			{ VObject.Jukebox, Purple },
			{ VObject.Lamp, Yellow },
			{ VObject.LaserEmitter, Blue },
			{ VObject.LoadoutMachine, Blue },
			{ VObject.PawnShopMachine, Green },
			{ VObject.PoliceBox, Blue },
			{ VObject.PowerBox, Yellow },
			{ VObject.SatelliteDish, Red },
			{ VObject.SecurityCam, Red },
			{ VObject.SlimeBarrel, Green },
			{ VObject.SlimePuddle, Green },
			{ VObject.SlotMachine, Red },
			{ VObject.Speaker, Blue },
			{ VObject.SwitchBasic, Red },
			{ VObject.Turntables, Red },
			{ VObject.Television, Blue },
			{ VObject.Turret, Red },
		};
	}
}