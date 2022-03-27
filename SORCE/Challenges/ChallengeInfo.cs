using System;
using System.Collections.Generic;
using SORCE.Content.Challenges;
using RogueLibsCore;

namespace SORCE.Challenges
{
	public class ChallengeInfo
	{
		public string Name { get; }
		public UnlockBuilder UnlockBuilder { get; }

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

		public void FinalizeInfo()
		{
			finalized = true;
		}
	}
}