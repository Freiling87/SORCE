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

namespace SORCE.Challenges.C_Features
{
	public class CartOfTheDeal
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(CartOfTheDeal);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Cart of the Deal"))
				.WithDescription(new CustomNameInfo(
					"A lot of people - very important people - are saying the City has the best Vendor Carts. The best folks, just tremendous! Don't we love our Vendor Carts? Absolutely tremendous."));
		}
	}
}