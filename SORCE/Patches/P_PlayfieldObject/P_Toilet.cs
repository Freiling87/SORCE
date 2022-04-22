using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Extensions;
using SORCE.Logging;
using SORCE.MapGenUtilities;
using SORCE.Traits;
using System;
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
	[HarmonyPatch(declaringType: typeof(Toilet))]
	class P_Toilet
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// To remove vanilla buttons, until RL is patched to do so:
		//	FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
		//	List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
		//	interactions.RemoveAll(static i => i.ButtonName is "123");

		// TODO: Roguelibs 3.5.0b version

		[RLSetup]
		public static void Setup()
		{
			string t = VNameType.Interface;
			RogueLibs.CreateCustomName(CButtonText.TakeHugeShit, t, new CustomNameInfo("Take a huge shit"));
			RogueLibs.CreateCustomName(COperatingText.ToiletShitting, t, new CustomNameInfo("Taking a huge shit"));
			t = VNameType.Dialogue;
			RogueLibs.CreateCustomName(CDialogue.ToiletDisgusting, t, new CustomNameInfo("*Gag* Nope."));

			RogueInteractions.CreateProvider<Toilet>(h =>
			{
				if (h.Object.GetHook<P_Toilet_Hook>().disgusting)
				{
					InteractionModel<Toilet> model = h.Model;
					Agent agent = h.Agent;

					model.CancelCallback = () =>
					{
						agent.SayDialogue(CDialogue.ToiletDisgusting);
					};

					h.StopInteraction();
				}
				else
				{
					// Vanilla button removal
					FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
					List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
					interactions.RemoveAll(i => i.ButtonName is VButtonText.FlushYourself);
					interactions.RemoveAll(i => i.ButtonName is VButtonText.PurgeStatusEffects);

					int toiletCost = h.Object.GetHook<P_Toilet_Hook>().toiletCost;

					if (E_Agent.IsFlushable(h.Agent))
						h.AddButton(VButtonText.FlushYourself, toiletCost, m =>
						{
							m.Object.FlushYourself();
						});

					if (h.Object.hasPurgeStatusEffects())
						h.AddButton(VButtonText.PurgeStatusEffects, toiletCost, m =>
						{
							m.Object.PurgeStatusEffects();
						});

					// TODO: Structure this like Fountain, it's handled for you.
					if (Core.debugMode)
						h.AddButton(CButtonText.TakeHugeShit, toiletCost, m =>
						{
							m.StartOperating(2f, false, COperatingText.ToiletShitting);
						});
				}
			});
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Toilet.FlushYourself), argumentTypes: new Type[] { })]
		public static bool FlushYourself_Prefix(Toilet __instance)
		{
			if (!__instance.interactingAgent.HasTrait<UnderdankCitizen>())
				return true;

			P_Manhole.FlushYourself(__instance.interactingAgent, __instance);
			return false;
		}

		//[HarmonyPrefix, HarmonyPatch(methodName: nameof(Toilet.DetermineButtons), argumentTypes: new Type[] { })]
		public static bool DetermineButtons_Prefix(Toilet __instance)
        {
			P_00_ObjectReal.DetermineButtons_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

			return false;
        } 

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Toilet.SetVars), argumentTypes: new Type[] { })]
		public static void SetVars_Postfix(Toilet __instance)
        {
			__instance.AddHook<P_Toilet_Hook>();
		}

		public static void TakeHugeShit(Toilet toilet, bool loud = true)
		{
			Poopsplosion(toilet, loud);
			toilet.StopInteraction();
		}

		public static void Poopsplosion(ObjectReal targetObj, bool loud = true)
		{
			Agent agent = targetObj.interactingAgent;
			Vector2 pos = targetObj.transform.position;
			bool avoidPublic = !VFX.HasPublicLitter;
			bool avoidPrivate = !VFX.HasPrivateLitter;

			if (loud)
				GC.spawnerMain.SpawnExplosion(targetObj, pos, VExplosion.Water);
			else 
				GC.tileInfo.SpillLiquidLarge(pos, VExplosion.Water, false, 2, !avoidPrivate);

			VFX.SpawnWreckagePileObject_Granular(
				new Vector2(pos.x, pos.y - 0.08f),
				VObject.FlamingBarrel,
				false,
				Random.Range(1, 4),
				0.24f, 0.24f,
				0,
				avoidPublic, avoidPrivate);

			int chance = 100;
			while (GC.percentChance(chance))
			{
				VFX.SpawnWreckagePileObject_Granular(
					new Vector2(pos.x, pos.y - 0.08f),
					VObject.MovieScreen,
					false,
					Random.Range(3, 6),
					0.48f, 0.48f,
					0,
					avoidPublic, avoidPrivate);
				chance -= 25;
			}

			if (targetObj is Toilet toilet)
				toilet.GetComponent<PlayfieldObject>().GetHook<P_Toilet_Hook>().disgusting = true;
		}
	}

	public class P_Toilet_Hook : HookBase<PlayfieldObject>
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		protected override void Initialize() { }

		public bool disgusting = false;
		public bool hackedFree = false;
		public bool hackedPoopsplosion = false;
		public bool hackedWaterSpray = false;
		public int toiletCost = GC.challenges.Contains(nameof(AnCapistan)) 
			? 10 
			: 0;
	}
}
