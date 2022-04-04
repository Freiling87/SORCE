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
using static SORCE.Localization.NameLists;

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
				Cancellations = CChallenge.Overhauls.Where(i => i != name).ToList()
				// MixedUpLevels, NoGuns
			})
				.WithName(new CustomNameInfo(
					"Overhaul - AnCapistan"))
				.WithDescription(new CustomNameInfo(
					"Freedom, at last! Freedom to starve in the gutter and watch your children wallow in the poverty you could never escape." +
					"Keep on dreaming and you'll make it someday! Get out there and earn some bootstraps, you future zillionaire!" +
					"#RespectTheHustle #LiveToGrind #PleaseHelpImStarving\n\n" +
					"- Profit-making features are more common\n" +
					"- Public utility features are eliminated\n" +
					"- Many objects now cost money to use"));
		}
	}
}