using RogueLibsCore;

namespace SORCE.Challenges.C_Features
{
    public class FireGrates
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(FireGrates);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Fire Grates"))
				.WithDescription(new CustomNameInfo(
					"Spawns fire grates in all districts.\n\nWhat, you expected some dumb pun? I would never sink that low!"));
		}
	}
}