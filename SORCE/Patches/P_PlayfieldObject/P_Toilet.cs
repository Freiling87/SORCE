using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Logging;
using SORCE.MapGenUtilities;
using SORCE.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches.P_PlayfieldObject
{
	[HarmonyPatch(declaringType: typeof(Toilet))]
	class P_Toilet
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		const int toiletCost = 10;

		// To remove vanilla buttons, until RL is patched to do so:
		//	FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
		//	List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
		//	interactions.RemoveAll(static i => i.ButtonName is "123");

		// TODO: Roguelibs 3.5.0b version

		public static bool CanAgentFlushSelf(Agent agent) =>
			!agent.statusEffects.hasStatusEffect(VStatusEffect.Giant) &&
			agent.statusEffects.hasTrait(VTrait.Diminutive) ||
			agent.statusEffects.hasStatusEffect(VStatusEffect.Shrunk);

		[RLSetup]
		public static void Setup()
		{
			string t =
				VNameType.Interface;
			RogueLibs.CreateCustomName(CButtonText.TakeHugeShit, t, new CustomNameInfo("Take a huge shit"));

			RogueInteractions.CreateProvider<Toilet>(h =>
			{
				if (GC.levelType != "HomeBase" &&
					GC.challenges.Contains(nameof(AnCapistan)))
				{
					if (CanAgentFlushSelf(h.Agent))
					{
						h.AddButton(VButtonText.FlushYourself, toiletCost, m =>
						{
							m.Object.FlushYourself();
						});
					}

					if (h.Object.hasPurgeStatusEffects())
					{
						h.AddButton(VButtonText.PurgeStatusEffects, toiletCost, m =>
						{
							m.Object.PurgeStatusEffects();
						});
					}
				}

				if (Core.debugMode)
					h.AddButton(CButtonText.TakeHugeShit, m =>
					{
						TakeHugeShit(m.Agent, m.Object);
					});
			});
		}

		// TODO: Merge this with the ones in Manhole
		// TODO: Probably should also just be a transpiler
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Toilet.FlushYourself), argumentTypes: new Type[] { })]
		public static bool FlushYourself_Prefix(Toilet __instance) 
		{
			if (__instance.interactingAgent.HasTrait<UnderdankCitizen>())
			{
				if (!__instance.interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.Giant) &&
					(__instance.interactingAgent.statusEffects.hasTrait(VTrait.Diminutive) ||
					__instance.interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.Shrunk)))
					// TODO: Make this conditional into a method, CanFitIntoToilet
				{
					List<ObjectReal> exits = new List<ObjectReal>();

					for (int i = 0; i < GC.objectRealList.Count; i++)
					{
						ObjectReal exitCandidate = GC.objectRealList[i];

						if (exitCandidate != __instance && 
							!exitCandidate.destroyed && 
							exitCandidate.startingChunk != __instance.startingChunk)
						{
							if (exitCandidate is Manhole manhole
								&& manhole.opened)
								exits.Add(exitCandidate);
							else if (exitCandidate is Toilet)
								exits.Add(exitCandidate);
						}
					}

					if (exits.Count == 0)
						for (int j = 0; j < GC.objectRealList.Count; j++)
						{
							ObjectReal exitCandidate = GC.objectRealList[j];

							if (exitCandidate != __instance && !exitCandidate.destroyed)
							{
								if (exitCandidate is Manhole)
								{
									Manhole manhole = (Manhole)exitCandidate;

									if (manhole.opened)
										exits.Add(exitCandidate);
								}
								else if (exitCandidate is Toilet)
									exits.Add(exitCandidate);
							}
						}

					ObjectReal exit = __instance;

					if (exits.Count > 0)
						exit = exits[Random.Range(0, exits.Count - 1)];

					Vector3 exitSpot = Vector3.zero;
					string direction = exit.direction;

					switch (direction)
					{
						case "E":
							exitSpot = new Vector3(exit.tr.position.x + 0.32f, exit.tr.position.y, exit.tr.position.z);

							break;

						case "N":
							exitSpot = new Vector3(exit.tr.position.x, exit.tr.position.y + 0.32f, exit.tr.position.z);

							break;

						case "S":
							exitSpot = new Vector3(exit.tr.position.x, exit.tr.position.y - 0.32f, exit.tr.position.z);

							break;

						case "W":
							exitSpot = new Vector3(exit.tr.position.x - 0.32f, exit.tr.position.y, exit.tr.position.z);

							break;
					}

					GC.audioHandler.Play(__instance, "ToiletTeleportIn");
					__instance.interactingAgent.toiletTeleporting = true;
					__instance.interactingAgent.Teleport(exitSpot, false, true);
					GC.spawnerMain.SpawnExplosion(__instance.interactingAgent, exit.tr.position, "Water", false, -1, false,
							__instance.FindMustSpawnExplosionOnClients(__instance.interactingAgent));
				}

				return false;
			}

			return true;
		}

		private static void TakeHugeShit(Agent agent, Toilet toilet)
        {
			Vector2 loc = toilet.tr.position;

			Wreckage.SpawnWreckagePileObject_Granular(
				new Vector2(loc.x, loc.y),
				VObject.FlamingBarrel,
				false,
				3,
				0.64f, 0.64f);

			GC.spawnerMain.SpawnExplosion(agent, loc, VExplosion.Water);
			P_00_ObjectReal.AnnoyWitnessesVictimless(agent);
		}
	}
}
