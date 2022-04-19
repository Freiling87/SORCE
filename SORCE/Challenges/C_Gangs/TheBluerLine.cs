using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class TheBluerLine : GangChallenge
	{
		public static GameController GC => GameController.gameController;

		public TheBluerLine(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.SuperCop;
		public override string[] MiddleAgents =>	new string[] { VAgent.SuperCop };
        public override string LastAgent =>			VAgent.SuperCop;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new TheBluerLine(nameof(TheBluerLine))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - The Bluer Line"))
				.WithDescription(new CustomNameInfo(
					"Laziness! Prejudice! Outright corruption! Law enforcement simply isn't working for the people. " +
					"Clearly the answer is meaner cops with bigger guns!"));
		}
	}
}