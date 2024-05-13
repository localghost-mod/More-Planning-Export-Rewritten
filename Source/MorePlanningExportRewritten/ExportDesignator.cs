using System;
using MorePlanning.Designators;
using UnityEngine;
using Verse;
using static HarmonyLib.AccessTools;

namespace MorePlanningExportRewritten
{
    internal class ExportDesignator : Designator
    {
        static ExportCommand exportCommand = (ExportCommand)TypeByName("MorePlanning.Designators.ExportCommand")?.GetConstructor(new Type[] { }).Invoke(new object[] { });

        public ExportDesignator()
            : base()
        {
            defaultLabel = "MorePlanningExport.PlanExport".Translate();
            defaultDesc = "MorePlanningExport.PlanExportDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("MorePlanningExport/PlanExport");
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc) => true;

        public override void ProcessInput(Event ev)
        {
            exportCommand.OnClick();
            Find.WindowStack.Add(new DialogName());
        }
    }
}
