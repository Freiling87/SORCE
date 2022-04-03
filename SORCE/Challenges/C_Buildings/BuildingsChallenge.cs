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
        public BuildingsChallenge(string name) : base(name) { }

        public abstract string ConstructedFloorType { get; }
        public abstract string NaturalFloorType { get; }
        public abstract string RaisedFloorType { get; }
        public abstract string RugFloorType { get; }
        public abstract string UnraisedTileTilesType { get; }
    }
}
