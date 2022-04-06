using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Features
{
	public class SkywayDistrict
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(SkywayDistrict);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Skyway District"))
				.WithDescription(new CustomNameInfo(
					"Okay, the Mayor sold all the water. Before you freak out, here are the Pros:\n" +
					"    1. We just made a ton of money.\n" +
					"    2. Downtown smells better.\n" +
					"    3. The view is... uh, bottomless. But Chasm-side apartments are going up in value!\n\n" +
					"You might also plummet to your death, but that makes you the a broken egg that helped make the delicious omelet of massive profits!"));
		}
	}
}