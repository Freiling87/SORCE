using YamlDotNet.Serialization;

namespace SORCE.Localization
{
	public class LocalizationManager
	{
		public static LocalizationManager Instance => _instance ?? (_instance = new LocalizationManager());
		private static LocalizationManager _instance;

		private L_Challenge L_Challenge { get; }

		private LocalizationManager()
		{
			IDeserializer deserializer = new DeserializerBuilder().Build();
			L_Challenge = deserializer.Deserialize<L_Challenge>("BepInEx/config/SORCE/SORCE_Challenges.yaml");
		}
	}
}