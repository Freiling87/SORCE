using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Logging;
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
	[HarmonyPatch(declaringType: typeof(Fountain))]
    class P_Fountain
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[RLSetup]
		public static void Setup()
        {
			RogueLibs.CreateCustomName(cButtonText.FountainSteal, NameTypes.Interface, new CustomNameInfo("Loot"));
			RogueLibs.CreateCustomName(cOperatingText.BeingAPieceOfShit, NameTypes.Interface, new CustomNameInfo("Being a piece of shit"));

			RogueInteractions.CreateProvider<Fountain>(h =>
			{
				if (h.Helper.interactingFar)
					return;

				if (!h.Object.GetHook<FountainHook>().HasBeenLooted)
					h.AddButton(cButtonText.FountainSteal, m =>
					{
						GC.spawnerMain.SpawnExplosion(m.Object, m.Object.curPosition, vExplosion.Water);

						if (!m.Agent.statusEffects.hasTrait(vTrait.SneakyFingers))
						{
							GC.audioHandler.Play(m.Object, vAudioClip.JumpIntoWater);
							GC.spawnerMain.SpawnNoise(m.Object.tr.position, 0.4f, m.Agent, "Normal", m.Agent);
							GC.OwnCheck(m.Agent, m.Object.go, "Normal", 2);
						}

						m.StartOperating(2f, true, cOperatingText.BeingAPieceOfShit);
					});
			});
        }
		
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Fountain.SetVars), argumentTypes: new Type[0] { })]
		public static void SetVars_Postfix(Fountain __instance)
        {
			__instance.AddHook<FountainHook>();
			__instance.GetHook<FountainHook>().MoneyHeld = Random.Range(1, 20);

			__instance.damageThreshold = 50;
			__instance.damageAccumulates = false;
			__instance.pickUppable = false;
			__instance.fireProof = true;
			__instance.cantMakeFollowersAttack = true; 
		}

		public static void Loot(Fountain fountain)
		{
			Agent agent = fountain.interactingAgent;

			InvItem invItem = new InvItem();
			invItem.invItemName = vItem.Money;
			invItem.ItemSetup(false);
			invItem.invItemCount = fountain.GetHook<FountainHook>().MoneyHeld;
			invItem.ShowPickingUpText(fountain.interactingAgent);
			fountain.interactingAgent.inventory.AddItem(invItem);
			fountain.objectInvDatabase.DestroyAllItems();
			fountain.GetHook<FountainHook>().HasBeenLooted = true;
			fountain.interactable = false;
			agent.statusEffects.AddStatusEffect(vStatusEffect.FeelingUnlucky, true, true);
			// TODO: XP penalty for stealing

			if (!agent.statusEffects.hasTrait(vTrait.SneakyFingers))
			{
				GC.spawnerMain.SpawnExplosion(fountain, fountain.curPosition, vExplosion.Water);
				AnnoyWitnessesVictimless(agent);
			}
			else
				GC.audioHandler.Play(fountain, vAudioClip.JumpOutWater);

			fountain.interactable = false;
			fountain.StopInteraction();
		}

		public static void AnnoyWitnessesVictimless(Agent perp)
		{
			foreach (Agent bystander in GC.agentList)
			{
				if (Vector2.Distance(bystander.tr.position, perp.tr.position) < bystander.LOSRange / perp.hardToSeeFromDistance &&
					bystander != perp && !bystander.zombified && !bystander.ghost && !bystander.oma.hidden &&
					(!perp.aboveTheLaw || !bystander.enforcer) &&
					perp.prisoner == bystander.prisoner && !perp.invisible)
				{
					string perpRel = bystander.relationships.GetRel(perp);

					if (perpRel == nameof(relStatus.Neutral) || perpRel == nameof(relStatus.Friendly))
					{
						if (bystander.relationships.GetRelationship(perp).hasLOS)
						{
							relStatus perpRel2 = bystander.relationships.GetRelCode(perp);

							// TODO something isn't right here, condition always evaluates to true
							if (perpRel2 != relStatus.Aligned || perpRel2 != relStatus.Loyal)
								bystander.relationships.SetStrikes(perp, 2);
						}
					}
					else if (perpRel == nameof(relStatus.Annoyed) && bystander.relationships.GetRelationship(perp).hasLOS)
					{
						bystander.relationships.SetRelHate(perp, 5);
					}
				}
			}
		}
	}

	public class FountainHook : HookBase<PlayfieldObject>
    {
		protected override void Initialize() { }

		public bool HasBeenLooted = false;
		public bool WishMade = false;
		public int MoneyHeld;
	}
}
