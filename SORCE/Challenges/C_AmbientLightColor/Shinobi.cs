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

namespace SORCE.Challenges.C_AmbientLightColor
{
	public class Shinobi
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Shinobi);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.AmbientLightColor.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Shinobi"))
				.WithDescription(new CustomNameInfo(
					"DESCRIPTION HERE\n\n" +
					"- Blue tint\n"));
		}
	}
}