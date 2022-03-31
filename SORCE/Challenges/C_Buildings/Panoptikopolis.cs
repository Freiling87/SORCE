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

namespace SORCE.Challenges.C_Buildings
{
	public class Panoptikopolis
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(Panoptikopolis);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Buildings.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Panoptikopolis"))
				.WithDescription(new CustomNameInfo(
					"Authoritarian surveillance measures mandate that most buildings have to be built with glass walls. If you have nothing to hide, what are you worried about, citizen?\n\n" +
					"- Most buildings spawn with Glass walls\n" +
					"- People can see you pooping, but you can see them pooping too, sometimes at the same time. So it's best to avoid eye contact.\n" +
					"  Unless you're into that sort of thing." ));
		}
	}
}