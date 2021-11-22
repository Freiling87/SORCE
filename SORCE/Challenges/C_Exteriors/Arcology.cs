using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using BunnyMod;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using BunnyMod.Content.Challenges;

namespace SORCE.Challenges.C_Exteriors
{
	public class Arcology
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.Arcology, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Sustainable Eco-homes! Trees! Less pollution! What's not to love?\n\n(Answer: It's still miserable.)\n\n- Public floors are grass\n- Adds nature features to public areas",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.Arcology
				});

			ChallengeManager.RegisterChallenge<Arcology>(new ChallengeInfo(cChallenge.Panoptikopolis, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}