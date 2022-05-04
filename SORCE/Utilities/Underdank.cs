using BepInEx.Logging;
using Light2D;
using RogueLibsCore;
using SORCE.BigQuests;
using SORCE.Extensions;
using SORCE.Logging;
using SORCE.Patches.P_PlayfieldObject;
using SORCE.Traits;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Utilities
{
	public static class Underdank
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool UnderdankActive;

		public static PlayfieldObject ClosedExit(PlayfieldObject entry)
		{
			List<PlayfieldObject> exits = new List<PlayfieldObject>();
			exits.AddRange(GC.objectRealList.OfType<Manhole>().Where(manhole => !manhole.opened));
			exits.AddRange(GC.objectRealList.OfType<Toilet>().Where(toilet => !toilet.destroyed));

			if (exits.OfType<Manhole>().Any(mh => mh.hole == entry))
				exits.Remove(exits.OfType<Manhole>().FirstOrDefault(mh => mh.hole == entry));

			if (exits.Contains(entry))
				exits.Remove(entry);

			return exits.RandomElement();
		}

		public static List<PlayfieldObject> Exits(Agent agent, PlayfieldObject entry)
		{
			List<PlayfieldObject> exits = new List<PlayfieldObject>();
			exits.AddRange(GC.objectRealList.OfType<Manhole>().Where(manhole => manhole.opened));

			if (E_Agent.IsFlushableToilet(agent))
				exits.AddRange(GC.objectRealList.OfType<Toilet>().Where(toilet => !toilet.destroyed));

			if (exits.OfType<Manhole>().Any(mh => mh.hole == entry))
				exits.Remove(exits.OfType<Manhole>().FirstOrDefault(mh => mh.hole == entry));

			if (exits.Contains(entry))
				exits.Remove(entry);

			return exits;
		}

		public static void ExitUnderdank(Agent agent, PlayfieldObject exit, bool pressurizedExit)
		{
			Vector3 exitSpot = exit.curPosition;
			Vector2 exitFacing = Vector2.zero;

			if (exit is Manhole manhole)
			{
				exitFacing = Random.insideUnitCircle.normalized;

				if (pressurizedExit)
					PressurizedExitDamage(agent, manhole);
			}
			else if (exit is Toilet toilet)
			{
				switch (toilet.direction)
				{
					case "E":
						exitFacing = new Vector3(0.32f, 0f, 0f);
						break;
					case "N":
						exitFacing = new Vector3(0f, 0.32f, 0f);
						break;
					case "S":
						exitFacing = new Vector3(0f, -0.32f, 0f);
						break;
					case "W":
						exitFacing = new Vector3(-0.32f, 0f, 0f);
						break;
				}

				if (pressurizedExit)
					PressurizedExitDamage(agent, toilet);
			}

			agent.tr.position = exitSpot + (Vector3)exitFacing;
			agent.jumpDirection = agent.tr.position * exitFacing;
			agent.jumpSpeed = 0.5f;
			agent.Jump();

			GC.audioHandler.Play(agent, VAudioClip.ToiletTeleportOut);

			if (GC.percentChance(10))
				Shitsplode(exit, true, false);
			else if (!(pressurizedExit && exit is Toilet toilet)) // Explosion called in Toilet.DestroyMe
				GC.spawnerMain.SpawnExplosion(exit, exitSpot, VExplosion.Water, false, -1, false, ((ObjectReal)exit).FindMustSpawnExplosionOnClients(agent));
		}

		/// <summary>
		/// Damage player and destroy exit object
		/// </summary>
		/// <param name="agent"></param>
		/// <param name="exit"></param>
		private static void PressurizedExitDamage(Agent agent, PlayfieldObject exit)
        {
			float damage = -30f;

			if (agent.HasTrait<UnderdankCitizen>())
				damage -= 5f;
			else if (agent.HasTrait<UnderdankVIP>())
				damage -= 10f;

			if (GC.challenges.Contains(VChallenge.LowHealth))
				damage *= 0.50f;

			if (agent.inventory.equippedArmorHead != null)
            {
				InvItem helmet = agent.agentInvDatabase.equippedArmorHead;
				int storedDurability = helmet.invItemCount;
				agent.agentInvDatabase.DepleteArmor("Head", (int)damage);
				damage -= storedDurability;
			}

			damage = Mathf.RoundToInt(damage);

			if (damage > 0)
			{
				agent.deathMethod = "FellInHole";
				agent.deathKiller = "Self";
				agent.statusEffects.ChangeHealth(damage);
			}

			if (exit is Manhole manhole)
				manhole.HoleAppear();
			else if (exit is Toilet toilet)
				toilet.DestroyMe();
		}

		public static void FlushYourself(Agent agent, PlayfieldObject entry)
		{
			logger.LogDebug("FlushYourself");
			GC.audioHandler.Play(agent, VAudioClip.ToiletTeleportIn);
			List<PlayfieldObject> exits = Exits(agent, entry);

			if (exits.Any())
				ExitUnderdank(agent, exits.RandomElement(), false);
            else
				ExitUnderdank(agent, ClosedExit(entry), true);
		}

		public static void TakeHugeShit(Toilet toilet, bool loud = true)
		{
			Shitsplode(toilet, loud, true);

			if (toilet.interactingAgent != null)
			{
				Agent agent = toilet.interactingAgent;

				if (agent.bigQuest == nameof(ToiletTourist))
				{
					bool questFlag = true;

					foreach (ObjectReal objectReal in GC.objectRealList)
					{
						if (objectReal is Toilet toilet2
							&& toilet2.GetHook<P_Toilet_Hook>().bigQuestShidded
							&& toilet2.curChunk == toilet.curChunk)
						{
							questFlag = false;
							break;
						}
					}

					if (questFlag)
					{
						agent.SayDialogue(CDialogue.ToiletTouristPoint + Random.Range(1, 10));
						GC.quests.AddBigQuestPoints(agent, nameof(ToiletTourist));
					}
					else
						agent.SayDialogue(CDialogue.ToiletTouristNoPoint);
				}
			}

			toilet.StopInteraction();
		}

		public static void Shitsplode(PlayfieldObject targetObj, bool loud = true, bool toiletPaper = false)
		{
			Agent agent = targetObj.interactingAgent;
			Vector2 pos = targetObj.transform.position;
			bool avoidPublic = !Wreckage.HasPublicLitter;
			bool avoidPrivate = !Wreckage.HasPrivateLitter;

			if (loud)
				GC.spawnerMain.SpawnExplosion(targetObj, pos, VExplosion.Water);
			else
				GC.tileInfo.SpillLiquidLarge(pos, VExplosion.Water, false, 2, !avoidPrivate);

			Wreckage.SpawnWreckagePileObject_Granular(
				new Vector2(pos.x, pos.y - 0.08f),
				VObject.FlamingBarrel,
				false,
				Random.Range(1, 4),
				0.24f, 0.24f,
				0);

			int chance = 100;
			while (GC.percentChance(chance))
			{
				Wreckage.SpawnWreckagePileObject_Granular(
					new Vector2(pos.x, pos.y - 0.08f),
					VObject.MovieScreen,
					false,
					Random.Range(3, 6),
					0.48f, 0.48f,
					0);
				chance -= 25;
			}

			if (targetObj is Toilet toilet)
				toilet.GetComponent<PlayfieldObject>().GetHook<P_Toilet_Hook>().disgusting = true;
		}
	}
}
