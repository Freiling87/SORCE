using RogueLibsCore;
using SORCE.MapGenUtilities;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class ExperimentalExcursion : GangChallenge
	{
		public ExperimentalExcursion(string name) : base(name) { }

		public override string LeaderAgent =>		VanillaAgents.Bouncer;
		public override string[] MiddleAgents =>	new string[] { VanillaAgents.Scientist };
        public override string LastAgent =>			VanillaAgents.Scientist;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			(int)(GangChallengeTools.GangTotalCount * 1.10f);

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new ExperimentalExcursion(nameof(ExperimentalExcursion))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Experimental Excursion"))
				.WithDescription(new CustomNameInfo(
					"Roaming the streets randomly and getting killed for stupid reasons... for science!"));
		}
	}
}