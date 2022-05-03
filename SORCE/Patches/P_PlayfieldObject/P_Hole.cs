using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Extensions;
using SORCE.Logging;
using SORCE.Traits;
using SORCE.Utilities;
using System.Collections;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Hole))]
	public static class P_Hole
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
				__instance.GetComponent<ObjectMultHole>().objectHoleType == VObject.Manhole)
			{
				Agent agent = myObject.GetComponent<Agent>();

				if ((agent.HasTrait<UnderdankCitizen>() || agent.HasTrait<UnderdankVIP>())
					&& E_Agent.IsFlushableManhole(agent))
				{
					FallInHoleDamage(agent);
					Underdank.FlushYourself(agent, __instance);

					//agent.StartCoroutine(FallInManhole(agent, __instance, __instance.trapDoor));
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// An adaptation of vanilla Agent.AgentFell
		/// </summary>
		/// <param name="agent"></param>
		/// <param name="myHole"></param>
		/// <param name="trapDoor"></param>
		/// <returns></returns>
		private static IEnumerator FallInManhole(Agent agent, Hole myHole, TrapDoor trapDoor)
		{
			bool canTakeHealth = true;
			
			if (agent.health <= 0f && agent.resurrect)
				canTakeHealth = false;
			
			yield return new WaitForSeconds(1.5f);
			
			if (myHole != null)
				myHole.agentsInHole--;
			
			if (!agent.finishedLevel)
			{
				if (agent.health > 0f)
				{
					agent.deathMethod = "FellInHole";
					agent.deathKiller = "Self";
				}

				if (canTakeHealth)
					FallInHoleDamage(agent);

				agent.fakeDead = false;

				if (agent.ghost)
				{
					agent.SetOverHole(false);
					agent.fellInHole = false;
					agent.fellInHoleLocal = false;
					agent.fellInHoleWhileDead = false;
				}
				if ((agent.health > 0f || agent.mechFilled) && !agent.gc.mainGUI.questNotification.gameIsOver)
				{
					agent.bulletsCanPass = false;
					agent.meleeCanPass = false;
					agent.GetComponent<Rigidbody2D>().isKinematic = false;
					agent.EnableColliderIfPossible();
					agent.EnableHitboxes(true);
					agent.agentItemColliderTr.GetComponent<BoxCollider2D>().enabled = true;

					if (!agent.gc.consoleVersion)
						agent.EnableMouseboxes(true);
					
					agent.disappeared = false;
					agent.agentSpriteTransform.gameObject.SetActive(true);
					agent.agentHitboxScript.wholeBodyAnim.gameObject.SetActive(false);
					agent.agentHitboxScript.shadowGO.SetActive(true);

					if ((agent.gc.coopMode || agent.gc.fourPlayerMode || agent.gc.multiplayerMode) && agent.isPlayer > 0 && agent.playerIdentifier != null)
						agent.playerIdentifier.SetActive(true);
					
					if (agent.lightReal != null)
						agent.lightReal.gameObject.SetActive(true);
					
					agent.gun.enabled = true;
					agent.melee.enabled = true;
					agent.dead = false;
					agent.bodyVanished = false;
					agent.wholeBody.transform.Find("WholeBodyContainer").transform.localScale = Vector3.one;
					agent.melee.meleeHitbox.SetDisabled();
					agent.SetOverHole(false);
					agent.fellInHole = false;
					agent.fellInHoleLocal = false;
					agent.movement.SetPhysics("Normal");

					Underdank.FlushYourself(agent, myHole.GetComponent<PlayfieldObject>()); //

					//if (agent.gc.tileInfo.GetTileData(agent.mostRecentLandPos2).hole || 
					//	agent.gc.tileInfo.IsOverlapping(agent.mostRecentLandPos2, "Hole"))
					//	agent.tr.position = agent.gc.tileInfo.FindLocationNearLocation(agent.mostRecentLandPos2, agent, 2f, 5f, true, false);
					//else
					//	agent.tr.position = agent.mostRecentLandPos2;

					if (agent.mechFilled && agent.health <= 0f)
					{
						if (agent.gc.serverPlayer)
						{
							agent.statusEffects.MechTransformBackStart(true);
							agent.objectMult.SpecialAbility("MechTransformBackDead", null);
						}
						else
							agent.objectMult.SpecialAbility("MechTransformBackStartDead", null);
					}
					
					agent.curPosLateUpdate = agent.tr.position;
					agent.curTileData = agent.gc.tileInfo.GetTileDataAndNearWater(agent, agent.curPosLateUpdate);
					
					if (agent.curTileData.ice)
					{
						agent.onIce = true;
						agent.movement.SetPhysics("Ice");
					}
					else if ((!agent.curTileData.ice || agent.ghost) && agent.onIce)
					{
						agent.onIce = false;
						agent.movement.SetPhysics("Normal");
					}

					//if (canTakeHealth)
					//{
					//	agent.gc.spawnerMain.SpawnParticleEffect("Spawn", agent.tr.position, 0f);
					//	agent.gc.audioHandler.Play(agent, "SpawnOnClients");
					//}
				}
			}
			yield break;
		}

		private static void FallInHoleDamage(Agent agent)
		{
			float damage = -30f;

			if (agent.HasTrait<UnderdankCitizen>())
				damage -= 10f;
			else if (agent.HasTrait<UnderdankVIP>())
				damage -= 20f;

			if (GC.challenges.Contains(VChallenge.LowHealth))
				damage *= 0.50f;

			damage = Mathf.RoundToInt(damage);

			if (damage > 0)
			{
				agent.deathMethod = "FellInHole";
				agent.deathKiller = "Self";
				agent.statusEffects.ChangeHealth(damage);
			}
		}
	}
}
