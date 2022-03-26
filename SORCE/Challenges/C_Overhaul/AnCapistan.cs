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
	public class AnCapistan
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(AnCapistan);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = cChallenge.Overhaul.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Overhaul - AnCapistan"))
				.WithDescription(new CustomNameInfo(
					""));
		}
	}
}