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
	public class SkywayDistrict
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(SkywayDistrict);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<SkywayDistrict>(new ChallengeInfo(name, unlockBuilder));
		}
	}
}