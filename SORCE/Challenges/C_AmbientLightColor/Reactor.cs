using RogueLibsCore;
using SORCE.Localization;
using System.Linq;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public class Reactor : AmbientLightColorChallenge
	{
		public Reactor() : base(nameof(Reactor)) { }

		public override Color32 filterColor => new Color32(75, 200, 50, 125);

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock()
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