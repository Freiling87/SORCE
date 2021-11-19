using System;
using System.Collections.Generic;
using System.Linq;
using RogueLibsCore;
using BunnyMod.Content.Challenges;
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

		/// <summary>
		/// mapping of ConflictGroups to the ChallengeTypes in that ConflictGroup
		/// </summary>
		private static readonly Dictionary<EChallengeConflictGroup, List<Type>> conflictGroupDict = new Dictionary<EChallengeConflictGroup, List<Type>>();

		public static string GetActiveChallengeFromList(List<string> challengeList)
		{
			foreach (string mutator in challengeList)
				if (GC.challenges.Contains(mutator))
					return mutator;

			return null;
		}

		/// <summary>
		/// Should be called *after* all of the custom Challenges have been registered.
		/// </summary>
		public static void FinalizeChallenges()
		{
			foreach (KeyValuePair<Type, ChallengeInfo> ChallengeEntry in registeredChallenges)
			{
				RegisterCancellations(ChallengeEntry.Key, ChallengeEntry.Value);
			}
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

		public static void RegisterChallenge<ChallengeType>(ChallengeInfo info)
		{
			info.FinalizeInfo();
			registeredChallenges.Add(typeof(ChallengeType), info);
			RegisterChallengeConflictGroup<ChallengeType>(info);
		}

		private static void RegisterChallengeConflictGroup<ChallengeType>(ChallengeInfo info)
		{
			foreach (EChallengeConflictGroup conflictGroup in info.ConflictGroups)
			{
				if (!conflictGroupDict.ContainsKey(conflictGroup))
				{
					conflictGroupDict[conflictGroup] = new List<Type>();
				}

				conflictGroupDict[conflictGroup].Add(typeof(ChallengeType));
			}
		}

		/// <summary>
		/// Sets the cancellations for the given Challenge.
		/// </summary>
		private static void RegisterCancellations(Type challengeType, ChallengeInfo challengeInfo)
		{
			UnlockWrapper unlock = challengeInfo.UnlockBuilder.Unlock;
			HashSet<string> cancellations = new HashSet<string>();

			// cancel all Challenges in this conflictGroup 
			if (challengeInfo.ConflictGroups.Count > 0)
			{
				foreach (string cancelChallenge in challengeInfo.ConflictGroups
						.SelectMany(group => conflictGroupDict[group])
						.Where(type => type != challengeType) // prevent Challenge from cancelling itself
						.Select(GetChallengeInfo)
						.Where(info => info != null)
						.Select(info => info.Name))
				{
					cancellations.Add(cancelChallenge);
				}
			}

			// TODO conflicts with vanilla Challenges

			unlock.SetCancellations(cancellations);
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