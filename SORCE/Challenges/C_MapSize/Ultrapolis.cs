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
	public class Ultrapolis : MapSizeChallenge
	{
		private Ultrapolis() : base(nameof(Ultrapolis)) { }

		public override int ChunkCount => 64;

        [RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Ultrapolis()
			{
				Cancellations = CChallenge.MapSize.Where(i => i != nameof(Ultrapolis)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Ultrapolis"))
				.WithDescription(new CustomNameInfo(
					"Many citizens do not know there is a world outside the city, and even insist that nothing else exists.\n\n" +
					"Buncha morons."));
		}
	}
}