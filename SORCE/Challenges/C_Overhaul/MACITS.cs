using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Overhaul
{
	class MACITS
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(MACITS);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<MACITS>(new ChallengeInfo(name, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.Overhaul));
		}
	}
}
