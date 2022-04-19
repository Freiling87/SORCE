using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class ProtectAndServo : GangChallenge
	{
		public ProtectAndServo(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.CopBot;
		public override string[] MiddleAgents =>	new string[] { VAgent.CopBot };
        public override string LastAgent =>			VAgent.CopBot;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new ProtectAndServo(nameof(ProtectAndServo))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Protect & Servo"))
				.WithDescription(new CustomNameInfo(
					"Edison, Inc. presents: Police©!\n\n" +
					"The futuristic solution to law enforcement."));
		}
	}
}