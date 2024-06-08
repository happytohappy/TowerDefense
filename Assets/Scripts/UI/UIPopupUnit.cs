using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class UIPopupUnit : UIWindowBase
{
    [Header("유닛 상세 정보")]
    [SerializeField] private TMP_Text m_text_name = null;
    [SerializeField] private Image m_Image_hero = null;
    [SerializeField] private Image m_Image_lock = null;
    [SerializeField] private Image m_img_unit_type = null;
    [SerializeField] private List<Image> m_list_grade_star = new List<Image>();
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private Image m_Image_equipment = null;
    [SerializeField] private TMP_Text m_text_damage = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private List<GameObject> m_list_bg = new List<GameObject>();
    [SerializeField] private ExtentionButton m_btn_remove = null;
    [SerializeField] private ExtentionButton m_btn_merge = null;
    [SerializeField] private TMP_Text m_text_remove_tier = null;
    [SerializeField] private TMP_Text m_text_remove_energy = null;

    [Header("유닛 스킬")]
    [SerializeField] private List<Slot_Skill> m_skill = new List<Slot_Skill>();

    public int ToolTipIndex { get; set; }

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
        ToolTipIndex = -1;

        RefreshUI();
    }

    public void OnClickClose()
    {
        GameController.GetInstance.InputInit();

        Managers.UI.CloseLast();
    }

    public void RefreshUI()
    {
        var SpawnHeroList = GameController.GetInstance.LandInfo.FindAll(x => x.m_hero != null).ToList();
        var SameHero = SpawnHeroList.FindAll(x => x.m_hero.GetHeroData.m_info.m_kind == m_kind).ToList();
        m_btn_merge.Ex_SetActive(SameHero.Count >= 2);

        // 첫번째 타워 보여주기
        SetUnitInfo();
    }

    public void SetUnitInfo()
    {
        // 내가 보유한 타워의 레벨과 등급을 불러와서
        var hero = Managers.User.GetUserHeroInfo(m_kind);
        if (hero == null)
            return;

        var heroInfo = Managers.Table.GetHeroInfoData(m_kind);
        if (heroInfo == null)
            return;

        Util.SetHeroName(m_text_name, m_kind);
        Util.SetHeroImage(m_Image_hero, m_kind);
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(m_kind));

        for (int i = 0; i < m_list_bg.Count; i++)
            m_list_bg[i].Ex_SetActive(i == (int)heroInfo.m_rarity - 1);

        m_Image_hero.Ex_SetColor(Color.white);
        m_Image_lock.Ex_SetActive(false);
        m_text_level.Ex_SetText($"Lv.{hero.m_level}");
        m_Image_equipment.Ex_SetActive(true);
        Util.SetGradeStar(m_list_grade_star, hero.m_grade);

        var heroLevelInfo = Managers.Table.GetHeroLevelData(m_kind, hero.m_level);
        m_text_damage.Ex_SetText(heroLevelInfo.m_atk.ToString());
        m_text_speed.Ex_SetText(heroLevelInfo.m_speed.ToString());
        m_text_range.Ex_SetText(heroLevelInfo.m_range.ToString());

        m_text_remove_tier.Ex_SetText($"Tier {heroInfo.m_tier}");
        switch (heroInfo.m_tier)
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

        Util.SetSkill(m_skill, m_kind);
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

    public void OnClickToolTip(int in_skill_index)
    {
        Util.OpenToolTip(m_skill[in_skill_index].Contents, m_skill[in_skill_index].GetRoot);
    }
}