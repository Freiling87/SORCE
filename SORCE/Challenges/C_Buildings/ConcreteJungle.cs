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
	public class ConcreteJungle : BuildingsChallenge
	{
		public ConcreteJungle(string name) : base(name) { }

		public override string WallFence =>
			null;
		public override string WallStructural =>
			VWall.Border;
		public override bool WallsFlammable =>
			false;

		public override string FloorConstructed =>
			VFloor.Facility;
		public override string FloorNatural =>
			null;
        public override string FloorRaised =>
			VFloor.DirtyTiles;
		public override string FloorRug =>
			null;
		public override string FloorUnraisedTile =>
			VFloor.Facility;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(ConcreteJungle);

			RogueLibs.CreateCustomUnlock(new ConcreteJungle(name)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Concrete Jungle"))
				.WithDescription(new CustomNameInfo(
					"Don't focus so much on the negatives. The best things about concrete are that it's grey. Those are all of the upsides."));
		}
	}
}