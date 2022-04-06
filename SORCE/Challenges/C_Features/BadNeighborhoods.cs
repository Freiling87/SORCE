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
	public class BadNeighborhoods
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(BadNeighborhoods);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Bad Neighborhoods"))
				.WithDescription(new CustomNameInfo(
					"This place sure has gone to shit, hasn't it?"));
		}
	}
}