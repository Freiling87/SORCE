using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Logging;
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
        //  This is Abbysssal's example code for how to do this:
            //FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
            //List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
            //interactions.RemoveAll(static i => i.ButtonName is "123");

        // TODO: Transpilers & Eliminations
        // None of these have been tested since import, and I'm not 100% they were tested before.

        /*  Transpilers TODO
         * Method                  Line        +/-     Code
         *     DoLockdown           0005        +       || GC.challenges.Contains(nameof(AnCapistan))
         *     EndLockdown          0009        +       || GC.challenges.Contains(nameof(AnCapistan))
         *     ToggleSwitch         0019        +       || GC.challenges.Contains(nameof(AnCapistan))
         */

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