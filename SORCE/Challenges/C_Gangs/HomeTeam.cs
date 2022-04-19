using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class HomeTeam : GangChallenge
	{
		public static GameController GC => GameController.gameController;

		public HomeTeam(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Bouncer;
		public override string[] MiddleAgents =>	new string[] { GC.percentChance(25) ? VAgent.Wrestler : VAgent.Athlete };
        public override string LastAgent =>			VAgent.Doctor;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup] 
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new HomeTeam(nameof(HomeTeam))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Home Team"))
				.WithDescription(new CustomNameInfo(
					"The local BallSports team is off-season, roaming the city."));
		}
	}
}