using BepInEx.Logging;
using HarmonyLib;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SORCE.Patches.P_PlayfieldObject
{
	[HarmonyPatch(declaringType: typeof(Turret))]
	class P_Turret
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

#pragma warning disable CS0618 // Type or member is obsolete
        [HarmonyPrefix, HarmonyPatch(methodName: nameof (Turret.FireGun), argumentTypes: new[] { typeof(int), typeof(NetworkInstanceId) })]
        public static bool FireGun_Prefix(int bulletNetID, NetworkInstanceId opponentID, Turret __instance)
        {
			Audiovisual.MuzzleFlash(__instance.tr.position);

			return true;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(Turret.FireGun), argumentTypes: new[] { typeof(int), typeof(NetworkInstanceId) })]
		public static void FireGun_Postfix(int bulletNetID, NetworkInstanceId opponentID, Turret __instance)
		{
			Audiovisual.SpawnBulletCasing(__instance.tr.position, CSprite.RifleCasing); // This breaks firing, deletes the bullet or something
			//__instance.timeSinceLastBullet = 0.1f; // TODO: Move to Security mod
		}
#pragma warning restore CS0618 // Type or member is obsolete
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
