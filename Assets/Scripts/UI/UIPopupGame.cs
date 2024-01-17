using UnityEngine.SceneManagement;

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
        Managers.UI.Clear();

        SceneManager.LoadScene(2);

        Managers.UI.OpenWindow(WindowID.UIWindowGame);
    }
}
