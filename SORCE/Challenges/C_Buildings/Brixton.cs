using BepInEx.Logging;
using RogueLibsCore;
using SORCE.Localization;
using SORCE.Logging;
using System;
using System.Linq;
using System.Reflection;
using static SORCE.Localization.NameLists;

namespace SORCE.Challenges.C_Buildings
{
    public class Brixton : BuildingsChallenge
	{
		public Brixton(string name) : base(name) { }

		public override string WallFence =>
			null;
		public override string WallStructural =>
			VWall.Brick;
		public override bool WallsFlammable =>
			false;

        public override string FloorConstructed =>
			VFloor.BrickIndoor;
		public override string FloorNatural =>
			null;
        public override string FloorRaised =>
			VFloor.BrickIndoor;
		public override string FloorRug =>
			null;
		public override string FloorUnraisedTile =>
			VFloor.BrickIndoor;

		[RLSetup]
		static void Start()
		{
			const string name = nameof(Brixton);

			var entry = Assembly.GetEntryAssembly();

			RogueLibs.CreateCustomUnlock(new Brixton(name)
			{
				Cancellations = CChallenge.BuildingsNames.Where(i => i != name).ToList()
			})
				.WithName(new CustomNameInfo(
					"Buildings - Brixton"))
				.WithDescription(new CustomNameInfo(
					"Right, then, I'm well chuffed! All these buildings is made of bricks, like. But like, not proper good bricks. Ravver dodgy, innit, love?"));
		}
	}
}