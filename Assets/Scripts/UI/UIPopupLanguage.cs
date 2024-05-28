using UnityEngine;

public class UIPopupLanguage : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIPopupLanguage;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }
}
