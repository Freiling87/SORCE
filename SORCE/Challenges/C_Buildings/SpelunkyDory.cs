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

		public override string WallFence =>
			null;
		public override string WallStructural =>
			VWall.Cave;
		public override bool WallsFlammable =>
			false;

		public override string FloorConstructed =>
			VFloor.CaveFloor;
		public override string FloorNatural =>
			VFloor.CaveFloor;
		public override string FloorRaised =>
			VFloor.Grass;
		public override string FloorRug =>
			VFloor.Grass;
		public override string FloorUnraisedTile =>
			VFloor.DirtFloor;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(SpelunkyDory);

			RogueLibs.CreateCustomUnlock(new SpelunkyDory(name)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Spelunky Dory"))
				.WithDescription(new CustomNameInfo(
					"You and your fellow citizens live in a disgusting cave complex. As the mayor says, \"Don't be a CAN'Tibal, be a CANnibal!\" Man, fuck the Mayor."));
		}
	}
}