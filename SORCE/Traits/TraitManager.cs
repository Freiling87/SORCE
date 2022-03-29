using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SORCE.Traits
{
	class TraitManager
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool IsPlayerTraitActive<TraitType>() =>
			GC.agentList.Any(agent => agent.isPlayer != 0 && agent.HasTrait<TraitType>());

		public static bool IsPlayerTraitActive(string traitName) =>
			GC.agentList.Any(agent => agent.isPlayer != 0 && agent.statusEffects.hasTrait(traitName));
	}
}
 