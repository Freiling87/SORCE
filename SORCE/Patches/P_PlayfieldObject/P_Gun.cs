using BepInEx.Logging;
using HarmonyLib;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Utilities;
using System;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Gun))]
    public static class P_Gun
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;


		/// <summary>
		/// ShootierGuns Muzzle Flash
		/// </summary>
		/// <param name="specialAbility"></param>
		/// <param name="silenced"></param>
		/// <param name="rubber"></param>
		/// <param name="bulletNetID"></param>
		/// <param name="bulletStatusEffect"></param>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(Gun.Shoot), argumentTypes: new[] { typeof(bool), typeof(bool), typeof(bool), typeof(int), typeof(string) })]
        public static bool Shoot_Prefix(bool specialAbility, bool silenced, bool rubber, int bulletNetID, string bulletStatusEffect, Gun __instance)
        {
            if (!Gunplay.ModGunLighting)
                return true;

            string invItem = specialAbility
                ? __instance.agent.inventory.equippedSpecialAbility.invItemName
                : __instance.agent.inventory.equippedWeapon.invItemName;

            if (Gunplay.MuzzleFlashShort.Contains(invItem))
                Gunplay.MuzzleFlash(__instance.tr.position, false);
            else if (Gunplay.MuzzleFlashLong.Contains(invItem))
                Gunplay.MuzzleFlash(__instance.tr.position, true);

            return true;
        }

        /// <summary>
        /// ShootierGuns Casings
        /// </summary>
        /// <param name="specialAbility"></param>
        /// <param name="silenced"></param>
        /// <param name="rubber"></param>
        /// <param name="bulletNetID"></param>
        /// <param name="bulletStatusEffect"></param>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Gun.Shoot), argumentTypes: new[] { typeof(bool), typeof(bool), typeof(bool), typeof(int), typeof(string) })]
        public static void Shoot_Postfix(bool specialAbility, bool silenced, bool rubber, int bulletNetID, string bulletStatusEffect, Gun __instance)
        {
            if (!Gunplay.ModGunParticles)
                return;

            string invItem = specialAbility
                ? __instance.agent.inventory.equippedSpecialAbility.invItemName
                : __instance.agent.inventory.equippedWeapon.invItemName;

            switch (invItem)
            {
                case VItem.MachineGun:
                    Gunplay.SpawnBulletCasing(__instance.tr.position, CSprite.Casing);
                    break;
                case VItem.Pistol:
                    Gunplay.SpawnBulletCasing(__instance.tr.position, CSprite.Casing);
                    break;
                case VItem.Revolver:
                    break;
                case VItem.Shotgun:
                    Gunplay.SpawnBulletCasing(__instance.tr.position, CSprite.ShotgunShell);
                    break;
                default: break;
            }
        }
    }
}
