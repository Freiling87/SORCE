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

namespace SORCE.Challenges.C_Exteriors
{
	public class TransitExperiment
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(TransitExperiment);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = cChallenge.Exteriors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Exteriors - Transit Experiment"))
				.WithDescription(new CustomNameInfo(
					"DESCRIPTIONS? PSHAW\n\n" +
					"- Public floors are Ice"));
		}
	}
}