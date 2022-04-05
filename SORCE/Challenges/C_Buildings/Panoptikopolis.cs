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
	public class Panoptikopolis : BuildingsChallenge
	{
		public Panoptikopolis(string name) : base(name) { }

		public override string WallFence =>
			VWall.Bars;
		public override string WallStructural =>
			VWall.Glass;
		public override bool WallsFlammable =>
			false;

		public override string FloorConstructed =>
			VFloor.SmallTiles; // Parallax assistance
		public override string FloorNatural =>
			null;
		public override string FloorRaised =>
			VFloor.CleanTilesRaised;
		public override string FloorRug =>
			VFloor.BathroomTile; // So you can't hide things under it
		public override string FloorUnraisedTile =>
			VFloor.CleanTiles;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(Panoptikopolis);

			RogueLibs.CreateCustomUnlock(new Panoptikopolis(name)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Panoptikopolis"))
				.WithDescription(new CustomNameInfo(
					"Is the new all-glass building trend just a PsyOp to maximize surveillance? Or is it shiiiny? You decide!"));
		}
	}
}