using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches.P_PlayfieldObject
{
    class P_FireHydrant
    {
    }

	//TODO
    class FireHydrant_Import
    {
		//public static bool FireHydrant_DetermineButtons(FireHydrant __instance) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.AnCapistan))
		//	{
		//		ObjectReal_DetermineButtons_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

		//		if (__instance.interactingAgent.statusEffects.hasSpecialAbility("WaterCannon"))
		//		{
		//			__instance.buttons.Add("RefillWaterCannon");
		//			__instance.buttonPrices.Add(10);

		//			return false;
		//		}

		//		BMHeaderTools.SayDialogue(__instance.interactingAgent, "CantUseFireHydrant", vNameType.Dialogue);

		//		return false;
		//	}

		//	return true;
		//}

		//public static bool FireHydrant_PressedButton(string buttonText, int buttonPrice, FireHydrant __instance) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.AnCapistan))
		//	{
		//		PlayfieldObject_PressedButton_base.GetMethodWithoutOverrides<Action<string, int>>(__instance).Invoke(buttonText, buttonPrice);

		//		if (buttonText == "RefillWaterCannon")
		//		{
		//			if (__instance.moneySuccess(buttonPrice))
		//			{
		//				__instance.RefillWaterCannon();
		//				__instance.StopInteraction();
		//			}

		//			return false;
		//		}

		//		__instance.StopInteraction();

		//		return false;
		//	}

		//	return true;
		//}
	}
}
