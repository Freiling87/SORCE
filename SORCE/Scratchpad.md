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
####			C	Old Notes
## Public Security Cams + Turrets
- Align with Cops in conflict, regardless of settings
- Randomize direction for Panoptikopolis if attached to glass wall?
- Use Alarm Button / Police Box detection/ownership rules to align with police
  - Attempted
- Detect Guilty/Wanted
  - Add buttons from hack with indicators to check status. Or just log when Agent interacts with it.
    - Attempted
  - This:
      - [Message: Bunny Mod] SecurityCam_Interact_Temporary
        [Message: Bunny Mod]    Name:   SecurityCam (2288)
        [Message: Bunny Mod]    Owner:  85
        [Message: Bunny Mod]    Targets:        NonOwners
        [Message: Bunny Mod]    Turrets#:       0
        [Message: Bunny Mod] ObjectReal_Interact: SecurityCam (2288)
        [Message: Bunny Mod] Player Agent detected on Camera
        [Message: Bunny Mod]    OwnerID:        0
        [Message: Bunny Mod]    AgentsInView:   0   
- Attach to Turret
  - set securityType == "Turret"
    - Attempted
  - set SecurityCam.turrets to include paired turret
    - Attempted
- Special hacking rules for public cams will be necessary
  - If owner=42069 & agent is cop, that'll work
- Owncheck on tamper working?
- Add Tamper XP if PoliceState
  - Attempted
- Set ownership
  - At least one didn't work, I think it was set to the owner of its attached wall.

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
##		H	Laws
###			C	Legal Drugs
###			C	Legal Weapons
###			C	Stop & Frisk
Cops will occasionally confiscate contraband
###			C	Gun Ban
All guns are contraband
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
This would be a stand-in for the flashlight
###			C	Flashlight following player reticle
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
###			C	00 Common features
Second-class citizens are more likely to flee altercations
###			H	Aftermath
Random destruction, as if a riot just occurred. Sort of easy mode, meant more for ambience.
All objects have a chance to be broken at the start.
All NPCs have a chance to be missing some health.
Buildings have a chance to start out on fire, or to have every object inside them destroyed and all items stolen. Police stations have a higher chance.
###			H	AnCapistan
Newish
####			C	Paid Objects
Stove, Fire Hydrant, Alarm Button, 
####			C	Illegal to scavenge in trash
"That trash is property of TrashCorp!"
###			H	Arcology
####			√	Public Floors - Grass
####			H	Border Wall - Hedge
###			C	Battle City
- More common Arenas
- More roaming fighters
- Anyone can Challenge
- NPCs may challenge you
  - Onlookers annoyed if you decline
- No Guns
###			C	Blahd Reign / Crepes Almighty
Gangbangers get Above the Law, enforce laws
###			H	Canal City
####			C	Public Floors - Pool
###			C	Disco City Danceoff
Newish
Cocaine is FREE, that's right, FREE, that's right, FREE COCAINE, that's right, COCAINE is FOR FREE now
###			H	DUMP
####			C	Public Floors - Cave
####			C	Border Wall - Cave
###			H	Eisburg
####			H	Public Floors - Skating Rink
My gotdamn white whale. I just can't get this one to work.
###			C	Freak City
Just dial up all the weird, supernatural, and bizarre
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
###			C	Tindertown
Wood buildings, oil spills, flame grates, no fire departments
Incendiary weapons & tools are contraband
But cops have flamethrowers
####			C	Flamethrower Cops
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
###			C	Vampire City (rename)
Vampires are first-class citizens, everyone else isn't.
They are free to victimize whoever they like.
Blood economy should be important 
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
All broken for now
###			C	Always Spawn Arsonists
New
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

#   OLD NOTES
## Overhauls
- Will need to make use of LoadLevel.CanUseChunk. Verified that it works before, but shelved.
#### NPCs
- Firefighters
  - Only respond to fires in certain chunk types
  - Can pay them to put out fires if you want
- More Thieves
- More Drug Dealers
- More Slum Dwellers
- No Police, Cop Bots, or SuperCops
- Upper Crusters 
  - Roam with bodyguards
  - Fewer in number
  - All aligned to each other
