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
	public static class AgentsRelit
	{
		//[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(AgentsRelit), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables Agent Halos",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Lighting - Agents Re-lit",
				});
		}
	}
}
