using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches.P_PlayfieldObject
{
    class P_AlarmButton
    {
    }

	//TODO
    class AlarmButton_Import
    {
		//public static bool AlarmButton_DetermineButtons(AlarmButton __instance) // Replacement
		//{
		//	BMLog("AlarmButton_DetermineButtons");

		//	ObjectReal_DetermineButtons_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

		//	if (__instance.interactingAgent.interactionHelper.interactingFar)
		//	{
		//		if (!__instance.hacked)
		//			__instance.buttons.Add("AllAccessAlarmButton");

		//		if ((__instance.interactingAgent.oma.superSpecialAbility && __instance.interactingAgent.agentName == "Hacker") ||
		//				__instance.interactingAgent.statusEffects.hasTrait("HacksBlowUpObjects"))
		//			__instance.buttons.Add("HackExplode");
		//	}

		//	if (GC.challenges.Contains(cChallenge.AnCapistan) && !__instance.hacked)
		//	{
		//		if (!__instance.hacked)
		//		{
		//			__instance.buttons.Add("AlarmButtonAncapistan");
		//			__instance.buttonPrices.Add(25);
		//		}
		//		else
		//			__instance.buttons.Add("AlarmButtonAncapistan");
		//	}

		//	return false;
		//}

		//public static bool AlarmButton_DoLockdown(bool closePanicRoomDoors, AlarmButton __instance) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.AnCapistan))
		//	{
		//		if (AlarmButton.lockdownTimerCooldown > 0f)
		//			return false;

		//		if (__instance.gc.serverPlayer)
		//		{
		//			if (__instance.gc.lockdown)
		//				AlarmButton.lockdownTimer = 7.5f;
		//			else
		//			{
		//				AlarmButton.lockdownTimer = 7.5f;
		//				AlarmButton.lockdownTimerBig = 40f;

		//				if (!__instance.noLockdown)
		//					__instance.gc.playerAgent.objectMult.ObjectAction(__instance.objectNetID, "DoLockdown");

		//				__instance.endLockDownGoing = true;
		//				__instance.keepGoing = true;
		//				__instance.firstTick = true;
		//				__instance.mustKeepGoing = false;
		//				__instance.InvokeRepeating("EndLockdownCoroutine", 0.01f, 0.1f);

		//				if (!__instance.noLockdown)
		//					for (int i = 0; i < __instance.gc.agentList.Count; i++)
		//						if (__instance.gc.agentList[i].pathing == 1)
		//						{
		//							__instance.gc.agentList[i].pathfindingAI.rePath = true;
		//							__instance.gc.tileInfo.DoWandererRepath();
		//						}
		//			}
		//		}

		//		if (!__instance.noLockdown && __instance.gc.levelFeeling != "WarZone" && __instance.gc.levelFeeling != "Ooze" &&
		//				__instance.gc.levelFeeling != "Riot" && __instance.gc.levelFeeling != "Zombies" && !__instance.gc.challenges.Contains("ZombieMutator")
		//				&&
		//				!__instance.gc.lockdown)
		//		{
		//			__instance.gc.audioHandler.Play(__instance.gc.playerAgent, "LockdownWallUp");

		//			for (int j = 0; j < __instance.gc.lockdownWallList.Count; j++)
		//				__instance.gc.lockdownWallList[j].SetWallUpAnim();
		//		}

		//		if (__instance.gc.serverPlayer && closePanicRoomDoors)
		//		{
		//			int prison = __instance.gc.tileInfo.GetTileData(__instance.tr.position).prison;

		//			for (int k = 0; k < __instance.gc.objectRealList.Count; k++)
		//			{
		//				ObjectReal objectReal = __instance.gc.objectRealList[k];

		//				if (objectReal.startingChunk == __instance.startingChunk && objectReal.objectName == "Door" && prison != 0 &&
		//						prison == __instance.gc.tileInfo.GetTileData(objectReal.tr.position).prison)
		//				{
		//					bool flag = true;

		//					for (int l = 0; l < __instance.gc.agentList.Count; l++)
		//					{
		//						Agent agent = __instance.gc.agentList[l];

		//						if (agent.curTileData.prison == prison && agent.curTileData.chunkID == __instance.startingChunk &&
		//								(agent.ownerID != __instance.owner || agent.startingChunk != __instance.startingChunk) && !agent.upperCrusty)
		//							flag = false;
		//					}

		//					if (flag)
		//						((Door)objectReal).CloseDoor(null);
		//				}
		//			}
		//		}

		//		return false;
		//	}

		//	return true;
		//}

		//public static bool AlarmButton_EndLockdown(AlarmButton __instance) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.AnCapistan))
		//	{
		//		if (AlarmButton.lockdownTimerCooldown <= 0f)
		//			__instance.StartCoroutine(AlarmButton_LockdownCooldown(__instance));

		//		if (__instance.gc.levelTheme == 4 || __instance.gc.loadLevel.hasLockdownWalls)
		//		{
		//			if (__instance.gc.serverPlayer)
		//				__instance.gc.playerAgent.objectMult.ObjectAction(__instance.objectNetID, "EndLockdown");

		//			if (!__instance.noLockdown)
		//				for (int i = 0; i < __instance.gc.agentList.Count; i++)
		//					if (__instance.gc.agentList[i].pathing == 1)
		//					{
		//						__instance.gc.agentList[i].pathfindingAI.rePath = true;
		//						__instance.gc.tileInfo.DoWandererRepath();
		//					}

		//			if (__instance.gc.lockdown)
		//				__instance.gc.audioHandler.Play(__instance.gc.playerAgent, "LockdownWallDown");

		//			for (int j = 0; j < __instance.gc.lockdownWallList.Count; j++)
		//			{
		//				LockdownWall lockdownWall = __instance.gc.lockdownWallList[j];

		//				if (lockdownWall.objectCollider.enabled)
		//					lockdownWall.SetWallDownAnim();
		//			}

		//			for (int k = 0; k < __instance.gc.objectRealListWithDestroyed.Count; k++)
		//			{
		//				ObjectReal objectReal = __instance.gc.objectRealListWithDestroyed[k];

		//				if (objectReal.objectName == "AlarmButton" && objectReal != null)
		//				{
		//					int prison = __instance.gc.tileInfo.GetTileData(objectReal.tr.position).prison;

		//					for (int l = 0; l < __instance.gc.objectRealList.Count; l++)
		//					{
		//						ObjectReal objectReal2 = __instance.gc.objectRealList[l];

		//						if (objectReal2.startingChunk == objectReal.startingChunk && objectReal2.objectName == "Door" && prison != 0 &&
		//								prison == __instance.gc.tileInfo.GetTileData(objectReal2.tr.position).prison)
		//							((Door)objectReal2).OpenDoor(null);
		//					}
		//				}
		//			}

		//			__instance.StartCoroutine(AlarmButton_JustEndedLockdown(__instance));
		//		}

		//		return false;
		//	}

		//	return true;
		//}

		//public static bool AlarmButton_Interact(Agent agent, AlarmButton __instance) // Replacement
		//{
		//	BMLog("AlarmButton_Interact");

		//	ObjectReal_Interact_base.GetMethodWithoutOverrides<Action<Agent>>(__instance).Invoke(agent);

		//	__instance.lastHitByAgent = __instance.interactingAgent;

		//	if (!__instance.isBroken())
		//	{
		//		if (GC.challenges.Contains(cChallenge.AnCapistan))
		//		{
		//			__instance.ShowObjectButtons();

		//			return false;
		//		}
		//		else
		//		{
		//			if (agent.upperCrusty || __instance.hacked)
		//				__instance.ToggleSwitch(__instance.interactingAgent, null);
		//			else
		//				__instance.Say("CantUseAlarmButton"); // base
		//		}
		//	}

		//	__instance.StopInteraction();

		//	return false;
		//}

		//public static IEnumerator AlarmButton_JustEndedLockdown(AlarmButton __instance) // Non-Patch
		//{
		//	GC.justEndedLockdown = true;

		//	yield return new WaitForSeconds(0.4f);

		//	GC.justEndedLockdown = false;

		//	yield break;
		//}

		//public static IEnumerator AlarmButton_LockdownCooldown(AlarmButton __instance) // Non-Patch
		//{
		//	AlarmButton.lockdownTimerCooldown = 5f;

		//	while (AlarmButton.lockdownTimerCooldown > 0f)
		//	{
		//		AlarmButton.lockdownTimerCooldown -= 0.1f;

		//		if (!GC.loadComplete)
		//			AlarmButton.lockdownTimerCooldown = 0f;

		//		yield return new WaitForSeconds(0.1f);
		//	}

		//	AlarmButton.lockdownTimerCooldown = 0f;

		//	yield break;
		//}

		//public static bool AlarmButton_PressedButton(string buttonText, int buttonPrice, AlarmButton __instance) // Replacement
		//{
		//	ObjectReal_PressedButton_base.GetMethodWithoutOverrides<Action<string, int>>(__instance).Invoke(buttonText, buttonPrice);

		//	if (buttonText == "AllAccessAlarmButton")
		//	{
		//		if ((!__instance.interactingAgent.oma.superSpecialAbility || !(__instance.interactingAgent.agentName == "Hacker")) &&
		//				!__instance.interactingAgent.statusEffects.hasTrait("HacksBlowUpObjects"))
		//			__instance.hackable = false;

		//		__instance.hacked = true;

		//		if (!__instance.gc.serverPlayer)
		//			__instance.gc.playerAgent.objectMult.ObjectAction(__instance.objectNetID, "AllAccess");
		//	}
		//	else if (buttonText == "AlarmButtonAncapistan")
		//	{
		//		if (__instance.moneySuccess(buttonPrice))
		//			__instance.ToggleSwitch(__instance.interactingAgent, null);
		//		else
		//			BMHeaderTools.SayDialogue(__instance.interactingAgent, cDialogue.CantAffordAlarmButton, vNameType.Dialogue);
		//	}

		//	__instance.StopInteraction();

		//	return false;
		//}

		//public static IEnumerator AlarmButton_SetSwitchOn(AlarmButton __instance) // Non-Patch
		//{
		//	__instance.switchOn = true;

		//	yield return new WaitForSeconds(0.5f);

		//	__instance.switchOn = false;

		//	yield break;
		//}

		//public static bool AlarmButton_ToggleSwitch(Agent causerAgent, Agent criminal, AlarmButton __instance) // Prefix
		//{
		//	if (GC.challenges.Contains(cChallenge.AnCapistan))
		//	{
		//		if (GC.serverPlayer)
		//		{
		//			if (causerAgent.isPlayer != 0)
		//			{
		//				if (causerAgent.hasProtector)
		//				{
		//					__instance.Say("AlarmAlreadyActive");
		//					__instance.StopInteraction();
		//				}
		//				else if (causerAgent.spawnedSupercops >= 3 && !GC.challenges.Contains("NoLimits") && !GC.challenges.Contains(cChallenge.AnCapistan))
		//				{
		//					__instance.Say("AlarmLimitReached");
		//					__instance.StopInteraction();
		//				}
		//				else
		//				{
		//					GC.audioHandler.Play(__instance, "AlarmButton");
		//					__instance.SpawnEnforcer(causerAgent, criminal);
		//				}
		//			}
		//			else if (!__instance.switchOn)
		//			{
		//				if (!__instance.switchOn)
		//					__instance.StartCoroutine(AlarmButton_SetSwitchOn(__instance));

		//				if (!__instance.destroyed || __instance.destroying)
		//					GC.audioHandler.Play(__instance, "AlarmButton");

		//				if (__instance.functional)
		//				{
		//					__instance.StartCoroutine(AlarmButton_WaitToBecomeActive(__instance));
		//					__instance.SpawnEnforcer(causerAgent, criminal);
		//				}

		//				if (!__instance.destroying)
		//					causerAgent.usedAlarmButton = true;
		//			}

		//			__instance.DoLockdown(true);

		//			return false;
		//		}
		//		else
		//			__instance.interactingAgent.objectMult.ObjectAction(__instance.objectNetID, "ToggleSwitch");

		//		return false;
		//	}

		//	return true;
		//}

		//public static IEnumerator AlarmButton_WaitToBecomeActive(AlarmButton __instance) // Non-Patch
		//{
		//	for (; ; )
		//		yield return null;

		//	yield break;
		//}

	}
}