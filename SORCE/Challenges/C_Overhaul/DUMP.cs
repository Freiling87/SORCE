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
	public class DUMP
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(DUMP);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - DUMP"))
				.WithDescription(new CustomNameInfo(
					"Deep\nUnderground\nMetropolitan\nPrincipality\n\n" +
					"- Public floors are Cave floors\n" +
					"- Border walls are Cave walls\n" +
					"- Geological features spawn in all districts\n" +
					"- Cannibalism is legal\n" +
					"- Law enforced by Supercannibals"));
		}
	}
}
