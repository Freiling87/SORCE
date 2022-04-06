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
	public class PowerWhelming
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(PowerWhelming);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Power Whelming"))
				.WithDescription(new CustomNameInfo(
					"Look, you're not gonna be blown away by this. You might not want to sit down.\n\n" +
					"Power boxes spawn in every district. Are you whelmed yet?"));
		}
	}
}