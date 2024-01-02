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
        // 로딩이 필요할 경우
        LoadingParam param = new LoadingParam();
        param.SceneIndex = 1;
        param.NextWindow = WindowID.UIWindowMain;

        Managers.UI.OpenWindow(WindowID.UIWindowLoading, param);
    }
}