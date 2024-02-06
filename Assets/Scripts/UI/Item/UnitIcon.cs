using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour
{
    [SerializeField] private Image m_img_unit = null;
    [SerializeField] private GameObject m_go_star = null;
    [SerializeField] private GameObject m_go_lock = null;
    [SerializeField] private TMP_Text m_text_rarity = null;

    private int m_kind;

    public void SetHaveUnit(int in_kind, string in_rarity)
    {
        // 받아온 파라미터로 유닛 아이콘 셋팅 필수!
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return;

        var sprite = Managers.Resource.Load<Sprite>($"Image/Hero/Tier_{heroInfo.m_tier}/Hero_{heroInfo.m_kind}");
        
        m_kind = in_kind;
        m_go_star.Ex_SetActive(true);
        m_img_unit.Ex_SetColor(Color.white);
        m_img_unit.Ex_SetImage(sprite);
        m_text_rarity.Ex_SetText(in_rarity);
        m_text_rarity.Ex_SetActive(true);
        m_go_lock.Ex_SetActive(false);
    }

    public void SetNoneUnit(int in_kind)
    {
        // 받아온 파라미터로 유닛 아이콘 셋팅 필수!
        m_kind = in_kind;
        m_go_star.Ex_SetActive(false);
        m_img_unit.Ex_SetColor(Color.black);
        m_text_rarity.Ex_SetActive(false);
        m_go_lock.Ex_SetActive(true);
    }

    public void OnClickHeroInfo()
    {
        var ui = Managers.UI.GetWindow(WindowID.UIPopupUnit, false) as UIPopupUnit;
        if (ui == null)
            return;

        ui.SetUnitInfo(m_kind);
    }
}