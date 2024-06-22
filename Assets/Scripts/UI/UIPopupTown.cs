using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[Serializable]
public class RewardGroup
{
    public ETownType m_town_type;
    public GameObject m_root;
    public TMP_Text m_title;
    public Slot_Reward m_slot_reward;
    public TMP_Text m_contents;
}

public class UIPopupTown : UIWindowBase
{
    [SerializeField] List<RewardGroup> m_reward_group = new List<RewardGroup>();
    [SerializeField] private GameObject m_go_level;
    [SerializeField] private GameObject m_go_ad;
    [SerializeField] private Image m_image_level_icon;
    [SerializeField] private TMP_Text m_text_level_cost;
    [SerializeField] private GameObject m_go_normal_time;
    [SerializeField] private TMP_Text m_text_normal_time;
    [SerializeField] private GameObject m_go_ad_time;
    [SerializeField] private TMP_Text m_text_ad_time;
    [SerializeField] private ExtentionButton m_btn_level;
    [SerializeField] private ExtentionButton m_btn_normal;
    [SerializeField] private ExtentionButton m_btn_ad;

    private TownParam m_param;
    private bool m_is_reward;
    private WaitForSeconds m_wait = new WaitForSeconds(1.0f);

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupTown;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        if (in_param == null)
        {
            OnClickClose();
            return;
        }

        m_param = in_param as TownParam;
        if (m_param == null)
        {
            OnClickClose();
            return;
        }

        UIRefresh();
    }

    private void UIRefresh()
    {
        StopAllCoroutines();

        var townInfo = Managers.Table.GetTownInfoData((int)m_param.m_town_type);
        if (townInfo == null)
        {
            OnClickClose();
            return;
        }

        if (Managers.User.UserData.Town.TryGetValue(m_param.m_town_type, out var userData) == false)
        {
            OnClickClose();
            return;
        }

        var townLevel = Managers.Table.GetTownLevelDataByLevel((int)m_param.m_town_type, userData.m_town_level);
        if (townLevel == null)
        {
            OnClickClose();
            return;
        }

        var maxLevel = Managers.Table.GetTownMaxLevel((int)m_param.m_town_type);
        if (userData.m_town_level >= maxLevel)
        {
            m_go_level.Ex_SetActive(false);
        }
        else
        {
            var townNextLevel = Managers.Table.GetTownLevelDataByLevel((int)m_param.m_town_type, userData.m_town_level + 1);
            m_image_level_icon.Ex_SetImage(Util.GetResourceImage(townNextLevel.m_level_up_kind));
            m_text_level_cost.Ex_SetText($"{Util.CommaText(townNextLevel.m_level_up_cost)}");
            m_go_level.Ex_SetActive(true);

            m_btn_level.interactable = Managers.User.GetInventoryItem(townNextLevel.m_level_up_kind) >= townNextLevel.m_level_up_cost;
        }

        var nowTime = Util.UnixTimeNow();
        if (nowTime >= userData.m_last_reward_time + townLevel.m_timespan)
        {
            m_is_reward = true;
            m_go_normal_time.Ex_SetActive(false);
            m_go_ad_time.Ex_SetActive(false);
            m_btn_normal.interactable = true;
            m_btn_ad.interactable = true;
        }
        else
        {
            m_is_reward = false;
            m_go_normal_time.Ex_SetActive(true);
            m_go_ad_time.Ex_SetActive(true);
            m_btn_normal.interactable = false;
            m_btn_ad.interactable = false;

            StartCoroutine(CoRewardTime(userData, townLevel));
        }

        foreach (var e in m_reward_group)
        {
            if (e.m_town_type == m_param.m_town_type)
            {
                e.m_root.Ex_SetActive(true);
                e.m_title.Ex_SetText(string.Format(Util.SpecialString(Managers.Table.GetLanguage(townInfo.m_title)), userData.m_town_level));
                e.m_slot_reward.SetAmount(townLevel.m_reward_amount);

                var str = Util.RemainingToDate(townLevel.m_timespan);
                switch (e.m_town_type)
                {
                    case ETownType.Gold:
                    case ETownType.Ruby:
                    case ETownType.Dia:
                        e.m_contents.Ex_SetText(string.Format(Util.SpecialString(Managers.Table.GetLanguage(townInfo.m_contents)), str, townLevel.m_reward_amount));
                        break;
                    case ETownType.Unit:
                    case ETownType.Equip:
                        e.m_contents.Ex_SetText(string.Format(Util.SpecialString(Managers.Table.GetLanguage(townInfo.m_contents)), str));
                        break;
                }               
            }
            else
            {
                e.m_root.Ex_SetActive(false);
            }
        }

        // 광고 2배 얻기가 있는지
        m_go_ad.Ex_SetActive(townInfo.m_ad_reward);
    }

    private IEnumerator CoRewardTime(UserManager.TownInfo in_user_info, TownLevelData in_town_level)
    {
        while (true)
        {
            var nowTime = Managers.BackEnd.ServerTimeGetUTCTimeStamp();
            if (nowTime >= in_user_info.m_last_reward_time + in_town_level.m_timespan)
                break;

            var str = Util.RemainingToDate(in_user_info.m_last_reward_time + in_town_level.m_timespan - nowTime);
            m_text_normal_time.Ex_SetText(str);
            m_text_ad_time.Ex_SetText(str);

            yield return m_wait;
        }

        UIRefresh();
    }


    public void OnClickLevelUP()
    {
        if (Managers.User.UserData.Town.TryGetValue(m_param.m_town_type, out var userData) == false)
            return;

        var townNextLevel = Managers.Table.GetTownLevelDataByLevel((int)m_param.m_town_type, userData.m_town_level + 1);
        if (townNextLevel == null)
            return;

        if (Managers.User.GetInventoryItem(townNextLevel.m_level_up_kind) < townNextLevel.m_level_up_cost)
            return;

        Managers.User.UpsertInventoryItem(townNextLevel.m_level_up_kind, -townNextLevel.m_level_up_cost);
        userData.m_town_level++;

        UIRefresh();
    }

    public void OnClickReward(bool in_double)
    {
        if (m_is_reward == false)
            return;

        if (Managers.User.UserData.Town.TryGetValue(m_param.m_town_type, out var userData) == false)
            return;

        var townLevel = Managers.Table.GetTownLevelDataByLevel((int)m_param.m_town_type, userData.m_town_level);
        if (townLevel == null)
            return;

        userData.m_last_reward_time = Managers.BackEnd.ServerTimeGetUTCTimeStamp();

        switch (m_param.m_town_type)
        {
            case ETownType.Gold:
            case ETownType.Ruby:
            case ETownType.Dia:
                {
                    Managers.User.UpsertInventoryItem(townLevel.m_reward_kind, in_double ? townLevel.m_reward_amount * 2 : townLevel.m_reward_amount);
                }
                break;
            case ETownType.Unit:
                {

                }
                break;
            case ETownType.Equip:
                {

                }
                break;
        }

        UIRefresh();
    }
}
