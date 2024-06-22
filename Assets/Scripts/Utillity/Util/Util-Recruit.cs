using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public static partial class Util
{
    public static List<(EItemType, int)> Recruit(ERecruitType in_recruit_type, int in_count)
    {
        if (in_count == 0)
            return null;

        int recruitKind = 0;
        switch (in_recruit_type)
        {
            case ERecruitType.Normal:  recruitKind = 1000; break;
            case ERecruitType.Premium: recruitKind = 2000; break;
        }

        if (recruitKind == 0)
            return null;

        List<(EItemType, int)> recruitList = new List<(EItemType, int)>();
        for (int i = 0; i < in_count; i++)
        {
            var gachaReward = Managers.Table.GetGachaReward(recruitKind);
            if (gachaReward == null)
                continue;

            var item = Managers.Table.GetItemInfoData(gachaReward.m_item);
            if (item == null)
                continue;

            Managers.User.UpsertHero(gachaReward.m_item);
            recruitList.Add((item.m_item_type, item.m_kind));
        }

        return recruitList;
    }

    public static void EquipSortMerge(ref List<GachaRewardData> in_gacha_reward_list)
    {
        // 1. 유닛, 보물, 장비
        // 2. 확률이 낮은 것부터
        // 3. 확률이 같으면 KIND 내림차순 (높은것부터 낮은 곳으로)

        in_gacha_reward_list.Sort((reward_1, reward_2) =>
        {
            var item_1 = Managers.Table.GetItemInfoData(reward_1.m_item);
            var item_2 = Managers.Table.GetItemInfoData(reward_2.m_item);

            // 첫번째 조건
            if (item_1.m_item_type == EItemType.HERO && item_2.m_item_type != EItemType.HERO)
                return -1;
            else if (item_1.m_item_type != EItemType.HERO && item_2.m_item_type == EItemType.HERO)
                return 1;
            else
            {
                // 두번째 조건
                if (item_1.m_item_type == EItemType.TREASURE && item_2.m_item_type != EItemType.TREASURE)
                    return -1;
                else if (item_1.m_item_type != EItemType.TREASURE && item_2.m_item_type == EItemType.TREASURE)
                    return 1;
                else
                {
                    // 세번째 조건
                    if (item_1.m_item_type == EItemType.EQUIP && item_2.m_item_type != EItemType.EQUIP)
                        return -1;
                    else if (item_1.m_item_type != EItemType.EQUIP && item_2.m_item_type == EItemType.EQUIP)
                        return 1;
                    else
                    {
                        // 네번째 조건
                        if (reward_1.m_rate < reward_2.m_rate)
                            return -1;
                        else if (reward_1.m_rate > reward_2.m_rate)
                            return 1;
                        else
                        {
                            // 다섯번째 조건
                            if (reward_1.m_kind > reward_2.m_kind)
                                return -1;
                            else if (reward_1.m_kind < reward_2.m_kind)
                                return 1;
                            else
                                return 0;
                        }
                    }
                }
            }
        });
    }
}