using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RimWorld;
using HarmonyLib;
using Verse;

namespace ExpandedMaterials
{
    [StaticConstructorOnStartup]
    public class Patcher
    {
        static Patcher()
        {
            var harmony = new Harmony("Argon.VMEu");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(CompRefuelable))]
    [HarmonyPatch("Refuel")]
    [HarmonyPatch(new Type[] { typeof(List<Thing>) })]
    class Patch
    {
        static void Prefix(ref List<Thing> fuelThings)
        {
            foreach (Thing thing in fuelThings)
            {
                float finalFuelAmount;
                finalFuelAmount = thing.stackCount * (thing.def.HasModExtension<ThingDefExtension>() ? thing.def.GetModExtension<ThingDefExtension>().fuelPower : 1f);
                thing.stackCount = Convert.ToInt32(finalFuelAmount);
            }
        }
    }
}
