using BepInEx.Logging;
using Google2u;
using RogueLibsCore;
using SORCE.Logging;
using static SORCE.Localization.NameLists;

namespace SORCE.Extensions
{
    public static class E_Agent
	{
		private static readonly ManualLogSource logger = SORCELogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool IsEnforcer(this Agent agent) =>
			agent.enforcer ||
			agent.HasTrait(nameof(StatusEffectNameDB.rowIds.TheLaw)) ||
			VAgent.LawEnforcement.Contains(agent.agentName);

		public static bool IsCriminal(this Agent agent) =>
			agent.objectMultAgent.mustBeGuilty || 
			VAgent.Criminal.Contains(agent.agentName);

		public static bool IsFlushableToilet(Agent agent) =>
				!agent.statusEffects.hasStatusEffect(VStatusEffect.Giant) &&
				!agent.HasTrait(VTrait.Bulky) &&
				agent.statusEffects.hasTrait(VTrait.Diminutive) || agent.diminutive || // Latter untested
				agent.statusEffects.hasStatusEffect(VStatusEffect.Shrunk) || agent.shrunk; // Latter untested

		public static bool IsFlushableManhole(Agent agent) =>
				!agent.statusEffects.hasStatusEffect(VStatusEffect.Giant) &&
				!agent.HasTrait(VTrait.Bulky);
	}
}
