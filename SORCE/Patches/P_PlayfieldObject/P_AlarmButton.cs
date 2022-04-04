using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    // [HarmonyPatch(declaringType: typeof(AlarmButton))]
    class P_AlarmButton
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

        static int cost = 25;

        // TODO: Stop Callbacks (see Roguelibs documentation)
        // TODO: Remove vanilla buttons when appropriate
        //      This is Abbysssal's example code for how to do this:
        //          FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
        //          List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
        //          interactions.RemoveAll(static i => i.ButtonName is "123");
        // TODO: RL Costs are for display only. Add that logic.
        [RLSetup]
		public static void Setup()
        {
            RogueInteractions.CreateProvider<AlarmButton>(h =>
            {
                if (GC.challenges.Contains(nameof(AnCapistan)))
                {
                    if (h.Object.hacked || 
                        h.Agent.upperCrusty ||
                        h.Helper.interactingFar && h.Agent.HasTrait(vTrait.TechExpert))
                        h.AddButton(vButtonText.AlarmButtonAncapistan, m =>
                        {
                            m.Object.ToggleSwitch(m.Object.interactingAgent, null);
                        });
                    else
                        h.AddButton(vButtonText.AlarmButtonAncapistan, cost, m =>
                        {
                            m.Object.ToggleSwitch(m.Object.interactingAgent, null);
                        });
                }
            });
        }
    }
}