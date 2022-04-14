using RogueLibsCore;

namespace SORCE.Challenges.C_AmbientLightLevel
{
    public abstract class AmbientLightLevelChallenge : MutatorUnlock
    {
        public AmbientLightLevelChallenge(string name) : base(name, true) { }

        public abstract int LightLevel { get; }
    }
}