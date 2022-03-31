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
	public class SurveillanceSociety
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(SurveillanceSociety);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Surveillance Society"))
				.WithDescription(new CustomNameInfo(
					"Those cameras? For your safety.\n" +
					"Oh, the turrets? For the cameras' safety.\n" +
					"The midnight raids and disappearances? Hm... what's your name, citizen?\n\n" +
					"- Spawns Security Cams & Turrets in public, aligned with The Law."));
		}
	}
}