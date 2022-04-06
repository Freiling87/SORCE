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

namespace SORCE.Challenges.C_AmbientLightColor
{
	public class ShadowRealm
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(ShadowRealm);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Shadow Realm"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for a fuckin' flashlight."));
		}
	}
}