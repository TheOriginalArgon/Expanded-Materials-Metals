using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Verse;

namespace MaterialPatchingUtility
{
    class PatchOperationApplyHardCodedMaterialPatch : PatchOperation
    {
        public ThingDef resource;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            ThingDefCountClass res = new ThingDefCountClass();
            res.thingDef = resource;
            List<ThingDef> patchableThings = DefDatabase<ThingDef>.AllDefsListForReading;

            patchableThings.Remove(from q in patchableThings where !q.Minifiable && q.costList.Contains())
        }
    }
}
