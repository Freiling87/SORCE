using RogueLibsCore;

namespace SORCE.Challenges.C_VFX
{
    public class BachelorerPads
	{
		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock("BachelorerPads", true)
			{
			})
				.WithName(new CustomNameInfo(
					"VFX - Bachelorer Pads"))
				.WithDescription(new CustomNameInfo(
					"Now you don't just live in a disgusting dump, you play in a virtual one too!"));
		}
	}
}