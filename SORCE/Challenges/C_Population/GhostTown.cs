using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Population
{
    public class GhostTown : PopulationChallenge
	{
		private GhostTown() : base(nameof(GhostTown)) { }

		public override int PopulationMultiplier => 0;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new GhostTown()
			{
				Cancellations = CChallenge.Population.Where(i => i != nameof(GhostTown)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Ghost Town"))
				.WithDescription(new CustomNameInfo(
					"No one walks the streets in this city... something horrible must have happened here to make everyone hide indoors!\n\n" +
					"Don't act all innocent, I know what you do to people in this game!"));
		}
	}
}