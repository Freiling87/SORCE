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

namespace SORCE.Challenges.C_Overhaul
{
	public class LowTechLowLife
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(LowTechLowLife);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - Low-tech, Low-Life"))
				.WithDescription(new CustomNameInfo(
					"The future is already here, it's just not very evenly distributed. Especially not here.\n\n" +
					"- Removes all high-tech features."));
		}
	}
}