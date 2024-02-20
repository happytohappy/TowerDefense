public class UIPopupGame : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIPopupGame;
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

    public void OnClickGame()
    {
        Managers.UI.CloseLast();

        LoadingParam param = new LoadingParam();
        param.SceneIndex = 2;
        param.NextWindow = WindowID.UIWindowGame;

        Managers.UI.OpenWindow(WindowID.UIWindowLoading, param);
    }
}
