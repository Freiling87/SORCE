using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using System.Linq;
using SORCE.Localization;

namespace SORCE.Challenges.C_AmbientLightColor
{
	public class Shinobi : AmbientLightColorChallenge
	{
		public Shinobi() : base(nameof(Shinobi)) { }

        public override Color32 filterColor => new Color32(75, 75, 150, 200);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock()
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != nameof(Shinobi)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Shinobi"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for cool toe-shoes."));
		}
	}
}