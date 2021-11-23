- TODO
  - P_LoadLevel
    - Ambience 
    - Wreckage
    - Underdark Citizen
      - While it makes sense here, it'd need to be in an object mod.
  - Check all patches for whether they're IEnumerators
    - If so, they need to be split off into own classes and BT's method applied to the transpilers.
  - BadNeighborhoods
    - Increase occurrence of Locked/Steel doors?
- Verify MethodInfo signatures when accessing your own methods, as some were rewritten. In particular I'm thinking about those that went from requiring __instance passed to not requiring it.


Migrate to other mods:
    LevelGenTools.SpawnLitter
    LevelGenTools.SpawnManholes_Underdark
    BroughtBackFountain (Object mod)



[Info   :   BepInEx] Loading [SORCE 0.1.0]
[Warning:   BepInEx] Shimming C:\Program Files (x86)\Steam\steamapps\common\Streets of Rogue\BepInEx\plugins\SORCE.dll to use older version of Harmony (0Harmony20). Please update the plugin if possible.

[Error  :  HarmonyX] Failed to process patch Unknown patch - methodName can't be empty
[Error  : Unity Log] ArgumentException: No target method specified for class SORCE.Patches.P_BasicFloor (declaringType=BasicFloor, methodName =, methodType=, argumentTypes=NULL)
Stack trace:
HarmonyLib.PatchProcessor.PrepareType () (at <0febc8b424d54c2ca4aaacdbe68c9426>:0)
HarmonyLib.PatchProcessor..ctor (HarmonyLib.Harmony instance, System.Type type, HarmonyLib.HarmonyMethod attributes) (at <0febc8b424d54c2ca4aaacdbe68c9426>:0)
HarmonyLib.Harmony.ProcessorForAnnotatedClass (System.Type type) (at <0febc8b424d54c2ca4aaacdbe68c9426>:0)
System.Linq.Enumerable+WhereSelectArrayIterator`2[TSource,TResult].MoveNext () (at <55b3683038794c198a24e8a1362bfc61>:0)
System.Linq.Enumerable+WhereEnumerableIterator`1[TSource].MoveNext () (at <55b3683038794c198a24e8a1362bfc61>:0)
System.Collections.Generic.List`1[T]..ctor (System.Collections.Generic.IEnumerable`1[T] collection) (at <44afb4564e9347cf99a1865351ea8f4a>:0)
System.Linq.Enumerable.ToList[TSource] (System.Collections.Generic.IEnumerable`1[T] source) (at <55b3683038794c198a24e8a1362bfc61>:0)
HarmonyLib.Harmony.PatchAll (System.Reflection.Assembly assembly) (at <0febc8b424d54c2ca4aaacdbe68c9426>:0)
SORCE.Core.Awake () (at <4411e7ed04944fbcb55498754b60bcd5>:0)
UnityEngine.GameObject:AddComponent(Type)
BepInEx.Bootstrap.Chainloader:Start()
UnityEngine.Application:.cctor()
Rewired.InputManager_Base:Awake()