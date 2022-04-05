using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using RogueLibsCore;
using Random = UnityEngine.Random;
using System.Collections;
using System.Reflection;
using System;
using Light2D;
using SORCE.Challenges;
using SORCE.Logging;
using SORCE.Localization;
using static SORCE.Localization.NameLists;
using System.Linq;
using SORCE.Challenges.C_Buildings;

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
					wallType = LevelGenTools.FenceWallType(wallType);
				else if (VWall.Structural.Contains(wallType))
					wallType = LevelGenTools.BuildingWallType(wallType);
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
