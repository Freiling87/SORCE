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
	public class ShantyTown
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(ShantyTown);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Interiors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Interiors - Shanty Town"))
				.WithDescription(new CustomNameInfo(
					"A whole city made with cheap wooden construction: A tinderbox on cinder blocks. Hard mode for Firefighters, easy mode for arsonists. Fun mode for psychopaths.\n\n" +
					"- Most buildings spawn with Wood walls"));
		}
	}
}