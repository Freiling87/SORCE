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
			Vector2 loc = spawnPosition;
			int chance = 100;

			if (Wreckage.HasLeaves)
				switch (objectRealName)
				{
					case VObject.Bush:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
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
								new Vector2(loc.x, loc.y),
								VObject.Bush,
								false,
								5,
								0.64f, 0.64f);
							chance -= 20;
						}

						break;

					case VObject.Plant:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
								VObject.Plant,
								false,
								12,
								//0.08f, 0.16f, // On hold for visibility
								0.64f, 0.64f,
								1);
							//	Particle
							//		1
							//		2
							//		3
							//		4
							//		5
							chance -= 66;
						} // (Random.Range(1, 5)).ToString() whenever you need it, or GC.Choose(1, 4, 5) or whatever

						break;

					case VObject.Tree:
						while (GC.percentChance(chance + 40))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
								VObject.Bush,
								false,
								5,
								0.96f, 0.96f);
							chance -= 15;
						}

						break;
				}

			if (Wreckage.HasPrivateLitter)
            {

            }

			if (Wreckage.HasPublicLitter)
			{
				chance = 100;

				switch (objectRealName)
				{
					case VObject.ATMMachine:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
								VObject.MovieScreen,
								false,
								5,
								0.80f, 0.80f);
							chance -= 10;
						}

						break;

					case VObject.Barbecue:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y - 0.16f),
								VObject.Bush,
								true,
								5,
								0.16f, 0.0f);
							chance -= 25;
						}

						break;

					case VObject.Boulder:
						while (GC.percentChance(1))
							GC.spawnerMain.SpawnItem(new Vector2(
								loc.x + Random.Range(-0.48f, 0.48f),
								loc.y + Random.Range(-0.24f, 0.00f)),
								VItem.Rock);

						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y - 0.12f),
								VObject.FlamingBarrel,
								false,
								5,
								0.12f, 0.12f);
							chance -= 20;
						}

						break;

					case VObject.BoulderSmall:
						while (GC.percentChance(1))
							GC.spawnerMain.SpawnItem(new Vector2(
								loc.x + Random.Range(-0.16f, 0.16f),
								loc.y + Random.Range(-0.16f, 0.00f)),
								VItem.Rock);

						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y - 0.12f),
								VObject.FlamingBarrel,
								false,
								5,
								0.12f, 0.00f);
							chance -= 50;
						}

						break;

					case VObject.Elevator:
						if (GC.challenges.Contains(nameof(AnCapistan)))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
								VObject.MovieScreen,
								false,
								5,
								0.80f, 0.80f);
							chance -= 20;
						}

						break;

					case VObject.FlamingBarrel:
						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y - 0.20f),
								VObject.Bush,
								true,
								5,
								0.12f, 0.06f);
							chance -= 50;
						}

						break;

					case VObject.Toilet:
						if (GC.percentChance((int)(LevelGenTools.SlumminessFactor * 5f)))
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
								VObject.FlamingBarrel,
								false,
								5,
								0.24f, 0.24f);

						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
								VObject.MovieScreen,
								false,
								5,
								0.24f, 0.24f);
							chance -= 100;
						}

						break;

					case VObject.TrashCan:
						while (GC.percentChance(1)) // TODO: Move this part to Trash mod
							GC.spawnerMain.SpawnItem(new Vector2(
								loc.x + Random.Range(-0.32f, 0.32f),
								loc.y + Random.Range(-0.32f, 0.32f)),
								VItem.BananaPeel);

						while (GC.percentChance(chance))
						{
							Wreckage.SpawnWreckagePileObject_Granular(
								new Vector2(loc.x, loc.y),
								CObject.WreckageMisc.RandomElement(),
								GC.percentChance(25),
								5,
								0.48f, 0.48f);
							chance -= 15;
						}

						break;
				}
			}
		}
    }
}
