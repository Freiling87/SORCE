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

namespace SORCE.Challenges.C_Features
{
	public class ThisLandIsMineLand
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(ThisLandIsMineLand);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - This Land is Mine Land"))
				.WithDescription(new CustomNameInfo(
					"This hand was maimed for you and me!\n\n" +
					"- Land mines spawn in all districts"));
		}
	}
}