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

[Info   :   BepInEx] Loading [SORCE 0.1.0]
[Error  :SORCE_P_LoadLevel_SetupMore3_3_Patches] Patching SetupMore3_3_Transpiler_PoliceBoxes caused error: CodeReplacementPatch has found 0 matches, but expected to find 1! Mod may be outdated!
  at BTHarmonyUtils.TranspilerUtils.CodeReplacementPatch.Apply (System.Collections.Generic.List`1[T] instructions) [0x00141] in <693220fe48804439a90bd4a99a2cbf98>:0
  at BTHarmonyUtils.TranspilerUtils.CodeReplacementPatch.ApplySafe (System.Collections.Generic.List`1[T] instructions, BepInEx.Logging.ManualLogSource logger) [0x00002] in <693220fe48804439a90bd4a99a2cbf98>:0
[Warning:  HarmonyX] AccessTools.Field: Could not find field for type RandomOther and name component
[Error  :  HarmonyX] Failed to patch void RandomOther::fillOther(): System.ArgumentException: No such field defined in class RandomOther
Parameter name: component
  at HarmonyLib.Public.Patching.HarmonyManipulator.EmitCallParameter (HarmonyLib.Internal.Util.ILEmitter il, System.Reflection.MethodBase original, System.Reflection.MethodInfo patch, System.Collections.Generic.Dictionary`2[TKey,TValue] variables, System.Boolean allowFirsParamPassthrough, Mono.Cecil.Cil.VariableDefinition& tmpObjectVar, System.Collections.Generic.List`1[HarmonyLib.Public.Patching.HarmonyManipulator+ArgumentBoxInfo]& tmpBoxVars) [0x00216] in <48ac0133328b486983afe98eee5b730e>:0
  at HarmonyLib.Public.Patching.HarmonyManipulator.WritePostfixes (HarmonyLib.Internal.Util.ILEmitter il, System.Reflection.MethodBase original, HarmonyLib.Internal.Util.ILEmitter+Label returnLabel, System.Collections.Generic.Dictionary`2[TKey,TValue] variables, System.Collections.Generic.ICollection`1[T] postfixes, System.Boolean debug) [0x00146] in <48ac0133328b486983afe98eee5b730e>:0
  at HarmonyLib.Public.Patching.HarmonyManipulator.MakePatched (System.Reflection.MethodBase original, MonoMod.Cil.ILContext ctx, System.Collections.Generic.List`1[T] prefixes, System.Collections.Generic.List`1[T] postfixes, System.Collections.Generic.List`1[T] transpilers, System.Collections.Generic.List`1[T] finalizers, System.Collections.Generic.List`1[T] ilmanipulators, System.Boolean debug) [0x001a4] in <48ac0133328b486983afe98eee5b730e>:0
[Error  : Unity Log] ArgumentException: No such field defined in class RandomOther
Parameter name: component
Stack trace:
HarmonyLib.Public.Patching.HarmonyManipulator.EmitCallParameter (HarmonyLib.Internal.Util.ILEmitter il, System.Reflection.MethodBase original, System.Reflection.MethodInfo patch, System.Collections.Generic.Dictionary`2[TKey,TValue] variables, System.Boolean allowFirsParamPassthrough, Mono.Cecil.Cil.VariableDefinition& tmpObjectVar, System.Collections.Generic.List`1[HarmonyLib.Public.Patching.HarmonyManipulator+ArgumentBoxInfo]& tmpBoxVars) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.Public.Patching.HarmonyManipulator.WritePostfixes (HarmonyLib.Internal.Util.ILEmitter il, System.Reflection.MethodBase original, HarmonyLib.Internal.Util.ILEmitter+Label returnLabel, System.Collections.Generic.Dictionary`2[TKey,TValue] variables, System.Collections.Generic.ICollection`1[T] postfixes, System.Boolean debug) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.Public.Patching.HarmonyManipulator.MakePatched (System.Reflection.MethodBase original, MonoMod.Cil.ILContext ctx, System.Collections.Generic.List`1[T] prefixes, System.Collections.Generic.List`1[T] postfixes, System.Collections.Generic.List`1[T] transpilers, System.Collections.Generic.List`1[T] finalizers, System.Collections.Generic.List`1[T] ilmanipulators, System.Boolean debug) (at <48ac0133328b486983afe98eee5b730e>:0)
Rethrow as HarmonyException: IL Compile Error (unknown location)
HarmonyLib.Public.Patching.HarmonyManipulator.MakePatched (System.Reflection.MethodBase original, MonoMod.Cil.ILContext ctx, System.Collections.Generic.List`1[T] prefixes, System.Collections.Generic.List`1[T] postfixes, System.Collections.Generic.List`1[T] transpilers, System.Collections.Generic.List`1[T] finalizers, System.Collections.Generic.List`1[T] ilmanipulators, System.Boolean debug) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.Public.Patching.HarmonyManipulator.Manipulate (System.Reflection.MethodBase original, HarmonyLib.PatchInfo patchInfo, MonoMod.Cil.ILContext ctx) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.Public.Patching.ManagedMethodPatcher.Manipulator (MonoMod.Cil.ILContext ctx) (at <48ac0133328b486983afe98eee5b730e>:0)
MonoMod.Cil.ILContext.Invoke (MonoMod.Cil.ILContext+Manipulator manip) (at <0a7634c77ccf4d4bb38fdfcede8ae458>:0)
MonoMod.RuntimeDetour.ILHook+Context.InvokeManipulator (Mono.Cecil.MethodDefinition def, MonoMod.Cil.ILContext+Manipulator cb) (at <4fc6695ecceb4b02aae56cad81c83183>:0)
DMD<Refresh>?1131621632._MonoMod_RuntimeDetour_ILHook+Context::Refresh (MonoMod.RuntimeDetour.ILHook+Context this) (at <da877660644943608416fb36c11a78d7>:0)
DMD<>?1131621632.Trampoline<MonoMod.RuntimeDetour.ILHook+Context::Refresh>?-2029637632 (System.Object ) (at <07536512e9864713ae155788b5729ff0>:0)
HarmonyLib.Internal.RuntimeFixes.StackTraceFixes.OnILChainRefresh (System.Object self) (at <48ac0133328b486983afe98eee5b730e>:0)
MonoMod.RuntimeDetour.ILHook.Apply () (at <4fc6695ecceb4b02aae56cad81c83183>:0)
HarmonyLib.Public.Patching.ManagedMethodPatcher.DetourTo (System.Reflection.MethodBase replacement) (at <48ac0133328b486983afe98eee5b730e>:0)
Rethrow as HarmonyException: IL Compile Error (unknown location)
HarmonyLib.Public.Patching.ManagedMethodPatcher.DetourTo (System.Reflection.MethodBase replacement) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.PatchFunctions.UpdateWrapper (System.Reflection.MethodBase original, HarmonyLib.PatchInfo patchInfo) (at <48ac0133328b486983afe98eee5b730e>:0)
Rethrow as HarmonyException: IL Compile Error (unknown location)
HarmonyLib.PatchClassProcessor.ReportException (System.Exception exception, System.Reflection.MethodBase original) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.PatchClassProcessor.Patch () (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.Harmony.<PatchAll>b__11_0 (System.Type type) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.CollectionExtensions.Do[T] (System.Collections.Generic.IEnumerable`1[T] sequence, System.Action`1[T] action) (at <48ac0133328b486983afe98eee5b730e>:0)
HarmonyLib.Harmony.PatchAll (System.Reflection.Assembly assembly) (at <48ac0133328b486983afe98eee5b730e>:0)
SORCE.Core.Awake () (at <2de29c149773422ebcb8d70f8a5f3f7a>:0)
UnityEngine.GameObject:AddComponent(Type)
BepInEx.Bootstrap.Chainloader:Start()
UnityEngine.Application:.cctor()
Rewired.InputManager_Base:Awake()