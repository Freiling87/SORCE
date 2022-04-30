using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class NotSwearwolves : GangChallenge
	{
		public NotSwearwolves(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.OfficeDroneWerewolf;
		public override string[] MiddleAgents =>	new string[] { VAgent.OfficeDroneWerewolf };
        public override string LastAgent =>			VAgent.OfficeDroneWerewolf;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		true;
		public override bool MakeTrouble =>			false;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				GangChallengeTools.GangSize;
        public override int TotalSpawns =>			GangChallengeTools.GangTotalCount / 2;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new NotSwearwolves(nameof(NotSwearwolves))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Not Swear-wolves"))
				.WithDescription(new CustomNameInfo(
					"YES, they have insatiable bloodlust sometimes. But they're good guys, otherwise!"));
		}
	}
}