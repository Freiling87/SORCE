using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches.P_PlayfieldObject
{
    class P_Lamp
    {
    }

    //TODO
    class Lamp_Import
	{
		//public static bool Lamp_Start(Lamp __instance) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.DiscoCityDanceoff) || BMHeader.debugMode)
		//	{
		//		ObjectReal_Start_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

		//		if ((GC.serverPlayer || __instance.functional) && (__instance.gc.lightingType == "Full" || __instance.gc.lightingType == "Med"))
		//		{
		//			LightTemp lightTemp = __instance.gc.spawnerMain.SpawnLightTemp(__instance.transform.position, __instance, "Lamp");

		//			if (GC.percentChance(33))
		//				lightTemp.fancyLight.Color = new Color(0f, 0f, 0.75f, 0.75f);
		//			else if (GC.percentChance(50))
		//				lightTemp.fancyLight.Color = new Color(0f, 0.75f, 0f, 0.75f);
		//			else
		//				lightTemp.fancyLight.Color = new Color(0.75f, 0f, 0f, 0.75f);

		//			lightTemp.transform.position = new Vector3(__instance.transform.position.x, __instance.transform.position.y + 0.24f,
		//					lightTemp.transform.position.z);
		//			__instance.StartCoroutine(__instance.WaitToStartAmbientAudio());
		//		}

		//		return false;
		//	}

		//	return true;
		//}

	}
}
