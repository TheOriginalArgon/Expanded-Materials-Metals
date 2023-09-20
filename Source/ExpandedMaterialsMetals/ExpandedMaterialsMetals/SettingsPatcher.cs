using Verse;

namespace ExpandedMaterialsMetals
{
    /* 
       This class applies the settings that we previously set. Given the fact that this executes when the game is launch, these settings will require
       a restart to take effect.
    */

    [StaticConstructorOnStartup]
    public static class SettingsPatcher
    {
        static SettingsPatcher()
        {
            ApplySettingPatches();
        }

        public static void ApplySettingPatches()
        {
            if (!MetalsMod.settings.generateSteelVeins)
            {
                ThingDef mineableSteel = DefDatabase<ThingDef>.GetNamed("MineableSteel");
                mineableSteel.building.mineableScatterCommonality = 0;
                mineableSteel.building.mineableScatterLumpSizeRange = new IntRange(1, 2);
            }

            if (!MetalsMod.settings.generateComponentVeins)
            {
                ThingDef mineableSteel = DefDatabase<ThingDef>.GetNamed("MineableComponentsIndustrial");
                mineableSteel.building.mineableScatterCommonality = 0;
                mineableSteel.building.mineableScatterLumpSizeRange = new IntRange(1, 2);
            }
        }
    }
}
