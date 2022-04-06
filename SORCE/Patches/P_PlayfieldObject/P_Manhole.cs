using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using SORCE.Logging;
using SORCE.Traits;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Manhole))]
	class P_Manhole
	{
		//private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		private const int crowbarTamperCost = 30;

		[RLSetup]
		public static void Setup()
		{
			string t = 
				VNameType.Dialogue;
			RogueLibs.CreateCustomName(CDialogue.NeedCrowbar, t, new CustomNameInfo("I need a crowbar. I could break a nail!"));

			RogueInteractions.CreateProvider<Manhole>(h =>
			{
				if (h.Helper.interactingFar) 
					return;

				if (h.Object.opened)
				{
					if (h.Agent.HasTrait<UnderdankCitizen>())
						h.AddButton(VButtonText.FlushYourself, m =>
						{
							FlushYourself(m.Agent, m.Object);
						});
				}
				else
				{
					InvItem crowbar = h.Agent.inventory.FindItem(VItem.Crowbar);

					if (crowbar != null)
					{
						string extra = $" ({crowbar.invItemCount} - {crowbarTamperCost})";

						h.AddButton(VButtonText.UseCrowbar, extra, m =>
						{
							m.StartOperating(VItem.Crowbar, 2f, true, "Tampering");
						});
					}
					else
					{
						h.SetStopCallback(m =>
						{
							m.gc.audioHandler.Play(m.Agent, VDialogue.CantDo);
							m.Agent.SayDialogue(CDialogue.NeedCrowbar);
						});
					}
				}
			});
		}

		public static void FlushYourself(Agent agent, ObjectReal entryObject)
		{
			List<ObjectReal> exits = new List<ObjectReal>();

			foreach (ObjectReal objectReal in GC.objectRealList)
            {
				if ((objectReal is Manhole manhole && manhole.opened)
						|| (objectReal is Toilet toilet && !toilet.destroyed
						&& (agent.statusEffects.hasTrait(VTrait.Diminutive) || agent.shrunk)))
					exits.Add(objectReal);
            }

			exits.Remove(entryObject);
			ObjectReal exit = entryObject; // default

			if (exits.Count > 0)
				exit = exits[Random.Range(0, exits.Count - 1)];

			Vector3 exitSpot = Vector3.zero;

			if (exit is Manhole)
			{
				exitSpot = exit.curPosition;
				agent.Teleport((Vector2)exitSpot + Random.insideUnitCircle.normalized, true, true);
				GC.spawnerMain.SpawnExplosion(exit, exitSpot, VExplosion.Water, false, -1, false, 
					exit.FindMustSpawnExplosionOnClients(agent));
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

				agent.Teleport(exitSpot, false, true);
				GC.spawnerMain.SpawnExplosion(exit, exit.tr.position, "Water", false, -1, false,
						exit.FindMustSpawnExplosionOnClients(agent));
			}
		}

		public static void PryOpen(Manhole manhole)
		{
			if (GC.serverPlayer)
			{
				Vector3 position = manhole.tr.position;
				position = new Vector3(manhole.tr.position.x, manhole.tr.position.y - 0.24f, manhole.tr.position.z);

				manhole.hole = GC.spawnerMain.SpawnHole(manhole, position, new Vector3(1.5f, 1.5f, 1f), Quaternion.identity, false, true);
				manhole.hole.ObjectHoleAppear(VObject.Manhole);
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
    static class P_Manhole_Start
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> Start_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					// __instance.gc.levelTheme != 3

					new CodeInstruction(OpCodes.Ldarg_0),	//	this
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc.levelTheme
					new CodeInstruction(OpCodes.Ldc_I4_3),	//	this.gc.levelTheme, 3
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// This is a silly way to just bypass the if-block
					new CodeInstruction(OpCodes.Ldc_I4_3),	//	3
					new CodeInstruction(OpCodes.Ldc_I4_3),	//	3, 3
				});

			patch.ApplySafe(instructions, logger);
			return codeInstructions;
		} 
	}
}
