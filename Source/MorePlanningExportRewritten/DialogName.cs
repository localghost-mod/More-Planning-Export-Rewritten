using RimWorld;
using UnityEngine;
using Verse;

namespace MorePlanningExportRewritten
{
    public class DialogName : Dialog_Rename
    {
        protected override void SetName(string name) => Plannings.Add(name, GUIUtility.systemCopyBuffer);

        protected override string Title => "MorePlanningExport.PlanName".Translate();

        protected override AcceptanceReport NameIsValid(string name) => !Plannings.HasName(name);
    }

    public class Dialog_Rename : Window
    {
#if v1_4
        protected Dialog_Rename()
            : base()
#else
        protected Dialog_Rename()
            : base(null)
#endif
        {
            doCloseX = true;
            forcePause = true;
            closeOnAccept = false;
            closeOnClickedOutside = true;
            absorbInputAroundWindow = true;
        }

        protected virtual void SetName(string name) { }

        protected virtual string Title => "Rename".Translate();

        public override Vector2 InitialSize => new Vector2(280f, 175f);

        protected virtual AcceptanceReport NameIsValid(string name) => true;

        public override void DoWindowContents(Rect inRect)
        {
            Text.Font = GameFont.Small;
            bool flag = false;
            if (Event.current.type == EventType.KeyDown)
            {
                Log.Message($"{Event.current.keyCode}");
            }
            if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
            {
                flag = true;
                Event.current.Use();
            }
            Rect rect = new Rect(inRect);
            Text.Font = GameFont.Medium;
            rect.height = Text.LineHeight + 10f;
            Widgets.Label(rect, Title);
            Text.Font = GameFont.Small;
            GUI.SetNextControlName("RenameField");
            string text = Widgets.TextField(new Rect(0f, rect.height, inRect.width, 35f), curName);
            if (text.Length < 100)
                curName = text;
            else
                ((TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl)).SelectAll();

            if (!focusedRenameField)
            {
                UI.FocusControl("RenameField", this);
                focusedRenameField = true;
            }
            if (Widgets.ButtonText(new Rect(15f, inRect.height - 35f - 10f, inRect.width - 15f - 15f, 35f), "Confirm".Translate()) || flag)
            {
                AcceptanceReport acceptanceReport = NameIsValid(curName);
                if (!acceptanceReport.Accepted)
                {
                    if (acceptanceReport.Reason.NullOrEmpty())
                    {
                        Messages.Message("NameIsInvalid".Translate(), MessageTypeDefOf.RejectInput, false);
                        return;
                    }
                    Messages.Message(acceptanceReport.Reason, MessageTypeDefOf.RejectInput, false);
                    return;
                }
                else
                {
                    SetName(curName);
                    Find.WindowStack.TryRemove(this, true);
                }
            }
        }

        public string curName;
        public bool focusedRenameField;
    }
}
