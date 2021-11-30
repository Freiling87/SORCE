using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Overhaul
{
	class Technocracy
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Technocracy);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<Technocracy>(new ChallengeInfo(name, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.Overhaul));
		}
	}
}
