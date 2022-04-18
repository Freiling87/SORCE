using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Challenges.C_Gangs;
using SORCE.Challenges.C_Wreckage;
using SORCE.Logging;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.MapGenUtilities
{
    internal class Roamers
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static int GangTotalCount =>
			(int)(Random.Range(12, 24) * LevelSize.ChunkCountRatio);
		public static int GangSize =>
			Random.Range(3, 8);

		public static bool HasArsonists =>
			Core.debugMode;
		public static bool HasBlahdGangs =>
			Core.debugMode;
		public static bool HasCannibalGangs =>
			Core.debugMode;
		public static bool HasCopGangs =>
			Core.debugMode;
		public static bool HasCopBotGangs =>
			Core.debugMode;
		public static bool HasCrepeGangs =>
			Core.debugMode;
		public static bool HasDrugDealerGangs =>
			Core.debugMode;
		public static bool HasFirefighterGangs =>
			Core.debugMode;
		public static bool HasSlaverGangs =>
			Core.debugMode;
		public static bool HasSoldierGangs =>
			Core.debugMode;
		public static bool HasSupercopGangs =>
			Core.debugMode;
		public static bool HasThiefGangs =>
			Core.debugMode;
		public static bool HasVampireGangs =>
			Core.debugMode;
		public static bool HasWerewolfGangs =>
			Core.debugMode;

		public static bool HasButlerBot => // Vanilla?
			!GC.challenges.Contains(nameof(LowTechLowLife)) &&
			GC.challenges.Contains(nameof(Technocracy)); // Human butler?
		public static bool HasGangbangerGangs(bool vanilla) => // TODO: Consider eliminating this one, since it's mostly redundant to the more granular ones.
			!GC.challenges.Contains(nameof(MACITS)) &&
			!GC.challenges.Contains(nameof(PoliceState)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(RollingDeep)) ||
			Core.debugMode ||
			vanilla;
		public static bool HasMafiaGangs(bool vanilla) =>
			!GC.challenges.Contains(nameof(PoliceState)) &&
			!GC.challenges.Contains(nameof(MACITS)) &&
			GC.challenges.Contains(nameof(AnCapistan)) ||
			GC.challenges.Contains(nameof(UnionTown)) ||
			vanilla;

		public static int PopulationGang(int vanilla) =>
			GC.challenges.Contains(nameof(TurfWar)) ? 12 :
			vanilla;
		public static int PopulationMultiplier() =>
			RogueFramework.Unlocks.OfType<PopulationChallenge>().FirstOrDefault(c => c.IsEnabled)?.PopulationMultiplier
				?? 1;
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
			SpawnButlerBot();

			if (HasBlahdGangs)
				SpawnRoamerSquad(VAgent.Blahd, VAgent.Blahd,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasCannibalGangs)
				SpawnRoamerSquad(VAgent.Cannibal, VAgent.Cannibal,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasCopGangs)
				SpawnRoamerSquad(VAgent.Cop, VAgent.Cop,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);
			
			if (HasCopBotGangs)
				SpawnRoamerSquad(VAgent.CopBot, VAgent.CopBot,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasCrepeGangs)
				SpawnRoamerSquad(VAgent.Crepe, VAgent.Crepe,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasDrugDealerGangs)
				SpawnRoamerSquad(VAgent.DrugDealer, VAgent.Goon,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasFirefighterGangs)
				SpawnRoamerSquad(VAgent.Doctor, VAgent.Firefighter,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			// TODO: This will need special attention
			if (HasSlaverGangs)
				SpawnRoamerSquad(VAgent.Slavemaster, VAgent.Slave,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasSoldierGangs)
				SpawnRoamerSquad(VAgent.Soldier, VAgent.Soldier,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasSupercopGangs)
				SpawnRoamerSquad(VAgent.SuperCop, VAgent.SuperCop,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasThiefGangs)
				SpawnRoamerSquad(VAgent.Thief, VAgent.Thief,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasVampireGangs)
				SpawnRoamerSquad(VAgent.Vampire, VAgent.Vampire,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);

			if (HasWerewolfGangs)
				SpawnRoamerSquad(VAgent.Werewolf, VAgent.Werewolf,
					relationship: VRelationship.Neutral,
					alwaysRun: false,
					mustBeGuilty: true);
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
		public static void SpawnButlerBot()
        {
			if (!HasButlerBot)
				return;

			int level = Mathf.Clamp(GC.sessionDataBig.curLevel - 11, 0, 15);

			for (int i = 0; i <= level; i++)
				GC.spawnerMain.SpawnButlerBot();
		}

		// TODO: To Library
		public static void SpawnRoamerSquad(string leaderType, string bodyguardType, int totalSpawns = 0, int gangSize = 0, string relationship = VRelationship.Neutral, bool alwaysRun = false, bool mustBeGuilty = true)
		{
			if (totalSpawns == 0)
				totalSpawns = GangTotalCount;

			if (gangSize == 0)
				gangSize = GangSize;

			List<Agent> spawnedAgentList = new List<Agent>();
			Agent playerAgent = GC.playerAgent;
			//playerAgent.gangStalking = Agent.gangCount;
			Vector2 pos = Vector2.zero;
			
			totalSpawns = (int)(totalSpawns * LevelSize.ChunkCountRatio);

			for (int i = 0; i < totalSpawns; i++)
			{
				Vector2 vector = Vector2.zero;
				int attempts = 0;

				if (i % gangSize == 0)
				{
					while ((vector == Vector2.zero || Vector2.Distance(vector, GC.playerAgent.tr.position) < 20f) && attempts < 300)
					{
						vector = GC.tileInfo.FindRandLocationGeneral(0.32f);
						Agent.gangCount++; // Splits into groups
						attempts++;
					}

					pos = vector;
				}
				else
					vector = GC.tileInfo.FindLocationNearLocation(pos, null, 0.32f, 1.28f, true, true);

				if (vector != Vector2.zero)
				{
					string agentType =
						i % gangSize == 0
						? leaderType
						: bodyguardType;
					Agent agent = GC.spawnerMain.SpawnAgent(vector, null, agentType);
					agent.movement.RotateToAngleTransform((float)Random.Range(0, 360));
					agent.gang = Agent.gangCount;
					agent.modLeashes = 0;

					if (alwaysRun)
						agent.alwaysRun = true;

					agent.wontFlee = true;
					agent.agentActive = true;
					//agent.statusEffects.AddStatusEffect("InvisiblePermanent");
					agent.oma.mustBeGuilty = mustBeGuilty;
					spawnedAgentList.Add(agent);

					if (spawnedAgentList.Count > 1)
						for (int j = 0; j < spawnedAgentList.Count; j++)
							if (spawnedAgentList[j] != agent)
							{
								agent.relationships.SetRelInitial(spawnedAgentList[j], nameof(relStatus.Aligned));
								spawnedAgentList[j].relationships.SetRelInitial(agent, nameof(relStatus.Aligned));
							}

					agent.relationships.SetRel(playerAgent, relationship);
					playerAgent.relationships.SetRel(agent, relationship);

					switch (relationship.ToString())
					{
						case nameof(relStatus.Annoyed):
							agent.relationships.SetRelHate(playerAgent, 1);
							playerAgent.relationships.SetRelHate(agent, 1);
							break;
						case nameof(relStatus.Hostile):
							agent.relationships.SetRelHate(playerAgent, 5);
							playerAgent.relationships.SetRelHate(agent, 5);
							break;
					}

					agent.SetDefaultGoal(VAgentGoal.WanderLevel);
				}
			}
		}
	}
}
