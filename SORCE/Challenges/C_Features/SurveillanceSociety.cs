using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using BunnyMod;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using BunnyMod.Content.Challenges;

namespace SORCE.Challenges.C_Features
{
	public class SurveillanceSociety
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.SurveillanceSociety, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Those cameras? For your safety.\n\nOh, the turrets? For their safety.\n\nThe midnight raids and people disappearing? Um... what's your name, citizen?\n\n- Spawns Security Cameras & Turrets in public, aligned with The Law.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.SurveillanceSociety
				});

			ChallengeManager.RegisterChallenge<SurveillanceSociety>(new ChallengeInfo(cChallenge.SurveillanceSociety, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}