using RogueLibsCore;

namespace SORCE.Challenges.C_Preview
{
    public class ScaryGunsPreview
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(ScaryGunsPreview);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Preview - Scary Guns"))
				.WithDescription(new CustomNameInfo(
					"Reduces bullet size, increases speed. Pretty buggy, but fun anyway."));
		}
	}
}