using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIWindowGameResult : UIWindowBase
{
    private const string SLOT_REWARD_PATH = "UI/Item/Slot_PopupSynergy";

    [SerializeField] private TMP_Text m_text_wave_score = null;
    [SerializeField] private GameObject m_go_reward_none = null;
    [SerializeField] private Transform m_trs_root = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowGameResult;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        var param = in_param as WaveInfoParam;
        if (param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        m_text_wave_score.Ex_SetText($"{string.Format("{0:D2}", param.m_curr_wave - 1)} / {string.Format("{0:D2}", param.m_max_wave)}");
    }

    public void OnClickRetry()
    {
        Time.timeScale = Managers.User.UserData.GameSpeed;

        Managers.UI.Clear();

        Managers.UI.OpenWindow(WindowID.UIWindowGame);

        GameController.GetInstance.StageInit();
    }

    public void OnClickCancel()
    {
        Time.timeScale = 1;

        GameController.GetInstance.AllDestory();

        Managers.UI.Clear();

        SceneManager.LoadScene(1);

        Managers.UI.OpenWindow(WindowID.UIWindowMain);
    }

    public void OnClickNext()
    {
        Time.timeScale = Managers.User.UserData.GameSpeed;

        Debug.LogError("OnClickNext");
    }
}