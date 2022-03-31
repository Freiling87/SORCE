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
	public static class NoAgentLights
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(NoAgentLights), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables Agent Lights",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Lighting - No Agent Lights",
				});
		}
	}
}
