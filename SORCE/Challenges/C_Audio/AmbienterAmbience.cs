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

namespace SORCE.Challenges.C_Audio
{
	public class AmbienterAmbience
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(AmbienterAmbience);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Audio - Ambienter Ambience"))
				.WithDescription(new CustomNameInfo(
					"- Restores ambient audio for Casino chunks\n" +
					"- Applies general ambient audio depending on other mutators"));
		}
	}
}