using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.MapGenUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(Gun))]
    public static class P_Gun
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomSprite(CSprite.Casing, SpriteScope.Items, Properties.Resources.Casing);
            RogueLibs.CreateCustomSprite(CSprite.ShotgunShell, SpriteScope.Items, Properties.Resources.ShotgunShell);
        }

        //[HarmonyPostfix, HarmonyPatch(methodName: nameof(Gun.Shoot), argumentTypes: new[] { typeof(bool), typeof(bool), typeof(bool), typeof(int), typeof(string) })]
        public static void Shoot_Postfix(bool specialAbility, bool silenced, bool rubber, int bulletNetID, string bulletStatusEffect, Gun __instance)
        {
            string invItem;

            if (specialAbility)
                invItem = __instance.agent.inventory.equippedSpecialAbility.invItemName;
            else
                invItem = __instance.agent.inventory.equippedWeapon.invItemName;

            if (invItem == VItem.Shotgun ||
                invItem == VItem.Pistol ||
                invItem == VItem.MachineGun ||
                invItem == VItem.Revolver)
                SpawnBulletCasing(__instance, CSprite.Casing);
            else if (invItem == VItem.Shotgun)
                SpawnBulletCasing(__instance, CSprite.ShotgunShell);
        }

        private static void SpawnBulletCasing(Gun gun, string casingType)
        {
            Vector3 vector = new Vector3(gun.transform.position.x, gun.transform.position.y, Random.Range(-0.78f, -1.82f));

            Item casing = GC.spawnerMain.wreckagePrefab.Spawn(vector);
            casing.DoEnable();
            casing.isWreckage = true;
            //casing.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            casing.animator.enabled = true;
            casing.animator.Play("ItemJump 1", -1, 0f);
            casing.justSpilled = true;

            tk2dSprite component = casing.tr.GetChild(0).transform.GetChild(0).GetComponent<tk2dSprite>();
            component.SetSprite(component.GetSpriteIdByName(casingType));
            component.transform.localPosition = Vector3.zero;
            component.color = Color.yellow;

            casing.GetComponent<Movement>().Spill((float)Random.Range(90, 110), null, null);
            casing.FakeStart();
        }
    }
}
