﻿using BepInEx.Logging;
using SORCE.Challenges.C_Gangs;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Utilities.MapGen
{
    public class Chunks
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public static bool HasConfiscationCenters =>
            !GC.loadLevel.placedConfiscationCenter &&
            GC.levelTheme == 4 ||
            GC.challenges.Contains(nameof(ProtectAndServo));

        public static bool HasDeportationCenters =>
            !GC.loadLevel.placedDeportationCenter &&
            GC.levelTheme == 4 ||
            GC.challenges.Contains(nameof(ProtectAndServo));

    }
}
