using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using RogueLibsCore;

namespace SORCE.Challenges.C_Lighting
{
	public static class NoObjectLights
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(NoObjectLights), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables Object Lights, except diegetic ones like Lamps and Computers.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Lighting - No Object Lights",
				});
		}
	}
}