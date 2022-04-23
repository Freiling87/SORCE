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
					[LanguageCode.English] = "Disables Bullet Halos, and adds Muzzle Flash.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Lighting - Gunplay Re-Lit",
				});
		}
	}
}
