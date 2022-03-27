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
	public class GrandCityHotel
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(GrandCityHotel);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Exteriors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Exteriors - Grand City Hotel"))
				.WithDescription(new CustomNameInfo(
					"Here's your room key. Please note that we will assess a surcharge if you are murdered in a messy way.\n\n" +
					"- Public floors are Wood\n" +
					"- Border walls are Wood\n" +
					"- Spawns Comfy Shit in public"));
		}
	}
}
