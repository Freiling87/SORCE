using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Population
{
	class HordeAlmighty
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.HordeAlmighty, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The City administration is trying out a contraception ban to combat the high death rate. Hope it works, because they didn't think of a \"Plan B!\" Get it? I'm here all week, folks.\n\n- Wandering population set to 200%\n- You might get pregnant",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.HordeAlmighty,
				});

			ChallengeManager.RegisterChallenge<HordeAlmighty>(new ChallengeInfo(cChallenge.HordeAlmighty, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
