using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using SORCE.Logging;
using SORCE.Traits;
using SORCE.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using static SORCE.Localization.NameLists;

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
			string t = 
				VNameType.Dialogue;
			RogueLibs.CreateCustomName(CDialogue.NeedCrowbar, t, new CustomNameInfo("I need a crowbar. I could break a nail!"));

			RogueInteractions.CreateProvider<Manhole>(h =>
			{
				if (h.Helper.interactingFar) 
					return;

				if (h.Object.opened)
				{
					if (h.Agent.HasTrait<UnderdankCitizen>()
						&& Underdank.Exits(h.Agent).Count() > 1)
						h.AddButton(VButtonText.FlushYourself, m =>
						{
							Underdank.FlushYourself(m.Agent, m.Object);
							m.StopInteraction();
						});
					else
						h.StopInteraction();
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

		public static void PryOpen(Manhole manhole)
		{
			if (GC.serverPlayer)
			{
				Vector3 position = new Vector3(manhole.tr.position.x, manhole.tr.position.y - 0.24f, manhole.tr.position.z);

				manhole.hole = GC.spawnerMain.SpawnHole(manhole, position, new Vector3(1.5f, 1.5f, 1f), Quaternion.identity, false, true);
				manhole.hole.ObjectHoleAppear(VObject.Manhole);
				//GC.playerAgent.objectMult.ObjectAction(manhole.objectNetID, "HoleAppear");
				manhole.operatingAgent.inventory.SubtractFromItemCount(manhole.operatingItem, crowbarTamperCost); 
			}

			manhole.objectSprite.meshRenderer.enabled = false;
			manhole.opened = true;
			manhole.SetSDangerousToWalk(true);
			GC.audioHandler.Play(manhole, VAudioClip.ManholeOpen);

			if (GC.levelFeeling == VLevelFeeling.WarZone)
			{
				manhole.objectRealRealName = GC.nameDB.GetName(VObject.Hole, "Object");
				manhole.normalHole = true;
			}

			manhole.StopInteraction();
		}
	}

	[HarmonyPatch(declaringType: typeof(Manhole), methodName: "Start")]
	static class P_Manhole_Start
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> RemoveDistrictLimitation(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					// if (this.gc.levelTheme != 3 && ...

					new CodeInstruction(OpCodes.Ldarg_0),	//	this
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc
					new CodeInstruction(OpCodes.Ldfld),		//	this.gc.levelTheme
					new CodeInstruction(OpCodes.Ldc_I4_3),	//	this.gc.levelTheme, 3
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					// if (3 != 3 && ...
					new CodeInstruction(OpCodes.Ldc_I4_3),	//	3
					new CodeInstruction(OpCodes.Ldc_I4_3),	//	3, 3
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}
 