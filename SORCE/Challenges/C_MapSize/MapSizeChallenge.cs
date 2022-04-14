using RogueLibsCore;
using SORCE.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_MapSize
{
    public abstract class MapSizeChallenge : MutatorUnlock
    {
        public MapSizeChallenge(string name) : base(name, true) { }

        public abstract int ChunkCount { get; }
    }
}
