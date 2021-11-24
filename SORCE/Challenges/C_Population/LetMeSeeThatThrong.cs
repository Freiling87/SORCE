﻿using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Population
{
	class LetMeSeeThatThrong
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.LetMeSeeThatThrong, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = 
					"Ooh that City so scandalous\n" +
					"And you know another player couldn't handle it\n" +
					"See you playing that thing like \"Who's the ish?\"" +
					"With a look in your eye so devilish, ugh" +
					"\n\n- Wandering population set to 400%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.LetMeSeeThatThrong,
				});

			ChallengeManager.RegisterChallenge<LetMeSeeThatThrong>(new ChallengeInfo(cChallenge.LetMeSeeThatThrong, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
