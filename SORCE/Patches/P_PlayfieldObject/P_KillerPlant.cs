using BepInEx.Logging;
using HarmonyLib;
using SORCE.Logging;
using SORCE.Utilities;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches.P_PlayfieldObject
{
    [HarmonyPatch(declaringType: typeof(KillerPlant))]
	public static class P_KillerPlant
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// FloralerFlora leaf-throwing
		/// </summary>
		/// <param name="damagerObject"></param>
		/// <param name="damageAmount"></param>
		/// <param name="__instance"></param>
		/// <returns></returns>
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(KillerPlant.DamagedObject), argumentTypes: new[] { typeof(PlayfieldObject), typeof(float) })]
		public static bool DamagedObject_Prefix(PlayfieldObject damagerObject, float damageAmount, KillerPlant __instance)
		{
			if (!Wreckage.HasLeaves)
				return true;

			int piles = Random.Range(3, 6);
			int j = 1;

			for (int i = 0; i < piles; i++)
			{
				InvItem invItem = new InvItem();
				invItem.invItemName = "Wreckage";
				invItem.SetupDetails(false);
				invItem.LoadItemSprite(VObject.Bush + "Wreckage" + j++);
				GC.spawnerMain.SpawnWreckage(__instance.tr.position, invItem, __instance, damagerObject, false);

				if (j == 6)
					j = 1;
			}

			return true;
		}
	}
}
