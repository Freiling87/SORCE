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
	public class CartOfTheDeal
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(CartOfTheDeal);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<CartOfTheDeal>(new ChallengeInfo(name, unlockBuilder));
		}
	}
}