using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Overhaul
{
	public class PoliceState
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(PoliceState);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CChallenge.Overhauls.Where(i => i != name).ToList()
				// AddsCriminals, AddsNonhumans, RemovesLawEnforcement, Zombies, MixedUpLevels
			})
				.WithName(new CustomNameInfo(
					"Overhaul - Police State"))
				.WithDescription(new CustomNameInfo(
					""));
		}
	}
}