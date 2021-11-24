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
	public class MobTown
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.MobTown, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Enable dis mutatah, or else. Stugots!" +
					"\n\nThe Mob is in every district.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.MobTown,
				});

			ChallengeManager.RegisterChallenge<MobTown>(new ChallengeInfo(cChallenge.MobTown, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
