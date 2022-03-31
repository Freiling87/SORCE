using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using System.Linq;
using SORCE.Localization;

namespace SORCE.Challenges.C_Buildings
{
	public class CityOfSteel
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(CityOfSteel);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Buildings.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - City of Steel"))
				.WithDescription(new CustomNameInfo(
					"A gleaming city of steel! The world of the future, today. Mankind's dream in... Wow, it *really* smells like steel cleaner. Like, it fucking stinks.\n\n" +
					"- Most buildings spawn with Steel walls"));
		}
	}
}