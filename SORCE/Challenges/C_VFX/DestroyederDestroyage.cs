using RogueLibsCore;

namespace SORCE.Challenges.C_VFX
{
    public class DestroyederDestroyage
	{
		//[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock("DestroyederDestroyage", true)
			{
			})
				.WithName(new CustomNameInfo(
					"VFX - Destroyeder Destroyage"))
				.WithDescription(new CustomNameInfo(
					"Destroying certain objects creates special wreckage. Also teaches you cool new words that are actual real words, actually, really."));
		}
	}
}