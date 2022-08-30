using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class Goodsprings : AmbientLightColorChallenge
	{
		public Goodsprings() : base(nameof(Goodsprings)) { }

		public override Color32 FilterColor =>		new Color32(200, 125, 25, 190);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Goodsprings()
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != nameof(Goodsprings)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Goodsprings"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for a nuclear winter."));
		}
	}
}   