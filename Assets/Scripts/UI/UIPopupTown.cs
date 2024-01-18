using UnityEngine;

public class UIPopupTown : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIPopupTown;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }
}
