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

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    public void OnClickRetry()
    {
        Debug.LogError("OnClickRetry");
    }

    public void OnClickCancel()
    {
        GameController.GetInstance.AllDestory();

        Managers.UI.Clear();

        SceneManager.LoadScene(1);

        Managers.UI.OpenWindow(WindowID.UIWindowMain);
    }

    public void OnClickNext()
    {
        Debug.LogError("OnClickNext");
    }
}