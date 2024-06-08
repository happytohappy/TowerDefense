using UnityEngine.UI;
using System.Collections.Generic;

public static partial class Util
{
    public static void SetEquipIcon(Image in_image, string in_name)
    {
        in_image.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Equip, in_name));
    }

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

    public static void SetEquipGradeTextImage(Image in_grade_bg, EEquipGrade in_grade)
    {
        switch (in_grade)
        {
            case EEquipGrade.F:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_F"));
                break;
            case EEquipGrade.E:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_E"));
                break;
            case EEquipGrade.D:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_D"));
                break;
            case EEquipGrade.C:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_C"));
                break;
            case EEquipGrade.B:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_B"));
                break;
            case EEquipGrade.A:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_A"));
                break;
            case EEquipGrade.SS:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_SS"));
                break;
            case EEquipGrade.SR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_SR"));
                break;
            case EEquipGrade.SSR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_SSR"));
                break;
        }
    }

    public static void SetEquipGradeBG(Image in_grade_bg, EEquipGrade in_grade)
    {
        switch (in_grade)
        {
            case EEquipGrade.F:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_F"));
                break;
            case EEquipGrade.E:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_E"));
                break;
            case EEquipGrade.D:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_D"));
                break;
            case EEquipGrade.C:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_C"));
                break;
            case EEquipGrade.B:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_B"));
                break;
            case EEquipGrade.A:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_A"));
                break;
            case EEquipGrade.SS:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_SS"));
                break;
            case EEquipGrade.SR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_SR"));
                break;
            case EEquipGrade.SSR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_SSR"));
                break;
        }
    }

    public static void EquipSort(ref List<UserManager.EquipInfo> in_equip_list, long in_unique_id = 0)
    {
        in_equip_list.Sort((equip_1, equip_2) =>
        {
            // 첫번째 조건
            if (equip_1.m_unique_id == in_unique_id && equip_2.m_unique_id != in_unique_id)
                return -1;
            else if (equip_1.m_unique_id != in_unique_id && equip_2.m_unique_id == in_unique_id)
                return 1;
            else
            {
                // 두번째 조건 : 장착중인 장비
                if (equip_1.m_unique_id > 0 && equip_2.m_unique_id == 0)
                    return -1;
                else if (equip_1.m_unique_id == 0 && equip_2.m_unique_id > 0)
                    return 1;
                else
                {
                    // 세번째 조건 : 뉴 마크
                    if (equip_1.m_new && !equip_2.m_new)
                        return -1;
                    else if (!equip_1.m_new && equip_2.m_new)
                        return 1;
                    else
                    {
                        // 네번째 조건 : 장비 등급
                        if (equip_1.m_grade > equip_2.m_grade)
                            return -1;
                        else if (equip_1.m_grade < equip_2.m_grade)
                            return 1;
                        else
                        {
                            // 다섯번째 조건 : 카인드
                            if (equip_1.m_kind < equip_2.m_kind)
                                return -1;
                            if (equip_1.m_kind > equip_2.m_kind)
                                return 1;
                            else
                            {
                                // 여섯번째 조건 : 장비 번호
                                if (equip_1.m_unique_id < equip_2.m_unique_id)
                                    return -1;
                                if (equip_1.m_unique_id > equip_2.m_unique_id)
                                    return 1;
                                else
                                    return 0;
                            }
                        }
                    }
                }
            }
        });
    }

    public static void EquipSortMerge(ref List<UserManager.EquipInfo> in_equip_list, long in_unique_id = 0)
    {
        in_equip_list.Sort((equip_1, equip_2) =>
        {
            // 첫번째 조건
            if (equip_1.m_unique_id == in_unique_id && equip_2.m_unique_id != in_unique_id)
                return -1;
            else if (equip_1.m_unique_id != in_unique_id && equip_2.m_unique_id == in_unique_id)
                return 1;
            else
            {
                // 두번째 조건 : 장비 등급
                if (equip_1.m_grade > equip_2.m_grade)
                    return -1;
                else if (equip_1.m_grade < equip_2.m_grade)
                    return 1;
                else
                {
                    // 세번째 조건 : 카인드
                    if (equip_1.m_kind < equip_2.m_kind)
                        return -1;
                    if (equip_1.m_kind > equip_2.m_kind)
                        return 1;
                    else
                    {
                        // 네섯번째 조건 : 장비 번호
                        if (equip_1.m_unique_id < equip_2.m_unique_id)
                            return -1;
                        if (equip_1.m_unique_id > equip_2.m_unique_id)
                            return 1;
                        else
                            return 0;
                    }
                }
            }
        });
    }
}