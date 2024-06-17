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
}