using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Extensions;
using SORCE.Logging;
using SORCE.Traits;
using SORCE.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using static SORCE.Localization.NameLists;

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
			RogueLibs.CreateCustomName(CButtonText.ToiletTakeHugeShit, t, new CustomNameInfo("Take a huge shit"));
			RogueLibs.CreateCustomName(COperatingText.ToiletShitting, t, new CustomNameInfo("Taking a huge shit"));
			t = VNameType.Dialogue;
			RogueLibs.CreateCustomName(CDialogue.ToiletDisgusting, t, new CustomNameInfo("*Gag* Nope."));

			RogueInteractions.CreateProvider<Toilet>(h =>
			{
				if (h.Object.GetHook<P_Toilet_Hook>().disgusting
					&& !h.Agent.HasTrait<UnderdankVIP>())
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
					interactions.RemoveAll(i => i.ButtonName == VButtonText.FlushYourself); // Trying again with ==
					interactions.RemoveAll(i => i.ButtonName == VButtonText.PurgeStatusEffects);

					int toiletCost = h.Object.GetHook<P_Toilet_Hook>().toiletCost;

					if (E_Agent.IsFlushable(h.Agent))
						h.AddButton(VButtonText.FlushYourself, toiletCost, m =>
						{
							Underdank.FlushYourself(m.Agent, m.Object);
							m.StopInteraction();
						});

					if (h.Object.hasPurgeStatusEffects())
						h.AddButton(VButtonText.PurgeStatusEffects, toiletCost, m =>
						{
							m.Object.PurgeStatusEffects();
							m.StopInteraction();
						});

					// TODO: Structure this like Fountain, it's handled for you.
					if (DebugTools.debugMode)
						h.AddButton(CButtonText.ToiletTakeHugeShit, toiletCost, m =>
						{
							m.StartOperating(2f, false, COperatingText.ToiletShitting);
						});
				}
			});
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Toilet.FlushYourself), argumentTypes: new Type[] { })]
		public static bool FlushYourself_Prefix(Toilet __instance)
		{
			if (__instance.interactingAgent.HasTrait<UnderdankCitizen>())
            {
				Underdank.FlushYourself(__instance.interactingAgent, __instance);
				return false;
			}
				
			return true;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Toilet.SetVars), argumentTypes: new Type[] { })]
		public static void SetVars_Postfix(Toilet __instance)
        {
			__instance.AddHook<P_Toilet_Hook>();
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
		public bool hackedToiletSmurves = false;
		public bool hackedWaterSpray = false;
		public int toiletCost = GC.challenges.Contains(nameof(AnCapistan)) 
			? 10 
			: 0;
	}
}
