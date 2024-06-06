using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public static partial class Util
{
    public static float GetEquipStat(int in_hero_kind, EStat in_stat)
    {
        var userHero = Managers.User.GetUserHeroInfo(in_hero_kind);
        if (userHero == null)
            return 0f;

        var userEquip = Managers.User.GetEquip(userHero.m_equip_id);
        if (userEquip == null)
            return 0f;

        var tableEquip = Managers.Table.GetEquipInfoData(userEquip.m_kind);
        if (tableEquip == null)
            return 0f;

        var result = 0f;
        switch (in_stat)
        {
            case EStat.ATK:
                result = tableEquip.m_atk;
                break;
            case EStat.Speed:
                result = tableEquip.m_speed;
                break;
            case EStat.Range:
                result = tableEquip.m_range;
                break;
            case EStat.Critical:
                result = tableEquip.m_critical;
                break;
            case EStat.CriticalChance:
                result = tableEquip.m_critical_chance;
                break;
        }

        return result;
    }
}