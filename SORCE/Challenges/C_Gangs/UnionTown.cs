using RogueLibsCore;
using SORCE.MapGenUtilities;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class UnionTown : GangChallenge
	{
		public UnionTown(string name) : base(name) { }

		public override string LeaderAgent =>		VanillaAgents.Mobster;
		public override string[] MiddleAgents =>	new string[] { VanillaAgents.Mobster };
        public override string LastAgent =>			VanillaAgents.Mobster;

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
			RogueLibs.CreateCustomUnlock(new UnionTown(nameof(UnionTown))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Union Town"))
				.WithDescription(new CustomNameInfo(
					"Dis is just a quiet, hardworking place wit normal people who pay respect to da proper people. Enable dis mutatah, or else!"));
		}
	}
}