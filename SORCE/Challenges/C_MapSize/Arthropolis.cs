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

namespace SORCE.Challenges.C_MapSize
{
	public class Arthropolis
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Arthropolis);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = cChallenge.MapSize.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Arthropolis"))
				.WithDescription(new CustomNameInfo(
					"\"The Streets of Rogue City Building For Slum Dwellers Who Can't Be Rich Good\"\n\n" +
					"    - Inscription over the entrance to the Slums, District 420, Floor 69\n\n" + 
					" - Map size set to 12.5%\n" +
					"   (Average 4 chunks per level)"));
		}
	}
}