#### Object Interactions
- Alarm Buttons 
  - Attempted
            ```
            [Message: Bunny Mod] ObjectReal_Interact: AlarmButton (1664)
            [Message: Bunny Mod] AlarmButton_DetermineButtons
            [Warning:  HarmonyX] AccessTools.DeclaredMethod: Could not find method for type ObjectReal and name Interact and parameters ()
            [Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
            Stack trace:
            BunnyMod.Content.BunnyHeaderTools.GetMethodWithoutOverrides[T] (System.Reflection.MethodInfo method, System.Object callFrom) (at <6db2a189c9f94358a13f920d7777c3e6>:0)
            BunnyMod.Content.BMObjects.AlarmButton_DetermineButtons (AlarmButton __instance) (at <6db2a189c9f94358a13f920d7777c3e6>:0)
            AlarmButton.DetermineButtons () (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
            PlayfieldObject.Interact (Agent agent) (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
            BunnyMod.Content.BMObjects.ObjectReal_Interact (Agent agent, ObjectReal __instance) (at <6db2a189c9f94358a13f920d7777c3e6>:0)
            ObjectReal.Interact (Agent agent) (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
            AlarmButton.Interact (Agent agent) (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
            InteractionHelper.UpdateInteractionHelper () (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
            Updater.UpdateInterface () (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
            Updater.Update () (at <5b00a25014d74f7f862ecdd1d48f7c04>:0)
            ```
- Computer
  - Purchase admin access
  - Hack free Elevator ticket?
- Toilets 
  - Pay to use
- Elevators 
  - Pay to use
- Bathtubs 
  - Pay to use
- Fire Hydrants 
  - Pay to use
    - No effect, but didn't have firefighter
            ```
            [Message: Bunny Mod] ObjectReal_Interact: FireHydrant (1679)
            [Info   : Unity Log] Window Lose Focus
            ```
### Disco City Danceoff
- Songs
  - How to play
    - Hack or operate various machines
      - Computer + Satellite Dish (Hack, operate)
        - Affects all speakers in the level
      - Jukebox (Hack, operate)
        - Affects all speakers in chunk with same owner 
        - Distinguishes between public and private speakers
      - Turntables (Hack, operate)
        - Affects all speakers in chunk with same owner 
        - Distinguishes between public and private speakers
  - Songs
    - Disco Inferno
      - Speakers start shooting flames
    - Electric Boogaloo
      - Electro Touch
    - Thriller
      - Zombiism
    - Kung-Fu Fighting
      - Rage
    - Stayin' Alive
      - Resurrection
    - We are Family
      - Gain followers
    - Do the Hustle
      - Speed
    - It's Raining Men
      - People drop from the sky and are gibbed on impact, damaging anything they hit
- New Dance keybind
  - Anyone who sees you dance will stare at you
#### Appearance
- Floors
  - Interiors are mostly tacky rugs with Dance Floors
  - Exteriors are Dance Floor
- Light colors are randomized
  - Possible to make them pulsate?
#### Items
- Cocaine
  - Far more common
  - Bribe cops (and really anyone else) with it
#### NPCs
- Cop Bots
  - Hack to make them dance
- Cops
  - Bodies and shoes changed to garish colors
  - Will occasionally come up to you and yell at you to dance
- Gangsters
  - Gang wars are Dance-offs
- Musicians Roaming
  - Done
- Prisoners
  - Dance instead of stand
#### Object Interactions
- Alarm Button
  - call a DANCE ALERT, not really a law enforcement thing
- Lamp
- Jukebox
- Satellite Dish
  - Can hack/adjust to do something special... but what?
    - Achy Breaky Bladder - All musicians are targeted by everyone in the level
    - Make people storm the broadcasting station and attack everyone inside to destroy the dish
- Speaker
  - Works, but needs Direction
  - Bass Boost
    - Air blast anyone in range
  - Tweeter Tweak
    - Deafen anyone in range
- Turntables
  - Works, but doesn't always have speakers nearby
#### Traits
- Trait: Movebuster
  - Anyone who sees you dance will start dancing
- Trait: Movebuster +
  - Your dance is shorter, allows you to keep moving while they're busy
### Literally Hell

#### Levels
- All Cave walls & floors
- Fire Grates & oil slicks spawn
- All Lakes poisoned
#### NPCs
- Demons roam the level, killing anyone they see
- Everyone has infinite resurrection 
#### Objects
- Machines tend to backfire or explode
### Post-Apocalypse
Radiation plays a big role
### MACITS
- Eliminate money entirely
  - Force voucher rewards maybe?
