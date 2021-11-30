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
	public class CanalCity
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(CanalCity);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<CanalCity>(new ChallengeInfo(name, unlockBuilder)
				.WithConflictGroup(EChallengeConflictGroup.Exteriors));
		}
	}
}