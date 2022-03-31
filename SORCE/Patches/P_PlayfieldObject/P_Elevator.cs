using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches.P_PlayfieldObject
{
    class P_Elevator
    {
    }

    //TODO
    class Elevator_Import
	{
		//public static bool Elevator_DetermineButtons(Elevator __instance) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.AnCapistan))
		//	{
		//		ObjectReal_DetermineButtons_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

		//		if (Elevator_Variables[__instance].ticketPurchased)
		//			__instance.buttons.Add("ElevatorGoUp");
		//		else
		//		{
		//			__instance.buttons.Add("Elevator_PurchaseTicket");
		//			__instance.buttonPrices.Add(50);
		//		}

		//		return false;
		//	}

		//	return true;
		//}

		//public static bool Elevator_PressedButton(string buttonText, Elevator __instance, ref bool ___showingSecondButtonSet) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.AnCapistan))
		//	{
		//		ObjectReal_PressedButton_base.GetMethodWithoutOverrides<Action<string>>(__instance).Invoke(buttonText);

		//		if (buttonText == "StartTutorial")
		//		{
		//			GC.challenges.Clear();
		//			GC.SetDailyRunText();
		//			GC.sessionDataBig.coopMode = false;
		//			GC.sessionDataBig.fourPlayerMode = false;
		//			GC.sessionDataBig.threePlayer = false;
		//			GC.sessionDataBig.newCharacter = "Hobo";
		//			GC.loadLevel.RestartGame(101);

		//			return false;
		//		}
		//		else if (buttonText == "Elevator_PurchaseTicket")
		//			Elevator_PurchaseTicket(__instance);

		//		if (!(buttonText == "ElevatorGoUp"))
		//		{
		//			__instance.StopInteraction();

		//			return false;
		//		}

		//		Agent interactingAgent = __instance.interactingAgent;

		//		if (__instance.BigQuestRunning(interactingAgent) && !___showingSecondButtonSet)
		//		{
		//			___showingSecondButtonSet = true;
		//			__instance.RefreshButtons();
		//			__instance.SetObjectNameDisplay(interactingAgent);

		//			return false;
		//		}

		//		__instance.StopInteraction();
		//		GC.exitPoint.TryToExit(interactingAgent);
		//		interactingAgent.mainGUI.invInterface.justPressedInteract = false;

		//		return false;
		//	}

		//	return true;
		//}

		//public static void Elevator_PurchaseTicket(Elevator __instance)
		//{
		//	if (__instance.moneySuccess(50))
		//	{
		//		Elevator_Variables[__instance].ticketPurchased = true;
		//		//__instance.PlayAnim("MachineOperate", __instance.interactingAgent);
		//		GC.audioHandler.Play(__instance.interactingAgent, vAudioClip.ATMDeposit);
		//		BMHeaderTools.SayDialogue(__instance, cDialogue.PurchaseElevator, vNameType.Dialogue);
		//	}
		//	else
		//	{
		//		BMHeaderTools.SayDialogue(__instance, cDialogue.CantAffordElevator, vNameType.Dialogue);

		//		PlayfieldObject_StopInteraction_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

		//		return;
		//	}
		//}

		//public static Dictionary<Elevator, Elevator_Remora> Elevator_Variables = new Dictionary<Elevator, Elevator_Remora>();

	}
}
