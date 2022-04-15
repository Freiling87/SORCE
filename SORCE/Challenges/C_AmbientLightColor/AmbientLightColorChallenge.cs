using RogueLibsCore;
using UnityEngine;

namespace SORCE.Challenges.C_AmbientLightColor
{
    public abstract class AmbientLightColorChallenge : MutatorUnlock
    {
        public AmbientLightColorChallenge(string name) : base(name, true) { }

        public abstract Color32 FilterColor { get; }
    }
}
