using BepInEx.Logging;
using Light2D;
using SORCE.Challenges;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Wreckage;
using SORCE.Localization;
using SORCE.Logging;
using System.Collections.Generic;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.MapGenUtilities
{
    internal class Wreckage
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool HasLeaves =>
			Core.debugMode ||
			GC.challenges.Contains(nameof(Arcology)) || // Leaves
			GC.challenges.Contains(nameof(FloralerFlora));
		public static bool HasPrivateLitter =>
			Core.debugMode ||
			GC.challenges.Contains(nameof(BachelorerPads));
		public static bool HasPublicLitter =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			Core.debugMode ||
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(Arcology)) || // Leaves
			GC.challenges.Contains(nameof(DirtierDistricts)) ||
			GC.challenges.Contains(nameof(DUMP)) || // Rock Debris (FlamingBarrel)
			GC.challenges.Contains(nameof(Eisburg)) || // Ice chunks
			GC.challenges.Contains(nameof(Tindertown)) /*Ash*/;

		internal static void SpawnPublicLitter()
		{
			if (!HasPublicLitter)
				return;

			bool publicOnly = HasPublicLitter && !HasPrivateLitter;
			bool privateOnly = HasPrivateLitter && !HasPublicLitter;

			int numObjects = (int)(250 * LevelGenTools.SlumminessFactor * LevelSize.ChunkCountRatio);

			for (int i = 0; i < numObjects; i++)
			{
				// Vector2 location = GC.tileInfo.FindRandLocationGeneral(0f); // Vanilla 2f
				Vector2 location = LevelGenTools.RandomSpawnLocation(GC.tileInfo, 0.24f, publicOnly, privateOnly);

				//GC.spawnerMain.SpawnWreckagePileObject(location, OverhaulWreckageType(), false);
				SpawnWreckagePileObject_Granular(
					location,
					OverhaulWreckageType(),
					burnt: GC.percentChance(20),
					gibs: Random.Range(1, 12),
					0.64f, 0.64f,
					particleID: 0,
					false, true);
			} 
		}

		// TODO: This belongs in the library
		public static void SpawnWreckagePileObject_Granular(Vector3 origin, string objectType, bool burnt, int gibs, float radX, float radY, int particleID = 0, bool avoidPublic = false, bool avoidPrivate = false)
		{
			for (int i = 0; i < gibs; i++)
			{
				bool goodSpot = false;
				Vector3 spawnLoc = Vector3.zero;

				while (!goodSpot)
				{
					spawnLoc = new Vector3(
						origin.x + Random.Range(-radX, radX),
						origin.y + Random.Range(-radY, radY), 
						0f);

					bool isPublic = LevelGenTools.IsPublic(spawnLoc);

					if (!GC.tileInfo.IsOverlapping(spawnLoc, "Wall") &&
						!(avoidPublic && isPublic) &&
						!(avoidPrivate && !isPublic))
						goodSpot = true;
				}

				string wreckageType = objectType + "Wreckage" +
					(particleID != 0
						? particleID.ToString()
						: (Random.Range(1, 5)).ToString());

				if (spawnLoc != Vector3.zero)
					GC.spawnerMain.SpawnWreckage2(spawnLoc, wreckageType, burnt);
			}
		}

		public static string OverhaulWreckageType()
		{
			// TODO: Call SpawnWreckagePileObject in here instead of in SpawnLitter
			// because you need to determine whether trash is burnt or not
			// TODO: Wreckage types are fields for Overhaul challenge type
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
			return WreckageMisc.RandomElement();
		}

		public static string FoodWreckageType() =>
			WreckageFood.RandomElement();

		public static List<string> WreckageFood = new List<string>()
		{
			VObject.Shelf,
			VObject.VendorCart,
			VObject.BarStool,
		};
		public static List<string> WreckageMisc = new List<string>()
		{
			VObject.BarStool,
			VObject.MovieScreen, VObject.MovieScreen,
			VObject.Shelf,
			VObject.Stove,
			VObject.Television,
			VObject.TrashCan,
			VObject.VendorCart,
			VObject.Window, VObject.Window
		};
		public static List<string> WreckageWood = new List<string>()
		{
			VObject.Chair,
			VObject.Shelf,
			VObject.Table,
			VObject.TableBig,
		};
	}
}
