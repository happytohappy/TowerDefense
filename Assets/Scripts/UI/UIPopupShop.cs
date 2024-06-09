using UnityEngine;
using TMPro;

public class UIPopupShop : UIWindowBase
{
    [Header("재화")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("스크롤 렉트")]
    [SerializeField] private RectTransform m_rect_root = null;

    [Header("탭")]
    [SerializeField] private ParentTab m_parent_tab = null;

    private EShopTab m_tab = EShopTab.Recruit;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupShop;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        RefreshUI();
    }

    private void RefreshUI()
    {
        Util.SetGoods(EGoods.Gold, m_text_gold);
        Util.SetGoods(EGoods.Ruby, m_text_ruby);
        Util.SetGoods(EGoods.Diamond, m_text_diamond);
    }

    public void OnClickRecruit()
    {
        m_tab = EShopTab.Recruit;

        m_rect_root.Ex_SetValue(EScrollDir.Horizontal, 0f);
        m_parent_tab.SelectTab((int)m_tab);
    }

    public void OnClickSpecial()
    {
        m_tab = EShopTab.Special;

        m_rect_root.Ex_SetValue(EScrollDir.Horizontal, -1384f);
        m_parent_tab.SelectTab((int)m_tab);
    }

    public void OnClickPackage()
    {
        m_tab = EShopTab.Package;

        m_rect_root.Ex_SetValue(EScrollDir.Horizontal, -2216f);
        m_parent_tab.SelectTab((int)m_tab);
    }

    public void OnClickWeek()
    {
        m_tab = EShopTab.Week;

        m_rect_root.Ex_SetValue(EScrollDir.Horizontal, -2380f);
        m_parent_tab.SelectTab((int)m_tab);
    }

    public void OnClickDay()
    {
        m_tab = EShopTab.Day;

        m_rect_root.Ex_SetValue(EScrollDir.Horizontal, -3673);
        m_parent_tab.SelectTab((int)m_tab);
    }

    public void OnClickDia()
    {
        m_tab = EShopTab.Dia;

        m_rect_root.Ex_SetValue(EScrollDir.Horizontal, -4362f);
        m_parent_tab.SelectTab((int)m_tab);
    }

    public void OnClickProbability_Premium()
    {
        ProbabilityParam param = new ProbabilityParam();
        param.m_kind = 2000;

        Managers.UI.OpenWindow(WindowID.UIPopupProbabilityInfo, param);
    }

    public void OnClickProbability_Normal()
    {
        ProbabilityParam param = new ProbabilityParam();
        param.m_kind = 1000;

        Managers.UI.OpenWindow(WindowID.UIPopupProbabilityInfo, param);
    }
}