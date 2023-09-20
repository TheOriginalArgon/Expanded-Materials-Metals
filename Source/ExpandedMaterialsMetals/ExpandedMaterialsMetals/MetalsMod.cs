using RimWorld.QuestGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ExpandedMaterialsMetals
{
    public class MetalsMod : Mod
    {
        public static MetalsMod_ModSettings settings;
        public MetalsMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<MetalsMod_ModSettings>();
        }

        // Set settings. (Ironic, huh?)
        public override string SettingsCategory() => "EM_SettingsCategory".Translate();
        public override void DoSettingsWindowContents(Rect inRect) => settings.DoWindowContents(inRect);

        public override void WriteSettings()
        {
            base.WriteSettings();
            SettingsPatcher.ApplySettingPatches();
        }
    }


    // ===================================== MOD SETTINGS =====================================

    public class MetalsMod_ModSettings : ModSettings
    {
        // Vanilla mineable generation.
        public bool generateSteelVeins = false;
        public bool generateComponentVeins = false;

        // Save the settings.
        public override void ExposeData()
        {
            Scribe_Values.Look(ref generateSteelVeins, "EM_Settings_GenerateSteelVeins");
            Scribe_Values.Look(ref generateComponentVeins, "EM_Settings_GenerateComponentVeins");
            base.ExposeData();
        }

        // This is like designing a website without designing a website.
        public void DoWindowContents(Rect windowRect)
        {
            Listing_Standard canvas = new Listing_Standard();
            Color color = GUI.color;

            canvas.Begin(windowRect);
            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.UpperLeft;
            canvas.Gap(12f);
            canvas.TextEntry("EMSettings_NaturalGenerationOptions".Translate());
            canvas.CheckboxLabeled("EMSettings_EnableSteelGeneration".Translate(), ref generateSteelVeins, 12f);
            canvas.CheckboxLabeled("EMSettings_EnableComponentGeneration".Translate(), ref generateComponentVeins, 12f);
            canvas.End();

            MetalsMod.settings.Write();
        }
    }
}
