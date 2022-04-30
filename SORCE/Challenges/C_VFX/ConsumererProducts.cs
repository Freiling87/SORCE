using RogueLibsCore;

namespace SORCE.Challenges.C_VFX
{
    public class ConsumererProducts
	{
		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock("ConsumererProducts", true)
			{
			})
				.WithName(new CustomNameInfo(
					"VFX - Consumerer Products"))
				.WithDescription(new CustomNameInfo(
					"Experience the thrill of throwing trash right where you made it!\n\nPlease keep your littering in-game."));
		}
	}
}