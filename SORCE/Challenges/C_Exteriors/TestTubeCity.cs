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
	public class TestTubeCity
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(TestTubeCity);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Exteriors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Exteriors - Test Tube City"))
				.WithDescription(new CustomNameInfo(
					"Another guinea pig!\n\n" +
					"- Public floors are _____\n" +
					"- Border walls are Glass"));
		}
	}
}
