using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UIPopupGame : UIWindowBase
{
    [SerializeField] private TMP_Text m_text_stage;
    [SerializeField] private TMP_Text m_text_best_round;
    [SerializeField] private GameObject m_go_front;
    [SerializeField] private GameObject m_go_back;
    [SerializeField] private ExtentionButton m_btn_start;
    [SerializeField] private List<Slot_Reward> m_list_first_reward = new List<Slot_Reward>();
    [SerializeField] private List<Slot_Reward> m_list_repeat_reward = new List<Slot_Reward>();

    private int m_curr_stage;
    private int m_max_stage;
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

        m_curr_stage = Managers.User.UserData.LastClearStage + 1;
        m_max_stage = Managers.Table.GetStageCount();

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
        m_wave_count = Managers.Table.GetWaveCount(m_curr_stage);

        m_text_stage.Ex_SetText($"Stage {string.Format("{0:D2}", m_curr_stage)}");

        var rewardList = Managers.Table.GetStageReward(m_curr_stage);
        if (Managers.User.UserData.LastClearStage > m_curr_stage - 1)
        {
            // 이미 클리어한 스테이지
            m_text_best_round.Ex_SetText($"{string.Format("{0:D2}", m_wave_count)}/{string.Format("{0:D2}", m_wave_count)}");
            m_btn_start.interactable = true;
            for (int i = 0; i < rewardList.Count; i++)
            {
                var reward = rewardList[i];
                m_list_first_reward[i].SetReward(reward.m_first_clear_reward, reward.m_first_clear_reward_amount, true, $"{reward.m_wave}-{m_wave_count}");
            }
        }
        else if (Managers.User.UserData.LastClearStage == m_curr_stage - 1)
        {
            // 현재 도전중인 스테이지
            m_text_best_round.Ex_SetText($"{string.Format("{0:D2}", Managers.User.UserData.LastClearWave)}/{string.Format("{0:D2}", m_wave_count)}");
            m_btn_start.interactable = true;
            for (int i = 0; i < rewardList.Count; i++)
            {
                var reward = rewardList[i];
                var getReward = reward.m_wave <= Managers.User.UserData.LastClearWave;
                m_list_first_reward[i].SetReward(reward.m_first_clear_reward, reward.m_first_clear_reward_amount, getReward, $"{reward.m_wave}-{m_wave_count}");
            }
        }
        else
        {
            // 도전 불가능 스테이지
            m_text_best_round.Ex_SetText($"00/{string.Format("{0:D2}", m_wave_count)}");
            m_btn_start.interactable = false;
            for (int i = 0; i < rewardList.Count; i++)
            {
                var reward = rewardList[i];
                m_list_first_reward[i].SetReward(reward.m_first_clear_reward, reward.m_first_clear_reward_amount, false, $"{reward.m_wave}-{m_wave_count}");
            }
        }

        // 반복 보상
        int index = 0;
        for (int i = 0; i < rewardList.Count; i++)
        {
            var reward = rewardList[i];
            if (reward.m_repeat_clear_reward == 0)
                continue;

            m_list_repeat_reward[index++].SetReward(reward.m_repeat_clear_reward, reward.m_repeat_clear_reward_amount, false, string.Empty);
        }

        m_go_front.Ex_SetActive(m_curr_stage > 1);
        m_go_back.Ex_SetActive(m_curr_stage < m_max_stage);
    }

    public void OnClickStageChange(int in_value)
    {
        // 이전
        if (in_value == 0)
        {
            if (m_curr_stage <= 1)
                return;

            m_curr_stage--;
            SetWaveInfo();
        }
        else if (in_value == 1)
        {
            if (m_curr_stage >= m_max_stage)
                return;

            m_curr_stage++;
            SetWaveInfo();
        }
    }
}
