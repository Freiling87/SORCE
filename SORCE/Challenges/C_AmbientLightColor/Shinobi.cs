using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class Shinobi : AmbientLightColorChallenge
	{
		public Shinobi() : base(nameof(Shinobi)) { }

        public override Color32 FilterColor => new Color32(75, 75, 150, 200);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Shinobi()
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