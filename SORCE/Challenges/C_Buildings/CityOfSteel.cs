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

        public override string ConstructedFloorType =>
			VFloor.MetalFloor;
		public override string NaturalFloorType =>
			null;
        public override string RaisedFloorType =>
			VFloor.SolidPlates;
		public override string RugFloorType =>
			VFloor.MetalPlates;
		public override string UnraisedTileTilesType =>
			VFloor.SolidPlates;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(CityOfSteel);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - City of Steel"))
				.WithDescription(new CustomNameInfo(
					"A gleaming city of steel! The world of the future, today. Mankind's dream in... Wow, it *really* smells like steel cleaner. Like, it fucking stinks.\n\n" +
					"- Most buildings spawn with Steel walls"));
		}
	}
}