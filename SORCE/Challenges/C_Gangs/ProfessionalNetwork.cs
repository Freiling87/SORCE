using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class ProfessionalNetwork : GangChallenge
	{
		public ProfessionalNetwork(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.ResistanceLeader;
		public override string[] MiddleAgents =>	new string[] { VAgent.Athlete, VAgent.Thief, VAgent.Hacker, VAgent.Doctor };
        public override string LastAgent =>			VAgent.SlumDweller;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		true;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Friendly;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new ProfessionalNetwork(nameof(ProfessionalNetwork))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Professional Network"))
				.WithDescription(new CustomNameInfo(
					"The Resistance has deployed squads of agents to help the cause wherever they can."));
		}
	}
}