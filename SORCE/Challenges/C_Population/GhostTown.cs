using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Population
{
	class GhostTown
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.GhostTown, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "No one walks the streets in this city. Don't act all innocent, I know what you do to people in this game!\n\n- Wandering population set to 0%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.GhostTown,
				});

			ChallengeManager.RegisterChallenge<GhostTown>(new ChallengeInfo(cChallenge.GhostTown, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
