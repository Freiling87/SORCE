using SORCE.Challenges.C_AmbientLightLevel;
using System.Collections.Generic;
using UnityEngine;

namespace SORCE.Localization
{
    public static class CColor
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
			{ nameof(Goodsprings),      Goodsprings },
			{ nameof(Hellscape),        Hellscape },
			{ nameof(NuclearWinter),    NuclearWinter },
			{ nameof(Reactor),          Reactor },
			{ nameof(Sepia),            Sepia },
			{ nameof(ShadowRealm),      ShadowRealm },
			{ nameof(Shinobi),          Shinobi },
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
			// Convert these to inherited classes with fields
			{ nameof(Blinding), 255 },
			{ nameof(Daytime),  200 },
			{ nameof(Evening),  150 },
			{ nameof(FullMoon), 100 },
			{ nameof(HalfMoon), 50 },
			{ nameof(NewMoon),  0 },
		};
	}
}