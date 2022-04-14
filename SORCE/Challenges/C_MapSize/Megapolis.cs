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

namespace SORCE.Challenges.C_MapSize
{
	public class Megapolis : MapSizeChallenge
	{
		public Megapolis(string name) : base(name) { }

		public override int ChunkCount => 48;

        [RLSetup]
		static void Start()
		{
			const string name = nameof(Megapolis);

			RogueLibs.CreateCustomUnlock(new Megapolis(name)
			{
				Cancellations = CChallenge.MapSize.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Megapolis"))
				.WithDescription(new CustomNameInfo(
					"This town has so gotten big. You remember when it was just a local Mega-Arcology. Now it's a Mega-Mega-Arcology."));
		}
	}
}