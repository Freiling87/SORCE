using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;

namespace SORCE.Content.Challenges.C_Buildings
{
	public class ShantyTown
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.ShantyTown, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "More wooden construction: A tinderbox on cinder blocks.\n\nHard mode for Firefighters, easy mode for arsonists. Fun mode for psychopaths.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.ShantyTown,
				});

			ChallengeManager.RegisterChallenge<ShantyTown>(new ChallengeInfo(cChallenge.ShantyTown, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
