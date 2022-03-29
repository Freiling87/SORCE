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
	public class MonkeTown
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(MonkeTown);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - MonkeTown"))
				.WithDescription(new CustomNameInfo(
					"Daa na-na, won't you take me to\n" +
					"Daa na-na, Monke Town!\n" +
					"- Laws enforced by Gorillas\n"));
		}
	}
}