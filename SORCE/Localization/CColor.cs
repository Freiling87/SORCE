using SORCE.Challenges.C_AmbientLightColor;
using SORCE.Challenges.C_AmbientLightLevel;
using System.Collections.Generic;
using UnityEngine;

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