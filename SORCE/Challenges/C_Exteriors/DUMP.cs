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

namespace SORCE.Challenges.C_Exteriors
{
	public class DUMP
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(DUMP);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = cChallenge.Exteriors.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Exteriors - DUMP"))
				.WithDescription(new CustomNameInfo(
					"Deep\nUnderground\nMetropolitan\nPrincipality\n\n" +
					"- Exteriors are Cave"));
		}
	}
}
