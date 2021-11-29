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
	public class SurveillanceSociety
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(SurveillanceSociety); 

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<SurveillanceSociety>(new ChallengeInfo(name, unlockBuilder));
		}
	}
}