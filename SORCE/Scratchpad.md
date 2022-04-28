#	 	Insane Markdown note format
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
##		CT	Features
###         C   00 District Object Delimitation
####            C   00 Export all to Delimitation mod
Pending test of basic features
####            C   00 Add SORCE dependency and test
New
####            √	Flame Grate
Complete
####            √   Manhole
Complete
####            √   SawBlade
Complete
####            √   SlimeBarrel
Complete
####            √   Tube
Complete
###         √H  Pollution Solution
####			H	Split up into granular mutators?
Oil Spills
Slime Barrels
Poisoned Lakes
####            H   Modify smoke particles
Vary timing, speed, transparency.
###         H   Department of Public Comfiness
####			C	Recommend for Grand City Hotel
####			C	Spawn public Armchairs & Fireplaces
####			C	Spawn public Rugs (overlap with Grand City Hotel)
###         H   Lake Coloration test
Pending Overhauls
###         H   Life Downhill
Spawn Pipes as Trash Chutes in the slums or industrial
May drop usable items, or live banana peels, etc.
###         H   Lockdown Walls
New
###         H   Public Pools
Spawn as square or circle
Always have water pump
###         C   Screens
####			C	Custom Sprites
####			C	Corner spawning issues
Exclude diagonal wallchecks?
####            C   Wall type issues
Specifically seems to not work on Concrete walls. Hopefully there's another way to check and allow those.
I think this is just WallMaterialType == None. Concrete are an exception to avoid against-wall spawns.
####            C   Did not spawn in Downtown
Might have been wallmod
####            C   Exclude Interiors
not 100% they actually spawn indoors
####            C   Light spawns without screen 
(or screen invisible?)
####            C   Disable vanilla screen light to improve saturation of selected color
Might be in GetComponent if we want to be lazy.
####            C   Specific placement
Commercial chunks
Next to front doors (avoid unlocked & prohibited)
Avoid alleys if possible, signs need vantage
####            H   Content
|Overhaul                       |Palette    |Audio  |
|:------------------------------|:----------|:------|
|AnCapistan                     |Neon       |TV noise
|DiscoCityDanceoff              |Primary    |Disco  
|GrandCityHotel                 |Blue       |Customer Service Muzak
|MACITS                         |Red        |Anthem/Propaganda
|PoliceState                    |?          |Dystopian Loudspeaker
|Technocracy                    |?          |Beep Boop Bop
|Test Tube City                 |?          |Study Participant Warnings, futuristic PSA music
|TinderTown                     |Blue       |Ads for water
###         H   Signs
Pending resolution of Wall Layer for wall mounted signs
Pending resolution of Custom Objects for floor-based signs

Generate signs with premade text near business entrances
Vary sprite
	Slums:      A-Frame 
	Industrial: Futuristic, Cautiony
	Park: 
	Downtown:   Fancy wrought iron
	Uptown:     Floating hologram thing
Use wall-attached sprite if generated next to a wall, or free-standing sprite if not
Show their text on hover/Space, with color added
Make this data-driven so people can add content?
	If so, divide them by chunk type
###         H   Surveillance Society
On hold until Overhaul update 
Require owned wall, as spawning on junk walls doesn't make sense
####			C	Old Notes
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
###         H   Traintracks
New
###         H   Transit Experiment
Change canals to Ice Rink
Search (if floorName == "Canal") in SpawnerFloor.spawn & BasicFloor.Spawn
###         H   Ultra-Turrets
All turrets are Ultra, meaning don't fuck with them
###         √H  Bad Neighborhoods
Complete
####            C   Darkness
Select N% windows with the least-well lit tiles, and prefer those. 
This will prefer dark alleys, etc.
####            √   Scaled to District
###         √H   Brought Back Fountain
####			H	AnCapistan: Poisoned Fountains
New
###         √H   Cart of the Deal
Complete
####			H	Bodyguards
If their spawns are more frequent, bodyguards might balance it
###         √H  Lake it or Leave it
####			H	Do not make lakes over Downtown bridges
This is low-priority. I've just excluded Downtown for the time being.
mapChunk.Special = "Canal" in LoadLevel
####            H   Prohibit Water Pumps
Certain overhauls
###         √H  Verdant Vistas
####            H   Exclude Bush Cannibals in certain circumstances
Arcology overhaul is only one so far
##		C	Light Sources
CameraScript
	.SetLighting
		Objects & Items
