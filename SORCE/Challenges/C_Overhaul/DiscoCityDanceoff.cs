using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Overhaul
{
	class DiscoCityDanceoff
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(DiscoCityDanceoff);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<DiscoCityDanceoff>(new ChallengeInfo(name, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.Overhaul));
		}
	}
}
