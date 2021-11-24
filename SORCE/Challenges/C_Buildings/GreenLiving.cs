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
	public class GreenLiving
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.GreenLiving, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The Mayor has retrofitted most buildings to eco-friendly plant-based construction. The air is mighty fresh... except near the compost-burning stoves.",
				})
				.WithName(new CustomNameInfo 
				{
					[LanguageCode.English] = cChallenge.GreenLiving,
				});

			ChallengeManager.RegisterChallenge<GreenLiving>(new ChallengeInfo(cChallenge.GreenLiving, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