StatusEffects
	.WerewolfTransformBack
LoadLevel
	.SetNormalLighting 

###			C	Objects Re-Lit
P_SpawnerMain.SetLighting2_Prefix
####			C	Disable Object Glow removal
Most players will not want this. It's better for CCU.
####			C	00 Some objects lack a native light
Slime Barrel
Flame Grate
####			C	Flicker Lamp light
####			C	Flicker Flaming Barrel light
####			C	Pulse radioactive lights
####			C	Blink SecurityCam light
###         H   Agents Re-Lit
Light stays until body is gibbed
	See what method that is and analyze it
Attempts
	P_Agent
		Awake_Postfix
	P_Agent
		RecycleAwake_Postfix
	P_Agent
		SetLightBrightness_Prefix
	P_SpawnerMain
		SetLighting2_Prefix
		SpawnLightReal
			NOT TRIED YET
####			C	Exclude Ghosts
Or even add a blue glow
####			C	Red light around Werewolf if you have Awareness
###         H   Player Agent Light Size
New
This would be a stand-in for the flashlight
###         H   Flashlight following player reticle
New
###         H   Flashlight Gun Mod?
Someday
###			√H	Items Re-Lit
P_SpawnerMain.SetLighting2_Prefix
####			H	Omit briefcase
Because Pulp Fiction
###			C	Gunplay Re-Lit
P_Bullet.RealAwake_Postfix
Ensmallen BulletHit particle effect
##		C	Gangs
###			C	00 Gang Minimum
Have a gang size minimum at which to cut off the gangspawner, since some of them don't make sense in too few numbers.
###			C	00 Exclude Home Base
New
###			C	Gangs stop navigating
Stopped in place
###			C	Initial Relationship
###			C	Not Swearwolves
WerewolfB Spawned perma-wolves
	Might want one specific to that anyway
Try making an Office drone and giving him the special ability.
###			H	Modifiers
####			H	Blahd Runs Thick
Blahd gangs are larger, same total number of agents.
####			H	Crêpin' on the Low Low
I be, I be rollin' solo
Crepe gangs of 1, same total number of agents.
####			C	Fast Gangs
####			C	Large Gang Size
####			C	Small Gang Size
####			C	More Gang Spawns
####			H	Guilty Gangs
####			H	Innocent Gangs
###			√H	Protect & Servo
####			H	Split into enforcement & spawn types
###			√	Back Draft
###			√	Banana Smugglers
###			√	Bando Brothers
###			√	BURPs
###			√	Crooklyn Ave.
###			√	Experimental Excursion
###			√	Headhunters
###			√	Home Team
###			√	Lunch Hour
###			√	Merchant Caravans
###			√	Piru St.
###			C	Professional Network
####			C	Disable thief's pickpocket
###			√	Rougher Ruffians
###			√	Team Melvin
###			√	The Blue Line
###			√	The Bluer Line
###			√	Union Town
###			√	We Love Our Job Creators
##		C	VFX
###         C   00 Spawns litter on level editor
New
###			C	00 Wreckage Rotation
###			C	00 Spawn Trash when destroying Trashcan
###			C   Bachelorer Pads
####            C   Scale litter to chunk type & Slumminess
House: scale to chance. Some people are slobs, some aren't.
Some are also slobs in different ways. 
####            H   Refrigerator
Food waste
Will need custom sprites I think
####            √   Bathtub
####            √   Bed
####            √   Desk
####            √   Stove
####            √   Table (Big)
####            √   Table (Small)
####            √   Toilet
###			H	Consumerer Products
On using consumables, spawn litter
####			C	00 Add some of these to generic litter sprite inventories
####			C	ATM Receipt
Also for most paid machines
####            C   Cigarette Butt
Fucking everywhere 
Tiny smoke particle that burns out quickly
If it lands on oil, light it up!
####            C   Beer can
Though this might not work with the throwable beer can
####			C	Fud Jar
With scorched Hot variant
####			C	Smeared Banana peel
After someone slips on it
####			C	Whiskey Bottle
###			H	Flammable Wreckage
New
###			T	Dirtier Districts
PoolsScene.SpawnObjectReal
####			T	00 Fix public litter algo
Reduced walldistance, set gibs to 1 in VFX.SpawnLitterPublic
###			H	Lootier Boxes
Spawn "hoard" sprites around Safe or Chest in Armory
Vary spawns per chunk type
###         CT  Shootier Guns
####			T	00 Destroy on Level End
Lasted into Home Base
####			T	Decals
#####			T	Bullet holes
######				T	Determine layer
Test Layer -1, found it in a wreckage spawner
######				T	Adjust placement per wall facing
Reattempted
######				T	Different sprites for wall types
Glass might be the only one where it's appropriate
Hedge can fully omit the hole
Reattempted
#####				H	Blood 
######					C	Exit wound
######					C	Pool
Small pool on death
	Tracked shoe prints are probably a no-go
