using UnityEngine;
using Verse;

namespace MorePlanningExportRewritten
{
    public class DialogName : Dialog_Rename
    {
        protected override void SetName(string name) =>
            Plannings.Add(name, GUIUtility.systemCopyBuffer);

        protected override AcceptanceReport NameIsValid(string name) => !Plannings.HasName(name);
    }
}
