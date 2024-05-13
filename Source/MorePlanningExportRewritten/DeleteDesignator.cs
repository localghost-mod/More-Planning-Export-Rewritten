using System.Linq;
using UnityEngine;
using Verse;

namespace MorePlanningExportRewritten
{
    internal class DeleteDesignator : Designator
    {
        public DeleteDesignator()
            : base()
        {
            defaultLabel = "MorePlanningExport.PlanDelete".Translate();
            defaultDesc = "MorePlanningExport.PlanDeleteDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("MorePlanningExport/PlanDelete");
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc) => !Plannings.Empty();

        public override void ProcessInput(Event ev) =>
            Find.WindowStack.Add(new FloatMenu(Plannings.GetPlannings().Select(pair => new FloatMenuOption(pair.Key, () => Plannings.Del(pair.Key))).ToList()));
    }
}
