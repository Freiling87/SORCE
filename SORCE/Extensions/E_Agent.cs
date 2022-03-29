using Google2u;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SORCE.Localization.NameLists;

namespace SORCE.Extensions
{
	public static class E_Agent
	{
		public static bool IsEnforcer(this Agent agent) =>
			agent.enforcer ||
			agent.HasTrait(nameof(StatusEffectNameDB.rowIds.TheLaw)) ||
			vAgent.LawEnforcement.Contains(agent.agentName);

		public static bool IsCriminal(this Agent agent) =>
			agent.objectMultAgent.mustBeGuilty || 
			vAgent.Criminal.Contains(agent.agentName);
	}
}
