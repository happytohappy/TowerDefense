using UnityEngine;

public class UIPopupPause : UIWindowBase
{
    WaveInfoParam m_param = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupPause;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        m_param = in_param as WaveInfoParam;
        if (m_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        Time.timeScale = 0;
    }

    public void OnClickContinue()
    {
        Managers.UI.CloseLast();

        Time.timeScale = Managers.User.GameSpeed;
    }

    public void OnClickExit()
    {
        Managers.UI.OpenWindow(WindowID.UIWindowGameResult, m_param);
    }
}
