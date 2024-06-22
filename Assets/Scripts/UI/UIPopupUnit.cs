using UnityEngine;
using System.Linq;
using TMPro;

public class UIPopupUnit : UIWindowBase
{
    [Header("영웅 정보")]
    [SerializeField] private HeroBaseInfo m_hero_base_info = null;

    [Header("버튼")]
    [SerializeField] private ExtentionButton m_btn_remove = null;
    [SerializeField] private ExtentionButton m_btn_merge = null;
    [SerializeField] private TMP_Text m_text_remove_tier = null;
    [SerializeField] private TMP_Text m_text_remove_energy = null;

    private int m_kind;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowUnit;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        if (in_param == null)
            Managers.UI.CloseLast();

        var param = in_param as UnitInfoParam;
        if (param == null)
            Managers.UI.CloseLast();

        m_kind = param.m_kind;

        RefreshUI();
    }

    public void RefreshUI()
    {
        var SpawnHeroList = GameController.GetInstance.LandInfo.FindAll(x => x.m_hero != null).ToList();
        var SameHero = SpawnHeroList.FindAll(x => x.m_hero.GetHeroData.m_info.m_kind == m_kind).ToList();
        m_btn_merge.Ex_SetActive(SameHero.Count >= 2);

        // 첫번째 타워 보여주기
        SetHeroInfo();
    }

    public void SetHeroInfo()
    {
        // 영웅 정보 셋팅
        m_hero_base_info.SetData(m_kind);

        var heroTableInfo = Managers.Table.GetHeroInfoData(m_kind);
        if (heroTableInfo == null)
            return;

        m_text_remove_tier.Ex_SetText($"Tier {heroTableInfo.m_tier}");
        switch (heroTableInfo.m_tier)
        {
            case 1: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_1}"); break;
            case 2: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_2}"); break;
            case 3: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_3}"); break;
            case 4: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_4}"); break;
            case 5: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_5}"); break;
            case 6: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_6}"); break;
            case 7: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_7}"); break;
            case 8: m_text_remove_energy.Ex_SetText($"{CONST.STAGE_ENERGY_SELL_8}"); break;
        }
    }

    public void OnClickUnitRemove()
    {
        GameController.GetInstance.HeroSell();
        
        OnClickClose();
    }

    public void OnClickUnitMerge()
    {
        var SpawnHeroList = GameController.GetInstance.LandInfo.FindAll(x => x.m_hero != null).ToList();
        var SameHero = SpawnHeroList.FindAll(x => x.m_hero.GetHeroData.m_info.m_kind == m_kind).ToList();

        var SelectLand = GameController.GetInstance.LandInfo.Find(x => x.m_hero == GameController.GetInstance.SelectHero);
        SameHero.Remove(SelectLand);
        GameController.GetInstance.EndLand = SelectLand;
        GameController.GetInstance.SelectHero = SameHero[UnityEngine.Random.Range(0, SameHero.Count)].m_hero;
        GameController.GetInstance.HeroMerge();

        var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
        gui.OnCheckHeroSynergy();

        OnClickClose();
    }
}