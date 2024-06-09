using TMPro;
using UnityEngine;

public class Slot_ProbabilityInfo : MonoBehaviour
{
    [Header("ΩΩ∑‘")]
    [SerializeField] private UnitIcon m_slot_hero = null;
    [SerializeField] private Slot_Equip m_slot_equip = null;

    [Header("≈ÿΩ∫∆Æ")]
    [SerializeField] private TMP_Text m_text_name = null;
    [SerializeField] private TMP_Text m_text_probabiliy = null;

    public void SetData(RecruitInfoData in_data)
    {
        var item = Managers.Table.GetItemInfoData(in_data.m_reward_kind);
        if (item == null)
            return;

        m_slot_hero.Ex_SetActive(false);
        m_slot_equip.Ex_SetActive(false);

        switch (item.m_item_type)
        {
            case EItemType.GOLD:
                break;
            case EItemType.RUBY:
                break;
            case EItemType.DIAMOND:
                break;
            case EItemType.ENERGY:
                break;
            case EItemType.AD_REMOVE:
                break;
            case EItemType.HERO:
                m_slot_hero.Ex_SetActive(true);
                m_slot_hero.SetDataRecruit(item.m_kind);
                break;
            case EItemType.EQUIP:
                m_slot_equip.Ex_SetActive(true);
                m_slot_equip.SetData(0, item.m_kind, false , false, false, null);
                break;
            case EItemType.TREASURE:
                break;
            default:
                break;
        }

        m_text_name.Ex_SetText(item.m_title);
        m_text_probabiliy.Ex_SetText($"{in_data.m_rate}%");
    }
}