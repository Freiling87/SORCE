using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class WeLoveOurJobCreators : GangChallenge
	{
		public WeLoveOurJobCreators(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Slavemaster;
		public override string[] MiddleAgents =>	new string[] { VAgent.Slavemaster, VAgent.Slave, VAgent.Slave, VAgent.Slave, VAgent.Slave, VAgent.Slave };
        public override string LastAgent =>			VAgent.Slave;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new WeLoveOurJobCreators(nameof(WeLoveOurJobCreators))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - We Love Our Job Creators"))
				.WithDescription(new CustomNameInfo(
					"Say what you want about chattel slavery, but unemployment is lower than ever!"));
		}
	}
}