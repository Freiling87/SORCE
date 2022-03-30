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
using JetBrains.Annotations;
using BTHarmonyUtils.TranspilerUtils;
using System.Reflection.Emit;
using BTHarmonyUtils;

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
			RogueInteractions.CreateProvider<Manhole>(h =>
			{
				if (h.Helper.interactingFar) 
					return;

				if (h.Object.opened)
				{
					if (h.Agent.HasTrait<UnderdankCitizen>())
						h.AddButton(vButtonText.FlushYourself, m =>
						{
							FlushYourself(m.Agent, m.Object);
						});
				}
				else
				{
					InvItem crowbar = h.Agent.inventory.FindItem(vItem.Crowbar);

					if (crowbar != null)
					{
						string extra = $" ({crowbar.invItemCount} - {crowbarTamperCost})";

						h.AddButton("UseCrowbar", extra, m =>
						{
							m.StartOperating(vItem.Crowbar, 2f, true, "Tampering");
						});
					}
					else
					{
						h.SetStopCallback(m =>
						{
							m.gc.audioHandler.Play(m.Agent, "CantDo");
							m.Agent.SayDialogue("NeedCrowbar");
						});
					}
				}
			});

			//RogueLibs.CreateCustomName(vButtonText.FlushYourself, NameTypes.Interface,
			//	new CustomNameInfo("Flush Yourself"));
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

		public static void PryOpen(Manhole manhole)
		{
			if (GC.serverPlayer)
			{
				Vector3 position = manhole.tr.position;
				position = new Vector3(manhole.tr.position.x, manhole.tr.position.y - 0.24f, manhole.tr.position.z);

				manhole.hole = GC.spawnerMain.SpawnHole(manhole, position, new Vector3(1.5f, 1.5f, 1f), Quaternion.identity, false, true);
				manhole.hole.ObjectHoleAppear(vObject.Manhole);
				GC.playerAgent.objectMult.ObjectAction(manhole.objectNetID, "HoleAppear");
				manhole.operatingAgent.inventory.SubtractFromItemCount(manhole.operatingItem, crowbarTamperCost); 
			}

			manhole.objectSprite.meshRenderer.enabled = false;
			manhole.opened = true;
			manhole.SetSDangerousToWalk(true);
			GC.audioHandler.Play(manhole, "ManholeOpen");

			if (GC.levelFeeling == "WarZone")
			{
				manhole.objectRealRealName = GC.nameDB.GetName("Hole", "Object");
				manhole.normalHole = true;
			}

			manhole.StopInteraction();
		}
	}

    [HarmonyPatch(typeof(Manhole))]
    [HarmonyPatch("Start")]
	[HarmonyDebug]
    static class P_Manhole_Start
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Start_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo gc = AccessTools.Field(typeof(PlayfieldObject), "gc");
			FieldInfo levelTheme = AccessTools.Field(typeof(GameController), "levelTheme");

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					// __instance.gc.levelTheme != 3

					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldc_I4_3),
					new CodeInstruction(OpCodes.Beq_S),
				});

			patch.ApplySafe(instructions, logger);
			return codeInstructions;
		} 
	}
}
