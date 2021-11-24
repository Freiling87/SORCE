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
	public class SkywayDistrict
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.SkywayDistrict, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The Canal water Downtown was sold off for a pretty penny. So now there's just a huge chasm where it used to be. It's a hazard, but the profit was massive!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.SkywayDistrict
				});

			ChallengeManager.RegisterChallenge<SkywayDistrict>(new ChallengeInfo(cChallenge.SkywayDistrict, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}