using RogueLibsCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using SORCE;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using SORCE.Challenges;
using SORCE.Localization;
using System.Linq;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Buildings
{
	public class SpelunkyDory : BuildingsChallenge
	{
		public SpelunkyDory(string name) : base(name) { }

		public override string ConstructedFloorType =>
			vFloor.CaveFloor;
		public override string NaturalFloorType =>
			vFloor.CaveFloor;
		public override string RaisedFloorType =>
			vFloor.Grass;
		public override string RugFloorType =>
			vFloor.Grass;
		public override string UnraisedTileTilesType =>
			vFloor.DirtFloor;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(SpelunkyDory);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Spelunky Dory"))
				.WithDescription(new CustomNameInfo(
					"You and your fellow citizens live in a disgusting cave complex. As the mayor says, \"Don't be a CAN'Tibal, be a CANnibal!\" Man, fuck the Mayor.\n\n" +
					"- Most buildings spawn with Cave walls"));
		}
	}
}