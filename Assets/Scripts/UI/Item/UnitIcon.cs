using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public void SetHaveUnit(int in_kind, ERarity in_rarity, int in_grade, int in_level)
    {
        m_kind = in_kind;
        m_go_select.Ex_SetActive(false);
        Util.SetGradeStar(m_list_star, in_grade);
        m_img_unit.Ex_SetColor(Color.white);
        m_img_unit.Ex_SetImage(Util.GetHeroImage(in_kind));
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
            var ui = Managers.UI.GetWindow(WindowID.UIWindowUnit, false) as UIWindowUnit;
            if (ui == null)
                return;

            ui.LastSelect = m_go_select;
            m_go_select.Ex_SetActive(true);
        }
    }

    public void SetNoneUnit(int in_kind, ERarity in_rarity)
    {
        m_kind = in_kind;
        m_go_select.Ex_SetActive(false);
        Util.SetGradeStar(m_list_star, 1);
        m_img_unit.Ex_SetColor(Color.black);
        m_img_unit.Ex_SetImage(Util.GetHeroImage(in_kind));
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
        m_img_unit.Ex_SetImage(Util.GetHeroImage(in_kind));
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(in_kind));
        Util.SetRarityBG(m_img_grade_bg, in_rarity);
        m_go_lock.Ex_SetActive(false);
        m_text_level.Ex_SetText($"Lv.{in_level}");
        m_go_red_dot.Ex_SetActive(false);
    }

    public void OnClickHeroInfo()
    {
        var ui = Managers.UI.GetWindow(WindowID.UIWindowUnit, false) as UIWindowUnit;
        if (ui == null)
            return;

        ui.LastSelect.Ex_SetActive(false);
        ui.SetUnitInfo(m_kind);
        m_go_select.Ex_SetActive(true);

        ui.LastSelect = m_go_select;
    }
}