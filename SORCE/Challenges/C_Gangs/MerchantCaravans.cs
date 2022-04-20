using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class MerchantCaravans : GangChallenge
	{
		public static GameController GC => GameController.gameController;

		public MerchantCaravans(string name) : base(name) { }

		public override string LeaderAgent =>		GC.percentChance(33) ? VAgent.Bartender : VAgent.Shopkeeper;
		public override string[] MiddleAgents =>	new string[] { VAgent.Goon };
        public override string LastAgent =>			VAgent.Goon;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		false;
		public override bool MakeTrouble =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup] 
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MerchantCaravans(nameof(MerchantCaravans))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Merchant Caravans"))
				.WithDescription(new CustomNameInfo(
					"They're learning... evolving. They seem to understand that owning a building is nothing but a liability."));
		}
	}
}