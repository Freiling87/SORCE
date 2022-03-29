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
	public class TestTubeCity
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(TestTubeCity);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - Test Tube City"))
				.WithDescription(new CustomNameInfo(
					"Eat your agar, little piggies!\n\n" +
					"- Public floors are _____\n" +
					"- Border walls are Glass\n" +
					"- Certain mutators are more likely\n" + 
					"- Law enforced by Super Scientists\n" + 
					"- Scientists have The Law\n" +
					"- Gorillas have Fair Game\n"));
		}
	}
}
