﻿using BepInEx.Logging;
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
                        "The Underdank is a vast subterranean network of caverns & tunnels, stretching between entire continents. It also smells like SHIT, because of our sewer system. The upside is that you might get to meet Pizzt Do'Turden, the Dank Smellf of legend!\n\n" +
                        "- Manholes spawn in all Districts\n" + 
                        "- Thieves and Cannibals hiding in Manholes no longer target you\n" + 
                        "- Activate or Fall in Open Manhole: Teleport to another Underdank entrypoint\n" + 
                        "- Diminutive: Toilets are part of the Underdank network",
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
                    CharacterCreationCost = 5,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }

        public static void Handle_StatusEffects_BecomeHidden(StatusEffects instance, ObjectReal hiddenInObject)
        {
            if (hiddenInObject.objectName == nameof(VanillaObjects.Manhole) &&
                TraitManager.IsPlayerTraitActive<UnderdankCitizen>() &&
                instance.agent.isPlayer == 0)
                    instance.BecomeNotHidden();
        }
    }
}