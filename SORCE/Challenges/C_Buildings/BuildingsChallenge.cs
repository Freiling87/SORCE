using RogueLibsCore;
using SORCE.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Buildings
{
    public abstract class BuildingsChallenge : MutatorUnlock
    {
        public BuildingsChallenge(string name) : base(name, true) { }

        public abstract string WallFence { get; }
        public abstract string WallStructural{ get; }
        public abstract bool WallsFlammable { get; }

        public abstract string FloorConstructed { get; }
        public abstract string FloorNatural { get; }
        public abstract string FloorRaised { get; }
        public abstract string FloorRug { get; }
        public abstract string FloorUnraisedTile { get; }
    }
}
