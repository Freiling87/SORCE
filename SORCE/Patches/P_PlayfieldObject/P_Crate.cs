using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using System;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Crate))]
    public static class P_Crate
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: "Start", argumentTypes: new Type[0] { })]
        public static bool Start_Prefix(Crate __instance)
        {
            if (__instance.supplyCrate == "")
                __instance.supplyCrate = "NoActuallyWeLikeCratesAndWantThemToStayButThanksForOffering";

            return true;
        }
    }
}
