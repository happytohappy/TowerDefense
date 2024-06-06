using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupUnitBuy : UIWindowBase
{
    [Header("유닛 상세 정보")]
    [SerializeField] private TMP_Text m_text_name = null;
    [SerializeField] private TMP_Text m_text_rarity = null;
    [SerializeField] private Image m_Image_hero = null;
    [SerializeField] private TMP_Text m_text_damage = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private TMP_Text m_text_tier = null;

    private GachaHeroParam m_param = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupUnitBuy;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);
        
        if (in_param == null)
            Managers.UI.CloseLast();

        m_param = in_param as GachaHeroParam;
        if (m_param == null)
            Managers.UI.CloseLast();

        RefreshUI(m_param.m_hero_kind);
    }

    private void RefreshUI(int in_kind)
    {
        // 타워 보유하지 않은 셋팅 해주면 됨
        Util.SetHeroName(m_text_name, in_kind);
        m_text_rarity.Ex_SetText(Util.GetHeroRarityToString(in_kind));
        Util.SetHeroImage(m_Image_hero, in_kind);
        m_text_tier.Ex_SetText($"Class {Util.GetHeroTier(in_kind)}");

        var hero = Managers.User.GetUserHeroInfo(in_kind);
        var heroLevelInfo = Managers.Table.GetHeroLevelData(in_kind, hero.m_level);
        m_text_damage.Ex_SetText(heroLevelInfo.m_atk.ToString());
        m_text_speed.Ex_SetText(heroLevelInfo.m_speed.ToString());
        m_text_range.Ex_SetText(heroLevelInfo.m_range.ToString());
    }

    public void OnClickUnitBuy()
    {
        var gachaReward = Managers.Table.GetGachaHero(1000);
        if (gachaReward == null)
            return;

        Managers.User.UpsertHero(gachaReward.m_item);
        RefreshUI(gachaReward.m_item);

        //var uiUnitPopup = Managers.UI.GetWindow(WindowID.UIWindowUnit, false) as UIWindowUnit;
        //if (uiUnitPopup != null)
        //    uiUnitPopup.RefreshUI();
    }
}
