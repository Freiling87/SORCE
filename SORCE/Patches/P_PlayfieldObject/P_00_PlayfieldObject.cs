using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using System;
using System.Reflection;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(PlayfieldObject))]
    class P_00_PlayfieldObject
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public static MethodInfo PlayfieldObject_StopInteraction_base = AccessTools.DeclaredMethod(typeof(PlayfieldObject), nameof(PlayfieldObject.StopInteraction), new Type[0] { });
        public static MethodInfo PlayfieldObject_FinishedOperating_base = AccessTools.DeclaredMethod(typeof(PlayfieldObject), "FinishedOperating", new Type[0] { });
        public static MethodInfo PlayfieldObject_Interact_base = AccessTools.DeclaredMethod(typeof(PlayfieldObject), "Interact", new Type[1] { typeof(Agent) });
        public static MethodInfo PlayfieldObject_PressedButton_base = AccessTools.DeclaredMethod(typeof(PlayfieldObject), "PressedButton", new Type[2] { typeof(string), typeof(int) });
    }
}
