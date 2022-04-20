using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class RougherRuffians : GangChallenge
	{
		public static GameController GC => GameController.gameController;

		public RougherRuffians(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Thief;
		public override string[] MiddleAgents =>	new string[] { VAgent.Thief };
        public override string LastAgent =>			VAgent.Thief;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		false;
		public override bool MakeTrouble =>			true;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize / 2;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new RougherRuffians(nameof(RougherRuffians))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Rougher Ruffians"))
				.WithDescription(new CustomNameInfo(
					"Be careful, they aren't just mugging for the camera!"));
		}
	}
}