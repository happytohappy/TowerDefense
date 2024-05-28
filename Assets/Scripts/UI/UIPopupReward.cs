using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UserManager;

public class UIPopupReward : UIWindowBase
{
    [SerializeField] private List<Slot_DailyReward> m_slot_daily_reward = new List<Slot_DailyReward>();
    [SerializeField] private ExtentionButton m_btn_get = null;
    [SerializeField] private ExtentionButton m_btn_ad = null;
    [SerializeField] private ExtentionButton m_btn_buy = null;
    [SerializeField] private GameObject m_go_active = null;
    [SerializeField] private TMP_Text m_text_price = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupReward;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        RefreshUI();
    }

    private void RefreshUI()
    {
        m_go_active.Ex_SetActive(Managers.User.UserData.DailyRewardPremium);
        m_btn_buy.interactable = !Managers.User.UserData.DailyRewardPremium;
        m_btn_get.interactable = true;
        m_btn_ad.interactable = true;

        var serverDate = Managers.BackEnd.ServerDateTime();
        if (Managers.User.UserData.DailyRewardDateTime.Year == serverDate.Year && Managers.User.UserData.DailyRewardDateTime.Month == serverDate.Month && Managers.User.UserData.DailyRewardDateTime.Day == serverDate.Day)
        {
            m_btn_get.interactable = false;
            m_btn_ad.interactable = false;
        }
        else
        {
            m_btn_get.interactable = true;
            m_btn_ad.interactable = true;
        }

        int index = 0;
        var repo = Managers.Table.GetAllAttendanceInfoData();
        foreach (var e in repo)
        {         
            m_slot_daily_reward[index].SetDailyReward(e.Key);          
            index++;
        }
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void OnClickBuy()
    {
        Managers.User.UserData.DailyRewardPremium = true;

        bool todayReward = false;
        var serverDate = Managers.BackEnd.ServerDateTime();
        if (Managers.User.UserData.DailyRewardDateTime.Year == serverDate.Year && Managers.User.UserData.DailyRewardDateTime.Month == serverDate.Month && Managers.User.UserData.DailyRewardDateTime.Day == serverDate.Day)
            todayReward = true;

        var repo = Managers.Table.GetAllAttendanceInfoData();
        foreach (var e in repo)
        {
            if (e.Key < Managers.User.UserData.DailyRewardKIND)
            {
                var attendanceData = Managers.Table.GetAttendanceInfoData(e.Key);
                if (attendanceData == null)
                    continue;

                Managers.User.UpsertInventoryItem(attendanceData.m_premium_reward_kind, attendanceData.m_premium_reward_amount);
            }

            if (todayReward)
            {
                if (e.Key == Managers.User.UserData.DailyRewardKIND)
                {
                    var attendanceData = Managers.Table.GetAttendanceInfoData(e.Key);
                    if (attendanceData == null)
                        continue;

                    Managers.User.UpsertInventoryItem(attendanceData.m_premium_reward_kind, attendanceData.m_premium_reward_amount);
                }
            }
        }

        MainGoodsRefresh();
        RefreshUI();
    }

    public void OnClickGet()
    {
        var serverDate = Managers.BackEnd.ServerDateTime();
        if (Managers.User.UserData.DailyRewardDateTime.Year == serverDate.Year && Managers.User.UserData.DailyRewardDateTime.Month == serverDate.Month && Managers.User.UserData.DailyRewardDateTime.Day == serverDate.Day)
            return;

        var attendanceData = Managers.Table.GetAttendanceInfoData(Managers.User.UserData.DailyRewardKIND);
        if (attendanceData == null)
            return;

        Managers.User.UpsertInventoryItem(attendanceData.m_free_reward_kind, attendanceData.m_free_reward_amount);

        if (Managers.User.UserData.DailyRewardPremium)
            Managers.User.UpsertInventoryItem(attendanceData.m_premium_reward_kind, attendanceData.m_premium_reward_amount);

        Managers.User.UserData.DailyRewardDateTime = serverDate;

        if (Managers.User.UserData.DailyRewardKIND == 7)
            Managers.User.UserData.DailyAllReward = true;

        MainGoodsRefresh();
        RefreshUI();
    }

    public void OnClickAd()
    {
        var serverDate = Managers.BackEnd.ServerDateTime();
        if (Managers.User.UserData.DailyRewardDateTime.Year == serverDate.Year && Managers.User.UserData.DailyRewardDateTime.Month == serverDate.Month && Managers.User.UserData.DailyRewardDateTime.Day == serverDate.Day)
            return;

        var attendanceData = Managers.Table.GetAttendanceInfoData(Managers.User.UserData.DailyRewardKIND);
        if (attendanceData == null)
            return;

        Managers.AD.ShowAd(() =>
        {
            Managers.User.UpsertInventoryItem(attendanceData.m_free_reward_kind, attendanceData.m_free_reward_amount * 2);

            if (Managers.User.UserData.DailyRewardPremium)
                Managers.User.UpsertInventoryItem(attendanceData.m_premium_reward_kind, attendanceData.m_premium_reward_amount * 2);

            Managers.User.UserData.DailyRewardDateTime = serverDate;

            if (Managers.User.UserData.DailyRewardKIND == 7)
                Managers.User.UserData.DailyAllReward = true;

            MainGoodsRefresh();
            RefreshUI();
        });       
    }

    private void MainGoodsRefresh()
    {
        var gui = Managers.UI.GetWindow(WindowID.UIWindowMain, false) as UIWindowMain;
        if (gui != null)
            gui.RefreshUI();
    }
}
