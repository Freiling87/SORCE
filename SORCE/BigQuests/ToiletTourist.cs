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
                .WithDescription(new CustomNameInfo("Your secret pleasure is to explore and try out bathrooms across town. "+
                "You've eaten your protein, and you're fibered up. A round is in the chamber, locked and loaded!"));
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}