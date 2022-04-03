using System;
using System.Collections.Generic;
using System.Linq;
using RogueLibsCore;
using SORCE.Challenges;
using BepInEx.Logging;
using SORCE.Logging;
using SORCE.Challenges.C_Buildings;

namespace SORCE.Challenges
{
	public static class ChallengeManager
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		private static GameController GC => GameController.gameController;

		private static readonly Dictionary<Type, ChallengeInfo> registeredChallenges = new Dictionary<Type, ChallengeInfo>();

		public static string GetActiveChallengeFromList(List<string> challengeList) =>
			challengeList.Where(c => GC.challenges.Contains(c)).FirstOrDefault();

		public static Type GetActiveChallengeFromList(List<Type> challengeList) =>
			challengeList.Where(c => GC.challenges.Contains(nameof(c))).FirstOrDefault();

		public static ChallengeInfo GetChallengeInfo<ChallengeType>() =>
			GetChallengeInfo(typeof(ChallengeType));

		public static ChallengeInfo GetChallengeInfo(Type ChallengeType) =>
			registeredChallenges.ContainsKey(ChallengeType)
				? registeredChallenges[ChallengeType]
				: null;

		public static bool IsChallengeFromListActive(List<string> challengeList) =>
			challengeList.Where(c => GC.challenges.Contains(c)).Any();

		public static T SetCancellations<T>(this T wrapper, IEnumerable<string> cancellations) where T : UnlockWrapper
		{
			if (wrapper.Unlock.cancellations == null)
				wrapper.Unlock.cancellations = new List<string>();
			
			wrapper.Unlock.cancellations.Clear();
			wrapper.Unlock.cancellations.AddRange(cancellations);
			return wrapper;
		}
	}
}