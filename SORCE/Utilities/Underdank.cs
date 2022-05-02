using BepInEx.Logging;
using RogueLibsCore;
using SORCE.BigQuests;
using SORCE.Extensions;
using SORCE.Logging;
using SORCE.MapGenUtilities;
using SORCE.Patches.P_PlayfieldObject;
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

		public static List<PlayfieldObject> Exits(Agent agent)
		{
			List<PlayfieldObject> exits = new List<PlayfieldObject>();

			exits.AddRange(GC.objectRealList.OfType<Manhole>().Where(manhole => manhole.opened));
			exits.AddRange(GC.objectRealList.OfType<Hole>().Where(e => e.objectHoleType == VObject.Manhole));

			if (E_Agent.IsFlushable(agent))
				exits.AddRange(GC.objectRealList.OfType<Toilet>().Where(toilet => !toilet.destroyed));

			return exits;
		}

		public static void ExitUnderdank(Agent agent, PlayfieldObject exit)
        {
			Vector3 exitSpot = exit.tr.position;

			if (exit is Manhole manhole)
			{
				exitSpot = manhole.curPosition;
				Vector2 exitFacing = Random.insideUnitCircle.normalized;
				agent.Teleport((Vector2)exitSpot + exitFacing, true, true);
				//agent.jumpDirection = exitFacing - (Vector2)exitSpot;
				//agent.jumpSpeed = 8f;
				//agent.Jump();
			}
			else if (exit is Toilet toilet)
			{
				switch (((ObjectReal)exit).direction)
				{
					case "E":
						exitSpot += new Vector3(0.32f, 0f, 0f);
						break;
					case "N":
						exitSpot += new Vector3(0f, 0.32f, 0f);
						break;
					case "S":
						exitSpot += new Vector3(0f, -0.32f, 0f);
						break;
					case "W":
						exitSpot += new Vector3(-0.32f, 0f, 0f);
						break;
				}
				
				agent.Teleport(exitSpot, false, true);
			}

			if (GC.percentChance(10))
				Poopsplosion(exit, true, false);
			else
				GC.spawnerMain.SpawnExplosion(exit, exitSpot, VExplosion.Water, false, -1, false, ((ObjectReal)exit).FindMustSpawnExplosionOnClients(agent));
		}

		public static void FlushYourself(Agent agent, PlayfieldObject entryObject)
		{
			List<PlayfieldObject> exits = Exits(agent);
	
			if (exits.Contains(entryObject))
				exits.Remove(entryObject);

			PlayfieldObject exit = exits[Random.Range(0, exits.Count - 1)];
			ExitUnderdank(agent, exit);
		}

		public static void TakeHugeShit(Toilet toilet, bool loud = true)
		{
			Agent agent = toilet.interactingAgent;
			Poopsplosion(toilet, loud, true);

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

			toilet.StopInteraction();
		}

		public static void Poopsplosion(PlayfieldObject targetObj, bool loud = true, bool toiletPaper = false)
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
