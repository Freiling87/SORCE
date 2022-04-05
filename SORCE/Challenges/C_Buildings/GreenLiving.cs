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

		public override string WallFence =>
			null;
		public override string WallStructural =>
			VWall.Hedge;
		public override bool WallsFlammable =>
			true;

		public override string FloorConstructed =>
			VFloor.DirtFloor;
		public override string FloorNatural =>
			VFloor.Grass;
		public override string FloorRaised =>
			VFloor.CaveFloor;
		public override string FloorRug =>
			VFloor.Grass;
		public override string FloorUnraisedTile =>
			VFloor.CaveFloor;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(GreenLiving);

			RogueLibs.CreateCustomUnlock(new GreenLiving(name)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Green Living"))
				.WithDescription(new CustomNameInfo(
					"The Mayor has retrofitted most buildings to eco-friendly plant-based construction. The air is mighty fresh... except near the compost-burning stoves."));
		}
	}
}