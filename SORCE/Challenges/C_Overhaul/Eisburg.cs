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
	public class Eisburg
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(Eisburg);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - Eisburg"))
				.WithDescription(new CustomNameInfo(
					"A quaint little arco, but bring a sweater!\n\n" +
					"- Public floors are Ice\n" +
					"- Eliminates all Fire features\n" +
					"- Some cops are armed with Freeze Rays"));
		}
	}
}