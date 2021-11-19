using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using BunnyMod;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;

namespace BunnyMod.Content.Challenges.C_Buildings
{
	public class CityOfSteel
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.CityOfSteel, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "A gleaming city of steel! The world of the future, today. Mankind's dream in... Wow, it *really* smells like steel cleaner. Like, it fucking stinks. This is pungent.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.CityOfSteel,
				});

			ChallengeManager.RegisterChallenge<CityOfSteel>(new ChallengeInfo(cChallenge.CityOfSteel, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
