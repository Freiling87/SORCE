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

namespace SORCE.Challenges.C_Population
{
	public class LetMeSeeThatThrong
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(LetMeSeeThatThrong);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Population.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Let Me See That Throng"))
				.WithDescription(new CustomNameInfo(
				"Ooh that City so scandalous\n" +
				"and you know another player couldn't handle it,\n" +
				"see you playing that thing like \"Who's the ish?\"\n" +
				"with a look in your eye so devilish, uh!\n\n" +
				"- Roaming population set to 400%"));
		}
	}
}