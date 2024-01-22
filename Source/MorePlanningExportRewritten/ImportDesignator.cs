using System;
using System.Linq;
using MorePlanning.Designators;
using UnityEngine;
using Verse;
using static HarmonyLib.AccessTools;

namespace MorePlanningExportRewritten
{
    internal class ImportDesignator : Designator
    {
        public ImportDesignator()
            : base()
        {
            defaultLabel = "MorePlanningExport.PlanImport".Translate();
            defaultDesc = "MorePlanningExport.PlanImportDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("MorePlanningExport/PlanImport");
        }

        public static ImportCommand importCommand = (ImportCommand)
            TypeByName("MorePlanning.Designators.ImportCommand")
                ?.GetConstructor(new Type[] { })
                .Invoke(new object[] { });

        public override AcceptanceReport CanDesignateCell(IntVec3 loc) => !Plannings.Empty();

        public override void ProcessInput(Event ev)
        {
            Find.WindowStack.Add(
                new FloatMenu(
                    Plannings
                        .GetPlannings()
                        .Select(
                            pair =>
                                new FloatMenuOption(
                                    pair.Key,
                                    () =>
                                    {
                                        GUIUtility.systemCopyBuffer = pair.Value;
                                        importCommand.OnClick();
                                    }
                                )
                        )
                        .ToList()
                )
            );
        }
    }
}
