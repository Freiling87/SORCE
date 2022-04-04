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
	public class DiscoCityDanceoff
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(DiscoCityDanceoff);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CChallenge.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - Disco City Danceoff"))
				.WithDescription(new CustomNameInfo(
					"Here's the skinny: the freaky deaky Mayor is just bad vibes, man. Time to make this city copacetic, can you dig it? Outta sight!"));
		}
	}
}