using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using static UserManager;

public class Slot_PopupQuest : MonoBehaviour
{
    [SerializeField] private Slot_Reward m_slot_reward = null;
    [SerializeField] private TMP_Text m_text_title = null;
    [SerializeField] private Slider m_slider_progress = null;
    [SerializeField] private TMP_Text m_text_progress = null;
    [SerializeField] private ExtentionButton m_btn_get = null;

    private EQuestType m_quest_type;
    private MissionStageData in_stage_data;
    private MissionAchievementData m_achievement_data;
    private bool m_is_reward;

    public void SetData(MissionStageData in_data)
    {
        m_quest_type = EQuestType.Stage;
        in_stage_data = in_data;
        m_is_reward = false;

        m_slot_reward.SetReward(in_data.m_reward_item_kind, in_data.m_reward_item_amound, false, string.Empty);

        if (in_data.m_value_2 == 0)
            m_text_title.Ex_SetText(string.Format(Util.SpecialString(Managers.Table.GetLanguage(in_data.m_desc)), in_data.m_value_1));
        else
            m_text_title.Ex_SetText(string.Format(Util.SpecialString(Managers.Table.GetLanguage(in_data.m_desc)), in_data.m_value_1, in_data.m_value_2));

        switch (in_data.m_mission_condition)
        {
            case EMissionCondition.HERO_MERGE:
                {
                    var totalCnt = 0;
                    for (int i = in_data.m_value_1; i <= 8; i++)
                    {
                        if (GameController.GetInstance.MergeCount.ContainsKey(i))
                            totalCnt += GameController.GetInstance.MergeCount[i];
                    }
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", totalCnt)}/{string.Format("{0:D2}", in_data.m_value_2)}");
                    m_slider_progress.Ex_SetValue((float)totalCnt / in_data.m_value_2);
                    m_btn_get.interactable = totalCnt >= in_data.m_value_2;
                    m_is_reward = totalCnt >= in_data.m_value_2;
                }
                break;
            case EMissionCondition.KILL_MONSTER:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", GameController.GetInstance.MonsterKillCount)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)GameController.GetInstance.MonsterKillCount / in_data.m_value_1);
                    m_btn_get.interactable = GameController.GetInstance.MonsterKillCount >= in_data.m_value_1;
                    m_is_reward = GameController.GetInstance.MonsterKillCount >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.HERO_SAME:
                {
                    Dictionary<int, int> heroCount = new Dictionary<int, int>();
                    foreach (var e in GameController.GetInstance.LandInfo)
                    {
                        if (e.m_build == false)
                            continue;

                        var kind = e.m_hero.GetHeroData.m_info.m_kind;
                        if (heroCount.ContainsKey(kind))
                            heroCount[kind]++;
                        else
                            heroCount.Add(kind, 1);
                    }

                    var maxCount = 0;
                    foreach (var e in heroCount)
                    {
                        if (e.Value > maxCount)
                            maxCount = e.Value;
                    }

                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", maxCount)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)maxCount / in_data.m_value_1);
                    m_btn_get.interactable = maxCount >= in_data.m_value_1;
                    m_is_reward = maxCount >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.HERO_DIFF:
                {
                    HashSet<int> heroClass = new HashSet<int>();
                    foreach (var e in GameController.GetInstance.LandInfo)
                    {
                        if (e.m_build == false)
                            continue;

                        var kind = e.m_hero.GetHeroData.m_info.m_kind;
                        if (!heroClass.Contains(kind))
                            heroClass.Add(kind);
                    }

                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", heroClass.Count)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)heroClass.Count / in_data.m_value_1);
                    m_btn_get.interactable = heroClass.Count >= in_data.m_value_1;
                    m_is_reward = heroClass.Count >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.HERO_TIER:
                {
                    var heroCount = 0;
                    foreach (var e in GameController.GetInstance.LandInfo)
                    {
                        if (e.m_build == false)
                            continue;

                        if (in_data.m_value_1 == e.m_hero.GetHeroData.m_info.m_tier)
                            heroCount++;
                    }

                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", heroCount)}/{string.Format("{0:D2}", in_data.m_value_2)}");
                    m_slider_progress.Ex_SetValue((float)heroCount / in_data.m_value_2);
                    m_btn_get.interactable = heroCount >= in_data.m_value_2;
                    m_is_reward = heroCount >= in_data.m_value_2;
                }
                break;
            case EMissionCondition.KILL_BOSS:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", GameController.GetInstance.BossKillCount)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)GameController.GetInstance.BossKillCount / in_data.m_value_1);
                    m_btn_get.interactable = GameController.GetInstance.BossKillCount >= in_data.m_value_1;
                    m_is_reward = GameController.GetInstance.BossKillCount >= in_data.m_value_1;
                }
                break;
        }
    }

    public void SetData(MissionAchievementData in_data)
    {
        m_quest_type = EQuestType.Achievement;
        m_achievement_data = in_data;
        m_is_reward = false;

        m_slot_reward.SetReward(in_data.m_reward_item_kind, in_data.m_reward_item_amound, false, string.Empty);

        if (in_data.m_value_2 == 0)
            m_text_title.Ex_SetText(string.Format(Util.SpecialString(Managers.Table.GetLanguage(in_data.m_desc)), in_data.m_value_1));
        else
            m_text_title.Ex_SetText(string.Format(Util.SpecialString(Managers.Table.GetLanguage(in_data.m_desc)), in_data.m_value_1, in_data.m_value_2));

        switch (in_data.m_mission_condition)
        {
            case EMissionCondition.HERO_LEVEL:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_2)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_2);
                    m_btn_get.interactable = 0 >= in_data.m_value_2;
                    m_is_reward = 0 >= in_data.m_value_2;
                }
                break;
            case EMissionCondition.VILLAGE:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_1);
                    m_btn_get.interactable = 0 >= in_data.m_value_1;
                    m_is_reward = 0 >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.EQUIP_MERGE:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_1);
                    m_btn_get.interactable = 0 >= in_data.m_value_1;
                    m_is_reward = 0 >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.GACHA_NORMAL:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_1);
                    m_btn_get.interactable = 0 >= in_data.m_value_1;
                    m_is_reward = 0 >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.GACHA_PREMIUM:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_1);
                    m_btn_get.interactable = 0 >= in_data.m_value_1;
                    m_is_reward = 0 >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.CLEAR_STAGE:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_1);
                    m_btn_get.interactable = 0 >= in_data.m_value_1;
                    m_is_reward = 0 >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.CLEAR_INFINITE:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_1);
                    m_btn_get.interactable = 0 >= in_data.m_value_1;
                    m_is_reward = 0 >= in_data.m_value_1;
                }
                break;
            case EMissionCondition.KILL_BOSS:
                {
                    m_text_progress.Ex_SetText($"{string.Format("{0:D2}", 0)}/{string.Format("{0:D2}", in_data.m_value_1)}");
                    m_slider_progress.Ex_SetValue((float)0 / in_data.m_value_1);
                    m_btn_get.interactable = 0 >= in_data.m_value_1;
                    m_is_reward = 0 >= in_data.m_value_1;
                }
                break;
        }
    }

    public void OnClickReward()
    {
        switch (m_quest_type)
        {
            case EQuestType.Stage:
                {
                    if (in_stage_data == null || m_is_reward == false || GameController.GetInstance.ClearMission.Contains(in_stage_data.m_index))
                        return;

                    Managers.User.UpsertInventoryItem(in_stage_data.m_reward_item_kind, in_stage_data.m_reward_item_amound);
                    GameController.GetInstance.ClearMission.Add(in_stage_data.m_index);

                    var gui = Managers.UI.GetWindow(WindowID.UIPopupQuest, false) as UIPopupQuest;
                    if (gui != null)
                        gui.RefreshUI_Stage();
                }
                break;
            case EQuestType.Achievement:
                {
                    if (m_achievement_data == null)
                        return;

                    Managers.User.UpsertInventoryItem(m_achievement_data.m_reward_item_kind, m_achievement_data.m_reward_item_amound);

                    var gui = Managers.UI.GetWindow(WindowID.UIPopupQuest, false) as UIPopupQuest;
                    if (gui != null)
                        gui.RefreshUI_Achievement();
                }
                break;
        }
    }
}