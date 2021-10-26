using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace ExpandedMaterials
{
    public class ThingDefExtension : DefModExtension
    {
        [Description("The multiplier this thing applies when refueling.")]
        [DefaultValue(1f)]
        public float fuelPower;
    }
}
