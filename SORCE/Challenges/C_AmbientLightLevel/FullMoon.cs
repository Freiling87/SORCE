using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_AmbientLightLevel
{
    public class FullMoon : AmbientLightLevelChallenge
	{
		private FullMoon() : base(nameof(FullMoon)) { }

		public override int LightLevel => 100;

        //[RLSetup]
        static void Start()
		{
			RogueLibs.CreateCustomUnlock(new FullMoon()
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != nameof(FullMoon)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - Full Moon"))
				.WithDescription(new CustomNameInfo(
					""));
		}
	}
}