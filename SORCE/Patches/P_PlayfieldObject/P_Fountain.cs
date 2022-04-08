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
			string t = NameTypes.Interface;

			RogueLibs.CreateCustomName(CButtonText.FountainSteal, t, new CustomNameInfo("Loot"));
			RogueLibs.CreateCustomName(CButtonText.FountainWishFabulousWealth, t, new CustomNameInfo("Wish for fabulous wealth"));
			RogueLibs.CreateCustomName(CButtonText.FountainWishFameAndGlory, t, new CustomNameInfo("Wish for fame & glory"));
			RogueLibs.CreateCustomName(CButtonText.FountainWishGoodHealth, t, new CustomNameInfo("Wish for good health"));
			RogueLibs.CreateCustomName(CButtonText.FountainWishTrueFriendship, t, new CustomNameInfo("Wish for true friendship"));
			RogueLibs.CreateCustomName(CButtonText.FountainWishWorldPeace, t, new CustomNameInfo("Wish for world peace"));

			RogueLibs.CreateCustomName(COperatingText.FountainStealing, t, new CustomNameInfo("Being a piece of shit"));

			RogueInteractions.CreateProvider<Fountain>(h =>
			{
				if (h.Helper.interactingFar)
					return;

				if (!h.Object.GetHook<P_Fountain_Hook>().HasBeenLooted)
					h.AddButton(CButtonText.FountainSteal, m =>
					{
						GC.spawnerMain.SpawnExplosion(m.Object, m.Object.curPosition, VExplosion.Water);

						if (!m.Agent.statusEffects.hasTrait(VTrait.SneakyFingers))
						{
							GC.audioHandler.Play(m.Object, VAudioClip.JumpIntoWater);
							GC.spawnerMain.SpawnNoise(m.Object.tr.position, 0.4f, m.Agent, "Normal", m.Agent);
							GC.OwnCheck(m.Agent, m.Object.go, "Normal", 2);
						}

						m.StartOperating(2f, true, COperatingText.FountainStealing);
					});
			});
        }
		
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Fountain.SetVars), argumentTypes: new Type[0] { })]
		public static void SetVars_Postfix(Fountain __instance)
        {
			__instance.AddHook<P_Fountain_Hook>();
			__instance.GetHook<P_Fountain_Hook>().MoneyHeld = Random.Range(1, 20);

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
			invItem.invItemName = VItem.Money;
			invItem.ItemSetup(false);
			invItem.invItemCount = fountain.GetHook<P_Fountain_Hook>().MoneyHeld;
			invItem.ShowPickingUpText(fountain.interactingAgent);
			fountain.interactingAgent.inventory.AddItem(invItem);
			fountain.objectInvDatabase.DestroyAllItems();
			fountain.GetHook<P_Fountain_Hook>().HasBeenLooted = true;
			fountain.interactable = false;
			agent.statusEffects.AddStatusEffect(VStatusEffect.FeelingUnlucky, true, true);
			// TODO: XP penalty for stealing

			if (!agent.statusEffects.hasTrait(VTrait.SneakyFingers))
			{
				GC.spawnerMain.SpawnExplosion(fountain, fountain.curPosition, VExplosion.Water);
				P_00_ObjectReal.AnnoyWitnessesVictimless(agent);
			}
			else
				GC.audioHandler.Play(fountain, VAudioClip.JumpOutWater);

			//fountain.interactable = false;
			fountain.MakeChestNonInteractable();
			fountain.StopInteraction();
		}
	}

	public class P_Fountain_Hook : HookBase<PlayfieldObject>
    {
		protected override void Initialize() { }

		public bool HasBeenLooted = false;
		public bool WishMade = false;
		public int MoneyHeld;
	}
}
