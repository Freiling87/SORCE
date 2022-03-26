using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using SORCE.Content.Challenges;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Interiors
{
	public class GreenLiving
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(GreenLiving);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = cChallenge.Interiors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Interiors - Green Living"))
				.WithDescription(new CustomNameInfo(
					"The Mayor has retrofitted most buildings to eco-friendly plant-based construction. The air is mighty fresh... except near the compost-burning stoves.\n\n" +
					"- Most buildings spawn with Hedge walls"));
		}
	}
}