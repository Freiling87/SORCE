using RogueLibsCore;

namespace SORCE.Challenges.C_Features
{
    public class PublicScreens
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(PublicScreens);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Public Screens"))
				.WithDescription(new CustomNameInfo(
					"Spawns digital screens in public, meant to represent advertisements, PSAs, or whatever else. Purely audiovisual.\n\n" +
					"Bug: Screen persists when wall destroyed\n" +
					"Bug: Screen does not spawn on certain wall types"));
		}
	}
}