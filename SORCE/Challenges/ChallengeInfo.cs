using System;
using System.Collections.Generic;
using BunnyMod.Content.Challenges;
using RogueLibsCore;

namespace SORCE.Challenges
{
	public class ChallengeInfo
	{
		public string Name { get; }
		public UnlockBuilder UnlockBuilder { get; }
		public List<EChallengeConflictGroup> ConflictGroups { get; } = new List<EChallengeConflictGroup>();

		private bool finalized;

		public ChallengeInfo(string name, UnlockBuilder builder)
		{
			Name = name;
			UnlockBuilder = builder;
		}

		private void AssertNotFinalized()
		{
			if (finalized)
				throw new NotSupportedException("cannot modify finalized ChallengeInfo!");
		}

		public ChallengeInfo WithConflictGroup(params EChallengeConflictGroup[] conflictGroup)
		{
			AssertNotFinalized();
			ConflictGroups.AddRange(conflictGroup);
			return this;
		}

		public void FinalizeInfo()
		{
			finalized = true;
		}
	}
}