using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;

namespace SORCE.Challenges.C_MapSize
{
	public class ACityForAnts
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.ACityForAnts, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "\"The Streets of Rogue City Building For Slum Dwellers Who Can't Be Rich Good\"\n\n  - Inscription over the entrance to the Slums, District 420, Floor 69 \n\n- Map size set to 12.5%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.ACityForAnts,
				});

			ChallengeManager.RegisterChallenge<ACityForAnts>(new ChallengeInfo(cChallenge.ACityForAnts, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