######					C	Trail
From recently-hit agents 
	(no need for bleeding status effect yet)
####			CT	Shell Casings
#####				T   Orient spill consistently
Test
#####				H   Bounce
Item.OnCollisionEnter2D
	dw
item.invItemName == "Wreckage" 
!Item.isrealItem is for wreckage I think
GC.AudioHandler.Play(item, "ShellCasing")
######					C   Randomize sprite rotation on bounce
New
######					C   Audio on bounce
New
GC.AudioHandler.Play(objectReal, "BulletHitwall")
if objectReal.CompareTag("Wall")
Then branch to wall types from there
####			H	Smoke from Rockets
New
####			H	Wall Fragments
Spawn TINY fragments of wood, glass, etc. when you shoot a wall
Could also use a particle effect that resembles dust, which would dispose of itself rather than cluttering/slowing
####			C   Muzzle Flash
P_Gun.Shoot_Prefix
P_Turret.FireGun_Postfix
#####				C	Dark Spot
Dark spot is spawned when too many flashes occur in a given area
###			C	Floraler Flora
PoolsScene.SpawnObjectReal
####			C	Stopped working on Hedge Walls
New
####			C	Throw leaves around when you hit a plant
Before destruction
##		H	Roamers
###         H   Arsonist
DW
###         H   Butler Bot
Spawned, but only cleans bodies & not wreckage
How to exclude private areas?
##		H	Decals
Category pending resolution of Wall Decal Layer problem.

Generate decoration across level.
Most of these can be treated as Wreckage particles.
Others would not be reactive in the same way.
I think we can safely use the Wreckage layer, even though these won't be spawned in the same way.
Will also need to figure out how to destroy decals when their host wall is destroyed.

Item.rb.collisionDetectionMode = CollisionDetectionmode2D.None;
SpawnerMain.SpawnFloorDecal
###			C	Content
Trash bags spawned near trash cans

###         C   Private floors
####            C   Bath mat
Placed in front of bathtub
####            C   Toilet paper roll
Near toilets
###         C   Private walls
####            C   Painting
####            C   Poster
###         C   Public floors
####            C   Body outline
Murder scene
####            C   Crushed cardboard box
####            C   Plastic bag
Only if you can make it blow in the wind.
That said, if you could have a few frames of sprites that'd be really cool.
####            C   Puddle of Piss
Near toilets
####			C	Trash bags
Near trashcan spawns
###         C   Public walls
####            C   Graffiti
Gang-related, political, or just obscene
####            C   Poster
Political, entertainment event, PSA
##		H	Laws
###			C	Classes
Class statuses defined by mutators
Affects: 
	prejudice from law enforcement
	prices when dealing with other class
	Gates to service from agents & machines
###			C	Prohibitions
####			C	Alcohol Ban
Contraband
####			C	Alcohol Public Use Ban
Law strikes
####			C	Alcohol Disapproval
Bartenders & drinkers annoy public
####			C	Gun Ban
All guns are contraband
Having a weapon out is instant hostility
####			C	Gun Disapproval
Open carry & use annoys
Raise threat level of guns to cause fleeing instead of fighting
####			C	Gun Legalization
LevelTransition.ChangeLevel_Postfix
	Set placedConfiscationCenter to true
