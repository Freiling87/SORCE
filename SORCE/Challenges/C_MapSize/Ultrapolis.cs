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
	public class Ultrapolis
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Ultrapolis);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.MapSize.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Ultrapolis"))
				.WithDescription(new CustomNameInfo(
					"Many citizens do not know there is a world outside the city, and even insist that nothing else exists.\n\n" +
					"Buncha dipshits.\n\n" +
					" - Map size set to 200%\n" +
					"   (Average 60 chunks per level)"));
		}
	}
}