using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Population
{
	class SwarmWelcome
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.SwarmWelcome, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This whole city feels like a crowded subway. Pickpocketing is bad enough, but the frottage is out of control!" +
					"\n\n- Wandering population set to 800%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.SwarmWelcome,
				});

			ChallengeManager.RegisterChallenge<SwarmWelcome>(new ChallengeInfo(cChallenge.SwarmWelcome, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
