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
	public class HalfMoon
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(HalfMoon);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Lighting - Half Moon"))
				.WithDescription(new CustomNameInfo(
					""));
		}
	}
}