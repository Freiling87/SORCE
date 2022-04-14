using RogueLibsCore;

namespace SORCE.Challenges.C_Buildings
{
    public abstract class BuildingsChallenge : MutatorUnlock
    {
        public BuildingsChallenge(string name) : base(name, true) { }

        public abstract string FloorConstructed { get; }
        public abstract string FloorNatural { get; }
        public abstract string FloorRaised { get; }
        public abstract string FloorRug { get; }
        public abstract string FloorUnraisedTile { get; }

        public abstract string WallFence { get; }
        public abstract string WallStructural{ get; }

        public abstract bool WallsFlammable { get; }
    }
}
