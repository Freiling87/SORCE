using RogueLibsCore;

namespace SORCE.Challenges.C_Features
{
    public class FlameGrates
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(FlameGrates);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Flame Grates"))
				.WithDescription(new CustomNameInfo(
					"Allows flame grates in all districts.\n\nWhat, you expected some dumb pun? I would never sink that low!"));
		}
	}
}