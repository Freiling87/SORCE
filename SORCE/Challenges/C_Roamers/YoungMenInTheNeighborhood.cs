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
	public class YoungMenInTheNeighborhood
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.YoungMenInTheNeighborhood, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Beause the young gentlemen in the hood are always polite; If you start acting rude, we'll set you right!" +
					"\n\nYour friendly local Gangbangers now roam every district."
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.YoungMenInTheNeighborhood,
				});

			ChallengeManager.RegisterChallenge<YoungMenInTheNeighborhood>(new ChallengeInfo(cChallenge.YoungMenInTheNeighborhood, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
