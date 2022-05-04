using BepInEx.Logging;
using SORCE.Challenges.C_Audio;
using SORCE.Challenges.C_Overhaul;
using SORCE.Challenges.C_Population;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SORCE.Localization.NameLists;

namespace SORCE.Utilities
{
    internal class AmbientAudio
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static string SetChunkAmbientAudio(string trackname, string chunkType)
		{
			if (!GC.challenges.Contains(nameof(AmbienterAmbience)))
				return trackname;

			if (chunkType == VChunkType.Casino)
				trackname = VAmbience.Casino;
			else if (
				chunkType != VChunkType.Bathhouse &&
				chunkType != VChunkType.Casino &&
				chunkType != VChunkType.Cave &&
				chunkType != VChunkType.CityPark &&
				chunkType != VChunkType.Graveyard)
			{
				if (GC.challenges.Contains(nameof(Arcology)))
					trackname = VAmbience.Park;

				if (GC.challenges.Contains(nameof(DiscoCityDanceoff)))
					trackname = VAmbience.ClubMusic; // ClubMusic, ClubMusic_Long, ClubMusic_Huge

				if (GC.challenges.Contains(nameof(DUMP)))
					trackname = VAmbience.Cave;

				if (GC.challenges.Contains(nameof(Eisburg)) ||
					GC.challenges.Contains(nameof(GhostTown)) ||
					GC.challenges.Contains(nameof(Hell)))
					trackname = VAmbience.Graveyard;
			}

			return trackname;
		}
	}
}
