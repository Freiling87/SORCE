using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Population
{
    public abstract class PopulationChallenge : MutatorUnlock
    {
        public PopulationChallenge(string name) : base(name, true) { }

        public abstract int PopulationMultiplier { get; }
    }
}