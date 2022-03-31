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

namespace SORCE.Challenges.C_Roamers
{
	public class HoodlumsWonderland
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(HoodlumsWonderland);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Roamers - Hoodlum's Wonderland"))
				.WithDescription(new CustomNameInfo(
					"The annual charity drive for the Blahds and Crepes happened to overlap this year. They're in tough competition to sell the most cookies... by any means necessary!\n\n" +
					"- Roaming gang spawns are increased. By a lot."));
		}
	}
}