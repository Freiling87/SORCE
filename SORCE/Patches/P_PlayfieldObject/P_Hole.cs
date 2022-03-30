﻿using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Logging;
using SORCE.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
	[HarmonyPatch(declaringType: typeof(Hole))]
	class P_Hole
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// TODO: Turn into Transpiler method
		/// Underdank Citizen teleportation
		/// </summary>
		/// <param name="myObject"></param>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Hole.EnterRange), argumentTypes: new[] { typeof(GameObject) })]
		public static bool Hole_EnterRange(GameObject myObject, Hole __instance)
		{
			if (GC.loadComplete && 
				myObject.CompareTag("Agent") && 
				__instance.GetComponent<ObjectMultHole>().objectHoleType == vObject.Manhole)
			{
				Agent agent = myObject.GetComponent<Agent>();

				if (agent.HasTrait<UnderdankCitizen>() &&
					!agent.statusEffects.hasStatusEffect(vStatusEffect.Giant))
				{
					P_Manhole.FlushYourself(agent, (Manhole)__instance.GetComponent<ObjectReal>());

					if (GC.challenges.Contains(vChallenge.LowHealth))
						agent.statusEffects.ChangeHealth(-7f);
					else
						agent.statusEffects.ChangeHealth(-15f);

					return false;
				}
			}

			return true;
		}
	}
}