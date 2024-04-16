using TMPro;
using UnityEngine;

public class UIPopupGame : UIWindowBase
{
    [SerializeField] private TMP_Text m_text_best_round;

    private int m_curr_stage;
    private int m_wave_count;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupGame;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        m_curr_stage = 1;
        m_wave_count = Managers.Table.GetWaveCount(m_curr_stage);

        SetWaveInfo();
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

    public void SetWaveInfo()
    {
        m_text_best_round.Ex_SetText($"{string.Format("{0:D2}", Managers.User.UserData.LastClearWave)}/{string.Format("{0:D2}", m_wave_count)}");
    }
}
