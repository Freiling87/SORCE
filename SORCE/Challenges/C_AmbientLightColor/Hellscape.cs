using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class Hellscape : AmbientLightColorChallenge
	{
		public Hellscape() : base(nameof(Hellscape)) { }

        public override Color32 FilterColor => new Color32(255, 125, 125, 100);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Hellscape()
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != nameof(Hellscape)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Hellscape"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for not being in hell."));
		}
	}
}