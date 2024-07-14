using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UIPopupReward : UIWindowBase
{
    [SerializeField] private List<Slot_DailyReward> m_slot_daily_reward = new List<Slot_DailyReward>();
    [SerializeField] private ExtentionButton m_btn_get = null;
    [SerializeField] private ExtentionButton m_btn_ad = null;
    [SerializeField] private ExtentionButton m_btn_buy = null;
    [SerializeField] private GameObject m_go_active = null;
    [SerializeField] private TMP_Text m_text_price = null;

    [Header("튜토리얼")]
    [SerializeField] private GameObject m_go_normal = null;
    [SerializeField] private GameObject m_go_premium = null;

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

        CheckTutorial();
    }

    private void CheckTutorial()
    {
        if (Managers.User.UserData.ClearTutorial.Contains(9))
            return;

        // 일반 보상 설명
        Managers.Tutorial.TutorialStart(m_go_normal, ETutorialDir.Center, new Vector3(0f, 100f, 0f), "#NONE TEXT 일일 보상 설명");
    }

    private void RefreshUI()
    {
        var todayReward = Util.TodayDailyReward();

        m_go_active.Ex_SetActive(Managers.User.UserData.DailyRewardPremium);
        m_btn_buy.interactable = !Managers.User.UserData.DailyRewardPremium;
        m_btn_get.interactable = !todayReward;
        m_btn_ad.interactable = !todayReward;

        int index = 0;
        var repo = Managers.Table.GetAllAttendanceInfoData();
        foreach (var e in repo)
        {         
            m_slot_daily_reward[index].SetDailyReward(e.Key);          
            index++;
        }
    }

    public void OnClickBuy()
    {
        // TODO. 인앱 결제 연동 후 처리에 이게 들어가야 함

        Managers.User.UserData.DailyRewardPremium = true;

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
        }

        RefreshUI();
    }

    public void OnClickGet()
    {
        if (Managers.Tutorial.TutorialProgress)
        {
            Managers.Tutorial.TutorialEnd();

            m_go_premium.Ex_SetActive(true);
            Managers.Tutorial.TutorialStart(m_go_premium, ETutorialDir.Center, new Vector3(0f, 100f, 0f), "#NONE TEXT 프리미엄 설명");
            return;
        }

        Util.AttendanceReward(false);

        RefreshUI();
        Managers.Observer.UpdateObserverRedDot(EContent.Attendance);
    }

    public void OnClickAd()
    {
        Managers.AD.ShowAd(() =>
        {
            Util.AttendanceReward(true);

            RefreshUI();
            Managers.Observer.UpdateObserverRedDot(EContent.Attendance);
        });       
    }

    public void OnClickTutoPremium()
    {
        Managers.Tutorial.TutorialEnd();
        Managers.Tutorial.TutorialClear(9);
    }
}