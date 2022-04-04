using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using System.Linq;
using SORCE.Localization;

namespace SORCE.Challenges.C_AmbientLightLevel
{
	public class Daytime
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(Daytime);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - Daytime"))
				.WithDescription(new CustomNameInfo(
					"- Ambient light to 150%"));
		}
	}
}