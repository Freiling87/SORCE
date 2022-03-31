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
	public class YoungMenInTheNeighborhood
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(YoungMenInTheNeighborhood);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Roamers - Young Men in the Neighborhood"))
				.WithDescription(new CustomNameInfo(
				"Because the young gentlemen in the hood are always polite;" +
				"If you start acting rude, we'll set you right!" +
				"- Friendly local Gangbangers now roam every district"));
		}
	}
}