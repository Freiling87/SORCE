using System;
using System.Collections.Generic;
using System.Linq;
using RogueLibsCore;
using BunnyMod.Content.Challenges;
using SORCE.Challenges;
using BepInEx.Logging;
using SORCE.Logging;

namespace BunnyMod.Content.Challenges
{
	public static class BMChallengesManager
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		private static GameController GC => GameController.gameController;

		private static readonly Dictionary<Type, ChallengeInfo> registeredChallenges = new Dictionary<Type, ChallengeInfo>();

		/// <summary>
		/// mapping of ConflictGroups to the ChallengeTypes in that ConflictGroup
		/// </summary>
		private static readonly Dictionary<EChallengeConflictGroup, List<Type>> conflictGroupDict = new Dictionary<EChallengeConflictGroup, List<Type>>();

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

		//	public static void InitializeChallenges()
		//	{
		//		CustomMutator ArcologyEcology = RogueLibs.CreateCustomMutator(cChallenge.ArcologyEcology, true,
		//			new CustomNameInfo("Exteriors: Arcology Ecology"),
		//			new CustomNameInfo(
		//				"Sustainable Eco-homes! Trees! Less pollution! What's not to love?\n\n(Answer: It's still miserable.)\n\n- Public floors are grass\n- Adds nature features to public areas"));
		//		ArcologyEcology.Available = false;
		//		ArcologyEcology.Conflicting.AddRange(cChallenge.AffectsFloors);
		//		ArcologyEcology.IsActive = false;


		//		CustomMutator BadNeighborhoods = RogueLibs.CreateCustomMutator(cChallenge.BadNeighborhoods, true,
		//			new CustomNameInfo("Features: Bad Neighborhoods"),
		//			new CustomNameInfo("This place sure went to shit, didn't it?\n\n- Small chance for any given window to start out broken"));
		//		BadNeighborhoods.Available = false;
		//		BadNeighborhoods.Conflicting.AddRange(new string[] { cChallenge.PoliceState, cChallenge.MACITS });
		//		BadNeighborhoods.IsActive = false;

		//		CustomMutator BroughtBackFountain = RogueLibs.CreateCustomMutator(cChallenge.BroughtBackFountain, true,
		//			new CustomNameInfo("Features: Broughtback Fountain"),
		//			new CustomNameInfo(
		//				"\"He could smell Jack - the intensely familiar odor of cigarettes, musky sweat, and a faint sweetness like grass, and with it the rushing cold of the fountain.\"\n\n- Adds Fountains\n- Purely aesthetic for now"));
		//		BroughtBackFountain.Available = false;
		//		BroughtBackFountain.Conflicting.AddRange(new string[] { });
		//		BroughtBackFountain.IsActive = false;

		//		CustomMutator CartOfTheDeal = RogueLibs.CreateCustomMutator(cChallenge.CartOfTheDeal, true,
		//			new CustomNameInfo("Features: Cart of the Deal"),
		//			new CustomNameInfo(
		//				"A lot of people, very important people, are saying the City has the best Vendor Carts. The best folks, just tremendous. Don't we love our Vendor Carts?"));
		//		CartOfTheDeal.Available = false;
		//		CartOfTheDeal.Conflicting.AddRange(new string[] { });
		//		CartOfTheDeal.IsActive = false;

		//		CustomMutator LakeItOrLeaveIt = RogueLibs.CreateCustomMutator(cChallenge.LakeItOrLeaveIt, true,
		//			new CustomNameInfo("Features: Lake it or Leave it"),
		//			new CustomNameInfo("Don't like large inland bodies of water? Too fuckin' bad, buddy!"));
		//		LakeItOrLeaveIt.Available = false;
		//		LakeItOrLeaveIt.Conflicting.AddRange(new string[] { });
		//		LakeItOrLeaveIt.IsActive = false;


		//		CustomMutator PowerWhelming = RogueLibs.CreateCustomMutator(cChallenge.PowerWhelming, true,
		//			new CustomNameInfo("Features: Power Whelming"),
		//			new CustomNameInfo("You're not gonna be *over*whelmed, but you will see Power Boxes in every district. And that's something, I guess."));
		//		PowerWhelming.Available = false;
		//		PowerWhelming.Conflicting.AddRange(new string[] { });
		//		PowerWhelming.IsActive = false;


		//		CustomMutator SkywayDistrict = RogueLibs.CreateCustomMutator(cChallenge.SkywayDistrict, true,
		//			new CustomNameInfo("Skyway District"),
		//			new CustomNameInfo(
		//				"The Canal water Downtown was sold off for a pretty penny, so now there are just deep, empty holes where it used to be. It's a hazard, but the profit was massive!"));
		//		SkywayDistrict.Available = false;
		//		SkywayDistrict.Conflicting.AddRange(new string[] { });
		//		SkywayDistrict.IsActive = false;

