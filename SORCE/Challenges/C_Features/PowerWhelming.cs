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
					"Look, you're not gonna be blown away by this. You might want to sit down before reading on, but there'd be no point.\n\n" +
					"- Power Boxes spawn in all districts\n" +
					"- Are you whelmed yet??"));
		}
	}
}