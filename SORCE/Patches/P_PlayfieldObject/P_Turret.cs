using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches.P_PlayfieldObject
{
    class P_Turret
    {
    }

    //TODO
    class Turret_Import
    {
		//public static void Turret_IsOpponent(Agent myAgent, bool brainMustBeActive, Turret __instance, ref bool __result) // Postfix
		//{
		//	// Public security cams
		//	// Police State

		//	if ((!brainMustBeActive || myAgent.brain.active) &&
		//			!myAgent.invisible &&
		//			!myAgent.ghost &&
		//			!myAgent.objectAgent &&
		//			(myAgent.prisoner <= 0 || myAgent.ownerID != 0) &&
		//			!myAgent.statusEffects.hasTrait("InvisibleToCameras"))
		//	{
		//		if (__instance.targets == "Wanted")
		//		{
		//			if (myAgent.statusEffects.hasTrait(vTrait.Wanted))
		//				__result = true;
		//		}

		//		if (GC.challenges.Contains(cChallenge.PoliceState)) // Can always override others
		//			if (myAgent.statusEffects.hasTrait(vTrait.Wanted) || myAgent.objectMultAgent.mustBeGuilty)
		//				__result = true;
		//	}
		//}
	}
}
