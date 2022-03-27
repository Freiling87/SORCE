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

namespace SORCE.Challenges.C_AmbientLightLevel
{
	public class NewMoon
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(NewMoon);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.AmbientLightLevel.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Lighting - New Moon"))
				.WithDescription(new CustomNameInfo(
					"Brand new moon, and it don't light up one bit!\n\n" +
					"- Removes ambient lighting. Darkness is pitch black.\n"));
		}
	}
}