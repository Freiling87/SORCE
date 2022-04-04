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
	public class Arcology
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(Arcology);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CChallenge.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - Arcology"))
				.WithDescription(new CustomNameInfo(
					"Sustainable Eco-homes! Trees! Less pollution! What's not to love?\n" +
					"(Answer: Sharing a home with bugs and frogs.)\n\n" +
					"- Public floors are Grass\n" +
					"- Border walls are Hedges\n" +
					"- Nature features spawn in all districts\n" + 
					"- Pollution features disabled"));
		}
	}
} 