#### Chunks
- More happy/nature chunks
  - Greenhouse
  - Music Hall
- No mansions/ gated communities/ Shacks
  - Replaced with simple houses and apartments
- No arenas
- No banks
- No casinos
- No churches
- No Confiscation/Deportation Centers
- No slave shops
- No Zoos
#### NPCs
- General
  - Not annoyed by property damage
- Doctors
  - Treat for free
- Gangster
  - None
- Musicians
  - More common
- Slum Dweller
  - Replaced with Worker
- Thief
  - None
- Upper Cruster
  - Replaced with Worker
#### Object Interactions
- Vending machines all free
### Police State
- Bribery disabled

#### Chunks
  - Enable/disable, list already created
#### NPCs
- Cop warnings
  - Littering
  - Sleeping in public
  - Looting trashcans
  - Wearing Gas Mask in public
- Roamers
  - Guilty NPCs automatically have Wanted, get beatdowns in public
  - Non-Humans no longer spawn in public areas
    - Cannibals
    - Zombies
    - Vampires
    - Werewolves are ok, they're hidden
- Secret Police
  - Assassin-based
  - Silenced machine guns
  - This when you done fucked up good
- Upper Cruster 
  - similar to AnCapistan
#### Objects
- Elevator requires access credentials 
  - Get from an UpperCruster
    - Neutralize, Mug, Pickpocket, etc.
      - Percent success based on skin/hair match
  - Register credentials in City database
    - Computer in Law Enforcement chunks
      - Ensure generation if not present in chunk
        - Random location within chunk
          - Far from door +
          - Owned Tiles +
          - In Cell ---
  - Buy fake pass from Fixer
    - Resistance Leader would work in meantime?
  - Hack Elevator
    - Failure means flood of law enforcement and lockout to that avenue
  - Bribe Clerk in Deportation Center
  - Buy Day Pass at Elevator
    - Not available if Wanted
    - Most expensive option
  - Shapeshifter in Upper-Cruster or other authorized person
- Security Cams
  - Always trigger for Wanted
  - Always trigger for Guilty
  - Don't spawn correctly on North-facing walls.
    - It is possible that SecurityCam has Snap-To-Wall behaviors built-in that Trashcans don't. 
      - Try removing this algorithm's snap-to-wall portions in order to not interrupt the Camera's code from doing its thing.
    - Postfixed SetVars with `shiftAmountS = -0.16f`
      - Didn't work
  - I think I get this algorithm now: the original spot they're locating is the blank between two placed trashcans. That's why you often see them right next to each other with a gap in between.
    - So you can probably greatly simplify this algorithm to just use the spot in the middle.
#### Traits
- The Law
  - This would be a whole different playstyle here, wouldn't it?
  - No removal of Wanted if you fuck up this badly. You're now persona non grata.
- Trait Contraindications
  - Cops Don't Care
    - They do
  - Wanted
    - You are
- Wanted Status
  - Not removeable if you have the Wanted trait to begin with
  - Applied if Cop goes Hostile
  - Can remove by accessing Police Station Computer
  - Applied automatically to all NPCs with Guilty
    - Exempt if Chunk Owner?
    - Types

## Litter & Leaves
- Overflow indoors
  - Check for wall and don't go past it
- Add Butler Bots to Uptown & MV
  - They generated at the entrance, but stayed put.
- Main generator should not avoid confined spaces, since it can't block anything. I.e., why is it not spawning in alleys?
  - Attempted: modified argument to FindRandLocationGeneral
- Specific executions:
  - ATM
  - Barbecue
    - Bring in slightly and slightly reduce quantity
      - Attempted
  - Boulder (large)
    - Bring in & reduce quantity slightly
      - Attempted
  - BoulderSmall
    - Ok?
  - Bush
    - Ok?
  - Flaming Barrels
    - These are still *really* spread out. Is there a built-in spread?
  - Goodie Dispenser
    - Add Vendor Cart parts
  - Hedge Walls
    - Attempted, BasicWall.Spawn
  - Killer Plant
    - Ok?
  - Tree
    - Ok?

## Wall Mods
- Non-Randomized Walls
- BasicWall.Spawn
- Leave Bars & Barbed Wire alone. An Uptown House has itself encircled by bars, so it merges into the main walls.

