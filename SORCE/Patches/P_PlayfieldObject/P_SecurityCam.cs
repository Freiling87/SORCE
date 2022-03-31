using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches.P_PlayfieldObject
{
    class P_SecurityCam
    {
    }

    //TODO
    class SecurityCam_Import
	{
		//public static bool SecurityCam_DetermineButtons(SecurityCam __instance) // Replacement
		//{
		//	ObjectReal_DetermineButtons_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

		//	if (__instance.interactingAgent.interactionHelper.interactingFar)
		//	{
		//		if (__instance.functional)
		//			__instance.buttons.Add("TurnCameraOff");
		//		else
		//			__instance.buttons.Add("TurnCameraOn");

		//		__instance.buttonsExtra.Add("");


		//		__instance.buttons.Add("CamerasCaptureOwners");

		//		if (__instance.targets == "Owners")
		//			__instance.buttonsExtra.Add(" *");
		//		else
		//			__instance.buttonsExtra.Add("");


		//		__instance.buttons.Add("CamerasCaptureNonOwners");

		//		if (__instance.targets == "NonOwners")
		//			__instance.buttonsExtra.Add(" *");
		//		else
		//			__instance.buttonsExtra.Add("");


		//		__instance.buttons.Add("CamerasCaptureEveryone");

		//		if (__instance.targets == "Everyone")
		//			__instance.buttonsExtra.Add(" *");
		//		else
		//			__instance.buttonsExtra.Add("");


		//		__instance.buttons.Add(cButtonText.CamerasCaptureWanted);

		//		if (__instance.targets == "Wanted")
		//			__instance.buttonsExtra.Add(" *");
		//		else
		//			__instance.buttonsExtra.Add("");


		//		__instance.buttons.Add(cButtonText.CamerasCaptureGuilty);

		//		if (__instance.targets == "Guilty")
		//			__instance.buttonsExtra.Add(" *");
		//		else
		//			__instance.buttonsExtra.Add("");


		//		if ((__instance.interactingAgent.oma.superSpecialAbility && __instance.interactingAgent.agentName == "Hacker") ||
		//				__instance.interactingAgent.statusEffects.hasTrait("HacksBlowUpObjects"))
		//		{
		//			__instance.buttons.Add("HackExplode");
		//			__instance.buttonsExtra.Add("");

		//			return false;
		//		}
		//	}
		//	else
		//	{
		//		__instance.buttons.Add("AttemptTurnOffSecurityCam");
		//		__instance.buttonsExtra.Add(" (" + __instance.FindDisarmPercentage(false) + "%)");
		//	}

		//	return false;
		//}

		//public static bool SecurityCam_FinishedOperating(SecurityCam __instance) // Replacement
		//{
		//	if (__instance.interactingAgent.interactionHelper.interactingFar)
		//	{
		//		__instance.ShowObjectButtons();

		//		return false;
		//	}

		//	if (__instance.operatingBarType == "TurningOffSecurityCam")
		//	{
		//		if (__instance.gc.percentChance(__instance.FindDisarmPercentage(true)))
		//		{
		//			if (__instance.gc.serverPlayer)
		//				__instance.MakeNonFunctional(null);
		//			else
		//				__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, "TurnCameraOff");

		//			__instance.interactingAgent.skillPoints.AddPoints("TamperPoliceBoxPoints");
		//			__instance.StopInteraction();

		//			return false;
		//		}

		//		__instance.FailToDisable();
		//		__instance.StopInteraction();
		//	}

		//	return false;
		//}

		//public static void SecurityCam_Interact_Temporary(Agent agent, SecurityCam __instance) // Postfix
		//{
		//	BMLog("SecurityCam_Interact_Temporary");
		//	BMLog("\tName:\t" + __instance.name);
		//	BMLog("\tOwner:\t" + __instance.owner);
		//	BMLog("\tTargets:\t" + __instance.targets);
		//	BMLog("\tTurrets#:\t" + __instance.turrets.Count());
		//}

		//public static bool SecurityCam_MyUpdate(ref IEnumerator __result, SecurityCam __instance, ref bool ___agentsPreviouslyInView) // Prefix
		//{
		//	BMLog("LoadLevel_FillFloors_Prefix");

		//	// Structure advised by Abbysssal for patch-replacing IEnumerators.
		//	__result = SecurityCam_MyUpdate_Replacement(__instance, ___agentsPreviouslyInView);

		//	return false;
		//}

		//public static IEnumerator SecurityCam_MyUpdate_Replacement(SecurityCam __instance, bool ___agentsPreviouslyInView) // Non-Patch
		//{
		//	// Detect Guilty/Wanted for PoliceState & Public Cameras
		//	// Confirmed via logging:
		//	// Modes are set correctly
		//	// Player is detected
		//	// Turrets do count to turret total

		//	for (; ; )
		//	{
		//		#region Detection

		//		if (__instance.functional && !__instance.destroyed && __instance.activeObject)
		//		{
		//			__instance.agentsInView.Clear();

		//			for (int i = 0; i < __instance.gc.activeBrainAgentList.Count; i++)
		//			{
		//				bool agentFlag = false;
		//				Agent agent = __instance.gc.activeBrainAgentList[i];

		//				if (agent.brain.active && !agent.invisible && !agent.ghost && !agent.objectAgent && !agent.mechEmpty && !agent.dead &&
		//						!agent.underBox &&
		//						(agent.prisoner <= 0 || agent.ownerID != 0 || agent.isPlayer != 0))
		//				{
		//					if (__instance.targets == "NonOwners")
		//						agentFlag =
		//								(agent.ownerID != __instance.owner && agent.ownerID != 99) ||
		//								(agent.startingChunk != __instance.startingChunk &&
		//										(__instance.startingSector == 0 || agent.startingSector != __instance.startingSector));
		//					else if (__instance.targets == "Owners")
		//						agentFlag =
		//								((agent.ownerID == __instance.owner || agent.ownerID == 99) &&
		//										(agent.startingChunk == __instance.startingChunk ||
		//												(__instance.startingSector != 0 && agent.startingSector == __instance.startingSector))) ||
		//								((agent.enforcer || agent.statusEffects.hasTrait(vTrait.TheLaw)) && __instance.owner == 85);
		//					else if (__instance.targets == "Everyone")
		//						agentFlag = true;
		//					else if (__instance.targets == "Wanted")
		//						agentFlag = agent.statusEffects.hasTrait(vTrait.Wanted);
		//					else if (__instance.targets == "Guilty")
		//						agentFlag = agent.objectMultAgent.mustBeGuilty || agent.statusEffects.hasTrait(vTrait.Wanted) ||
		//								agent.HasTrait<Priors>();
		//				}

		//				if (agentFlag && agent.curTileData.chunkID == __instance.startingChunk && agent.curTileData.floorMaterial != floorMaterialType.None)
		//				{
		//					float num = Vector2.Distance(agent.tr.position, __instance.tr.position);

		//					if (num > __instance.blindSpotDistance && num < 9f &&
		//							__instance.HasLOSObjectNormal(agent, num) &&
		//							agent.curOwnerTile == __instance.owner &&
		//							!agent.statusEffects.hasTrait("InvisibleToCameras") &&
		//							__instance.functional &&
		//							!__instance.destroyed &&
		//							!__instance.destroying)
		//						__instance.agentsInView.Add(agent);

		//					BMLog("\tAgentsInView:\t" + __instance.agentsInView.Count());
		//				}
		//			}

		//			#endregion

		//			#region Post-Detection

		//			if (__instance.securityType == "Turret")
		//			{
		//				for (int j = 0; j < __instance.turrets.Count; j++)
		//				{
		//					if (__instance.agentsInView.Count > 0)
		//					{
		//						if (!__instance.turrets[j].camerasViewing.Contains(__instance.UID))
		//						{
		//							__instance.turrets[j].camerasViewing.Add(__instance.UID);
		//							__instance.turrets[j].camerasViewingReal.Add(__instance);
		//						}
		//					}
		//					else if (__instance.turrets[j].camerasViewing.Count > 0)
		//					{
		//						__instance.turrets[j].camerasViewing.Remove(__instance.UID);
		//						__instance.turrets[j].camerasViewingReal.Remove(__instance);
		//					}
		//				}

		//				if (__instance.agentsInView.Count > 0 && __instance.countdownToNoise <= 0f)
		//				{
		//					for (int k = 0; k < __instance.agentsInView.Count; k++)
		//					{
		//						__instance.ChangeDoorTags();

		//						if (!__instance.gc.cinematic)
		//							__instance.gc.audioHandler.Play(__instance, "SecurityCamSpot");

		//						__instance.gc.spawnerMain.SpawnStateIndicator(__instance, "HighVolume");
		//						__instance.gc.spawnerMain.SpawnNoise(__instance.agentsInView[k].tr.position, 4f, __instance, "Alarm",
		//								__instance.agentsInView[k]);

		//						if ((__instance.targets == "Everyone" || __instance.targets == "Owners") &&
		//								(__instance.agentsInView[k].ownerID == __instance.owner || __instance.agentsInView[k].ownerID == 99) &&
		//								__instance.agentsInView[k].startingChunk == __instance.startingChunk)
		//						{
		//							__instance.agentsInView[k].relationships.SetRel(__instance.objectAgent, "Hateful");
		//							__instance.agentsInView[k].relationships.SetRelHate(__instance.objectAgent, 5);
		//						}

		//						__instance.countdownToNoise = 1f;
		//					}
		//				}
		//				else if (__instance.agentsInView.Count == 0)
		//					__instance.countdownToNoise = 0f;
		//			}
		//			else if (__instance.securityType == "Noise")
		//			{
		//				if (__instance.agentsInView.Count > 0 && __instance.countdownToNoise <= 0f)
		//				{
		//					for (int j = 0; j < __instance.agentsInView.Count; j++)
		//					{
		//						__instance.ChangeDoorTags();

		//						if (!__instance.gc.cinematic)
		//							__instance.gc.audioHandler.Play(__instance, "SecurityCamSpot");

		//						__instance.gc.spawnerMain.SpawnStateIndicator(__instance, "HighVolume");
		//						__instance.gc.spawnerMain.SpawnNoise(__instance.agentsInView[j].tr.position, 4f, __instance, "Alarm",
		//								__instance.agentsInView[j]);

		//						if ((__instance.targets == "Everyone" || __instance.targets == "Owners") &&
		//								(__instance.agentsInView[j].ownerID == __instance.owner || __instance.agentsInView[j].ownerID == 99) &&
		//								__instance.agentsInView[j].startingChunk == __instance.startingChunk)
		//						{
		//							__instance.agentsInView[j].relationships.SetRel(__instance.objectAgent, "Hateful");
		//							__instance.agentsInView[j].relationships.SetRelHate(__instance.objectAgent, 5);
		//						}

		//						__instance.countdownToNoise = 1f;
		//					}
		//				}
		//				else if (__instance.agentsInView.Count == 0)
		//					__instance.countdownToNoise = 0f;
		//			}

		//			#endregion

		//			if (!___agentsPreviouslyInView && __instance.agentsInView.Count > 0)
		//			{
		//				___agentsPreviouslyInView = true;
		//				__instance.RemoveThenSpawnParticles("");
		//			}
		//			else if (___agentsPreviouslyInView && __instance.agentsInView.Count == 0)
		//			{
		//				___agentsPreviouslyInView = false;
		//				__instance.RemoveThenSpawnParticles("");
		//			}
		//		}

		//		yield return new WaitForSeconds(0.1f);
		//	}

		//	yield break;
		//}

		//public static bool SecurityCam_PressedButton(string buttonText, int buttonPrice, SecurityCam __instance) // Replacement
		//{
		//	ObjectReal_PressedButton_base.GetMethodWithoutOverrides<Action<string, int>>(__instance).Invoke(buttonText, buttonPrice);

		//	if (buttonText == "AttemptTurnOffSecurityCam")
		//	{
		//		__instance.StartCoroutine(__instance.Operating(__instance.interactingAgent, null, 2f, true, "TurningOffSecurityCam"));

		//		return false;
		//	}
		//	else if (buttonText == "CamerasCaptureEveryone")
		//	{
		//		__instance.targets = "Everyone";

		//		if (!__instance.gc.serverPlayer)
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, "CamerasCaptureEveryone");

		//		__instance.RefreshButtons();

		//		return false;
		//	}
		//	else if (buttonText == cButtonText.CamerasCaptureGuilty)
		//	{
		//		__instance.targets = "Guilty";

		//		if (!__instance.gc.serverPlayer)
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, cButtonText.CamerasCaptureGuilty);

		//		__instance.RefreshButtons();

		//		return false;
		//	}
		//	else if (buttonText == cButtonText.CamerasCaptureWanted)
		//	{
		//		__instance.targets = "Wanted";

		//		if (!__instance.gc.serverPlayer)
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, cButtonText.CamerasCaptureWanted);

		//		__instance.RefreshButtons();

		//		return false;
		//	}
		//	else if (buttonText == "CamerasCaptureNonOwners")
		//	{
		//		__instance.targets = "NonOwners";

		//		if (!__instance.gc.serverPlayer)
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, "CamerasCaptureNonOwners");

		//		__instance.RefreshButtons();

		//		return false;
		//	}
		//	else if (buttonText == "CamerasCaptureOwners")
		//	{
		//		__instance.targets = "Owners";

		//		if (!__instance.gc.serverPlayer)
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, "CamerasCaptureOwners");

		//		__instance.RefreshButtons();

		//		return false;
		//	}
		//	else if (buttonText == "TurnCameraOff")
		//	{
		//		if (__instance.gc.serverPlayer)
		//			__instance.MakeNonFunctional(null);
		//		else
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, "TurnCameraOff");

		//		__instance.RefreshButtons();

		//		return false;
		//	}
		//	else if (buttonText == "TurnCameraOn")
		//	{
		//		__instance.MakeFunctional();

		//		if (!__instance.gc.serverPlayer)
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, "TurnCameraOn");

		//		__instance.RefreshButtons();

		//		return false;
		//	}

		//	__instance.StopInteraction();

		//	return false;
		//}

		//public static bool SecurityCam_ObjectAction(string myAction, string extraString, float extraFloat, Agent causerAgent, PlayfieldObject extraObject,
		//		SecurityCam __instance, ref bool ___noMoreObjectActions) // Replacement
		//{
		//	ObjectReal_ObjectAction_base.GetMethodWithoutOverrides<Action<string, string, float, Agent, PlayfieldObject>>(__instance)
		//			.Invoke(myAction, extraString, extraFloat, causerAgent, extraObject);

		//	if (!___noMoreObjectActions)
		//	{
		//		if (myAction == "CamerasCaptureEveryone")
		//			__instance.targets = "Everyone";
		//		else if (myAction == cButtonText.CamerasCaptureGuilty)
		//			__instance.targets = "Guilty";
		//		else if (myAction == "CamerasCaptureNonOwners")
		//			__instance.targets = "NonOwners";
		//		else if (myAction == "CamerasCaptureOwners")
		//			__instance.targets = "Owners";
		//		else if (myAction == cButtonText.CamerasCaptureWanted)
		//			__instance.targets = "Wanted";
		//		else if (myAction == "FailToDisable")
		//			__instance.FailToDisable();
		//		else if (myAction == "RemoveParticles")
		//		{
		//			__instance.functional = false;
		//			__instance.RemoveParticles(true, false);
		//		}
		//		else if (myAction == "RemoveThenSpawnParticles")
		//		{
		//			__instance.functional = true;
		//			__instance.RemoveThenSpawnParticles(extraString);
		//		}
		//		else if (myAction == "SpawnParticles")
		//		{
		//			__instance.functional = true;
		//			__instance.SpawnParticles(false);
		//		}
		//		else if (myAction == "TurnCameraOff")
		//			__instance.MakeNonFunctional(null);
		//		else if (myAction == "TurnCameraOn")
		//			__instance.MakeFunctional();
		//	}

		//	___noMoreObjectActions = false;

		//	return false;
		//}

		//public static void SecurityCam_StartLate(SecurityCam __instance) // Postfix
		//{
		//	if (__instance.owner == 85)
		//	{
		//		if (GC.challenges.Contains(cChallenge.PoliceState))
		//			__instance.targets = "Guilty";
		//		else
		//			__instance.targets = "Wanted";
		//	}
		//}
	}
}
