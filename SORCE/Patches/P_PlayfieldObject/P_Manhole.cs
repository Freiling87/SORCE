using BepInEx.Logging;
using HarmonyLib;
using SORCE.Extensions;
using SORCE.Logging;
using SORCE.Traits;
using RogueLibsCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches.P_PlayfieldObject
{
	[HarmonyPatch(declaringType: typeof(Manhole))]
	class P_Manhole
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		private const int crowbarTamperCost = 30;

		[RLSetup]
		public static void Setup()
		{
			RogueInteractions.CreateProvider<Manhole>(static h =>
			{
				if (h.Helper.interactingFar) 
					return;

				if (h.Object.opened)
				{
					if (h.Agent.HasTrait<UnderdankCitizen>())
						h.AddButton(cButtonText.FlushYourself, static m =>
						{
							FlushYourself(m.Agent, m.Object);
						});
				}
				else
				{
					InvItem crowbar = h.Agent.inventory.FindItem(vItem.Crowbar);

					if (crowbar is not null)
					{
						string extra = $" ({crowbar.invItemCount} - {crowbarTamperCost})";

						h.AddButton("UseCrowbar", extra, static m =>
						{
							m.StartOperating(vItem.Crowbar, 2f, true, "Tampering");
						});
					}
					else
					{
						h.SetStopCallback(static m =>
						{
							m.gc.audioHandler.Play(m.Agent, "CantDo");
							m.Agent.SayDialogue("NeedCrowbar");
						});
					}
				}
			});

			RogueLibs.CreateCustomName(cDialogue.FlushYourself, NameTypes.Interface,
				new CustomNameInfo("Flush Yourself"));


		}

		public static void FlushYourself(Agent agent, ObjectReal __instance)
		{
			List<ObjectReal> exits = new List<ObjectReal>();

			for (int i = 0; i < GC.objectRealList.Count; i++)
			{
				ObjectReal objectReal = GC.objectRealList[i];

				if (objectReal == __instance)
					continue;
				else if (objectReal is Manhole)
				{
					Manhole manhole = (Manhole)objectReal;

					if (manhole.opened)
						exits.Add(objectReal);
				}
				else if (objectReal is Toilet && (agent.statusEffects.hasTrait(vTrait.Diminutive) || agent.shrunk))
					if (!objectReal.destroyed)
						exits.Add(objectReal);
			}

			ObjectReal exit = __instance;

			if (exits.Count > 0)
				exit = exits[Random.Range(0, exits.Count - 1)];

			Vector3 exitSpot = Vector3.zero;

			if (exit is Manhole)
			{
				exitSpot = exit.curPosition;
				agent.Teleport((Vector2)exitSpot + Random.insideUnitCircle.normalized, true, true);
				GC.spawnerMain.SpawnExplosion((PlayfieldObject)exit, exitSpot, "Water", false, -1, false, exit.FindMustSpawnExplosionOnClients(agent));
			}
			else if (exit is Toilet)
			{
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

				__instance.interactingAgent.Teleport(exitSpot, false, true);
				GC.spawnerMain.SpawnExplosion(__instance.interactingAgent, exit.tr.position, "Water", false, -1, false,
						__instance.FindMustSpawnExplosionOnClients(__instance.interactingAgent));
			}
		}

		public static void PryOpen(Manhole __instance)
		{
			if (GC.serverPlayer)
			{
				Vector3 position = __instance.tr.position;
				position = new Vector3(__instance.tr.position.x, __instance.tr.position.y - 0.24f, __instance.tr.position.z);

				__instance.hole = GC.spawnerMain.SpawnHole(__instance, position, new Vector3(1.5f, 1.5f, 1f), Quaternion.identity, false, true);
				__instance.hole.ObjectHoleAppear(vObject.Manhole);
				GC.playerAgent.objectMult.ObjectAction(__instance.objectNetID, "HoleAppear");
				__instance.operatingAgent.inventory.SubtractFromItemCount(__instance.operatingItem, crowbarTamperCost); 
			}

			__instance.objectSprite.meshRenderer.enabled = false;
			__instance.opened = true;
			__instance.SetSDangerousToWalk(true);
			GC.audioHandler.Play(__instance, "ManholeOpen");

			if (GC.levelFeeling == "WarZone")
			{
				__instance.objectRealRealName = GC.nameDB.GetName("Hole", "Object");
				__instance.normalHole = true;
			}
		}
	}

	public class Manhole_Remora
	{
		public Manhole host;
		public bool splashed = false;
	}
}
