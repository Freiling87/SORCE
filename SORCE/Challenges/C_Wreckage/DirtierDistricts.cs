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

namespace SORCE.Challenges.C_Wreckage
{
	public class DirtierDistricts
	{
		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock("DirtierDistricts", true)
			{
			})
				.WithName(new CustomNameInfo(
					"Wreckage - Dirtier Districts"))
				.WithDescription(new CustomNameInfo(
					"Also known as Realistic Mode."));
		}
	}
}