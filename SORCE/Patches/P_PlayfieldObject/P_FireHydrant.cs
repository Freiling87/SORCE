using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Logging;
using System.Collections.Generic;
using System.Reflection;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    //[HarmonyPatch(declaringType: typeof(FireHydrant))]
    class P_FireHydrant
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        const int refillPrice = 10;

        [RLSetup]
        public static void Setup()
        {
            string t = vNameType.Dialogue;
            RogueLibs.CreateCustomName(cDialogue.FireHydrantBuyFail, t, new CustomNameInfo("Insufficient funds to deserve transaction."));
            RogueLibs.CreateCustomName(cDialogue.FireHydrantBuySuccess, t, new CustomNameInfo("Transaction complete. Thank you for deserving Nastly Water Products, Inc. Have a profitable day."));

            t = vNameType.Interface;
            RogueLibs.CreateCustomName(cButtonText.FireHydrantBuy, t, new CustomNameInfo("Deserve Water"));

            RogueInteractions.CreateProvider<FireHydrant>(h =>
            {
                if (GC.challenges.Contains(nameof(AnCapistan)) &&
                    h.Agent.statusEffects.hasSpecialAbility(VanillaAbilities.WaterCannon))
                {
                    FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
                    List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
                    interactions.RemoveAll(i => i.ButtonName is vButtonText.RefillWaterCannon);

                    h.AddButton(cButtonText.FireHydrantBuy, refillPrice, m =>
                    {
                        TryRefillWaterCannon(m.Object);
                    });
                }
            });
        }

        public static void TryRefillWaterCannon(FireHydrant hydrant)
        {
            // TODO: Ignore if already full

            if (hydrant.moneySuccess(refillPrice))
            {
                hydrant.RefillWaterCannon();
                GC.audioHandler.Play(hydrant, vAudioClip.ATMDeposit);
                CoreTools.SayDialogue(hydrant, cDialogue.FireHydrantBuySuccess, vNameType.Dialogue);
            }
            else
            {
                GC.audioHandler.Play(hydrant, vAudioClip.CantDo);
                CoreTools.SayDialogue(hydrant, cDialogue.FireHydrantBuyFail, vNameType.Dialogue);
            }

            hydrant.StopInteraction();
        }
    }
}
