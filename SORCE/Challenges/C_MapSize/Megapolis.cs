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

namespace SORCE.Challenges.C_MapSize
{
	public class Megapolis
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Megapolis);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.MapSize.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Megapolis"))
				.WithDescription(new CustomNameInfo(
					"This town has so gotten big. You remember when it was just a local Mega-Arcology. Now it's a Mega-Mega-Arcology.\n\n" +
					" - Map size set to 150%\n" +
					"   (Average 45 chunks per level)"));
		}
	}
}