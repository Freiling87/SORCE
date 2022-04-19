using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class BandoBrothers : GangChallenge
	{
		public BandoBrothers(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Goon;
		public override string[] MiddleAgents =>	new string[] { VAgent.DrugDealer, VAgent.Goon, VAgent.SlumDweller, VAgent.SlumDweller, VAgent.SlumDweller, VAgent.SlumDweller };
        public override string LastAgent =>			VAgent.Goon;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		true;	// TODO: MustBeGuilty => Overhaul.DrugsIllegal()

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new BandoBrothers(nameof(BandoBrothers))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Bando Brothers"))
				.WithDescription(new CustomNameInfo(
					"Dealers run the streets, followed by their loyal customers."));
		}
	}
}