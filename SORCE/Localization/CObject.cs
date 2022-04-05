using System.Collections.Generic;

namespace SORCE.Localization
{
    public static partial class NameLists
	{
        public static class CObject
		{
			public static List<string> WreckageMisc = new List<string>()
			{
					VObject.BarStool,
					VObject.MovieScreen,
					VObject.Shelf,
			};

			public static List<string> WreckageOrganic = new List<string>() // All should have gibs with visible burn
			{
					VObject.Chair,
					VObject.Shelf,
					VObject.Table,
					VObject.TableBig,
			};
		}
	}
}