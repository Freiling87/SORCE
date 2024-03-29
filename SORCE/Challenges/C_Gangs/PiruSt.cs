﻿using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class PiruSt : GangChallenge
	{
		public PiruSt(string name) : base(name) { }

		public override string LeaderAgent =>		VanillaAgents.GangsterBlahd;
		public override string[] MiddleAgents =>	new string[] { VanillaAgents.GangsterBlahd };
        public override string LastAgent =>			VanillaAgents.GangsterBlahd;

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
            _ = RogueLibs.CreateCustomUnlock(new PiruSt(nameof(PiruSt))
            {
            })
                .WithName(new CustomNameInfo(
                    "Gangs - Piru St."))
                .WithDescription(new CustomNameInfo(
                    "Blahd up!"));
		}
	}
}