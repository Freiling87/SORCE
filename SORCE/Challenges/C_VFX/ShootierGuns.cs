using RogueLibsCore;

namespace SORCE.Challenges.C_VFX
{
    public class ShootierGuns
	{
		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new MutatorUnlock("ShootierGuns", true)
			{
			})
				.WithName(new CustomNameInfo(
					"VFX - Shootier Guns"))
				.WithDescription(new CustomNameInfo(
					"Muzzle Flash, shell casings, and other visual adjustments for realism."));
		}
	}
}