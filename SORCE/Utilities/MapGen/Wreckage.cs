using BepInEx.Logging;
using Light2D;
using SORCE.Challenges;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_VFX;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Resources;
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
			DebugTools.debugMode ||
			GC.challenges.Contains(nameof(Arcology)) || // Leaves
			GC.challenges.Contains(nameof(FloralerFlora));
		public static bool HasPrivateLitter =>
			DebugTools.debugMode ||
			GC.challenges.Contains(nameof(BachelorerPads));
		public static bool HasPublicLitter =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			DebugTools.debugMode ||
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(Arcology)) || // Leaves
			GC.challenges.Contains(nameof(DirtierDistricts)) ||
			GC.challenges.Contains(nameof(DUMP)) || // Rock Debris (FlamingBarrel)
			GC.challenges.Contains(nameof(Eisburg)) || // Ice chunks
			GC.challenges.Contains(nameof(Tindertown)) /*Ash*/;
		public static bool HasObjectExtraWreckage;

		internal static void SpawnPublicLitter()
		{
			if (!HasPublicLitter)
				return;

			logger.LogInfo("Spawning Litter");

			int numObjects = (int)(2400 * LevelGenTools.SlumminessFactor * LevelSize.ChunkCountRatio);

            for (int i = 0; i < numObjects; i++)
            {
                Vector2 location = LevelGenTools.RandomSpawnLocation(GC.tileInfo, 0.56f);

				if (GC.percentChance(100)) // Temporarily disabling custom trash
					SpawnWreckagePileObject_Granular(
						location,
						OverhaulWreckageType(),
						burnt: GC.percentChance(15),
						gibs: 1,
						0.32f, 0.32f,
						particleID: 0);
				else
					SpawnCustomLitter(
						location,
						WreckageCustom.RandomElement(),
						burnt: false,
						gibs: 1,
						0.32f, 0.32f,
						particleID: 0);
			}
		}

		// TODO: This belongs in the library
		public static void SpawnWreckagePileObject_Granular(Vector3 origin, string objectType, bool burnt, int gibs, float radX, float radY, int particleID = 0)
		{
			if (objectType == VObject.Window)
			{
				gibs = Random.Range(4, 6);
				burnt = false;
				radX = 0.32f;
				radY = 0.32f;
			}

			for (int i = 0; i < gibs; i++)
			{
				bool goodSpot = false;
				Vector3 spawnLoc = Vector3.zero;

				for (int j = 0; j < 1000 && !goodSpot; j++)
				{
					spawnLoc = new Vector3(
						origin.x + Random.Range(-radX, radX),
						origin.y + Random.Range(-radY, radY),
						0f);

					bool isPublic = LevelGenTools.IsPublic(spawnLoc);

					if (!
						(GC.tileInfo.IsOverlapping(spawnLoc, "Wall") ||
						(!HasPublicLitter && isPublic) ||
						(!HasPrivateLitter && !isPublic)))
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

		public static void SpawnCustomLitter(Vector3 origin, string spriteGroup, bool burnt, int gibs, float radX, float radY, int particleID = 0)
		{
			logger.LogDebug("SpawnCustomLitter");
	
			for (int i = 0; i < gibs; i++)
			{
				bool goodSpot = false;
				Vector3 spawnLoc = Vector3.zero;

				for (int j = 0; j < 1000 && !goodSpot; j++)
				{
					spawnLoc = new Vector3(
						origin.x + Random.Range(-radX, radX),
						origin.y + Random.Range(-radY, radY),
						0f);

					bool isPublic = LevelGenTools.IsPublic(spawnLoc);

					if (!
						(GC.tileInfo.IsOverlapping(spawnLoc, "Wall") ||
						(!HasPublicLitter && isPublic) ||
						(!HasPrivateLitter && !isPublic)))
						goodSpot = true;
				}

				logger.LogDebug("goodSpot: " + goodSpot);

				string spriteName = spriteGroup +
					(particleID != 0
						? particleID.ToString()
						: (Random.Range(1, 5)).ToString());

				if (!SpriteLoader.SpriteGroups.Contains(spriteGroup))
					spriteName = spriteGroup;

				if (spawnLoc != Vector3.zero)
                {

					Item trash = GC.spawnerMain.wreckagePrefab.Spawn(spawnLoc);
					trash.DoEnable();
					trash.isWreckage = true;
					//trash.invItem.itemType = "NonItem";
					trash.itemName = "CustomWreckage";
					tk2dSprite component = trash.tr.GetChild(0).transform.GetChild(0).GetComponent<tk2dSprite>();
					component.SetSprite(spriteName);
					component.transform.localPosition = spawnLoc;
					//Movement movement = trash.GetComponent<Movement>();
					//trash.animator.Play("ItemJump 1", -1, 0f);
					//movement.Spill(120, null, null); // Handles null towardObject
					trash.FakeStart();
				}

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

		public static string CustomLitterType() =>
			WreckageCustom.RandomElement();

		public static string FoodWreckageType() =>
			WreckageFood.RandomElement();

		public static void ThrowTrash(Vector3 origin, string spriteName, GameObject towardObject = null, bool rotate = true)
		{
			Vector3 vector = new Vector3(origin.x, origin.y, Random.Range(-0.78f, -1.82f));

			Item trash = GC.spawnerMain.wreckagePrefab.Spawn(vector);
			trash.itemName = "CustomWreckage"; // So far, to avoid SetLighting2 errors. Test
			trash.DoEnable();
			trash.isWreckage = true;
			trash.justSpilled = true;
			if (rotate) 
				trash.tr.Rotate(0, 0, Random.Range(0, 360));
			tk2dSprite component = trash.tr.GetChild(0).transform.GetChild(0).GetComponent<tk2dSprite>();
			component.SetSprite(spriteName);
			if (rotate) 
				component.transform.Rotate(0, 0, Random.Range(0, 360));
			component.transform.localPosition = Vector3.zero;
			Movement movement = trash.GetComponent<Movement>();
			//movement.SetPhysics("Ice"); Looks a little like rolling
			trash.animator.enabled = true;
			//trash.animator.Play("ItemJump 1", -1, 0f);
			movement.Spill(90, towardObject, null); // Handles null towardObject
			trash.FakeStart();
		}

		public static List<string> WreckageFood = new List<string>()
		{
			VObject.Shelf,
			VObject.VendorCart,
			VObject.BarStool,
		};
		public static List<string> WreckageCustom = new List<string>()
		{
			CSprite.BeerCan,
			CSprite.CigaretteButt,
			CSprite.FudJar,
			CSprite.FudJarScorched,
			CSprite.Hypo,
			CSprite.WhiskeyBottle,
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
