using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_Overhaul
{
    public class CanalCity
	{
		//[RLSetup]
		static void Start()
		{
			const string name = nameof(CanalCity);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CChallenge.Overhauls.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo("Overhaul - Canal City"))
				.WithDescription(new CustomNameInfo(
					"Sure, it's like Venice... Venice this filthy water gonna get cleaned up?\n\n" +
					"- Public floors are Water"));
		}
	}
}