## Wall Mods - Border Walls
- LoadLevel.SetupBasicLevel?
  - Accepted. Omits some borders but it works.
## FloorMod Exteriors
### Scratchpad notes
- Check out the Lake generator in LevelGen, it might have what you need to finally figure this out.
- BasicFloor
  - SetExtraFloorParams
    - Attempted: Water
      - WORKS: Interior floors only, and only seems to make stuff float if set to water. No appearance or other behaviors. Maybe upstream this?
  - Spawn
    - WORKS: Interior floors
- BasicSpawn
  - Spawn
    - This one calls BasicFloor.Spawn
- LoadLevel
  - FillFloors
    - Does not seem to have an effect. Left as Industrial to keep an eye out.
  - FillMapChunks
    - WORKS: Exterior floors, split by level
  - LoadStuff2
- ObjectMult
  - LoadChunkWorldDataFloor
- RandomFloorsWalls
- ReadChunks
  - ReadChild
- SpawnerFloor
  - SetExtraFloorParams
    - WORKS: Affects only certain portions of Home Base
  - spawn
    - WORKS: Affects Home Base only
- TileInfo
  - BuildFloorTileAtPosition
  - setFloor *
    - Attempted
      - I think this ended up turning the whole homebase into water. SunkenCity had just been on and no effect was observed on the game itself.
  - SetupFloorTile
  - SetupFloorTiles


			CustomMutator SunkenCity = RogueLibs.CreateCustomMutator(cChallenge.SunkenCity, true,
				new CustomNameInfo("Floor Exteriors: Sunken City"),
				new CustomNameInfo("More like \"Stinkin' Shitty!\" No, but seriously, that's all sewage."));
			SunkenCity.Available = true;
			SunkenCity.Conflicting.AddRange(cChallenge.AffectsFloors);
			SunkenCity.IsActive = false;

			CustomMutator TransitExperiment = RogueLibs.CreateCustomMutator(cChallenge.TransitExperiment, true,
				new CustomNameInfo("Floor Exteriors: Transit Experiment"),
				new CustomNameInfo("The City's authorities considered making all the public streets into conveyor belts, but that was too hard to mod into the game... er, I mean construct. Yeah."));
			TransitExperiment.Available = true;
			TransitExperiment.Conflicting.AddRange(cChallenge.AffectsFloors);
			TransitExperiment.IsActive = false;
#   BunnyMod Shelved Release notes

## Overhauls

These are all accessed via the Mutators menu. These affect large swathes of gameplay content.

### AnCapistan
This just makes the game act like normal life in America. By the way, it's polite to *leave a tip* when someone mugs you.

---

#### Chunks
- All Law Enforcement chunks disabled
- All Criminal-type chunks made more common

#### Map Features
- All Pollution-related features made more common
- Most public good features disabled or made pay-only

#### NPCs
- Law Enforcement roaming NPCs disabled
- Criminal roaming NPCs made more common

#### Object Interactions
|Name                   |Effects
|:----------------------|
|Alarm Button           |- Now costs money
|Elevator               |- Now costs money
|Fire Hydrant           |- Now costs money
|Toilet                 |- Now costs money

### Disco City Dance-off

### Mostly Automated Comfortable Inclusive Terrestrial Socialism
A post-scarcity mode. It's a lot easier and friendlier, if you're into that kind of thing. Turns out you thrive on conflict!

---

#### Chunks
- Artistic & Nature chunks more common
- Shacks & Mansions disabled, replaced with apartments

#### Map Features
- Public Good features more common
- Pollution features disabled

#### NPCs
- Healing with Doctor is always free
- Thieves no longer spawn

#### Object Interactions
|Name                   |Effects
|:----------------------|

### Police State
You saw how the last guy became the Mayor - did you think he was gonna be nice once he was in office?

Consider this one hard-mode.

---

#### Chunks
- Law Enforcement chunks always spawn
- Criminal-type chunks disabled

#### Map Features
- Pollution-related features disabled
- Security Cameras now generate in public
- 
#### NPCs
- Law Enforcers always spawn 
- Law Enforcers start out Annoyed
- If a Law Enforcer goes Hostile, you gain the Wanted Trait until you remove it [Methods TBD]
- Criminal roaming NPCs disabled

#### Object Interactions
|Name                   |Effects
|:----------------------|
|Security Cam           |- Triggers alarm if anyone Wanted is detected
