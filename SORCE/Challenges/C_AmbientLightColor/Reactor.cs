using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class Reactor : AmbientLightColorChallenge
	{
		public Reactor() : base(nameof(Reactor)) { }

		public override Color32 FilterColor => new Color32(150, 255, 150, 100);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Reactor()
			{
				Cancellations = CColor.AmbientLightColor.Where(i => i != nameof(Reactor)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Color - Reactor"))
				.WithDescription(new CustomNameInfo(
					"Kinda makes you wish for a hazmat suit, or at least some fun drugs."));
		}
	}
}