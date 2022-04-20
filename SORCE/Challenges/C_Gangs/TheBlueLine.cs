using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class TheBlueLine : GangChallenge
	{
		public TheBlueLine(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Cop;
		public override string[] MiddleAgents =>	new string[] { VAgent.Cop };
        public override string LastAgent =>			VAgent.Cop;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

		// Force pairs of 2
        public override int GangSize =>				2;
        public override int TotalSpawns =>			(GangChallengeTools.GangTotalCount / 2) * 2; 

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new TheBlueLine(nameof(TheBlueLine))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - The Blue Line"))
				.WithDescription(new CustomNameInfo(
					"It used to be \"The Thin Blue Line\", but that doughnut budget is invincible!"));
		}
	}
}