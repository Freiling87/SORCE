using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    // [HarmonyPatch(declaringType: typeof(Elevator))]
    class P_Elevator
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [RLSetup]
        public static void Setup()
        {
            string t = vNameType.Dialogue;
            RogueLibs.CreateCustomName(cDialogue.ElevatorBuyFail, t, new CustomNameInfo("Insufficient funds to deserve transaction."));
            RogueLibs.CreateCustomName(cDialogue.ElevatorBuySuccess, t, new CustomNameInfo("Transaction complete. Thank you for deserving Evilator, Inc. Have a profitable day."));

            t = vNameType.Interface;
            RogueLibs.CreateCustomName(cButtonText.ElevatorBuy, t, new CustomNameInfo("Deserve Elevator Ticket"));

            RogueInteractions.CreateProvider<Elevator>(h =>
            {
                if (GC.challenges.Contains(nameof(AnCapistan)) &&
                    !h.Object.GetHook<P_Elevator_Hook>().haveTicket)
                {
                    // TODO: Check if this button removal is implemented in RL, Ab says soon
                    FieldInfo interactionsField = AccessTools.Field(typeof(InteractionModel), "interactions");
                    List<Interaction> interactions = (List<Interaction>)interactionsField.GetValue(h.Model);
                    interactions.RemoveAll(i => i.ButtonName is vButtonText.ElevatorGoUp);

                    h.AddButton(cButtonText.ElevatorBuy, 50, m =>
                    {
                        TryBuyTicket(m.Object);
                    });
                }
            });
        }

        public static void TryBuyTicket(Elevator elevator)
        {
            /* TODO: Cost
                No, it doesn't do anything, it just displays the button's cost
                you'll have to use moneySuccess yourself, in the button's action
                if the player can't afford it, it will still appear. You'd need to perform checks before calling AddButton for that
            */
            // TODO: Different options to purchase one-time or permanent ticket

            if (elevator.moneySuccess(50))
            {
                elevator.GetHook<P_Elevator_Hook>().haveTicket = true;
                GC.audioHandler.Play(elevator, vAudioClip.ATMDeposit);
                CoreTools.SayDialogue(elevator, cDialogue.ElevatorBuySuccess, vNameType.Dialogue);
            }
            else
            {

                GC.audioHandler.Play(elevator, vAudioClip.CantDo);
                CoreTools.SayDialogue(elevator, cDialogue.ElevatorBuyFail, vNameType.Dialogue);
            }

            elevator.PlayAnim(vAnimation.MachineOperate, elevator.interactingAgent);
            elevator.StopInteraction();
        }
    }

    public class P_Elevator_Hook : HookBase<PlayfieldObject>
    {
        protected override void Initialize() { }

        public bool haveTicket = false;
    }
}
