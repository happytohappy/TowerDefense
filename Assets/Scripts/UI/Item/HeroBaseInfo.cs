using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HeroBaseInfo : MonoBehaviour
{
    [Header("유닛 정보")]
    [SerializeField] private TMP_Text m_text_name = null;                           
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private Image m_Image_hero = null;
    [SerializeField] private Image m_img_unit_type = null;
    [SerializeField] private GameObject m_go_lock = null;                             
    [SerializeField] private GameObject m_go_equipment_empty = null;
    [SerializeField] private List<Image> m_list_grade_star = new List<Image>();
    [SerializeField] private List<GameObject> m_list_bg = new List<GameObject>();

    [Header("착용 장비")]
    [SerializeField] private Slot_Equip m_slot_equip = null;

    [Header("스킬")]
    [SerializeField] private List<Slot_Skill> m_slot_skill = new List<Slot_Skill>();

    [Header("유닛 스탯")]
    [SerializeField] private TMP_Text m_text_damage = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private TMP_Text m_text_critical = null;
    [SerializeField] private TMP_Text m_text_critical_chance = null;

    [Header("장비 스탯")]
    [SerializeField] private TMP_Text m_text_equip_damage = null;
    [SerializeField] private TMP_Text m_text_equip_speed = null;
    [SerializeField] private TMP_Text m_text_equip_range = null;
    [SerializeField] private TMP_Text m_text_equip_critical = null;
    [SerializeField] private TMP_Text m_text_equip_critical_chance = null;

    public int m_tooltip_index = -1;

    public void SetData(int in_kind)
    {
        bool haveHero = false;
        int heroLevel = 1;
        int heroGrade = 1;
        long equipID = 0;

        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return;

        var userHero = Managers.User.GetUserHeroInfo(in_kind);
        if (userHero != null)
        {
            haveHero = true;
            heroLevel = userHero.m_level;
            heroGrade = userHero.m_grade;
            equipID = userHero.m_equip_id;
        }

        var heroLevelInfo = Managers.Table.GetHeroLevelData(in_kind, heroLevel);
        if (heroLevelInfo == null)
            return;

        Util.SetHeroName(m_text_name, in_kind);
        Util.SetHeroImage(m_Image_hero, in_kind);
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(in_kind));
        Util.SetGradeStar(m_list_grade_star, heroGrade);
        m_text_level.Ex_SetText($"Lv.{heroLevel}");
        m_Image_hero.Ex_SetColor(haveHero ? Color.white : Color.black);
        m_go_lock.Ex_SetActive(!haveHero);
        m_go_equipment_empty.Ex_SetActive(haveHero);

        // 스킬 셋팅
        Util.SetSkill(m_slot_skill, in_kind);

        // 기본 스탯
        m_text_damage.Ex_SetText($"{heroLevelInfo.m_atk}");
        m_text_speed.Ex_SetText($"{heroLevelInfo.m_speed}");
        m_text_range.Ex_SetText($"{heroLevelInfo.m_range}");
        m_text_critical.Ex_SetText($"{heroLevelInfo.m_critical}");
        m_text_critical_chance.Ex_SetText($"{heroLevelInfo.m_critical_chance}");

        // 장비 스탯
        // 장비가 있어도 스탯이 없을 수 있어서 일단 초기화 시킨다.
        m_slot_equip.Ex_SetActive(false);
        m_text_equip_damage.Ex_SetText(string.Empty);
        m_text_equip_speed.Ex_SetText(string.Empty);
        m_text_equip_range.Ex_SetText(string.Empty);
        m_text_equip_critical.Ex_SetText(string.Empty);
        m_text_equip_critical_chance.Ex_SetText(string.Empty);

        var userEquip = Managers.User.GetEquip(equipID);
        if (userEquip != null)
        {
            var tableEquip = Managers.Table.GetEquipInfoData(userEquip.m_kind);
            if (tableEquip != null)
            {
                m_slot_equip.Ex_SetActive(true);
                m_slot_equip.SetData(equipID, userEquip.m_kind, true, false, false, null);

                if (tableEquip.m_atk > 0)
                    m_text_equip_damage.Ex_SetText($"(+{tableEquip.m_atk})");

                if (tableEquip.m_speed > 0)
                    m_text_equip_speed.Ex_SetText($"(+{tableEquip.m_speed})");

                if (tableEquip.m_range > 0)
                    m_text_equip_range.Ex_SetText($"(+{tableEquip.m_range})");

                if (tableEquip.m_critical > 0)
                    m_text_equip_critical.Ex_SetText($"(+{tableEquip.m_critical})");

                if (tableEquip.m_critical_chance > 0)
                    m_text_equip_critical_chance.Ex_SetText($"(+{tableEquip.m_critical_chance})");
            }
        }

        // 배경 작업
        for (int i = 0; i < m_list_bg.Count; i++)
            m_list_bg[i].Ex_SetActive(i == (int)heroInfo.m_rarity - 1);
    }

    public void OnClickToolTip(int in_skill_index)
    {
        // 이미 열려있는 툴팁이라면
        if (m_tooltip_index == in_skill_index)
        {
            m_tooltip_index = -1;
            Util.CloseToolTip();
            return;
        }

        Util.OpenToolTip(m_slot_skill[in_skill_index].Contents, m_slot_skill[in_skill_index].GetRoot, () =>
        {
            m_tooltip_index = -1;
        });
    }
}