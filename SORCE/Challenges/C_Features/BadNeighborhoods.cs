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
	public class BadNeighborhoods
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.BadNeighborhoods, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This place sure went to shit, didn't it?\n\n- Small chance for any given window to start out broken.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.BadNeighborhoods
				});

			ChallengeManager.RegisterChallenge<BadNeighborhoods>(new ChallengeInfo(cChallenge.BadNeighborhoods, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}