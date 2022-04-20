using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class BURPs : GangChallenge
	{
		public BURPs(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Soldier;
		public override string[] MiddleAgents =>	new string[] { VAgent.Soldier };
        public override string LastAgent =>			VAgent.Soldier;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		true;
		public override bool MakeTrouble =>			true;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				4;
        public override int TotalSpawns =>			(GangChallengeTools.GangTotalCount / 4) * 4;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new BURPs(nameof(BURPs))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - BURPs"))
				.WithDescription(new CustomNameInfo(
					"Battle-Ready Urban Reconaissance Patrols. Law just got a little bit martialer."));
		}
	}
}