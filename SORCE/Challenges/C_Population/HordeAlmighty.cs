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

namespace SORCE.Challenges.C_Population
{
	public class HordeAlmighty
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(HordeAlmighty);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Population.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Horde Almighty"))
				.WithDescription(new CustomNameInfo(
					"The City administration is trying out a contraception ban to combat the high death rate. Hope it works, because they didn't think of a \"Plan B!\" Get it? I'm here all week, folks.\n\n" +
					"- Roaming population set to 200%\n" +
					"- You might get pregnant"));
		}
	}
}