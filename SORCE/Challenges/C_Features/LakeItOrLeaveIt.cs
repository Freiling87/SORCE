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
	public class LakeItOrLeaveIt
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.LakeItOrLeaveIt, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Don't like large inland bodies of water? Too fuckin' bad, buddy!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.LakeItOrLeaveIt
				});

			ChallengeManager.RegisterChallenge<LakeItOrLeaveIt>(new ChallengeInfo(cChallenge.LakeItOrLeaveIt, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}