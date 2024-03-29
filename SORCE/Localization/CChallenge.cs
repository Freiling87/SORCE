﻿using SORCE.Challenges.C_Buildings;
using SORCE.Challenges.C_MapSize;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Challenges.C_Gangs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SORCE.Localization.NameLists;

namespace SORCE.Localization
{
    class CChallenge
    {
		// TODO: Eliminate most of these lists 
		// https://makolyte.com/csharp-get-a-list-of-types-defined-in-an-assembly-without-loading-it/
		//public static List<string> AddsCriminals = new List<string>()
		//{
		//	nameof(UnionTown),
		//	nameof(BiggerGangs),
		//};
		public static List<string> AffectsLights = new List<string>()
		{
			nameof(DiscoCityDanceoff),
			nameof(GreenLiving),
		};
		// TODO: Eliminate almost all of these. I can't eliminate the Cancellation lists yet because 
		// The MutatorUnlock type adds its Cancellations during creation, meaning 
		// The cancelled types don't even exist yet.
		public static List<Type> BuildingsTypes = new List<Type>()
		{
			typeof(Brixton),
			typeof(CityOfSteel),
			typeof(ConcreteJungle),
			typeof(GreenLiving),
			typeof(Panoptikopolis),
			typeof(ShantyTown),
			typeof(SpelunkyDory)
		};
		public static List<string> BuildingsNames =
			BuildingsTypes.Select(c => c.Name).ToList(); // nameof(c) would be "c" in Linq
		public static List<string> MapSize = new List<string>()
		{
			nameof(Arthropolis),
			nameof(Claustropolis),
			nameof(Megapolis),
			nameof(Ultrapolis)
		};
		public static List<string> Overhauls = new List<string>()
		{
			VChallenge.MixedUpLevels,
			nameof(AnCapistan),
			nameof(MACITS),
			nameof(PoliceState)
		};
		public static List<string> Population = new List<string>()
		{
			nameof(GhostTown),
			nameof(HordeAlmighty),
			nameof(LetMeSeeThatThrong),
			nameof(SwarmWelcome),
		};
	}
}
