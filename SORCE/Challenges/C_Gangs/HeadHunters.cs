using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class HeadHunters : GangChallenge
	{
		public HeadHunters(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Cannibal;
		public override string[] MiddleAgents =>	new string[] { VAgent.Cannibal };
        public override string LastAgent =>			VAgent.Cannibal;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		true;
		public override bool MakeTrouble =>			true;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new HeadHunters(nameof(HeadHunters))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Head Hunters"))
				.WithDescription(new CustomNameInfo(
					"Not the kind of head hunters who are seeking talent."));
		}
	}
}