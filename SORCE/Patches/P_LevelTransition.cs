using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_Gangs;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(LevelTransition))]
    public static class P_LevelTransition
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof (LevelTransition.ChangeLevel), argumentTypes: new Type[0])]
        public static void ChangeLevel_Postfix()
        {
            if (GC.levelTheme != 4 &&
                !GC.challenges.Contains(nameof(ProtectAndServo)))
            {
                GC.loadLevel.placedConfiscationCenter = true;
                GC.loadLevel.placedDeportationCenter = true;
            }
        }
    }
}
