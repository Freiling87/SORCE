using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Logging;
using SORCE.Utilities.MapGen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Utilities
{
    internal class Roamers
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool HasArsonist =>
			false;

		public static bool HasButlerBot => // Vanilla?
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(Technocracy)); // Human butler?

		public static int PopulationMultiplier() =>
			(int)((RogueFramework.Unlocks.OfType<PopulationChallenge>().FirstOrDefault(c => c.IsEnabled)?.PopulationMultiplier
				?? 1) * LevelSize.ChunkCountRatio);

		public static void Spawner_Main()
        {
			SpawnArsonist();
			SpawnButlerBot();
		}

		public static void SpawnArsonist()
        {
			//if (!HasArsonists)
			//	return;

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
		public static void SpawnButlerBot()
        {
			if (!HasButlerBot)
				return;

			int level = Mathf.Clamp(GC.sessionDataBig.curLevel - 11, 0, 15);

			for (int i = 0; i <= level; i++)
				GC.spawnerMain.SpawnButlerBot();
		}

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

	}
}
