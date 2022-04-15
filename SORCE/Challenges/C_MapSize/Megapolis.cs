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
		private Megapolis() : base(nameof(Megapolis)) { }

		public override int ChunkCount => 48;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Megapolis()
			{
				Cancellations = CChallenge.MapSize.Where(i => i != nameof(Megapolis)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Megapolis"))
				.WithDescription(new CustomNameInfo(
					"This town has so gotten big. You remember when it was just a local Mega-Arcology. Now it's a Mega-Mega-Arcology."));
		}
	}
}