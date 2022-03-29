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
	public class Skyscraper
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(Skyscraper);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Exteriors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Exteriors - Skyscraper"))
				.WithDescription(new CustomNameInfo(
					"Shiny!\n\n" +
					"- Public floors are _____\n" +
					"- Border walls are Glass"));
		}
	}
}
