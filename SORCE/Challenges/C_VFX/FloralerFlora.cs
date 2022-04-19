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

namespace SORCE.Challenges.C_VFX
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
					"VFX - Floraler Flora"))
				.WithDescription(new CustomNameInfo(
					"The #1 most requested mutator ever. An absolute game-changer, practically Streets of Rogue 2!\n\n" +
					"- Spawns leaves around plants"));
		}
	}
}