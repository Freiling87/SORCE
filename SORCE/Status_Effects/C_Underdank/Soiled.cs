using RogueLibsCore;

namespace SORCE.Status_Effects
{
    [EffectParameters]
    class Soiled : CustomEffect
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomEffect<Soiled>()
                .WithName(new CustomNameInfo("Soiled"))
                .WithDescription(new CustomNameInfo("You're disgusting, go clean yourself up!"));
        }

        public override int GetEffectHate() => 5;

        public override int GetEffectTime() => 60;

        public override void OnAdded() { }

        public override void OnRemoved() { }

        public override void OnUpdated(EffectUpdatedArgs e)
        {
            e.UpdateDelay = 1f;
            CurrentTime--;
        }
    }
}
