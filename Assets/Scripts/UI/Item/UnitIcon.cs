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
    [SerializeField] private List<Image> m_list_star = new List<Image>();
    [SerializeField] private GameObject m_go_lock = null;
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private GameObject m_go_red_dot = null;
    [SerializeField] private Image m_img_unit_type = null;

    private int m_kind;
    private Action<int, GameObject> m_callback = null;

    public void SetHaveUnit(int in_kind, ERarity in_rarity, int in_grade, int in_level, Action<int, GameObject> in_callback)
    {
        m_callback = in_callback;

        m_kind = in_kind;
        m_go_select.Ex_SetActive(false);
        Util.SetGradeStar(m_list_star, in_grade);
        m_img_unit.Ex_SetColor(Color.white);
        Util.SetHeroImage(m_img_unit, in_kind);
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(in_kind));
        Util.SetRarityBG(m_img_grade_bg, in_rarity);
        m_go_lock.Ex_SetActive(false);
        m_text_level.Ex_SetText($"Lv.{in_level}");
        m_go_red_dot.Ex_SetActive(false);

        var HeroGrade = Managers.Table.GetHeroGradeData(in_kind, in_grade + 1);
        if (HeroGrade != null)
        {
            if (Managers.User.GetInventoryItem(HeroGrade.m_item_kind) >= HeroGrade.m_grade_up_piece)
                m_go_red_dot.Ex_SetActive(true);
        }

        var HeroLevel = Managers.Table.GetHeroLevelData(in_kind, in_level + 1);
        if (HeroLevel != null)
        {
            if (Managers.User.GetInventoryItem(HeroLevel.m_item_kind) >= HeroLevel.m_item_amount)
                m_go_red_dot.Ex_SetActive(true);
        }

        if (in_kind == 1001)
        {
            OnClickHeroInfo();
        }
    }

    public void SetNoneUnit(int in_kind, ERarity in_rarity, Action<int, GameObject> in_callback)
    {
        m_callback = in_callback;

        m_kind = in_kind;
        m_go_select.Ex_SetActive(false);
        Util.SetGradeStar(m_list_star, 1);
        m_img_unit.Ex_SetColor(Color.black);
        Util.SetHeroImage(m_img_unit, in_kind);
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(in_kind));
        Util.SetRarityBG(m_img_grade_bg, in_rarity);
        m_go_lock.Ex_SetActive(true);
        m_text_level.Ex_SetText($"Lv.1");
        m_go_red_dot.Ex_SetActive(false);
    }

    public void SetSimpleUnit(int in_kind, ERarity in_rarity, int in_grade, int in_level)
    {
        m_kind = in_kind;
        m_go_select.Ex_SetActive(false);
        Util.SetGradeStar(m_list_star, in_grade);
        m_img_unit.Ex_SetColor(Color.white);
        Util.SetHeroImage(m_img_unit, in_kind);
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(in_kind));
        Util.SetRarityBG(m_img_grade_bg, in_rarity);
        m_go_lock.Ex_SetActive(false);
        m_text_level.Ex_SetText($"Lv.{in_level}");
        m_go_red_dot.Ex_SetActive(false);
    }

    public void OnClickHeroInfo()
    {
        m_go_select.Ex_SetActive(true);

        m_callback?.Invoke(m_kind, m_go_select);
    }
}