using BepInEx.Logging;
using HarmonyLib;
using SORCE.Localization;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch (typeof(Item))]
    public static class P_Item
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        //[HarmonyPrefix, HarmonyPatch(methodName: "UpdateItem", argumentTypes: new Type[0] { })]
        public static void UpdateItem_Postfix(Item __instance)
        {
            if (__instance.itemName == CSprite.Casing || __instance.itemName == CSprite.ShotgunShell)
                GC.audioHandler.Play(__instance, VAudioClip.ItemHitItem);
        }

        [HarmonyPrefix, HarmonyPatch(methodName: "OnCollisionEnter2D", argumentTypes: new[] { typeof(Collision2D) })]
        public static void OnCollisionEnter2D_Postfix(Collision2D other, Item __instance)
        {
            if (__instance.itemName == CSprite.Casing)
                GC.audioHandler.Play(__instance, VAudioClip.BulletHitWall);
            else if (__instance.itemName == CSprite.ShotgunShell)
                GC.audioHandler.Play(__instance, VAudioClip.BulletHitWall);
        }
    }
}
