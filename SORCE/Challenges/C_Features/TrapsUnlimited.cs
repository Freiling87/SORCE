using RogueLibsCore;

namespace SORCE.Challenges.C_Features
{
    public class TrapsUnlimited
	{
		[RLSetup]
		static void Start()
		{
			const string name = nameof(TrapsUnlimited);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
			})
				.WithName(new CustomNameInfo(
					"Features - Traps Unlimited"))
				.WithDescription(new CustomNameInfo(
					"Allows flame grates & sawblades in all districts."));
		}
	}
}