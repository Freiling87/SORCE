using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class LunchHour : GangChallenge
	{
		public LunchHour(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.InvestmentBanker;
		public override string[] MiddleAgents =>	new string[] { VAgent.Clerk, VAgent.OfficeDrone, VAgent.Worker, VAgent.Hacker };
        public override string LastAgent =>			VAgent.OfficeDrone;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		false;
		public override bool MakeTrouble =>			true;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new LunchHour(nameof(LunchHour))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Lunch Hour"))
				.WithDescription(new CustomNameInfo(
					"Various losers & dweebs are out to grab lunch."));
		}
	}
}