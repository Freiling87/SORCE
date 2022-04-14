using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_AmbientLightLevel
{
    public class Daytime : AmbientLightLevelChallenge
	{
		private Daytime() : base(nameof(Daytime)) { }

		public override int LightLevel => 200;

        //[RLSetup]
        static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Daytime()
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != nameof(Daytime)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - Daytime"))
				.WithDescription(new CustomNameInfo(
					"- Ambient light to 150%"));
		}
	}
}