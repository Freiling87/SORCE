using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;

namespace SORCE.Content.Challenges.C_FloorInteriorsWalls
{
	public class Panoptikopolis
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.Panoptikopolis, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Authoritarian surveillance measures mandate that most buildings have to be built with glass walls. If you have nothing to hide, what are you worried about, citizen?",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.Panoptikopolis,
				});

			ChallengeManager.RegisterChallenge<Panoptikopolis>(new ChallengeInfo(cChallenge.Panoptikopolis, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
