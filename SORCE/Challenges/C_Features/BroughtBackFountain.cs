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
	public class BroughtBackFountain
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.BroughtBackFountain, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "\"He could smell Jack - the intensely familiar odor of cigarettes, musky sweat, and a faint sweetness like grass, and with it the rushing cold of the fountain.\"\n\n- Adds Fountains\n- Doesn't make you gay",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.BroughtBackFountain
				});

			ChallengeManager.RegisterChallenge<BroughtBackFountain>(new ChallengeInfo(cChallenge.BroughtBackFountain, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}