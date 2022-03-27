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
	public class Arcology
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Arcology);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Exteriors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Exteriors - Arcology"))
				.WithDescription(new CustomNameInfo(
					"Sustainable Eco-homes! Trees! Less pollution! What's not to love?\n" +
					"(Answer: Sharing a home with bugs and frogs.)\n\n" +
					"- Public floors are Grass\n"));
					// "- Border walls are Hedges\n" + Not implemented
		}
	}
} 