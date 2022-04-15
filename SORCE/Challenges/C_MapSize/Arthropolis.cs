using RogueLibsCore;
using SORCE.Localization;
using System.Linq;

namespace SORCE.Challenges.C_MapSize
{
    public class Arthropolis : MapSizeChallenge
	{
		private Arthropolis() : base(nameof(Arthropolis)) { }

		public override int ChunkCount => 4;

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Arthropolis()
			{
				Cancellations = CChallenge.MapSize.Where(i => i != nameof(Arthropolis)).ToList()
			})
				.WithName(new CustomNameInfo(
					"Map Size - Arthropolis"))
				.WithDescription(new CustomNameInfo(
					"\"The Streets of Rogue City Building For Slum Dwellers Who Can't Be Rich Good\"\n\n" +
					"    - Inscription over entrance to Slum District 69, Floor 420"));
		}
	}
}