using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_AmbientLightLevel
{
    public class NewMoon : AmbientLightLevelChallenge
	{
		private NewMoon() : base(nameof(NewMoon)) { }

		public override int LightLevel => 0;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new NewMoon()
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != nameof(NewMoon)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - New Moon"))
				.WithDescription(new CustomNameInfo(
					"Moon must be busted. Brand new, and it's completely dark!"));
		}
	}
}