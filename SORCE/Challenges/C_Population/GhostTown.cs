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
	public class GhostTown
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(GhostTown);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Population.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Ghost Town"))
				.WithDescription(new CustomNameInfo(
					"No one walks the streets in this city... something horrible must have happened here to make everyone hide indoors!\n\n" +
					"Don't act all innocent, I know what you do to people in this game!\n\n" +
					"- Roaming population set to 0%"));
		}
	}
}