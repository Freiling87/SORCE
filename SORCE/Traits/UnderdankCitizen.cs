using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Traits
{
    public class UnderdankCitizen : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<UnderdankCitizen>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = 
                        "The Underdank is a vast subterranean network of caverns & tunnels, stretching between entire continents." +
                        "It also smells like SHIT, because of our sewer system. The upside is that you might get to meet Pizzt Do'Turden, the Dank Smellf of legend!",
                    [LanguageCode.Russian] = "Вы с лёгкостью оринтируетесь в канализации. Жители этих мест не считают вас лёгкой целью",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = "Underdank Citizen",
                    [LanguageCode.Russian] = "Подземный житель",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { VanillaTraits.TheLaw },
                    CharacterCreationCost = 4,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 0,
                    Unlock = { upgrade = nameof(UnderdankVIP) },
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
