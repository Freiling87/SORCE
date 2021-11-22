using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using BunnyMod;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using BunnyMod.Content.Challenges;

namespace SORCE.Challenges.C_Features
{
	public class PowerWhelming
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.PowerWhelming, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You're not gonna be *over*whelmed, but you will see Power Boxes in every district. And that's something, I guess.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.PowerWhelming
				});

			ChallengeManager.RegisterChallenge<PowerWhelming>(new ChallengeInfo(cChallenge.PowerWhelming, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}