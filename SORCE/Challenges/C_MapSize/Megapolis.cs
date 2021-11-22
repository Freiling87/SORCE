using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using BunnyMod;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;

namespace BunnyMod.Content.Challenges.C_MapSize
{
	public class Megalopolis
	{
		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(cChallenge.Megalopolis, true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This town has so gotten big. You remember when it was just a local Mega-Arcology. Now it's a Mega-Mega-Arcology.\n\n- Map size set to 150%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = cChallenge.Megalopolis,
				});

			ChallengeManager.RegisterChallenge<Megalopolis>(new ChallengeInfo(cChallenge.Megalopolis, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.BuildingChallenges));
		}
	}
}
