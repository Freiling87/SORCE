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
		public Claustropolis(string name) : base(name) { }

		public override int ChunkCount => 12;

        [RLSetup]
		static void Start()
		{
			const string name = nameof(Claustropolis);

			RogueLibs.CreateCustomUnlock(new Claustropolis(name)
			{
				Cancellations = CChallenge.MapSize.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Claustropolis"))
				.WithDescription(new CustomNameInfo(
					"Damn, this city is cramped! Who's this Claus guy, anyway?"));
		}
	}
}