using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Utilities.MapGen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public abstract class GangChallenge : MutatorUnlock
    {
        public GangChallenge(string name) : base(name, true) { }

        public abstract string LeaderAgent { get; }
        public abstract string[] MiddleAgents { get; } // Can be in order, or a random pool?
        public abstract string LastAgent { get; }

        public abstract bool AlwaysRun { get; }
		public abstract bool GangsAligned { get; }
		public abstract bool MakeTrouble { get; }
        public abstract bool MustBeGuilty { get; }

        public abstract int GangSize { get; }
        public abstract int TotalSpawns { get; }

        public abstract string Relationship { get; }
    }

    public static class GangChallengeTools
    {
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static int GangTotalCount =>
            Mathf.Max(4, (int)(Random.Range(8f, 16f) * LevelSize.ChunkCountRatio));
        public static int GangSize =>
            Random.Range(4, 6);

		public static void Spawner_Main()
		{
			foreach (GangChallenge challenge in RogueFramework.Unlocks.OfType<GangChallenge>().Where(c => c.IsEnabled))
				SpawnGangs(challenge);
		}

		public static void SpawnGangs(GangChallenge challenge) =>
			SpawnGangs(challenge.LeaderAgent, challenge.MiddleAgents, challenge.LastAgent, challenge.TotalSpawns, challenge.GangSize, challenge.Relationship, challenge.AlwaysRun, challenge.MustBeGuilty, challenge.MakeTrouble, challenge.GangsAligned);
		public static void SpawnGangs(string leaderAgent, string[] middleAgents, string lastAgent, int totalSpawns = 0, int gangSize = 0, string relationship = VRelationship.Neutral, bool alwaysRun = false, bool mustBeGuilty = true, bool makeTrouble = true, bool gangsAligned = false)
		{
			if (totalSpawns == 0)
				totalSpawns = GangTotalCount;

			if (gangSize == 0)
				gangSize = GangSize;

			List<Agent> spawnedAgentList = new List<Agent>();
			Agent playerAgent = GC.playerAgent;
			//playerAgent.gangStalking = Agent.gangCount;
			Vector2 pos = Vector2.zero;
			int middleAgentIndex = 0;
			List<Agent> slaveMasters = new List<Agent>();
			string securityType = GC.Choose("Normal", "Weapons", "ID");
			int ownerGroupOffset = 0;

			for (int i = 0; i < totalSpawns; i++)
			{
				int attempts = 0;
				string agentType;

				if (i % gangSize == 0) // First
				{
					// Prevent gangs of 1
					if (i == totalSpawns)
						return;

					Agent.gangCount++;

					if (!gangsAligned)
						spawnedAgentList.Clear();

					if (slaveMasters.Any())
						slaveMasters.Clear();

					securityType = GC.Choose("Normal", "Weapons", "ID"); // Match per gang

					while ((pos == Vector2.zero || Vector2.Distance(pos, GC.playerAgent.tr.position) < 20f) && attempts < 300)
					{
						pos = GC.tileInfo.FindRandLocationGeneral(0.32f);
						attempts++;
					}

					agentType = leaderAgent;
					middleAgentIndex = 0;
				}
				else if (i % gangSize == gangSize - 1) // Last
                {
					pos = GC.tileInfo.FindLocationNearLocation(pos, null, 0.32f, 1.28f, true, true);
					agentType = lastAgent;
					middleAgentIndex = 0;
				}
                else // Middle
                {
					pos = GC.tileInfo.FindLocationNearLocation(pos, null, 0.32f, 1.28f, true, true);
					agentType = middleAgents[middleAgentIndex++];

					if (middleAgentIndex > middleAgents.Count() - 1)
						middleAgentIndex = 0;
				}

				if (pos != Vector2.zero)
				{
					bool specialWerewolf = false;

					if (agentType == VAgent.OfficeDroneWerewolf)
                    {
						specialWerewolf = true;
						agentType = VAgent.OfficeDrone;
					}
						
					Agent agent = GC.spawnerMain.SpawnAgent(pos, null, agentType);
					agent.movement.RotateToAngleTransform((float)Random.Range(0, 360));
					agent.gang = Agent.gangCount;
					agent.modLeashes = 0;
					agent.alwaysRun = alwaysRun;
					agent.wontFlee = true;
					agent.agentActive = true;
					//agent.statusEffects.AddStatusEffect("InvisiblePermanent");
					agent.oma.mustBeGuilty = mustBeGuilty;
					spawnedAgentList.Add(agent);

					if (agentType == VAgent.Slavemaster)
					{
						slaveMasters.Add(agent);
					}
					else if (agentType == VAgent.Slave &&
						slaveMasters.Any())
					{
						logger.LogDebug("Slave");
						logger.LogDebug("Slavemasters: " + slaveMasters.Count());
						foreach (Agent slavemaster in slaveMasters)
						{
							slavemaster.slavesOwned.Add(agent);
							agent.slaveOwners.Add(slavemaster);
						}
					}
					else if (agentType == VAgent.CopBot)
						agent.oma.securityType = agent.oma.convertSecurityTypeToInt(securityType);
					else if (specialWerewolf)
                    {
						agent.originalWerewolf = true;
						agent.statusEffects.GiveSpecialAbility(VSpecialAbility.WerewolfTransformation);
                    }
					
					if (agentType == VAgent.Bartender || 
						agentType == VAgent.DrugDealer ||
						agentType == VAgent.Shopkeeper ||
						agentType == VAgent.Slavemaster)
                    {
						agent.oma.shookDown = false;
						agent.ownerID = 42069 + ownerGroupOffset++;
					}

					if (agentType == VAgent.Cannibal ||
						agentType == VAgent.CopBot ||
						agentType == VAgent.Thief ||
						agentType == VAgent.Vampire)
					{
						if (makeTrouble)
							agent.losCheckAtIntervals = GC.percentChance(50);
						else if (!makeTrouble)
							agent.losCheckAtIntervals = false;
					}

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

				if (i % gangSize == gangSize - 1)
					pos = Vector2.zero;
			}
		}
	}
}
