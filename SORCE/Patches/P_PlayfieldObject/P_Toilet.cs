using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Logging;
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
			!agent.statusEffects.hasStatusEffect(vStatusEffect.Giant) &&
			agent.statusEffects.hasTrait(vTrait.Diminutive) ||
			agent.statusEffects.hasStatusEffect(vStatusEffect.Shrunk);

		[RLSetup]
		public static void Setup()
		{
			RogueInteractions.CreateProvider<Toilet>(h =>
			{
				if (GC.levelType != "HomeBase" &&
					GC.challenges.Contains(nameof(AnCapistan)))
				{
					if (CanAgentFlushSelf(h.Agent))
					{
						h.AddButton("FlushYourself", toiletCost, m =>
						{
							m.Object.FlushYourself();
						});
					}
					if (h.Object.hasPurgeStatusEffects())
					{
						h.AddButton("PurgeStatusEffects", toiletCost, m =>
						{
							m.Object.PurgeStatusEffects();
						});
					}
				}
			});
		}

		// TODO: Merge this with the ones in Manhole
		// TODO: Probably should also just be a transpiler
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Toilet.FlushYourself), argumentTypes: new Type[] { })]
		public static bool FlushYourself_Prefix(Toilet __instance) 
		{
			if (__instance.interactingAgent.HasTrait<UnderdankCitizen>())
			{
				if (!__instance.interactingAgent.statusEffects.hasStatusEffect(vStatusEffect.Giant) &&
					(__instance.interactingAgent.statusEffects.hasTrait(vTrait.Diminutive) ||
					__instance.interactingAgent.statusEffects.hasStatusEffect(vStatusEffect.Shrunk)))
					// TODO: Make this conditional into a method, CanFitIntoToilet
				{
					List<ObjectReal> exits = new List<ObjectReal>();

					for (int i = 0; i < GC.objectRealList.Count; i++)
					{
						ObjectReal exitCandidate = GC.objectRealList[i];

						if (exitCandidate != __instance && !exitCandidate.destroyed && exitCandidate.startingChunk != __instance.startingChunk)
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
	}

	public class Toilet_Remora
	{
		public Toilet host;
		public bool splashed = false;
	}
}
