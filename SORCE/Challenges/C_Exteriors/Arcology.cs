using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using SORCE.Content.Challenges;

namespace SORCE.Challenges.C_Exteriors
{
	public class Arcology
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Arcology);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<Arcology>(new ChallengeInfo(name, unlockBuilder)
				.WithConflictGroup(
				EChallengeConflictGroup.Arcology, 
				EChallengeConflictGroup.Exteriors));
		}
	}
}