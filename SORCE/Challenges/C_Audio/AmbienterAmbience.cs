using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using System.Linq;

namespace SORCE.Challenges.C_Audio
{
	public class AmbienterAmbience
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(AmbienterAmbience);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Audio - Ambienter Ambience"))
				.WithDescription(new CustomNameInfo(
					"Restores ambient audio for Casino chunks"));
		}
	}
}