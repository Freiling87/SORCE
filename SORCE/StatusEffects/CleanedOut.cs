using RogueLibsCore;

namespace SORCE.StatusEffects
{
    [EffectParameters]
    class CleanedOut : CustomEffect
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomEffect<CleanedOut>()
                .WithName(new CustomNameInfo("Cleaned Out"))
                .WithDescription(new CustomNameInfo("Your bowels require more food and drink to make shits!"));
        }

        public override int GetEffectHate() => 0;

        public override int GetEffectTime() => 12; // Food counter

        public override void OnAdded()
        {
        }

        public override void OnRemoved()
        {
        }

        public override void OnUpdated(EffectUpdatedArgs e)
        {
        }
    }
}
