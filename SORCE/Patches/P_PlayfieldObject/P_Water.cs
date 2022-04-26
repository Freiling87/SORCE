using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Water))]
    class P_Water
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Water.SpreadPoison), argumentTypes: new[] { typeof(int), typeof(int), typeof(bool), typeof(string) })]
        public static void SpreadPoisonStart_Postfix(int posX, int posY, bool firstSpread, string effectType, Water __instance)
        {
            //foreach (string str in __instance.effectContents)
            //    logger.LogDebug("Effect: " + str);

            //foreach (string str in __instance.syringeContents)
            //    logger.LogDebug("Syringe: " + str);

            if (effectType != "Clean" && GC.percentChance(1) && GC.percentChance(50)
                && GC.tileInfo.GetTileData(new Vector3(posX, posY, 0f)).water)
                GC.spawnerMain.SpawnParticleEffect(VParticleEffect.Smoke, new Vector3(posX * 0.64f, posY * 0.64f, __instance.tr.position.z), 0f);
        }
    }
}
