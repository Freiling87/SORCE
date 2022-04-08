using RogueLibsCore;

namespace SORCE.Challenges.C_Features
{
    public class ThePollutionSolution
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(ThePollutionSolution);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - The Pollution Solution"))
				.WithDescription(new CustomNameInfo(
					"We've finally solved pollution! Make more, dump it everywhere, and then ignore it. It will become so ubiquitous you won't even notice it anymore. Problem solved!"));
		}
	}
}