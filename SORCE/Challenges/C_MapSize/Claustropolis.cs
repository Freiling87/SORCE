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
	public class Claustropolis
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Claustropolis);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.MapSize.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Claustropolis"))
				.WithDescription(new CustomNameInfo(
					"Damn, this city is cramped! Who's Claus, anyway?\n\n" +
					" - Map size set to ~53%\n" +
					"   (Average 16 chunks per level)"));
		}
	}
}