using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIWindowGameResult : UIWindowBase
{
    private const string SLOT_REWARD_PATH = "UI/Item/Slot_Reward";

    [SerializeField] private TMP_Text m_text_wave_score = null;
    [SerializeField] private GameObject m_go_reward_none = null;
    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private GameObject m_go_next = null;

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
        m_go_next.Ex_SetActive(Managers.User.UserData.LastClearStage >= Managers.User.SelectStage || param.m_curr_wave - 1 == param.m_max_wave);

        if (GameController.GetInstance.GetRewardData.Count == 0)
        {
            m_go_reward_none.Ex_SetActive(true);
        }
        else
        {
            m_go_reward_none.Ex_SetActive(false);

            for (int i = 0; i < GameController.GetInstance.GetRewardData.Count; i++)
            {
                var reward = GameController.GetInstance.GetRewardData[i];

                var slot = Managers.Resource.Instantiate(SLOT_REWARD_PATH, Vector3.zero, m_trs_root);
                var sc = slot.GetComponent<Slot_Reward>();

                sc.SetReward(reward.m_reward, reward.m_amount, false, reward.m_text);
            }
        }
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