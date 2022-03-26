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

namespace SORCE.Challenges.C_Overhaul
{
	public class MACITS
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(MACITS);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = cChallenge.Overhaul.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - MACITS"))
				.WithDescription(new CustomNameInfo(
					"Mostly Automated Comfortable Inclusive Terrestrial Socialism\n\n" +
					"- Money is obsolete\n"));
		}
	}
}