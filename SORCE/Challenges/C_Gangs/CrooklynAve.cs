using RogueLibsCore;
using SORCE.MapGenUtilities;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class CrooklynAve : GangChallenge
	{
		public CrooklynAve(string name) : base(name) { }

		public override string LeaderAgent =>		VanillaAgents.GangsterCrepe;
		public override string[] MiddleAgents =>	new string[] { VanillaAgents.GangsterCrepe };
        public override string LastAgent =>			VanillaAgents.GangsterCrepe;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new CrooklynAve(nameof(CrooklynAve))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Crooklyn Ave."))
				.WithDescription(new CustomNameInfo(
					"If you're not Crepe-walking, you'd better be running."));
		}
	}
}