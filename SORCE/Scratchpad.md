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