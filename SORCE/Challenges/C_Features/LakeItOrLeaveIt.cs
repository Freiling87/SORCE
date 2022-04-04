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
	public class LakeItOrLeaveIt
	{
		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(LakeItOrLeaveIt), true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Lake it or Leave it"))
				.WithDescription(new CustomNameInfo(
					"Don't like large inland bodies of water? Too fuckin' bad, buddy!\n\n" +
					"- Lakes spawn in all districts\n" +
					"- Pond scum costs extra"));
		}
	}
}