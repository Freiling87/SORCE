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

		public override string ConstructedFloorType =>
			VFloor.SmallTiles; // Parallax assistance
		public override string NaturalFloorType =>
			null;
		public override string RaisedFloorType =>
			VFloor.CleanTilesRaised;
		public override string RugFloorType =>
			VFloor.BathroomTile; // So you can't hide things under it
		public override string UnraisedTileTilesType =>
			VFloor.CleanTiles;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(Panoptikopolis);

			RogueLibs.CreateCustomUnlock(new MutatorUnlock(name, true)
			{
				Cancellations = BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Panoptikopolis"))
				.WithDescription(new CustomNameInfo(
					"Is the new all-glass building trend just a PsyOp to maximize surveillance? Or is it shiiiny? You decide!\n\n" +
					"- Most buildings spawn with Glass walls\n" +
					"- People can see you pooping, but you can see them pooping too, sometimes at the same time. So it's best to avoid eye contact.\n" +
					"  Unless you're into that sort of thing." ));
		}
	}
}