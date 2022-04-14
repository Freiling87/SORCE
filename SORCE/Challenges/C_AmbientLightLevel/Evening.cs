using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_AmbientLightLevel
{
    public class Evening : AmbientLightLevelChallenge
	{
		private Evening() : base(nameof(Evening)) { }

		public override int LightLevel => 150;

        //[RLSetup]
        static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Evening()
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != nameof(Evening).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - Evening"))
				.WithDescription(new CustomNameInfo(
					""));
		}
	}
}