		//		CustomMutator SurveillanceSociety = RogueLibs.CreateCustomMutator(cChallenge.SurveillanceSociety, true,
		//			new CustomNameInfo("Features: Surveillance Society"),
		//			new CustomNameInfo(
		//				"Those cameras? For your safety.\n\nOh, the turrets? For their safety.\n\nThe invasion of privacy and midnight raids? What's your name, citizen?\n\n- Spawns Security Cameras & Turrets in public, aligned with The Law"));
		//		SurveillanceSociety.Available = false;
		//		SurveillanceSociety.Conflicting.AddRange(new string[] { });
		//		SurveillanceSociety.IsActive = false;


		//		CustomMutator ThePollutionSolution = RogueLibs.CreateCustomMutator(cChallenge.ThePollutionSolution, true,
		//			new CustomNameInfo("Features: The Pollution Solution"),
		//			new CustomNameInfo(
		//				"We've finally solved pollution! Make more, dump it everywhere, and then ignore it. Done.\n\n- Adds pollution features to levels\n- Lakes have 80% chance of being poisoned"));
		//		ThePollutionSolution.Available = false;
		//		ThePollutionSolution.Conflicting.AddRange(new string[] { });
		//		ThePollutionSolution.IsActive = false;


		//		#region Map Size

		//		CustomMutator ACityForAnts = RogueLibs.CreateCustomMutator(cChallenge.ACityForAnts, true,
		//			new CustomNameInfo("MapSize: A City for Ants?!"),
		//			new CustomNameInfo("Yes, that is indeed what it is, figuratively speaking.\n\n- Map size set to 12.5%"));
		//		ACityForAnts.Available = false;
		//		ACityForAnts.Conflicting.AddRange(cChallenge.MapSize);
		//		ACityForAnts.IsActive = false;

		//		CustomMutator Claustropolis = RogueLibs.CreateCustomMutator(cChallenge.Claustropolis, true,
		//			new CustomNameInfo("MapSize: Claustrophobia"),
		//			new CustomNameInfo("Damn, this city is cramped! Who's Claus, anyway?\n\n- Map size set to 37.5%"));
		//		Claustropolis.Available = false;
		//		Claustropolis.Conflicting.AddRange(cChallenge.MapSize);
		//		Claustropolis.IsActive = false;

		//		CustomMutator Megalopolis = RogueLibs.CreateCustomMutator(cChallenge.Megalopolis, true,
		//			new CustomNameInfo("MapSize: Megalopolis"),
		//			new CustomNameInfo(
		//				"This town has so gotten big. You remember when it was just a small Mega-Arcology. Now it's a Mega-Mega-Arcology.\n\n- Map size set to 150%"));
		//		Megalopolis.Available = false;
		//		Megalopolis.Conflicting.AddRange(cChallenge.MapSize);
		//		Megalopolis.IsActive = false;

		//		CustomMutator Ultrapolis = RogueLibs.CreateCustomMutator(cChallenge.Ultrapolis, true,
		//			new CustomNameInfo("MapSize: Ultrapolis"),
		//			new CustomNameInfo("You get vertigo when you look up. This city is MASSIVE.\n\n- Map size set to 200%"));
		//		Ultrapolis.Available = false;
		//		Ultrapolis.Conflicting.AddRange(cChallenge.MapSize);
		//		Ultrapolis.IsActive = false;

		//		#endregion
		//	}


		public static string GetFloorTile()
		{
			string curMutator = "";

			foreach (string mutator in cChallenge.FloorMutators)
				if (GC.challenges.Contains(mutator))
					curMutator = mutator;

			switch (curMutator)
			{
				case cChallenge.ArcologyEcology:
					return vFloor.Grass;
				default:
					return null;
			}
		}

		public static string GetFloorTileGroup()
		{
			string curMutator = "";

			foreach (string mutator in cChallenge.FloorMutators)
				if (GC.challenges.Contains(mutator))
					curMutator = mutator;

			switch (curMutator)
			{
				case cChallenge.ArcologyEcology:
					return vFloorTileGroup.Park;
				default:
					return vFloorTileGroup.Building;
			}
		}

		public static string GetWallMutator()
		{
			foreach (string mutator in GC.challenges)
				if (cChallenge.WallMutators.Contains(mutator))
					return mutator;

			return null;
		}
	}
}