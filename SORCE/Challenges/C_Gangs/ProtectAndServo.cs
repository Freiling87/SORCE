﻿using RogueLibsCore;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Gangs
{
    public class ProtectAndServo : GangChallenge
	{
		public ProtectAndServo(string name) : base(name) { }

		public override string LeaderAgent =>		VAgent.CopBot;
		public override string[] MiddleAgents =>	new string[] { VAgent.CopBot };
        public override string LastAgent =>			VAgent.CopBot;

		public override bool AlwaysRun =>			false;
        public override bool GangsAligned =>		true;
		public override bool MakeTrouble =>			true;
        public override bool MustBeGuilty =>		false;

        public override int GangSize =>				2;
        public override int TotalSpawns =>			(GangChallengeTools.GangTotalCount / 2) * 2;

        public override string Relationship =>		VRelationship.Neutral;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new ProtectAndServo(nameof(ProtectAndServo))
			{
			})
				.WithName(new CustomNameInfo(
					"Gangs - Protect & Servo"))
				.WithDescription(new CustomNameInfo(
					"Edison, Inc. presents: Police©! The futuristic solution to law enforcement.\n\n" +
					"Bobby the Cop Bot says, \"I [ERROR: Emotion not found, 'E_Love'] all Citizens!\""));
		}
	}
}