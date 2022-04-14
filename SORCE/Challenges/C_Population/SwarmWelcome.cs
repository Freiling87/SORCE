using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Population
{
    public class SwarmWelcome : PopulationChallenge
	{
		private SwarmWelcome() : base(nameof(SwarmWelcome)) { }

		public override int PopulationMultiplier => 8;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new SwarmWelcome()
			{
				Cancellations = CChallenge.Population.Where(i => i != nameof(SwarmWelcome)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Swarm Welcome"))
				.WithDescription(new CustomNameInfo(
					"This whole city feels like a crowded subway. Pickpocketing is bad enough, but the frottage is out of control!"));
		}
	}
}