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
    }

    public void OnClickContinue()
    {
        Managers.UI.CloseLast();
    }

    public void OnClickExit()
    {
        Managers.UI.OpenWindow(WindowID.UIWindowGameResult);
    }
}
