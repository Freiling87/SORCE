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

namespace SORCE.Challenges.C_Roamers
{
	public class MobTown
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(MobTown);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Roamers - Mob Town"))
				.WithDescription(new CustomNameInfo(
					"Enable dis mutatah, or else. Stugots!\n\n" +
					"- The Mob spawns in every district" +
					"- You gotta problem? I'd hate if we had a problem."));
		}
	}
}