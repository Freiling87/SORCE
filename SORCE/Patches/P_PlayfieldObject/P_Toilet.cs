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
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristNoPoint, t, new CustomNameInfo("*Sigh* Been there, done that, took a huge shit. Old news."));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 1, t, new CustomNameInfo("Worst... dump... EVER. I'd rather have someone shit on me than shit in there again. One Wype."));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 2, t, new CustomNameInfo("I'm... confused. Is that a toilet, or a porcelain piece of shit? Completely unacceptable. But I did find a dollar, so 2 Wypes."));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 3, t, new CustomNameInfo("Wow, what can I say? Not the worst bathroom ever, but damn close. There is something fragrant about the piss puddles here, so 3 Wypes."));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 4, t, new CustomNameInfo("They can't all be winners. I'd only give this toilet 4 Wypes, but I might be back sometime soon. *Grumble* Real soon."));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 5, t, new CustomNameInfo("Pretty lackluster, if I'm being honest. Still, I had a solid dump. Five Wypes."));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 6, t, new CustomNameInfo("This dump was solid. Check it out if you're in the area! Six Wypes."));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 7, t, new CustomNameInfo("Maybe the best dump I've taken all week. But I did have a lot of coffee, so I'll have to come back. Seven Wypes!"));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 8, t, new CustomNameInfo("Just left this big chunky chungus in a QUALITY 8-Wyper, and I'm not flushing it. Tag me in a photo and I'll give you a shoutout!"));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 9, t, new CustomNameInfo("Few things in life are perfect. This came close, but I thought I heard a fly. Nine Wypes!"));
			RogueLibs.CreateCustomName(CDialogue.ToiletTouristPoint + 10, t, new CustomNameInfo("That was frickin' incredible! I wish I could give more than 10 Wypes!"));
			RogueLibs.CreateCustomName(CDialogue.ToiletDisgustingOkay, t, new CustomNameInfo("Smells fine to me!"));

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
					interactions.RemoveAll(i => i.ButtonName is VButtonText.FlushYourself); // Trying again with ==
					interactions.RemoveAll(i => i.ButtonName is VButtonText.PurgeStatusEffects);

					if (h.Object.GetHook<P_Toilet_Hook>().disgusting &&
                        h.Agent.HasTrait<UnderdankVIP>())
                    {
						h.Agent.SayDialogue(CDialogue.ToiletDisgustingOkay);
					}

					int toiletCost = h.Object.GetHook<P_Toilet_Hook>().toiletCost;

					if (E_Agent.IsFlushableToilet(h.Agent))
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
			if (__instance.interactingAgent.HasTrait<UnderdankCitizen>() ||
				__instance.interactingAgent.HasTrait<UnderdankVIP>())
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

		public bool bigQuestShidded = false;
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
