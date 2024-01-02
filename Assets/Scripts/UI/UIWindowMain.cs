using UnityEngine.SceneManagement;

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
        // 로딩이 필요 없는 경우
        SceneManager.LoadScene(2);

        Managers.UI.OpenWindow(WindowID.UIWindowGame);
    }
}