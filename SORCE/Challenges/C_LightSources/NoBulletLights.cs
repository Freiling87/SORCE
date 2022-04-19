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
	public static class NoBulletLights
	{
		[RLSetup]
		public static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new MutatorUnlock(nameof(NoBulletLights), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Disables Bullet Lights",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = "Light Sources - No Bullet Lights",
				});
		}
	}
}
