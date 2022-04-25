using BepInEx.Logging;
using RogueLibsCore;
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

			foreach(ObjectReal or in exits)
				logger.LogDebug(or.name);

			return exits;
		}

		public static void ExitUnderdank(Agent agent, PlayfieldObject exit)
        {
			Vector3 exitSpot = Vector3.zero;

			if (exit is Manhole)
			{
				exitSpot = exit.curPosition;
				Vector2 exitFacing = Random.insideUnitCircle.normalized;
				agent.Teleport((Vector2)exitSpot + exitFacing, true, true);
				agent.jumpDirection = exitFacing - (Vector2)exitSpot;
				agent.jumpSpeed = 8f;
				agent.Jump();
			}
			else if (exit is Toilet)
			{
				switch (((ObjectReal)exit).direction)
				{
					case "E":
						exitSpot = new Vector3(exit.tr.position.x + 0.32f, exit.tr.position.y, exit.tr.position.z);
						break;
					case "N":
						exitSpot = new Vector3(exit.tr.position.x, exit.tr.position.y + 0.32f, exit.tr.position.z);
						break;
					case "S":
						exitSpot = new Vector3(exit.tr.position.x, exit.tr.position.y - 0.32f, exit.tr.position.z);
						break;
					case "W":
						exitSpot = new Vector3(exit.tr.position.x - 0.32f, exit.tr.position.y, exit.tr.position.z);
						break;
				}

				agent.Teleport(exitSpot, false, true);
			}

			if (GC.percentChance(10))
				Poopsplosion(exit, true);
			else
				GC.spawnerMain.SpawnExplosion(exit, exit.tr.position, VExplosion.Water, false, -1, false, ((ObjectReal)exit).FindMustSpawnExplosionOnClients(agent));
		}

		public static void FlushYourself(Agent agent, PlayfieldObject entryObject)
		{
			logger.LogDebug("FlushYourself:\t" + entryObject);
			List<PlayfieldObject> exits = Exits(agent);
			logger.LogDebug("Exits:\t" + exits.Count());
	
			if (exits.Contains(entryObject))
				exits.Remove(entryObject);

			PlayfieldObject exit = exits[Random.Range(0, exits.Count - 1)];
			ExitUnderdank(agent, exit);
		}

		public static void TakeHugeShit(Toilet toilet, bool loud = true)
		{
			Poopsplosion(toilet, loud);
			toilet.StopInteraction();
		}

		public static void Poopsplosion(PlayfieldObject targetObj, bool loud = true)
		{
			Agent agent = targetObj.interactingAgent;
			Vector2 pos = targetObj.transform.position;
			bool avoidPublic = !VFX.HasPublicLitter;
			bool avoidPrivate = !VFX.HasPrivateLitter;

			if (loud)
				GC.spawnerMain.SpawnExplosion(targetObj, pos, VExplosion.Water);
			else
				GC.tileInfo.SpillLiquidLarge(pos, VExplosion.Water, false, 2, !avoidPrivate);

			VFX.SpawnWreckagePileObject_Granular(
				new Vector2(pos.x, pos.y - 0.08f),
				VObject.FlamingBarrel,
				false,
				Random.Range(1, 4),
				0.24f, 0.24f,
				0,
				avoidPublic, avoidPrivate);

			int chance = 100;
			while (GC.percentChance(chance))
			{
				VFX.SpawnWreckagePileObject_Granular(
					new Vector2(pos.x, pos.y - 0.08f),
					VObject.MovieScreen,
					false,
					Random.Range(3, 6),
					0.48f, 0.48f,
					0,
					avoidPublic, avoidPrivate);
				chance -= 25;
			}

			if (targetObj is Toilet toilet)
				toilet.GetComponent<PlayfieldObject>().GetHook<P_Toilet_Hook>().disgusting = true;
		}
	}
}
