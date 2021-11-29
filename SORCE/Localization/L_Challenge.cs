using BepInEx.Logging;
using JetBrains.Annotations;
using RogueLibsCore;
using SORCE.Logging;
using System.Collections.Generic;

namespace SORCE.Localization
{
	public class L_Challenge
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		[UsedImplicitly] public Dictionary<string, Dictionary<LanguageCode, LocalizedChallenge>> challenges;

		public Dictionary<LanguageCode, LocalizedChallenge> GetLocalization<ChallengeType>()
		{
			string id = typeof(ChallengeType).Name;
			if (!challenges.ContainsKey(id))
			{
				logger.LogWarning($"ChallengeLocalization did not find Localization for ID: '{id}'");
				return null;
			}
			return challenges[id];
		}

		[UsedImplicitly]
		public class LocalizedChallenge
		{
			public string Name { get; set; }
			public string Desc { get; set; }
		}
	}
}
