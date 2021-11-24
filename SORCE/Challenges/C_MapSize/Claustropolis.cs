using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;

namespace SORCE.Content.Challenges.C_MapSize
{
	public class Claustropolis
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.Claustropolis, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Damn, this city is cramped! Who's Claus, anyway?\n\n- Map size set to 37.5%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.Claustropolis,
				});

			ChallengeManager.RegisterChallenge<Claustropolis>(new ChallengeInfo(cChallenge.Claustropolis, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
