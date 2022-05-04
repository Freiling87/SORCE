using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using SORCE.Challenges.C_Buildings;
using SORCE.Logging;
using SORCE.Utilities.MapGen;
using System.Linq;
using UnityEngine;
using static SORCE.Localization.NameLists;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(RandomWalls))]
	static class P_RandomWalls
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(RandomWalls.fillWalls))]
		public static bool FillWalls_Prefix()
		{
			string wallType = null;

			if (RogueFramework.Unlocks.OfType<BuildingsChallenge>().Any(i => i.IsEnabled))
			{
				if (VWall.Fence.Contains(wallType))
					wallType = Structures.FenceWallType(wallType);
				else if (VWall.Structural.Contains(wallType))
					wallType = Structures.BuildingWallType(wallType);
			}

			if (wallType == null)
				return true;
			else
			{
				RandomSelection component = GameObject.Find("ScriptObject").GetComponent<RandomSelection>();
				RandomList rList;

				rList = component.CreateRandomList(VWallGroup.Normal, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);

				rList = component.CreateRandomList(VWallGroup.Weak, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);

				rList = component.CreateRandomList(VWallGroup.Strong, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);

				rList = component.CreateRandomList(VWallGroup.Hideout, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);
			}

			return false;
		}
	}
}
