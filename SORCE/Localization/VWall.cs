using System.Collections.Generic;

namespace SORCE.Localization
{
    public static partial class NameLists
	{
        public static class VWall // Vanilla Walls
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
	}
}