In CopBot spawn, set security type to anything but "Weapons"
####			C	Gun Limitation
Certain weapons are completely banned (rockets, etc.)
####			C	Gun Open Carry Ban
Putting a weapon away ends Accost
####			C	Drug Ban
New
####			C	Drug Decriminalization
New
####			C	Drug Disapproval
Annoys
####			C	Drug Legalization
LevelTransition.ChangeLevel_Postfix
	Set placedConfiscationCenter to true
In CopBot spawn, set security type to anything but "Normal"
###			C	Punishments
####			C	Fine, Just Fine!
Cops may Fine you instead of attacking, depending on severity
####			C	Stop & Frisk
Cops will occasionally confiscate contraband
####			C	Time Served
Cops may Deport you instead of attacking, depending on severity
##		H	Overhauls
Scope:
	Public Floors
	Border Walls
	Features
Note:
	There is overhaul-specific content still in BM. Haven't bothered to migrate it over yet, because this is on hold.
- Will need to make use of LoadLevel.CanUseChunk. Verified that it works before, but shelved.
- LoadLevel.SetupBasicLevel manages SOME Border Walls 
###         C   00 Public Floors
Since shelving these, I've moved this to a Dictionary system. It's not tested yet. Don't expect it to work immediately.
It needs to be moved to transpilers anyway.
###         C   Old notes: FloorMod Exteriors
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
###         C   00 Common features
Second-class citizens are more likely to flee altercations
A method that determines what social status someone is afforded, based on overhaul & whichever appropriate criteria. It will be used frequently.
###			H	Aftermath
Random destruction, as if a riot just occurred. Sort of easy mode, meant more for ambience.
All objects have a chance to be broken at the start.
All NPCs have a chance to be missing some health.
Buildings have a chance to start out on fire, or to have every object inside them destroyed and all items stolen. Police stations have a higher chance.
###			H	AnCapistan
Newish
####			C	Paid Objects
Stove, Fire Hydrant, Alarm Button, 
All of them should have a tiny chance to be really shitty or steal your money
####			C	Illegal to scavenge in trash
"That trash is property of TrashCorp!"
####            C   NPCs
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
  - All aligned to each other (might work with Class Solidarity)
####            C   Object Interactions
- Computer
  - Purchase admin access
  - Hack free Elevator ticket?
- Toilets 
  - Pay to use
- Elevators 
  - Pay to use
  - Hack option
- Bathtubs 
  - Pay to use
- Fire Hydrants 
  - Test
###			H	Arcology
####			√	Public Floors - Grass
####			H	Border Wall - Hedge
####            C   Spawn trashcan next to ATM & omit litter
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
  - Your dance is shorter, allows you to keep moving while they're busy
- Trait: Movebuster +
  - Your dance is super short, theirs is longer
###			H	DUMP
####			C	Public Floors - Cave
####			C	Border Wall - Cave
###			H	Eisburg
####			H	Public Floors - Skating Rink
My gotdamn white whale. I just can't get this one to work.
####            C   Ice wreckage
Check out StatusEffects.IceGib
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
###         H   Hell
Brick borders
Floors can resemble dungeon, wasteland, cave, idk
Features
	Lakes
		poison
		blood
			ObjectMult.ChangeLakeColor
			Heal vampires
		magma
			Too hot to touch, bounce you out
			Smoke particles?
	Flame spewers where public cameras would be
	Flame grates throughout
	Oil spills
Red ambient light
Agents
	Everyone is meaner
	More ghosts & shapeshifters
	Doctors are Lepers, no one can go near them
	Slavers are slaves to demons
		Obviously still wearing the mask
	Cops are helpless babies who cry all the time
		In the mod, I mean
	Firefighters shoot oil at fire and explode
Everything costs too much

#### Levels
- All Cave walls & floors
- Fire Grates & oil slicks spawn
- All Lakes poisoned
#### NPCs
- Demons roam the level, killing anyone they see
- Everyone has infinite resurrection 
- Some demons jump out of holes in the ground
  - similar to warzone hole but use the warzone cannibal sprite
#### Objects
- Machines tend to backfire or cost impossible amounts
###         C   Low-Tech Low-Life
####            C   Replace Refrigerator with...?
####            C   Replace Stove with Barbecue
####            C   Replace Toilet with Bush
####            C   Replace TV with...?
###			C	MACITS
####			C	No Money
- Eliminate money entirely
  - Force voucher rewards maybe?
  - All machines accept vouchers
