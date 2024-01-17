public class UIWindowMain : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIWindowMain;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    public void OnClickGame()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupGame);
    }
}