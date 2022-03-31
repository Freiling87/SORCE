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

namespace SORCE.Challenges.C_AmbientLightColor
{
	public class Reactor
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Reactor);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.AmbientLightColor.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Reactor"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for a hazmat suit, or at least some fun drugs.\n\n"));
		}
	}
}