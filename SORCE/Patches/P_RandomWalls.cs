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

			if (LevelGenTools.IsInteriorsModActive())
				wallType = LevelGenTools.InteriorWallType();

			if (wallType == null)
				return true;
			else
			{
				RandomSelection component = GameObject.Find("ScriptObject").GetComponent<RandomSelection>();
				RandomList rList;

				rList = component.CreateRandomList(vWallGroup.Normal, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);

				rList = component.CreateRandomList(vWallGroup.Weak, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);

				rList = component.CreateRandomList(vWallGroup.Strong, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);

				rList = component.CreateRandomList(vWallGroup.Hideout, "Walls", "Wall");
				component.CreateRandomElement(rList, wallType, 3);
			}

			return false;
		}
	}
}
