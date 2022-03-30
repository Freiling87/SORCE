#	√	Insane Markdown note format
This file is meant to be viewed in raw format. I just use markdown because its collapsible headers are really useful.

|Tag				|Meaning	|
|:-----------------:|:----------|
|√					|Feature Complete
|!					|Urgent
|C					|To Code (Includes any non-testing task)
|H					|On hold (Should have specifics in header)
|N					|To be implemented in next release
|T					|To Test

#	CT	Mutators
##		√	Ambient Light Color
###			√	00 Test with Werewolf
Works
###			√	Goodsprings
###			√	Hellscape	
###			√	NuclearWinter
###			√	Sepia
###			√	ShadowRealm
###			√	Shinobi
##		√H	Ambient Light Level
###			√	00 Test with Werewolf
Works
###			H	Blinding
###			H	Daytime
###			H	Evening
###			H	FullMoon
###			H	HalfMoon
###			√H	New Moon
####			H	Now do it modularly
##		H	Audio
###			C	Ambienter Ambience
Only seems to apply Park audio, check BM for others
##		C	Buildings
###			C	00 Move Borders to other mutator set
Borders should be tied to Public floors
###			C	00 Hideout Steel Floors not affected
###			√	City of Steel
###			√	Green Living
###			C	Panoptikopolis
No floor type?
###			√	Shanty Town
###			C	Spelunky Dory
Exterior walls are now wood?? Lol something got messed up, but maybe use it in the other.
Change rugs back to Grass
##		√	Features
###			H	Department of Public Comfiness
####			C	Recommend for Grand City Hotel
####			C	Spawn public Armchairs & Fireplaces
####			C	Spawn public Rugs (overlap with Grand City Hotel)
###			H	Lake it or Leave it
Honestly this one's not interesting enough to bother with yet.
####			C	Do not make lakes over Downtown bridges
LoadLevel.SetupMore3_3, after line 67
There's a method called TileInfo.IsearBridge or something like that, use it
###			H	Surveillance Society
####			C	00 Import SecurityCamera patches
####			C	Detect Wanted
###			H	The Pollution Solution
####			C	Slime Barrels
SlimeBarrels.Start has a district limitation
###			√	Bad Neighborhoods
Complete
###			√H	Brought Back Fountain
####			√	Spawns in Home Base
I don't really gaf unless people complain
####			H	AnCapistan: Poisoned Fountains
###			√	Cart of the Deal
Complete
###			√	Power Whelming
Complete
###			√	Skyway District
Complete
###			√	This Land is Mine Land
Complete
##		CT	Light Sources
- Content in CCU. Migrate, and put on hold.
- CameraScript.SetLighting
  - DW
- StatusEffects.WerewolfTransform
- StatusEffects.WerewolfTransformBack
- LoadLevel.SetNormalLighting 
- LoadLevel.SetRogueVisionLighting
###			C	No Agent Lights
- The most recent attempt didn't make them move feet-first, but they still all have lights.
- Didn't work, and made the agent move feet-first
  - Same outcome for both locations of attempt
- Agent.hasLight
  - Postfix to false in
    - Agent.Awake
      - Attempted
        - DW
    - Agent.RecycleAwake
      - Attempted
        - DW
  - Note, there are a total of four attempts at this active so you'll need to pare down once you find a working one.
- Exclude Ghosts!
###			C	No Item/Wreckage Lights
- SpawnerMain.SetLighting2
  - Tried this another way
    - DW
###			T	No Object Glow
This is the yellow glow for when you have usable items with an object. As you collect more, eventually everything glows.
- gc.objectGlowDisabled
- gc.sessionDataBig.objectGlowDisabled
- Attempted, GC.Awake3 Prefix
###			C	No Object Lights
- Works!
- Need to exclude working machines with lights from this. Maybe jazz up their halos if possible.
- Fire sources are fine since the particle creates the light anyway.
###			C	Player Agent Light Size
New
##		√	MapSize
###			√	A City For Ants
###			√	Claustropolis
###			√	Megapolis
###			√	Ultrapolis
##		H	Overhauls
Scope:
	Public Floors
	Border Walls
	Features
Note:
	There is overhaul-specific content still in BM. Haven't bothered to migrate it over yet, because this is on hold.
	
