using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Gangs;
using SORCE.Properties;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SORCE
{
    [BepInPlugin(ModInfo.BepInExPluginId, ModInfo.Title, ModInfo.Version)]
	[BepInProcess("StreetsOfRogue.exe")]
	[BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
	public class Core : BaseUnityPlugin
	{
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

	public static class CoreTools
	{
		private static GameController GC => GameController.gameController;

		public static T GetMethodWithoutOverrides<T>(this MethodInfo method, object callFrom)
			where T : Delegate
		{
			IntPtr ptr = method.MethodHandle.GetFunctionPointer();
			return (T)Activator.CreateInstance(typeof(T), callFrom, ptr);
		}

		public static void SayDialogue(Agent agent, string customNameInfo, string vNameType)
		{
			string text = GC.nameDB.GetName(customNameInfo, vNameType);
			agent.Say(text);
		}

		public static void SayDialogue(ObjectReal objectReal, string customNameInfo, string vNameType)
		{
			string text = GC.nameDB.GetName(customNameInfo, vNameType);
			objectReal.Say(text);
		}
	}
	 
	public static class DebugTools
	{
		public const bool debugMode = true;

		public static List<string> debugChallenges = new List<string>()
		{
		};
	}
}
