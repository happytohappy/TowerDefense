using UnityEngine;

public class UIPopupPause : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIPopupPause;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        Time.timeScale = 0;
    }

    public void OnClickContinue()
    {
        Managers.UI.CloseLast();

        Time.timeScale = 1;
    }

    public void OnClickExit()
    {
        Managers.UI.OpenWindow(WindowID.UIWindowGameResult);
    }
}
