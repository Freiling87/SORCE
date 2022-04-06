using BepInEx.Logging;
using SORCE.Challenges;
using SORCE.Challenges.C_Overhaul;
using SORCE.Localization;
using SORCE.Logging;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.MapGenUtilities
{
    internal class Wreckage
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		internal static void SpawnLitter()
		{
			int numObjects = (int)(125 * LevelGenTools.SlumminessFactor * LevelSize.LevelSizeRatio());

			for (int i = 0; i < numObjects; i++)
			{
				// Vector2 location = GC.tileInfo.FindRandLocationGeneral(0f); // Vanilla 2f
				Vector2 location = LevelGenTools.RandomSpawnLocation(GC.tileInfo, 0.2f);

				GC.spawnerMain.SpawnWreckagePileObject(location, OverhaulWreckageType(), false);
			}
		}
		public static void SpawnWreckagePileObject_Granular(Vector3 targetLoc, string objectType, bool burnt, int gibs, float radX, float radY)
		{
			for (int i = 0; i < gibs; i++)
			{
				string wreckageType = objectType + "Wreckage" + (Random.Range(1, 5)).ToString();
				targetLoc = new Vector3(
					targetLoc.x + Random.Range(radX * -1, radX),
					targetLoc.y + Random.Range(radY * -1, radY), 0f);
				GC.spawnerMain.SpawnWreckage2(targetLoc, wreckageType, burnt);
			}
		}
		private static string OverhaulWreckageType()
		{
			// TODO: Call SpawnWreckagePileObject in here, because you need to determine whether trash is burnt or not
			if (ChallengeManager.IsChallengeFromListActive(CChallenge.Overhauls))
			{
				switch (ChallengeManager.GetActiveChallengeFromList(CChallenge.Overhauls))
				{
					case nameof(Arcology):
						return VObject.Bush;
					case nameof(DUMP): // Rock
						return VObject.FlamingBarrel;
					case nameof(Eisburg): // Ice chunks, but see notes to see if ice gibs are preferable
						return VObject.Toilet;
					case nameof(Hell): // Rock
						return VObject.FlamingBarrel;
					case nameof(Tindertown): // Ashes
						return VObject.Bush;
				}
			}

			// Regular trash
			return GC.Choose(VObject.Shelf, VObject.MovieScreen, VObject.Counter, VObject.VendorCart, VObject.Window);
		}
	}
}
