using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class BackDraft : GangChallenge
	{
		public BackDraft(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Firefighter;
		public override string[] MiddleAgents =>	new string[] { VAgent.Firefighter };
        public override string LastAgent =>			VAgent.Doctor;

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
			RogueLibs.CreateCustomUnlock(new BackDraft(nameof(BackDraft))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Back Draft"))
				.WithDescription(new CustomNameInfo(
					"Being a firefighter in this city is so dangerous that administrators had to implement a draft! Who's eligible? Anyone who tries to quit."));
		}
	}
}