- How to adjust for that this is basically easy mode?
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
###		C	Martial Law
Soldiers spawn instead of Police, now enforce the law. 
They skip the Annoyed relationship and go straight to Hostile for the smallest infraction.
Maybe they don't take sides if you're in a fight, and just kill both parties unless one is fleeing.
No Bribery
###			C	Police State
####			C	Speech laws
Telling really bad or really good jokes makes police hostile
####            C   Bribery
Bribery would be either completely eliminated, or rampant. Which way?
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
###			C	Technocracy
Newish
###			H	Test Tube City
Glass walls & Glass-y floors
####			C	Scientist Cops
###			C	Tindertown
Wood buildings, oil spills, flame grates, no fire departments
Incendiary weapons & tools are contraband
But cops have flamethrowers
####			C	Flamethrower Cops
###			C	Vampire City (rename)
Vampires are first-class citizens, everyone else isn't.
They are free to victimize whoever they like.
Blood economy should be important 
###			H	Warzone
Spawns dead/burned/exploded bodies, blood splatters
##		√H	Ambient Light Level
###			√	00 Test with Werewolf
Works
###			H	Blinding
###			H	Daytime
###			H	Evening
###			H	FullMoon
###			H	HalfMoon
###			√H	New Moon
####            H   Move from ScrollingMenu
Maybe postfix the ambient light setter
####			H	Now do it modularly
Currently flips a switch, but it'd be better if we could set percent lighting values.
###         √H  Brought Back Fountain
####            √   Show (Empty) when looted
Fountain.MakeChestNonInteractable()
####			√	Spawns in Home Base
I don't really gaf unless people complain
####			H	AnCapistan: Poisoned Fountains
New
##		√	Ambient Light Color
###			√	00 Test with Werewolf
Works
###			√	Goodsprings
###			√	Hellscape	
###			√	NuclearWinter
###			√	Sepia
###			√	ShadowRealm
###			√	Shinobi
##		√	Audio
###			√	Ambienter Ambience
Complete, until Overhauls are scoped
###         C   Footsteps
New, for stealth
###         C   Zombies Moan
New
##		√	Buildings
###         √   00 Flammable buildings don't spawn Fire Spewers
P_RandomSelection.RandomSelect
###         √   Brixton
###			√	City of Steel
###         √   Concrete Jungle
###			√	Green Living
###			√	Panoptikopolis
###         √   Shanty Town
###         √	Spelunky Dory
##		√	Features - Archive
###         √   Meats of Rogue
Complete
###         √   Power Whelming
Complete
###         √   Skyway District
Complete
###         √   This Land is Mine Land
Complete
###         √   Welcome Mats
Complete
##		√	Map Size
###         √   Arthropolis
###         √   Claustropolis
###         √   Megapolis
###         √   Ultrapolis
#	C	Traits
##		C	Underdank Citizen
###         C   New features
CHECK FEATURE LIST
###			H	Death by E_
Tried reordering where damage is allocated
	DW
Honestly I don't care about this too much unless people gripe
Agent
	.deathMethod
	.deathMethodItem
	.deathMethodObject
	.deathKiller
###			T	Spawns near Water
TileInfo.WaterNearby has a levelTheme check 😡
	Transpiler patched, test
###			C	Manhole Agent Brain broken
Attempted calling BecomeUnhidden
	DW
##		T	Underdank VIP
###			T	Poison Resistance
Attempted:
P_StatusEffects.GetStatusEffectTime_Postfix
###			T	No falling damage for Manholes
Attempted
###			T	Not afraid of disgusting toilets
Attempted
#	C	Release
##		C	Export
- Scary Guns
##		C	Set Unlock costs
To get those sweet nuggets 😈
##		C	Disable Core.DebugMode
##		C	Version number
1.0.0
##		C	Documentation
###         C   ReadMe
###         C   Feature List
###         C   Planned Feature List
###         C   Thanks
##		C	Uploads
###			C	BananaMods
###			C	Discord
###			C	NexusModsw
#      H   Shelved Release notes
Overhauls are shelved for now, but I think this will be the right format. They might even need a page to each.

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
Notes

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
