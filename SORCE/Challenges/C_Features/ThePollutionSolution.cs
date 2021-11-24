using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using SORCE.Content.Challenges;

namespace SORCE.Challenges.C_Features
{
	public class ThePollutionSolution
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.ThePollutionSolution, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "We've finally solved pollution! Make more, dump it everywhere, and then ignore it. You won't even notice it eventually.\n\n- Adds pollution features to levels\n- Lakes have 80% chance of being poisoned.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.ThePollutionSolution
				});

			ChallengeManager.RegisterChallenge<ThePollutionSolution>(new ChallengeInfo(cChallenge.ThePollutionSolution, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}