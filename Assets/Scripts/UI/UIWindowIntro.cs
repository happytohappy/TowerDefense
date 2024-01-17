public class UIWindowIntro : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIWindowIntro;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    public void OnClickMain()
    {
        Managers.UI.Clear();

        LoadingParam param = new LoadingParam();
        param.SceneIndex = 1;
        param.NextWindow = WindowID.UIWindowMain;

        Managers.UI.OpenWindow(WindowID.UIWindowLoading, param);
    }
}