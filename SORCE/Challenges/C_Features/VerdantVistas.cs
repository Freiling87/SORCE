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
	public class VerdantVistas
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(VerdantVistas);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Verdant Vistas"))
				.WithDescription(new CustomNameInfo(
					"Greening up the place, one plant at a time."));
		}
	}
}