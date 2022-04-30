using RogueLibsCore;

namespace SORCE.Challenges.C_VFX
{
    public class BuggierBulletholes
	{
		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock("BuggierBulletholes", true)
			{
			})
				.WithName(new CustomNameInfo(
					"VFX - Buggier Bulletholes"))
				.WithDescription(new CustomNameInfo(
					"Spawns bullet holes on walls.\n\n" +
					"Bug: Holes spawn when wall is shot from North\n"+
					"Bug: Bullethole sprite displays above Agents\n" +
					"Bug: Bullethole sprites persist across levels\n" +
					"Basically, it's not worth it. Nothing's worth it. I hate this."));
		}
	}
}