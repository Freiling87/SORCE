using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_AmbientLightLevel
{
    public class Blinding : AmbientLightLevelChallenge
	{
		private Blinding() : base(nameof(Blinding)) { }

		public override int LightLevel => 255;

        //[RLSetup]
        static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock()
			{
				Cancellations = CColor.AmbientLightLevel.Where(i => i != nameof(Blinding)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Ambient Light Level - Blinding"))
				.WithDescription(new CustomNameInfo(
					"- Light level up to 11."));
		}
	}
}