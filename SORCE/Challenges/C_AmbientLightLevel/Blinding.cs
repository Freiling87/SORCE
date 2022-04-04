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

namespace SORCE.Challenges.C_AmbientLightLevel
{
	public class Blinding
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(Blinding);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - Blinding"))
				.WithDescription(new CustomNameInfo(
					"- Light level up to 11."));
		}
	}
}