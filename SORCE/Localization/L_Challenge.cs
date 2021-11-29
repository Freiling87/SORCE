using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using UnityEngine;

namespace SORCE.Localization
{
	public class L_Challenge
	{
		[UsedImplicitly] public Dictionary<string, Dictionary<LanguageCode, LocalizedChallenge>> traits;

		public Dictionary<LanguageCode, LocalizedChallenge> GetLocalization<ChallengeType>()
		{
			string id = typeof(ChallengeType).Name;
			if (!traits.ContainsKey(id))
			{
				Debug.LogWarning("ChallengeLocalization did not find Localization for ID: '" + id + "'");
				return null;
			}
			return traits[id];
		}

		[UsedImplicitly]
		public class LocalizedChallenge
		{
			public string Name { get; set; }
			public string Desc { get; set; }
		}
	}
}
