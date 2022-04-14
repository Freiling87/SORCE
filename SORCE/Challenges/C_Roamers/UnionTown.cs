﻿using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Roamers
{
	public class UnionTown
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(UnionTown);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Roamers - Union Town"))
				.WithDescription(new CustomNameInfo(
					"We run dis town. Enable dis mutatah, or else. Stugots!"));
		}
	}
}