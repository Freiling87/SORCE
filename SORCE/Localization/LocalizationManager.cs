using BepInEx.Logging;
using SORCE.Logging;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;

namespace SORCE.Localization
{
	public class LocalizationManager
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();

		public static LocalizationManager Instance => _instance ?? (_instance = new LocalizationManager());
		private static LocalizationManager _instance;

		public L_Challenge ChallengeLocalization { get; }

		private static ConfigType ReadFromFile<ConfigType>(IDeserializer deserializer, string path)
		{
			path = path.Replace('/', Path.DirectorySeparatorChar);
			if (!File.Exists(path))
			{
				logger.LogWarning($"Localization file not found at path: '{path}'");
				return default;
			}
			using (StreamReader reader = new StreamReader(path))
			{
				return deserializer.Deserialize<ConfigType>(reader);
			}
		}

		private LocalizationManager()
		{
			IDeserializer deserializer = new DeserializerBuilder().Build();

			string configBasePath = Application.dataPath + "/../BepInEx/config/";
			ChallengeLocalization = ReadFromFile<L_Challenge>(deserializer, configBasePath + "SORCE_Challenges.yaml");
		}
	}
}