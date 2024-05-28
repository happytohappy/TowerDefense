using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_DailyReward : MonoBehaviour
{
    [SerializeField] private GameObject m_go_active = null;
    [SerializeField] private Slot_Reward m_slot_free = null;
    [SerializeField] private Slot_Reward m_slot_premium = null;
    [SerializeField] private TMP_Text m_text_day = null;

    public void SetDailyReward(int in_daily_reward_kind)
    {
        var attendanceData = Managers.Table.GetAttendanceInfoData(in_daily_reward_kind);
        m_slot_free.SetReward(attendanceData.m_free_reward_kind, attendanceData.m_free_reward_amount, false, string.Empty);
        m_slot_premium.SetReward(attendanceData.m_premium_reward_kind, attendanceData.m_premium_reward_amount, false, string.Empty);
        m_text_day.Ex_SetText(in_daily_reward_kind.ToString());
        m_go_active.Ex_SetActive(false);
        m_slot_premium.SetLock(!Managers.User.UserData.DailyRewardPremium);

        if (in_daily_reward_kind == Managers.User.UserData.DailyRewardKIND)
        {
            var serverDate = Managers.BackEnd.ServerDateTime();
            if (Managers.User.UserData.DailyRewardDateTime.Year == serverDate.Year && Managers.User.UserData.DailyRewardDateTime.Month == serverDate.Month && Managers.User.UserData.DailyRewardDateTime.Day == serverDate.Day)
            {
                m_go_active.Ex_SetActive(false);
                m_slot_free.SetCheck(true);
                m_slot_premium.SetCheck(Managers.User.UserData.DailyRewardPremium);
                m_slot_premium.SetLock(!Managers.User.UserData.DailyRewardPremium);
            }
            else
            {
                m_go_active.Ex_SetActive(true);
                m_slot_free.SetCheck(false);
                m_slot_premium.SetCheck(false);
                m_slot_premium.SetLock(!Managers.User.UserData.DailyRewardPremium);
            }
        }
        else if (in_daily_reward_kind < Managers.User.UserData.DailyRewardKIND)
        {
            m_go_active.Ex_SetActive(false);
            m_slot_free.SetCheck(true);
            m_slot_premium.SetCheck(Managers.User.UserData.DailyRewardPremium);
            m_slot_premium.SetLock(!Managers.User.UserData.DailyRewardPremium);
        }
        else
        {
            m_go_active.Ex_SetActive(false);
            m_slot_free.SetCheck(false);
            m_slot_premium.SetCheck(false);
            m_slot_premium.SetLock(!Managers.User.UserData.DailyRewardPremium);
        }
    }
}