using RogueLibsCore;
using SORCE.MapGenUtilities;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class BananaSmugglers : GangChallenge
	{
		public BananaSmugglers(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.Gorilla;
		public override string[] MiddleAgents =>	new string[] { VAgent.Gorilla };
        public override string LastAgent =>			VAgent.Gorilla;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		true;
		public override bool MakeTrouble =>			true;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			(int)(GangChallengeTools.GangTotalCount * 0.90f);

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new BananaSmugglers(nameof(BananaSmugglers))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Banana Smugglers"))
				.WithDescription(new CustomNameInfo(
					"Yes, that is a banana. And no, they're not happy to see you."));
		}
	}
}