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
	public class ShantyTown : BuildingsChallenge
	{
		public ShantyTown(string name) : base(name) { }

		public override string WallFence =>
			VWall.BarbedWire;
		public override string WallStructural =>
			VWall.Wood;
		public override bool WallsFlammable =>
			true;

		public override string FloorConstructed =>
			VFloor.DrugDenFloor;
		public override string FloorNatural =>
			null;
		public override string FloorRaised =>
			VFloor.DirtyTiles;
		public override string FloorRug =>
			null; 
		public override string FloorUnraisedTile =>
			VFloor.BrickIndoor;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(ShantyTown);

			RogueLibs.CreateCustomUnlock(new ShantyTown(name)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Shanty Town"))
				.WithDescription(new CustomNameInfo(
					"A tinderbox on cinder blocks. Hard mode for Firefighters, easy mode for arsonists. Fun mode for psychopaths."));
		}
	}
}