using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Traits
{
    public class UnderdankVIP : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<UnderdankVIP>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = 
                        "You're pretty big shit down there. The mayor might be #1, but you're #2! Your immune system is stronger for your exposure to poopoo, and nothing disgusts you.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = "Underdank VIP",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { VanillaTraits.TheLaw },
                    CharacterCreationCost = 8,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
