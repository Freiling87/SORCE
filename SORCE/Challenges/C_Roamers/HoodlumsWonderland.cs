using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;

namespace SORCE.Content.Challenges.C_Roamers
{
	public class HoodlumsWonderland
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.HoodlumsWonderland, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The annual charity drive for the Blahds and Crepes happened to overlap this year. They're in tough competition to sell the most cookies!" +
					"\n\nRoaming gang spawns are increased. By a lot.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.HoodlumsWonderland,
				});

			ChallengeManager.RegisterChallenge<HoodlumsWonderland>(new ChallengeInfo(cChallenge.HoodlumsWonderland, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
