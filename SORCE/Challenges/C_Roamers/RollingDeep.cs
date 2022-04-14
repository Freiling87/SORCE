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

namespace SORCE.Challenges.C_Roamers
{
	public class RollingDeep
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(RollingDeep);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Roamers - Rolling Deep"))
				.WithDescription(new CustomNameInfo(
					"The City Environmental Preservation Agency's sole member, Carl, cautions against enforced curfews and other measures to reduce gang violence." +
					"\"Anytime you mess with nature, you upset a balance,\" he cautions.\n \"These hoodlum species have evolved for constant struggle.\"\n" +
					"According to CEPA models, any lapse in gang violence leads to gang overpopulation. This inevitably explodes in an orgy of violence - and not the cool kind with boobs.\n\n"+
					"  - City Herald, September 6th, 2420 issue"));
		}
	}
}