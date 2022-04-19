using RogueLibsCore;
using SORCE.Localization;

namespace SORCE.Resources
{
    public static class SpriteLoader
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomSprite(CSprite.BulletHole, SpriteScope.Decals, Properties.Resources.BulletHole);
            RogueLibs.CreateCustomSprite(CSprite.Casing, SpriteScope.Wreckage, Properties.Resources.Casing);
            RogueLibs.CreateCustomSprite(CSprite.RifleCasing, SpriteScope.Wreckage, Properties.Resources.RifleCasing);
            RogueLibs.CreateCustomSprite(CSprite.ShotgunShell, SpriteScope.Wreckage, Properties.Resources.ShotgunShell);
        }
    }
}
