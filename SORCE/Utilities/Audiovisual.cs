using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SORCE.Utilities
{
    internal class Audiovisual
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public static void MuzzleFlash(Vector3 location)
        {
            GC.spawnerMain.SpawnLightTemp(location, null, "PowerSap");
        }

        public static void SpawnBulletCasing(Vector3 origin, string casingType, GameObject towardObject = null)
        {
            Vector3 vector = new Vector3(origin.x, origin.y, Random.Range(-0.78f, -1.82f));

            Item casing = GC.spawnerMain.wreckagePrefab.Spawn(vector);
            casing.itemName = casingType;
            casing.DoEnable();
            casing.isWreckage = true;
            //casing.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            casing.animator.enabled = true;
            casing.justSpilled = true;
            tk2dSprite component = casing.tr.GetChild(0).transform.GetChild(0).GetComponent<tk2dSprite>();
            component.SetSprite(casingType);
            component.transform.localPosition = Vector3.zero;
            Movement movement = casing.GetComponent<Movement>();
            //movement.SetPhysics("Ice"); Looks a little like rolling
            casing.animator.Play("ItemJump 1", -1, 0f);
            movement.Spill(120, towardObject, null); // Handles null towardObject
            casing.FakeStart();
        }
	}
}
