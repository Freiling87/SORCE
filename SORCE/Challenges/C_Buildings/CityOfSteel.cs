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
	public class CityOfSteel : BuildingsChallenge
	{
		public CityOfSteel(string name) : base(name) { }

		public override bool WallsFlammable => false;

		public override string FloorConstructed =>	VFloor.MetalFloor;
		public override string FloorNatural =>		null;
        public override string FloorRaised =>		VFloor.SolidPlates;
		public override string FloorRug =>			VFloor.MetalPlates;
		public override string FloorUnraisedTile =>	VFloor.SolidPlates;
		public override string WallFence =>			VWall.Bars;
		public override string WallStructural =>	VWall.Steel;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(CityOfSteel);

			RogueLibs.CreateCustomUnlock(new CityOfSteel(name)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - City of Steel"))
				.WithDescription(new CustomNameInfo(
					"A gleaming city of steel! The world of the future, today. Mankind's dream in... Wow, it *really* smells like steel cleaner. Like, it fucking stinks."));
		}
	}
}