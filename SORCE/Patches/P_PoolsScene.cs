using BepInEx.Logging;
using HarmonyLib;
using Light2D;
using RogueLibsCore;
using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_VFX;
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
		/// Object-based wreckage spawners
		/// </summary>
		/// <param name="objectRealName"></param>
		/// <param name="objectRealPrefab"></param>
		/// <param name="spawnPosition"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(PoolsScene.SpawnObjectReal), argumentTypes: new Type[] {typeof(string), typeof(GameObject), typeof(Vector3)} )]
        public static void SpawnObjectReal_Postfix(string objectRealName, GameObject objectRealPrefab, Vector3 spawnPosition)
        {
			int chance = 100;
			bool avoidPublic = !VFX.HasPublicLitter;
			bool avoidPrivate = !VFX.HasPrivateLitter;
			bool isPublicObject = LevelGenTools.IsPublic(spawnPosition);

			if (VFX.HasLeaves)
			{
				switch (objectRealName)
				{
					case VObject.Bush:
						while (GC.percentChance(50))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.Bush,
								false,
								Random.Range(3, 5),
								0.64f, 0.64f);
						}

						break;

					case VObject.KillerPlant:
						while (GC.percentChance(50))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.Bush,
								false,
								Random.Range(3, 5),
								0.64f, 0.64f);
						}

						break;

					case VObject.Plant:

						while (GC.percentChance(33)) 
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.16f),
								VObject.Plant,
								false,
								1,
								0.32f, 0.24f,
								Random.Range(3, 5)); // Leaves only
						}

						break;

					case VObject.Tree:
						while (GC.percentChance(75))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.Bush,
								false,
								Random.Range(5, 8),
								1.28f, 1.28f);
						}

						break;
				}
			}

			if (VFX.HasPrivateLitter || VFX.HasPublicLitter)
			{
				switch (objectRealName)
				{
					case VObject.ATMMachine:
						while (GC.percentChance(80))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.MovieScreen,
								false,
								Random.Range(1, 3),
								0.80f, 0.80f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.Barbecue:
						while (GC.percentChance(75))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.24f),
								VObject.Bush,
								true,
								Random.Range(2, 4),
								0.24f, 0.0f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.Bathtub:
						//if (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 10f)))
						if (GC.percentChance(100))
							//GC.spawnerMain.SpawnExplosion(GC.playerAgent, spawnPosition, VExplosion.Water);
							GC.tileInfo.SpillLiquidLarge(spawnPosition, VExplosion.Water, false, 1, true); // Attempt at smaller pool. expandLevel is inverse to size.

						break;

					case VObject.Bed:
						while (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 10f)))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.16f),
								VObject.MovieScreen,
								false,
								Random.Range(2, 5),
								0.24f, 0.12f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.Boulder:
						while (GC.percentChance(1))
							GC.spawnerMain.SpawnItem(new Vector2(
								spawnPosition.x + Random.Range(-0.48f, 0.48f),
								spawnPosition.y + Random.Range(-0.24f, 0.00f)),
								VItem.Rock);

						while (GC.percentChance(80))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.FlamingBarrel,
								false,
								4,
								0.64f, 0.64f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.BoulderSmall:
						while (GC.percentChance(50))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.12f),
								VObject.FlamingBarrel,
								false,
								3,
								0.24f, 0.00f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.Desk:
						if (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 20f)))
						{
							while (GC.percentChance(chance))
							{
								VFX.SpawnWreckagePileObject_Granular(
									new Vector2(spawnPosition.x, spawnPosition.y - 0.12f),
									VObject.MovieScreen,
									false,
									Random.Range(1, 3),
									0.48f, 0.48f,
									0,
									avoidPublic, avoidPrivate);

								chance -= 34;
							}
						}

						break;

					case VObject.Elevator:
						if (GC.challenges.Contains(nameof(AnCapistan)))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VObject.MovieScreen,
								false,
								5,
								0.80f, 0.80f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.Fireplace:
						while (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 15f)))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.12f),
								VObject.MovieScreen,
								true,
								5,
								0.24f, 0.12f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.FlamingBarrel:
						while (GC.percentChance(chance))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.24f),
								VObject.MovieScreen,
								true,
								5,
								0.24f, 0.12f,
								0,
								avoidPublic, avoidPrivate);
							chance -= 50;
						}

						break;

					// need much smaller particles for this to look any good.
					//case VObject.Refrigerator:
					//	while (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 10f)))
					//	{
					//		Wreckage.SpawnWreckagePileObject_Granular(
					//			new Vector2(spawnPosition.x, spawnPosition.y - 0.24f),
					//			Wreckage.FoodWreckageType(),
					//			false,
					//			5,
					//			0.24f, 0.12f,
					//			0,
					//			avoidPublic, avoidPrivate);
					//	}

					//	break;

					case VObject.Shelf:
						if (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 20f)))
						{
							while (GC.percentChance(chance))
							{
								VFX.SpawnWreckagePileObject_Granular(
									new Vector2(spawnPosition.x, spawnPosition.y - 0.12f),
									VObject.MovieScreen,
									false,
									1,
									0.24f, 0.24f,
									0,
									avoidPublic, avoidPrivate);

								chance -= 34;
							}
						}

						break;

					case VObject.Stove:
						while (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 20f)))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.24f),
								VFX.FoodWreckageType(),
								GC.percentChance(75),
								5,
								0.24f, 0.12f,
								0,
								avoidPublic, avoidPrivate);
						}

						if (GC.percentChance(100)) // Adjust
							GC.tileInfo.SpillLiquidLarge(spawnPosition, "Oil", false, 1, true); // Attempt at smaller pool. expandLevel is inverse to size.

						break;

					case VObject.Table:
						while (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 20f)))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.24f),
								VFX.FoodWreckageType(),
								true,
								3,
								0.48f, 0.24f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.TableBig:
						while (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 20f)))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.12f),
								VFX.FoodWreckageType(),
								true,
								5,
								0.64f, 0.64f,
								0,
								avoidPublic, avoidPrivate);
						}

						break;

					case VObject.Toilet:
						if (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 10f)))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y - 0.08f),
								VObject.FlamingBarrel,
								false,
								Random.Range(1, 4),
								0.24f, 0.24f,
								0,
								avoidPublic, avoidPrivate);

							while (GC.percentChance(chance))
							{
								VFX.SpawnWreckagePileObject_Granular(
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

						//if (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 15f)))
						if (GC.percentChance(100))
							//GC.spawnerMain.SpawnExplosion(GC.playerAgent, spawnPosition, VExplosion.Water);
							GC.tileInfo.SpillLiquidLarge(spawnPosition, VExplosion.Water, false, 1, true); // Attempt at smaller pool. expandLevel is inverse to size.

						break;

					case VObject.TrashCan:
						while (GC.percentChance(1)) // TODO: Move this part to Trash mod
							GC.spawnerMain.SpawnItem(new Vector2(
								spawnPosition.x + Random.Range(-0.32f, 0.32f),
								spawnPosition.y + Random.Range(-0.32f, 0.32f)),
								VItem.BananaPeel);

						while (GC.percentChance(chance))
						{
							VFX.SpawnWreckagePileObject_Granular(
								new Vector2(spawnPosition.x, spawnPosition.y),
								VFX.OverhaulWreckageType(),
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
