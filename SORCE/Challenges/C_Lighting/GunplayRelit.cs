using RogueLibsCore;

namespace SORCE.Challenges.C_Lighting
{
    public static class GunplayRelit
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(GunplayRelit), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables Bullet Halos, and adds Muzzle Flash.\n\n" +
					"Bug: Lots of muzzle flash may create a permanent dark void on the map. It's probably your conscience telling you to stop shooting people.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Lighting - Gunplay Re-Lit",
				});
		}
	}
}
