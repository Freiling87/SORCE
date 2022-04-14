using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Population
{
    public class HordeAlmighty : PopulationChallenge
	{
		private HordeAlmighty() : base(nameof(HordeAlmighty)) { }

		public override int PopulationMultiplier => 2;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new HordeAlmighty()
			{
				Cancellations = CChallenge.Population.Where(i => i != nameof(HordeAlmighty)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Horde Almighty"))
				.WithDescription(new CustomNameInfo(
					"The City administration is trying out a contraception ban to combat the high death rate. Hope it works, because they didn't think of a \"Plan B!\" Get it? I'm here all week, folks."));
		}
	}
}