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
	public class Ultrapolis
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.Ultrapolis, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Many do not know there is a world outside the city, and in fact insist that it is all there is.\n\n- Map size set to 200%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.Ultrapolis,
				});

			ChallengeManager.RegisterChallenge<Ultrapolis>(new ChallengeInfo(cChallenge.Ultrapolis, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
