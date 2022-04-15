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
	public class Claustropolis : MapSizeChallenge
	{
		private Claustropolis() : base(nameof(Claustropolis)) { }

		public override int ChunkCount => 12;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Claustropolis()
			{
				Cancellations = CChallenge.MapSize.Where(i => i != nameof(Claustropolis)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Claustropolis"))
				.WithDescription(new CustomNameInfo(
					"Damn, this city is cramped! Who's this Claus guy, anyway?"));
		}
	}
}