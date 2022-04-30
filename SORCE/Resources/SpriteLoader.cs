using RogueLibsCore;
using SORCE.Localization;
using System.Collections.Generic;

namespace SORCE.Resources
{
    public static class SpriteLoader
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomSprite(CSprite.BulletHole, SpriteScope.Decals, Properties.Resources.BulletHole);
            RogueLibs.CreateCustomSprite(CSprite.BulletHoleGlass, SpriteScope.Decals, Properties.Resources.BulletHoleGlass);
            RogueLibs.CreateCustomSprite(CSprite.Casing, SpriteScope.Wreckage, Properties.Resources.Casing);
            RogueLibs.CreateCustomSprite(CSprite.RifleCasing, SpriteScope.Wreckage, Properties.Resources.RifleCasing);
            RogueLibs.CreateCustomSprite(CSprite.ShotgunShell, SpriteScope.Wreckage, Properties.Resources.ShotgunShell);

            RogueLibs.CreateCustomSprite(CSprite.BeerCan + 1, SpriteScope.Wreckage, Properties.Resources.BeerCan1);
            RogueLibs.CreateCustomSprite(CSprite.BeerCan + 2, SpriteScope.Wreckage, Properties.Resources.BeerCan2);
            RogueLibs.CreateCustomSprite(CSprite.BeerCan + 3, SpriteScope.Wreckage, Properties.Resources.BeerCan3);
            RogueLibs.CreateCustomSprite(CSprite.BeerCan + 4, SpriteScope.Wreckage, Properties.Resources.BeerCan4);
            RogueLibs.CreateCustomSprite(CSprite.BeerCan + 5, SpriteScope.Wreckage, Properties.Resources.BeerCan5);

            RogueLibs.CreateCustomSprite(CSprite.CigaretteButt + 1, SpriteScope.Wreckage, Properties.Resources.CigaretteButt1);
            RogueLibs.CreateCustomSprite(CSprite.CigaretteButt + 2, SpriteScope.Wreckage, Properties.Resources.CigaretteButt2);
            RogueLibs.CreateCustomSprite(CSprite.CigaretteButt + 3, SpriteScope.Wreckage, Properties.Resources.CigaretteButt3);
            RogueLibs.CreateCustomSprite(CSprite.CigaretteButt + 4, SpriteScope.Wreckage, Properties.Resources.CigaretteButt4);
            RogueLibs.CreateCustomSprite(CSprite.CigaretteButt + 5, SpriteScope.Wreckage, Properties.Resources.CigaretteButt5);

            RogueLibs.CreateCustomSprite(CSprite.FudJar, SpriteScope.Wreckage, Properties.Resources.FudJar);
            RogueLibs.CreateCustomSprite(CSprite.FudLid, SpriteScope.Wreckage, Properties.Resources.FudLid);

            RogueLibs.CreateCustomSprite(CSprite.Hypo + 1, SpriteScope.Wreckage, Properties.Resources.Hypo1);
            RogueLibs.CreateCustomSprite(CSprite.Hypo + 2, SpriteScope.Wreckage, Properties.Resources.Hypo2);
            RogueLibs.CreateCustomSprite(CSprite.Hypo + 3, SpriteScope.Wreckage, Properties.Resources.Hypo3);
            RogueLibs.CreateCustomSprite(CSprite.Hypo + 4, SpriteScope.Wreckage, Properties.Resources.Hypo4);
            RogueLibs.CreateCustomSprite(CSprite.Hypo + 5, SpriteScope.Wreckage, Properties.Resources.Hypo5);

            RogueLibs.CreateCustomSprite(CSprite.WhiskeyBottle, SpriteScope.Wreckage, Properties.Resources.WhiskeyBottle);
        }

        public static List<string> SpriteGroups = new List<string>()
        {
            CSprite.BeerCan,
            CSprite.CigaretteButt,
            CSprite.Hypo,
        };
    }
}
