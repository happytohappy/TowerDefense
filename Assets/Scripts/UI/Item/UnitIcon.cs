using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class UnitIcon : MonoBehaviour
{
    [SerializeField] private Image m_img_unit = null;
    [SerializeField] private Image m_img_grade_bg = null;
    [SerializeField] private GameObject m_go_select = null;
    [SerializeField] private GameObject m_go_lock = null;
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private GameObject m_go_red_dot = null;
    [SerializeField] private Image m_img_unit_type = null;
    [SerializeField] private List<Image> m_list_star = new List<Image>();

    private bool m_have_hero;
    private int m_kind;
    private int m_hero_level;
    private int m_hero_grade;
    private Action<int, GameObject> m_callback;

    public void SetData(int in_kind)
    {
        m_kind = in_kind;
        m_have_hero = false;
        m_hero_level = 1;
        m_hero_grade = 1;
        m_callback = null;

        var heroInfo = Managers.Table.GetHeroInfoData(m_kind);
        if (heroInfo == null)
            return;

        var userHero = Managers.User.GetUserHeroInfo(m_kind);
        if (userHero != null)
        {
            m_have_hero = true;
            m_hero_level = userHero.m_level;
            m_hero_grade = userHero.m_grade;
        }

        m_go_select.Ex_SetActive(false);
        Util.SetGradeStar(m_list_star, m_hero_grade);
        m_img_unit.Ex_SetColor(m_have_hero ? Color.white : Color.black);
        Util.SetHeroImage(m_img_unit, in_kind);
        Util.SetUnitType(m_img_unit_type, in_kind);
        Util.SetRarityBG(m_img_grade_bg, heroInfo.m_rarity);
        m_go_lock.Ex_SetActive(!m_have_hero);
        m_text_level.Ex_SetText($"Lv.{m_hero_level}");
        m_go_red_dot.Ex_SetActive(false);
    }

    public void SetDataRecruit(int in_kind)
    {
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return;

        m_go_select.Ex_SetActive(false);
        Util.SetGradeStar(m_list_star, 1);
        m_img_unit.Ex_SetColor(Color.white);
        Util.SetHeroImage(m_img_unit, in_kind);
        Util.SetUnitType(m_img_unit_type, in_kind);
        Util.SetRarityBG(m_img_grade_bg, heroInfo.m_rarity);
        m_go_lock.Ex_SetActive(false);
        m_text_level.Ex_SetText($"Lv.1");
        m_go_red_dot.Ex_SetActive(false);
    }

    public void SetDataInfo(Action<int, GameObject> in_callback)
    {
        m_callback = in_callback;

        if (m_have_hero)
        {
            var HeroGrade = Managers.Table.GetHeroGradeData(m_kind, m_hero_grade + 1);
            if (HeroGrade != null)
            {
                if (Managers.User.GetInventoryItem(HeroGrade.m_item_kind) >= HeroGrade.m_grade_up_piece)
                    m_go_red_dot.Ex_SetActive(true);
            }

            var HeroLevel = Managers.Table.GetHeroLevelData(m_kind, m_hero_level + 1);
            if (HeroLevel != null)
            {
                if (Managers.User.GetInventoryItem(HeroLevel.m_item_kind) >= HeroLevel.m_item_amount)
                    m_go_red_dot.Ex_SetActive(true);
            }
        }

        if (m_kind == 1001)
            OnClickHeroInfo();
    }

    public void OnClickHeroInfo()
    {
        m_go_select.Ex_SetActive(true);

        m_callback?.Invoke(m_kind, m_go_select);
    }
}