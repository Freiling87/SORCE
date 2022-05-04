using BepInEx.Logging;
using HarmonyLib;
using SORCE.Challenges.C_VFX;
using SORCE.Localization;
using SORCE.Logging;
using SORCE.Utilities;
using System.Collections.Generic;
using static SORCE.Localization.NameLists;
using Random = UnityEngine.Random;

namespace SORCE.Patches
{
    [HarmonyPatch(declaringType: typeof(ItemFunctions))]
    internal class P_ItemFunctions
    {
        private static readonly ManualLogSource logger = SORCELogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(ItemFunctions.UseItem), argumentTypes: new[] { typeof(InvItem), typeof(Agent) })]
        public static void UseItem_Postfix(InvItem item, Agent agent, ItemFunctions __instance)
        {
            if (!GC.challenges.Contains(nameof(ConsumererProducts)) && !DebugTools.debugMode)
                return;

            List<string> spriteStart = new List<string>() { };
            string spriteFinal = "";
            string particleEffect = "";
            string soundEffect = "";
            bool startFires = false;

            switch (item.invItemName)
            {
                case VItem.Beer:
                    //soundEffect = CSoundEffect.BeerCanCrush;
                    spriteStart.Add(CSprite.BeerCan + Random.Range(1, 5));
                    break;
                case VItem.Cigarettes:
                    particleEffect = VParticleEffect.SmokePuffs;

                    for (int i = 0; i < Random.Range(1, 3); i++)
                        spriteStart.Add(CSprite.CigaretteButt + Random.Range(1, 5));

                    startFires = true;
                    break;
                case VItem.Fud:
                    spriteStart.Add(CSprite.FudJar);
                    spriteStart.Add(CSprite.FudLid);
                    break;
                //case VItem.HotFud:
                //    spriteStart = CSprite.FudJarScorched;
                //    break;
                case VItem.Syringe:
                    spriteFinal = CSprite.Hypo + Random.Range(1, 5);
                    spriteStart.Add(CSprite.Hypo + 1); 
                    break;
                case VItem.Whiskey:
                    spriteStart.Add(CSprite.WhiskeyBottle);
                    //spriteStart.Add(CSprite.WhiskeyBottleLid);
                    //spriteFinal = CSprite.WhiskeyBottleBroken;
                    break;
            }

            if (soundEffect != "")
                GC.audioHandler.Play(agent, soundEffect);

            if (spriteStart.Count > 0)
            {
                foreach (string sprite in spriteStart)
                {
                    bool rotate = true;
                    if (sprite == CSprite.FudLid)
                        rotate = false;

                    Wreckage.ThrowTrash(agent.tr.position, sprite, null, rotate);

                }

                if (startFires)
                {

                }

                if (particleEffect != "")
                {

                }

                if (spriteFinal != "")
                {

                }
            }
        }
    }
}
