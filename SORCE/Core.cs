using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE
{
	[BepInPlugin(ModInfo.BepInExPluginId, ModInfo.Title, ModInfo.Version)]
	[BepInProcess("StreetsOfRogue.exe")]
	[BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
	public class Core : BaseUnityPlugin
    {
		public const bool debugMode = true;

		public static ManualLogSource ConsoleMessage;
		public static BaseUnityPlugin MainInstance;

		public void Awake()
		{
			MainInstance = this;
			ConsoleMessage = Logger;

			new Harmony(ModInfo.BepInExPluginId).PatchAll(GetType().Assembly);

			RogueLibs.LoadFromAssembly();
		}

		public static void Log(string logMessage) =>
				ConsoleMessage.LogMessage(logMessage);
	}
}
