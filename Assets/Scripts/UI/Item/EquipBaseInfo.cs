using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class EquipBaseInfo : MonoBehaviour
{
    [Header("Equip Main")]
    [SerializeField] private Image m_image_equip_icon = null;
    [SerializeField] private Image m_image_equip_hero_type = null;
    [SerializeField] private TMP_Text m_text_equip_name = null;
    [SerializeField] private List<GameObject> m_grade_layout = new List<GameObject>();

    [Header("Unit")]
    [SerializeField] private UnitIcon m_unit_slot = null;

    [Header("Stat")]
    [SerializeField] private TMP_Text m_text_atk = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private TMP_Text m_text_critical = null;
    [SerializeField] private TMP_Text m_text_critical_chance = null;

    public void SetData(long in_unique, int in_kind)
    {
        var equip = Managers.Table.GetEquipInfoData(in_kind);
        if (equip == null)
            return;

        Util.SetEquipIcon(m_image_equip_icon, equip.m_equip_icon);
        Util.SetUnitType(m_image_equip_hero_type, equip.m_hero_type);
        m_text_equip_name.Ex_SetText(equip.m_equip_type.ToString());

        for (int i = 0; i < m_grade_layout.Count; i++)
            m_grade_layout[i].Ex_SetActive(i == (int)equip.m_equip_grade);

        var hero = Managers.User.GetEquipMountHero(in_unique);
        if (hero == null)
        {
            m_unit_slot.Ex_SetActive(false);
        }
        else
        {
            m_unit_slot.Ex_SetActive(true);
            m_unit_slot.SetData(hero.m_kind);
        }

        // Àåºñ ½ºÅÈ
        m_text_atk.Ex_SetText($"{equip.m_atk}");
        m_text_speed.Ex_SetText($"{equip.m_speed}");
        m_text_range.Ex_SetText($"{equip.m_range}");
        m_text_critical.Ex_SetText($"{equip.m_critical}");
        m_text_critical_chance.Ex_SetText($"{equip.m_critical_chance}");
    }
}
