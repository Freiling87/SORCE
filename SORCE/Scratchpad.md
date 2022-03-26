#	C	Mutators
##		CT	Features
###			C	Bad Neighborhoods
Stopped working
###			C	Brought Back Fountain
####			C	Spawns in Home Base
####			C	Move this to Object mod
###			C	Cart of the Deal
Complete
###			C	Department of Public Comfiness
####			C	Recommend for Grand City Hotel
####			C	Spawn public Armchairs & Fireplaces
####			C	Spawn public Rugs (overlap with Grand City Hotel)
###			√	Lake it or Leave it
Complete
###			C	Power Whelming
Stopped working
###			√	Skyway District
Complete
###			C	Surveillance Society
Stopped working
###			C	The Pollution Solution
Stopped working
###			C	This Land is Mine Land
New
##		C	Floor Exteriors & Borders
###			√H	Arcology
####			√	Exterior Floor - Grass
####			H	Border Wall - Hedge
###			H	Canal City
####			C	Exterior Floor - Pool
###			H	DUMP
####			C	Exterior Floor - Cave
####			C	Border Wall - Cave
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

####			C	Exterior Floor - Fancy Wood
####			C	Border Wall - Wood
####			C	Spawn Rugs in clearings
###			H	Test Tube City
Glass walls & Glass-y floors
###			H	Transit Experiment
####			H	Exterior Floors - Skating Rink
My gotdamn white whale. I just can't get this one to work.
##		C	Floor Interiors & Walls
###			C	00 Move Borders to other mutator set
Borders should be tied to Exterior floors
###			C	00 Hideout Steel Floors not affected
###			√	City of Steel
###			√	Green Living
###			C	Panoptikopolis
No floor type?
###			√	Shanty Town
###			C	Spelunky Dory
Exterior walls are now wood?? Lol something got messed up, but maybe use it in the other.
Change rugs back to Grass
##		√	MapSize
###			√	A City For Ants
###			√	Claustropolis
###			√	Megapolis
###			√	Ultrapolis
##		C	Overhauls
###			C	AnCapistan
###			C	Disco City Danceoff
###			C	MACITS
####			C	No Money
Everything's free
Obviously would be easy-mode, so I wonder if there's a way to add a twist to this
###			C	Police State
####			C	Speech laws
Telling really bad or really good jokes makes police hostile
###			C	Technocracy
##		C	Population
###			√	Ghost Town
###			C	Horde Almighty
Error:

	[Info   : Unity Log] CREATING INITIAL MAP WITH SEED NUM: -1341233615 - UsedChunks Count: 0
	[Info   : Unity Log] LEVEL SIZE: 4
	[Error  : Unity Log] ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
	Parameter name: index
	Stack trace:
	System.ThrowHelper.ThrowArgumentOutOfRangeException (System.ExceptionArgument argument, System.ExceptionResource resource) (at <a1e9f114a6e64f4eacb529fc802ec93d>:0)
	System.ThrowHelper.ThrowArgumentOutOfRangeException () (at <a1e9f114a6e64f4eacb529fc802ec93d>:0)
	LoadLevel.CreateInitialMap () (at <9086a7372c854d5a8678e46a74a50fc1>:0)
	LoadLevel+<loadStuff2>d__137.MoveNext () (at <9086a7372c854d5a8678e46a74a50fc1>:0)
	UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at <a5d0703505154901897ebf80e8784beb>:0)
	UnityEngine.MonoBehaviour:StartCoroutine(IEnumerator)
	LoadLevel:loadStuff()
	GameController:AwakenObjects()
	GameController:CreateMultPlayerAgent(GameObject, Int32)
	<WaitForRealStart>d__480:MoveNext()
	UnityEngine.SetupCoroutine:InvokeMoveNext(IEnumerator, IntPtr)
###			C	Let Me See That Throng
No effect
###			C	Swarm Welcome
Didn't work
##		C	Roamers
None work
###			C	HoodlumsWonderland
###			C	Mob Town
###			C	YMITN
#	C	Traits
##		C	Underdark Citizen
#	Migrate Out
##		LevelGenTools.SpawnLitter
##		LevelGenTools.SpawnManholes_Underdark
##		BroughtBackFountain (Object mod)
##		Vendor Carts (Object mod)