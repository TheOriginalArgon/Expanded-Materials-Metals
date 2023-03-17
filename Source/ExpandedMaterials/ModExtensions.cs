using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace ExpandedMaterials
{
    public class ThingDefExtension_InterchangableMaterials : DefModExtension
    {
        [Description("The material this can be exchanged with")]
        public ThingDef thingDef;
    }
}
