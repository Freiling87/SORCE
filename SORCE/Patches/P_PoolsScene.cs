using BepInEx.Logging;
using HarmonyLib;
using Light2D;
using RogueLibsCore;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Wreckage;
using SORCE.Logging;
using SORCE.MapGenUtilities;
using System;
using System.Linq;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(PoolsScene))]
    class P_PoolsScene
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

		/// <summary>
		/// Wreckage
		/// </summary>
		/// <param name="objectRealName"></param>
		/// <param name="objectRealPrefab"></param>
		/// <param name="spawnPosition"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(PoolsScene.SpawnObjectReal), argumentTypes: new Type[] {typeof(string), typeof(GameObject), typeof(Vector3)} )]
        public static void SpawnObjectReal_Postfix(string objectRealName, GameObject objectRealPrefab, Vector3 spawnPosition)
        {
			int trashLevelInverse = GC.levelTheme; // 0 = Home Base, 5 = Mayor Village 
			int chance = 100;
			bool avoidPublic = !Wreckage.HasPublicLitter;
			bool avoidPrivate = !Wreckage.HasPrivateLitter;
			bool isPublicObject = LevelGenTools.IsPublic(spawnPosition);

			if (Wreckage.HasLeaves)
				switch (objectRealName)
				{
					case VObject.Bush:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.Bush,
								false,
								5,
								0.64f, 0.64f);
							chance -= 20;
						}

						break;

					case VObject.KillerPlant:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.Bush,
								false,
								5,
								0.64f, 0.64f);
							chance -= 20;
						}

						break;

					case VObject.Plant:
						chance -= 25;

						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.16f),
								VObject.Plant,
								false,
								1,
								0.32f, 0.24f,
								Random.Range(3, 5)); // Leaves only

							chance -= 25;
						}

						break;

					case VObject.Tree:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.Bush,
								false,
								Random.Range(3, 5),
								1.28f, 1.28f);
							chance -= 10;
						}

						break;
				}

			if (Wreckage.HasPrivateLitter || Wreckage.HasPublicLitter)
			{
				chance = 100;

				switch (objectRealName)
				{
					case VObject.ATMMachine:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.MovieScreen,
								false,
								Random.Range(1, 3),
								0.80f, 0.80f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 5;
						}

						break;

					case VObject.Barbecue:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.24f),
								VObject.Bush,
								true,
								5,
								0.24f, 0.0f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 50;
						}

						break;

						// Bathtub (Splash water)

						// Bed (Tissues)

					case VObject.Boulder:
						while (GC.percentChance(1))
							GC.spawnerMain.SpawnItem(new Vector2(
								spawnPosition.x + Random.Range(-0.48f, 0.48f),
								spawnPosition.y + Random.Range(-0.24f, 0.00f)),
								VItem.Rock);

						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.12f),
								VObject.FlamingBarrel,
								false,
								5,
								0.12f, 0.12f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 20;
						}

						break;

					case VObject.BoulderSmall:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.12f),
								VObject.FlamingBarrel,
								false,
								5,
								0.12f, 0.00f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 50;
						}

						break;

						// Desk (Paper)

					case VObject.Elevator:
						if (GC.challenges.Contains(nameof(AnCapistan)))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.MovieScreen,
								false,
								5,
								0.80f, 0.80f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 20;
						}

						break;

						// Fireplace

					case VObject.FlamingBarrel:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.20f),
								GC.Choose(VObject.Bush, VObject.MovieScreen),
								true,
								5,
								0.12f, 0.06f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 50;
						}

						break;

						// Stove (Food waste)
						// Table (Food waste)
						// Table (Big) (Food waste)

					case VObject.Toilet:
						if (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 10f)))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.08f),
								VObject.FlamingBarrel,
								false,
								Random.Range(1, 4),
								0.24f, 0.24f,
								0,
								avoidPublic, avoidPrivate);

							while (GC.percentChance(chance))
							{
								Wreckage.SpawnWreckagePileObject_Granular(
									new Vector2(spawnPosition.x, spawnPosition.y - 0.08f),
									VObject.MovieScreen,
									false,
									Random.Range(3, 6),
									0.48f, 0.48f,
									0,
									avoidPublic, avoidPrivate);
								chance -= 25;
							}
						}

						break;

					case VObject.TrashCan:
						while (GC.percentChance(1)) // TODO: Move this part to Trash mod
							GC.spawnerMain.SpawnItem(new Vector2(
								spawnPosition.x + Random.Range(-0.32f, 0.32f),
								spawnPosition.y + Random.Range(-0.32f, 0.32f)),
								VItem.BananaPeel);

						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								Wreckage.OverhaulWreckageType(),
								GC.percentChance(50),
								Random.Range(3, 7),
								0.64f, 0.64f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 15;
						}

						break;
				}
			}
		}
    }
}
