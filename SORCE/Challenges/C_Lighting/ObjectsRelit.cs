using RogueLibsCore;

namespace SORCE.Challenges.C_Lighting
{
    public static class ObjectsRelit
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(ObjectsRelit), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables Object Halos, and adds diegetic ones for objects like TVs and Computers.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Lighting - Objects Re-Lit",
				});
		}
	}
}