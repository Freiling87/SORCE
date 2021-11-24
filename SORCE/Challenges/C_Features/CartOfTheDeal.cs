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
	public class CartOfTheDeal
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.CartOfTheDeal, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "A lot of people, very important people, are saying the City has the best Vendor Carts. The best folks, just tremendous. Don't we love our Vendor Carts?",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.CartOfTheDeal
				});

			ChallengeManager.RegisterChallenge<CartOfTheDeal>(new ChallengeInfo(cChallenge.CartOfTheDeal, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}