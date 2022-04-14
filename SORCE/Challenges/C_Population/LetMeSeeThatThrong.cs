using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Population
{
    public class LetMeSeeThatThrong : PopulationChallenge
	{
		private LetMeSeeThatThrong() : base(nameof(LetMeSeeThatThrong)) { }

		public override int PopulationMultiplier => 4;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new LetMeSeeThatThrong()
			{
				Cancellations = CChallenge.Population.Where(i => i != nameof(LetMeSeeThatThrong)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Population - Let Me See That Throng"))
				.WithDescription(new CustomNameInfo(
					"Ooh that City so scandalous\n" +
					"and you know another player couldn't handle it,\n" +
					"see you playing that thing like \"Who's the ish?\"\n" +
					"with a look in your eye so devilish, uh!"));
		}
	}
}