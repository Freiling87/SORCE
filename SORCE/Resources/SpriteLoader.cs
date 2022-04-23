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

            foreach (string spriteGroup in SpriteGroups)
                for (int i = 0; i <= 5; i++)
                    RogueLibs.CreateCustomSprite(spriteGroup, SpriteScope.Wreckage, Properties.Resources.BulletHole);
        }

        private static List<string> SpriteGroups = new List<string>()
        {
            CSprite.CigaretteButt,
            CSprite.Hypo,
        };
    }
}
