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

namespace SORCE.Challenges.C_Features
{
	public class ThePollutionSolution
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(ThePollutionSolution);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - The Pollution Solution"))
				.WithDescription(new CustomNameInfo(
					"We've finally solved pollution! Make more, dump it everywhere, and then ignore it. It will become so ubiquitous you won't even notice it anymore. Problem solved!\n\n" +
					"- Pollution features spawn in all districts\n" +
					"- Lakes have 80% chance of being poisoned"));
		}
	}
}