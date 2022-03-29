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

namespace SORCE.Challenges.C_Buildings
{
	public class SpelunkyDory
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(SpelunkyDory);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Buildings.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Spelunky Dory"))
				.WithDescription(new CustomNameInfo(
					"You and your fellow citizens live in a disgusting cave complex. As the mayor says, \"Don't be a CAN'Tibal, be a CANnibal!\" Man, fuck the Mayor.\n\n" +
					"- Most buildings spawn with Cave walls"));
		}
	}
}