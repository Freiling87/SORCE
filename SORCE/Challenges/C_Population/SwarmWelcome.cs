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

namespace SORCE.Challenges.C_Population
{
	public class SwarmWelcome
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(SwarmWelcome);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Population.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Swarm Welcome"))
				.WithDescription(new CustomNameInfo(
					"This whole city feels like a crowded subway. Pickpocketing is bad enough, but the frottage is out of control!\n\n" +
					"- Roaming population set to 800%\n" +
					"- You might get pregnant"));
		}
	}
}