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

		static int toiletCost;

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

			RogueInteractions.CreateProvider<Toilet>(h =>
			{
				toiletCost = GC.challenges.Contains(nameof(AnCapistan))
					? 10
					: 0;

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

				if (Core.debugMode)
					h.AddButton(CButtonText.TakeHugeShit, toiletCost, m =>
					{
						// Add loading bar
						TakeHugeShit(m.Agent, m.Object);
					});
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

		private static void TakeHugeShit(Agent agent, Toilet toilet)
        {
			Vector2 loc = toilet.tr.position;

			Wreckage.SpawnWreckagePileObject_Granular(
				new Vector2(loc.x, loc.y),
				VObject.FlamingBarrel,
				false,
				3,
				0.64f, 0.64f);

			GC.spawnerMain.SpawnExplosion(agent, loc, VExplosion.Water);
			P_00_ObjectReal.AnnoyWitnessesVictimless(agent);
		}
	}
}
