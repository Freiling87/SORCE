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

namespace SORCE.Challenges.C_Wreckage
{
	public class FloralerFlora
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(FloralerFlora);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Wreckage - Floraler Flora"))
				.WithDescription(new CustomNameInfo(
					"Wreckage mutators are visual-only. They add particles to add a degree of realism and variety to the environment.\n\n" +
					"- Spawns leaves around plants"));
		}
	}
}