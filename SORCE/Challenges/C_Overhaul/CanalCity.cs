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

namespace SORCE.Challenges.C_Overhaul
{
	public class CanalCity
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(CanalCity);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = NameLists.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo("Overhaul - Canal City"))
				.WithDescription(new CustomNameInfo(
					"Sure, it's like Venice... Venice this filthy water gonna get cleaned up?\n\n" +
					"- Public floors are Water"));
		}
	}
}