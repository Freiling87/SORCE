using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class Sepia : AmbientLightColorChallenge
	{
		public Sepia() : base(nameof(Sepia)) { }

        public override Color32 FilterColor => new Color32(150, 150, 50, 190);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Sepia()
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != nameof(Sepia)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Sepia"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for a good old-fashioned rowdy-dowd."));
		}
	}
}