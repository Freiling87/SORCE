using RogueLibsCore;

namespace SORCE.Challenges.C_Features
{
    public class MeatsOfRogue
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(MeatsOfRogue);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Meats of Rogue"))
				.WithDescription(new CustomNameInfo(
					"Allows Meat Tubes in all districts. And no, I don't mean humans."));
		}
	}
}