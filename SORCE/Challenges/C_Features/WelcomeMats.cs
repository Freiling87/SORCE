using RogueLibsCore;

namespace SORCE.Challenges.C_Features
{
    public class WelcomeMats
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(WelcomeMats);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Welcome Mats"))
				.WithDescription(new CustomNameInfo(
					"Spawns bear traps. Not exactly welcome mats, but something you put out for people you're having over for dinner."));
		}
	}
}