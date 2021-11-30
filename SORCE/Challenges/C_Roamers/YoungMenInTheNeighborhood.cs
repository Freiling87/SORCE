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
			const string name = nameof(YoungMenInTheNeighborhood);

			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true));

			ChallengeManager.RegisterChallenge<YoungMenInTheNeighborhood>(new ChallengeInfo(name, unlockBuilder));
		}
	}
}
