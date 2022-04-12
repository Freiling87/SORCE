using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;

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
            if (effectType != "Clean" && GC.percentChance(1))
                GC.spawnerMain.SpawnParticleEffect(VParticleEffect.SmokePuffs, new Vector3(posX * 0.64f, posY * 0.64f, __instance.tr.position.z), 0f);
        }
    }
}