###			H	Aftermath
Random destruction, as if a riot just occurred. Sort of easy mode, meant more for ambience.
All objects have a chance to be broken at the start.
All NPCs have a chance to be missing some health.
Buildings have a chance to start out on fire, or to have every object inside them destroyed and all items stolen. Police stations have a higher chance.
###			H	AnCapistan
Newish
###			H	Arcology
####			√	Public Floors - Grass
####			H	Border Wall - Hedge
###			H	Canal City
####			C	Public Floors - Pool
###			C	Disco City Danceoff
Newish
###			H	DUMP
####			C	Public Floors - Cave
####			C	Border Wall - Cave
###			H	Eisburg
####			H	Public Floors - Skating Rink
My gotdamn white whale. I just can't get this one to work.
###			C	Grand City Hotel
On start:

	[Info   : Unity Log] LEVEL SIZE: 20
	[Info   : Unity Log] Random Number After CreateInitialMap: 653
	[Error  : Unity Log] FormatException: Input string was not in a correct format.
	Stack trace:
	System.Number.StringToNumber (System.String str, System.Globalization.NumberStyles options, System.Number+NumberBuffer& number, System.Globalization.NumberFormatInfo info, System.Boolean parseDecimal) (at <a1e9f114a6e64f4eacb529fc802ec93d>:0)
	System.Number.ParseInt32 (System.String s, System.Globalization.NumberStyles style, System.Globalization.NumberFormatInfo info) (at <a1e9f114a6e64f4eacb529fc802ec93d>:0)
	System.Int32.Parse (System.String s) (at <a1e9f114a6e64f4eacb529fc802ec93d>:0)
	SORCE.Patches.P_LoadLevel+<FillMapChunks_Replacement>d__5.MoveNext () (at <9d9121f757474c8c99743a650d130089>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <a5d0703505154901897ebf80e8784beb>:0)
	UnityEngine.MonoBehaviour:StartCoroutine(IEnumerator)
	<loadStuff2>d__137:MoveNext()
	UnityEngine.MonoBehaviour:StartCoroutine(IEnumerator)
	LoadLevel:loadStuff()
	GameController:AwakenObjects()
	GameController:CreateMultPlayerAgent(GameObject, Int32)
	<WaitForRealStart>d__480:MoveNext()
	UnityEngine.SetupCoroutine:InvokeMoveNext(IEnumerator, IntPtr)

####			C	Public Floors - Fancy Wood
####			C	Border Wall - Wood
####			C	Spawn Rugs in clearings
###			H	Test Tube City
Glass walls & Glass-y floors
####			C	Scientist Cops
###			C	MACITS
####			C	No Money
Everything's free
Obviously would be easy-mode, so I wonder if there's a way to add a twist to this
####		C	Martial Law
Soldiers spawn instead of Police, now enforce the law. 
They skip the Annoyed relationship and go straight to Hostile for the smallest infraction.
Maybe they don't take sides if you're in a fight, and just kill both parties unless one is fleeing.
###			C	Police State
####			C	Speech laws
Telling really bad or really good jokes makes police hostile
###			C	Technocracy
Newish
###			H	Warzone
Spawns dead/burned/exploded bodies, blood splatters
##		√	Population
###			√	Ghost Town
Complete
###			√	Horde Almighty
Complete
###			√	Let Me See That Throng
Complete
###			√	Swarm Welcome
Complete
##		C	Roamers
None work
###			C	HoodlumsWonderland
###			C	Mob Town
###			C	YMITN
##		C	Wreckage
###			C	00 Custom Wreckage Method
Benefits:
	More control over number of particles
	Can spread out more than normal
	Can set a check for Interior/Exterior/Water
###			C	00 Here's the issue
This only works on procedurally generated objects. Trashcans, boulders, bushes. Not toilets, etc.
So the goal here is to turn the current postfix method into one accessible by both original methods,
the current procgen one and whichever spawns hand-placed objects.

*** I think this other method is SpawnerObject.spawn.
###			√	Dirtier Districts 
###			C	Floraler Flora
###			C	Shittier Toilets
#	C	Traits
##		C	Underdank Citizen
###			C	Import
Still not spawning
Transpiler to remove levelTheme requirement complete
###			C	Disable Teleport to entry point
Works, but has a chance of rolling self
###			C	Old Notes
- Take small damage if you walk into manhole instead of activating
  - Attempted
- Walkover version of flushyourself:
    [Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
    Stack trace:
    BunnyMod.Content.BMObjects.Manhole_FlushYourself (Agent agent, ObjectReal __instance) (at <8abc5006f52b44d7a55c9ddabc9a0e08>:0)
    BunnyMod.Content.BMObjects.Hole_EnterRange (UnityEngine.GameObject myObject, Hole __instance) (at <8abc5006f52b44d7a55c9ddabc9a0e08>:0)
    Hole.EnterRange (UnityEngine.GameObject myObject) (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
    Hole.OnTriggerStay2D (UnityEngine.Collider2D other) (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
    - This only occurred when no other manholes were open. There were active toilets.
    - Walkover version also only sends to self.
    - One manhole keeps getting excluded. Check that the random selection from lists isn't excluding anything from the running.
- Water splash
  - Needs a delay. It's appearing before the player is.
    - Attempted Immediate teleportation
  - No longer working
- Manhole to Toilet
  - Attempted
- Toilet to Manhole
  - Attempted
#	C	Migrate In
- Anything from CCU that would be of interest to the non-designer - that includes all Ambience & Litter.
- Custom object interactions related to city overhauls, e.g. coin-op toilets. They don't all need to be in one mod.
#	C	Release
##		C	Disable Core.DebugMode
##		C	Version number
1.0.0
##		C	Documentation
##		C	Uploads
###			C	BananaMods
###			C	Discord
###			C	NexusMods