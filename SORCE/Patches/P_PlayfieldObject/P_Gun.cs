using BepInEx.Logging;
using HarmonyLib;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Utilities;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(Gun))]
    public static class P_Gun
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public static bool ShootierGuns = Core.debugMode;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Gun.Shoot), argumentTypes: new[] { typeof(bool), typeof(bool), typeof(bool), typeof(int), typeof(string) })]
        public static bool Shoot_Prefix(bool specialAbility, bool silenced, bool rubber, int bulletNetID, string bulletStatusEffect, Gun __instance)
        {
            if (P_Bullet.GunplayRelit)
            {
                string invItem = specialAbility
                    ? __instance.agent.inventory.equippedSpecialAbility.invItemName
                    : __instance.agent.inventory.equippedWeapon.invItemName;

                switch (invItem)
                {
                    case VItem.MachineGun:
                        Audiovisual.MuzzleFlash(__instance.tr.position);
                        break;
                    case VItem.Pistol:
                        Audiovisual.MuzzleFlash(__instance.tr.position);
                        break;
                    case VItem.Revolver:
                        Audiovisual.MuzzleFlash(__instance.tr.position);
                        break;
                    case VItem.Shotgun:
                        Audiovisual.MuzzleFlash(__instance.tr.position);
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Bullet casing spawners
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
            if (!ShootierGuns)
                return;

            string invItem = specialAbility
                ? __instance.agent.inventory.equippedSpecialAbility.invItemName
                : __instance.agent.inventory.equippedWeapon.invItemName;

            switch (invItem)
            {
                case VItem.MachineGun:
                    Audiovisual.SpawnBulletCasing(__instance.tr.position, CSprite.Casing);
                    break;
                case VItem.Pistol:
                    Audiovisual.SpawnBulletCasing(__instance.tr.position, CSprite.Casing);
                    break;
                case VItem.Revolver:
                    break;
                case VItem.Shotgun:
                    Audiovisual.SpawnBulletCasing(__instance.tr.position, CSprite.ShotgunShell);
                    break;
            }
        }

        public static GameObject towardObject = new GameObject();
    }
}
