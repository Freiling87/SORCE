using System;
using System.Collections.Generic;
using System.Linq;
using RogueLibsCore;
using SORCE.Challenges;
using BepInEx.Logging;
using SORCE.Logging;

namespace SORCE.Challenges
{
	public static class ChallengeManager
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		private static GameController GC => GameController.gameController;

		private static readonly Dictionary<Type, ChallengeInfo> registeredChallenges = new Dictionary<Type, ChallengeInfo>();

		public static string GetActiveChallengeFromList(List<string> challengeList)
		{
			foreach (string mutator in challengeList)
				if (GC.challenges.Contains(mutator))
					return mutator;

			return null;
		}

		public static ChallengeInfo GetChallengeInfo<ChallengeType>()
		{
			return GetChallengeInfo(typeof(ChallengeType));
		}

		public static ChallengeInfo GetChallengeInfo(Type ChallengeType)
		{
			return registeredChallenges.ContainsKey(ChallengeType)
					? registeredChallenges[ChallengeType]
					: null;
		}

		public static bool IsChallengeFromListActive(List<string> challengeList)
		{
			foreach (string mutator in challengeList)
				if (GC.challenges.Contains(mutator))
					return true;

			return false;
		}

		public static T SetCancellations<T>(this T wrapper, IEnumerable<string> cancellations) where T : UnlockWrapper
		{
			if (wrapper.Unlock.cancellations == null)
			{
				wrapper.Unlock.cancellations = new List<string>();
			}
			wrapper.Unlock.cancellations.Clear();
			wrapper.Unlock.cancellations.AddRange(cancellations);
			return wrapper;
		}
	}
}