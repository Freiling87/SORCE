using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using System.Linq;
using SORCE.Localization;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Buildings
{
	public class GreenLiving : BuildingsChallenge
	{
		public GreenLiving(string name) : base(name) { }

		public override string ConstructedFloorType =>
			vFloor.DirtFloor;
		public override string NaturalFloorType =>
			vFloor.Grass;
		public override string RaisedFloorType =>
			vFloor.CaveFloor;
		public override string RugFloorType =>
			vFloor.Grass;
		public override string UnraisedTileTilesType =>
			vFloor.CaveFloor;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(GreenLiving);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Green Living"))
				.WithDescription(new CustomNameInfo(
					"The Mayor has retrofitted most buildings to eco-friendly plant-based construction. The air is mighty fresh... except near the compost-burning stoves.\n\n" +
					"- Most buildings spawn with Hedge walls"));
		}
	}
}