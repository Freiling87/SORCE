using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class ShadowRealm : AmbientLightColorChallenge
	{
		public ShadowRealm() : base(nameof(ShadowRealm)) { }

        public override Color32 FilterColor => new Color32(75, 75, 75, 175);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new ShadowRealm()
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != nameof(ShadowRealm)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Shadow Realm"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for a fuckin' flashlight."));
		}
	}
}