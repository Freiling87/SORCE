using RogueLibsCore;

namespace SORCE.BigQuests
{
    public class ToiletTourist : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomUnlock(new BigQuestUnlock(nameof(ToiletTourist))
            { })
                .WithName(new CustomNameInfo("Toilet Tourist"))
                .WithDescription(new CustomNameInfo(
                    "You're a power-user on the WypeOut app, the premier social network for those who take dumps! " +
                    "You're undertaking an unprecedented tour of the City's septic amenities to become an influencer. " + 
                    "But the life of an influencer isn't all fun and games. You're gonna have to eat some serious protein."));
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}