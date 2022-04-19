using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class TeamMelvin : GangChallenge
	{
		public TeamMelvin(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Vampire;
		public override string[] MiddleAgents =>	new string[] { VAgent.Vampire };
        public override string LastAgent =>			VAgent.Vampire;

		public override bool AlwaysRun =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new TeamMelvin(nameof(TeamMelvin))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Team Melvin"))
				.WithDescription(new CustomNameInfo(
					"The vampires here are far less dreamy than the movies would make you think. Is that a bowl cut?!"));
		}
	}
}