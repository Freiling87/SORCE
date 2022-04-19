using RogueLibsCore;

namespace SORCE.Challenges.C_Lighting
{
    public static class NoBulletLights
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(NoBulletLights), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables Bullet Lights",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Light Sources - No Bullet Lights",
				});
		}
	}
}
