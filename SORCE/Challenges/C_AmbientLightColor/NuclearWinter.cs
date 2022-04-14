using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class NuclearWinter : AmbientLightColorChallenge
	{
		public NuclearWinter() : base(nameof(NuclearWinter)) { }

        public override Color32 filterColor => new Color32(255, 255, 255, 175);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new NuclearWinter()
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != nameof(NuclearWinter)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Nuclear Winter"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for a scorched wasteland."));
		}
	}
}