using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using System.Linq;

namespace SORCE.Challenges.C_Features
{
	public class BroughtbackFountain
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(BroughtbackFountain);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Broughtback Fountain"))
				.WithDescription(new CustomNameInfo(
					"\"He could smell Jack - the intensely familiar odor of cigarettes, musky sweat, and a faint sweetness like grass, and with it the rushing cold of the fountain."));
		}
	}
}