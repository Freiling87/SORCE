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
	public class SpelunkyDory
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.SpelunkyDory, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You and your fellow citizens live in a disgusting cave complex. As the mayor says, \"Don't be a CAN'Tibal, be a CANnibal!\"\n\nMan, fuck the Mayor.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.SpelunkyDory,
				});

			ChallengeManager.RegisterChallenge<SpelunkyDory>(new ChallengeInfo(cChallenge.SpelunkyDory, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
