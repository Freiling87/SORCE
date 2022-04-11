using BepInEx.Logging;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Challenges.C_Roamers;
using SORCE.Logging;
using System.Collections.Generic;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.MapGenUtilities
{
    internal class Roamers
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool HasArsonists =>
			Core.debugMode;
		public static bool HasRoamingGangbangers(bool vanilla) =>
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(YoungMenInTheNeighborhood)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasRoamingMafia(bool vanilla) =>
			!GC.challenges.Contains(nameof(PoliceState)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(UnionTown)) ||
			vanilla;

		public static int PopulationGang(int vanilla) =>
			GC.challenges.Contains(nameof(HoodlumsWonderland)) ? 12 :
			vanilla;
		public static int PopulationMultiplier() =>
			GC.challenges.Contains(nameof(GhostTown)) ? 0 :
			GC.challenges.Contains(nameof(HordeAlmighty)) ? 2 :
			GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 4 :
			GC.challenges.Contains(nameof(SwarmWelcome)) ? 8 :
			1;
		public static int PopulationMafia(int vanilla) =>
			vanilla;

		public static string RoamerAgentType(string agentType)
		{
			// TODO: Adjustments for overhauls, etc.

			if (agentType == VAgent.Thief)
			{
				int thiefReduction =
					GC.challenges.Contains(nameof(HordeAlmighty)) ? 50 :
					GC.challenges.Contains(nameof(LetMeSeeThatThrong)) ? 75 :
					GC.challenges.Contains(nameof(SwarmWelcome)) ? 87 :
					0;

				if (thiefReduction != 0 && GC.percentChance(thiefReduction))
					agentType = GC.levelTheme == 4 || GC.levelTheme == 5
						? VAgent.UpperCruster
						: VAgent.SlumDweller;
			}

			return agentType;
		}

		public static void Spawner_Main()
        {
			SpawnArsonist();
        }

		public static void SpawnArsonist()
        {
			if (!HasArsonists)
				return;

			List<Agent> list = new List<Agent>();
			for (int j = 0; j < GC.activeBrainAgentList.Count; j++)
			{
				Agent agent = GC.activeBrainAgentList[j];

				// Gross
				if (agent.isPlayer == 0 && !agent.dead && !agent.objectAgent && !agent.oma.rioter && !agent.ghost && !agent.inhuman && !agent.beast && !agent.zombified && !agent.oma.hidden && !agent.arsonist && !agent.oma.secretWerewolf && agent.ownerID == 0 && agent.startingChunk == 0 && !agent.enforcer && !agent.arsonist && !agent.activeArsonist && !agent.firefighter && !agent.hasEmployer && agent.slaveOwners.Count == 0 && !agent.statusEffects.hasTrait(VTrait.Pacifist) && !agent.statusEffects.hasTrait(VTrait.SausageFingers) && !agent.statusEffects.hasTrait(VTrait.NearHarmless) && !agent.QuestInvolvementFull())
					list.Add(agent);
			}

			if (list.Count > 0)
			{
				int num = Random.Range(0, list.Count);
				Agent agent2 = list[num];
				agent2.arsonist = true;
				agent2.mustFlee = false;

				if (!GC.quests.bigQuestObjectList.Contains(agent2))
					GC.quests.bigQuestObjectList.Add(agent2);

				agent2.activeArsonist = true;
				agent2.noEnforcerAlert = true;
				agent2.oma.mustBeGuilty = true;

				InvItem invItem = new InvItem();
				invItem.invItemName = VItem.MolotovCocktail;
				invItem.SetupDetails(false);
				invItem.invItemCount = 3;
				agent2.inventory.AddItem(invItem);
			}
		}
	}
}
