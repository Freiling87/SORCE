using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_AmbientLightLevel
{
    public class HalfMoon : AmbientLightLevelChallenge
	{
		private HalfMoon() : base(nameof(HalfMoon)) { }

		public override int LightLevel => 50;

        //[RLSetup]
        static void Start()
		{
			RogueLibs.CreateCustomUnlock(new HalfMoon()
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != nameof(HalfMoon)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - Half Moon"))
				.WithDescription(new CustomNameInfo(
					""));
		}
	}
}