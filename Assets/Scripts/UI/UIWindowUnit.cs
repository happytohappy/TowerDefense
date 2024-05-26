using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIWindowUnit : UIWindowBase
{
    private const string UNIT_TIER_GROUP_PATH = "UI/Item/UnitTierGroup";

    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private RectTransform m_rect_root = null;

    [Header("유닛 상세 정보")]
    [SerializeField] private TMP_Text m_text_name = null;
    [SerializeField] private Image m_Image_hero = null;
    [SerializeField] private Image m_Image_lock = null;
    [SerializeField] private List<Image> m_list_grade_star = new List<Image>();
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private Image m_Image_equipment = null;
    [SerializeField] private TMP_Text m_text_damage = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private List<GameObject> m_list_bg = new List<GameObject>();
    [SerializeField] private ExtentionButton m_btn_gradeup = null;
    [SerializeField] private ExtentionButton m_btn_levelup = null;
    [SerializeField] private TMP_Text m_text_gradeup = null;
    [SerializeField] private TMP_Text m_text_levelup = null;
    [SerializeField] private Slider m_slider_gradeup = null;
    [SerializeField] private Image m_image_resource = null;
    [SerializeField] private Image m_img_unit_type = null;
    [SerializeField] private Slot_Equip m_slot_equip = null;

    [Header("유닛 스킬")]
    [SerializeField] private List<Slot_Skill> m_skill = new List<Slot_Skill>();

    private int m_kind;
    public int ToolTipIndex { get; set; }

    public GameObject LastSelect { get; set; }

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowUnit;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        m_kind = 1001;
        ToolTipIndex = -1;

        RefreshUI();
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void RefreshUI()
    {
        m_rect_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        for (int tier = 1; tier <= 8; tier++)
        {
            var tierGroup = Managers.Resource.Instantiate(UNIT_TIER_GROUP_PATH, Vector3.zero, m_trs_root);
            var sc = tierGroup.GetComponent<UnitTierGroup>();

            sc.SetTierUnit(tier);
        }

        // 첫번째 타워 보여주기
        SetUnitInfo(m_kind);
    }

    public void SetUnitInfo(int in_kind)
    {
        m_kind = in_kind;
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return;

        m_text_name.Ex_SetText(Util.GetHeroName(m_kind));
        m_Image_hero.Ex_SetImage(Util.GetHeroImage(m_kind));
        m_img_unit_type.Ex_SetImage(Util.GetUnitType(in_kind));

        for (int i = 0; i < m_list_bg.Count; i++)
            m_list_bg[i].Ex_SetActive(i == (int)heroInfo.m_rarity - 1);

        m_slot_equip.gameObject.Ex_SetActive(false);

        //내가 보유한 타워의 레벨과 등급을 불러와서
        var hero = Managers.User.GetUserHeroInfo(m_kind);
        if (hero == null)
        {
            // 타워 보유하지 않은 셋팅 해주면 됨
            m_Image_hero.Ex_SetColor(Color.black);
            m_Image_lock.Ex_SetActive(true);
            m_text_level.Ex_SetText("Lv.1");
            m_Image_equipment.Ex_SetActive(false);
            Util.SetGradeStar(m_list_grade_star, 1);
            m_btn_gradeup.Ex_SetActive(false);
            m_btn_levelup.Ex_SetActive(false);
        }
        else
        {
            if (hero.m_equip_id > 0)
            {
                var userEquip = Managers.User.GetEquip(hero.m_equip_id);
                if (userEquip != null)
                {
                    m_slot_equip.gameObject.Ex_SetActive(true);
                    m_slot_equip.SetData(hero.m_equip_id, userEquip.m_kind, true, false, false);
                }
            }

            m_Image_hero.Ex_SetColor(Color.white);
            m_Image_lock.Ex_SetActive(false);
            m_text_level.Ex_SetText($"Lv.{hero.m_level}");
            m_Image_equipment.Ex_SetActive(true);
            Util.SetGradeStar(m_list_grade_star, hero.m_grade);

            var heroLevelInfo = Managers.Table.GetHeroLevelData(in_kind, hero.m_level);
            m_text_damage.Ex_SetText(heroLevelInfo.m_atk.ToString());
            m_text_speed.Ex_SetText(heroLevelInfo.m_speed.ToString());
            m_text_range.Ex_SetText(heroLevelInfo.m_range.ToString());
            m_btn_gradeup.Ex_SetActive(true);
            m_btn_levelup.Ex_SetActive(true);

            var HeroGrade = Managers.Table.GetHeroGradeData(in_kind, hero.m_grade + 1);
            if (HeroGrade == null)
            {
                m_btn_gradeup.Ex_SetActive(false);
            }
            else
            {
                m_text_gradeup.Ex_SetText($"{Managers.User.GetInventoryItem(HeroGrade.m_item_kind)}/{HeroGrade.m_grade_up_piece}");
                m_slider_gradeup.Ex_SetValue(Managers.User.GetInventoryItem(HeroGrade.m_item_kind) / HeroGrade.m_grade_up_piece);
                m_btn_gradeup.interactable = Managers.User.GetInventoryItem(HeroGrade.m_item_kind) >= HeroGrade.m_grade_up_piece;
            }

            var HeroLevel = Managers.Table.GetHeroLevelData(in_kind, hero.m_level + 1);
            if (HeroLevel == null)
            {
                m_btn_levelup.Ex_SetActive(false);
            }
            else
            {
                m_image_resource.Ex_SetImage(Util.GetResourceImage(HeroLevel.m_item_kind));
                m_text_levelup.Ex_SetText(Util.CommaText(HeroLevel.m_item_amount));
                m_btn_levelup.interactable = Managers.User.GetInventoryItem(HeroLevel.m_item_kind) >= HeroLevel.m_item_amount;
            }
        }

        Util.SetSkill(m_skill, in_kind);
    }

    public void OnClickUnitGradeUp()
    {
        var hero = Managers.User.GetUserHeroInfo(m_kind);
        if (hero == null)
            return;

        // 현재 등급을 10을 맥스라고 가정
        if (hero.m_grade >= 10)
            return;

        var HeroGrade = Managers.Table.GetHeroGradeData(m_kind, hero.m_grade + 1);
        if (Managers.User.GetInventoryItem(m_kind) < HeroGrade.m_grade_up_piece)
            return;

        hero.m_grade++;
        Managers.User.UpsertInventoryItem(m_kind, HeroGrade.m_grade_up_piece);

        RefreshUI();
    }

    public void OnClickUnitLevelUp()
    {
        var hero = Managers.User.GetUserHeroInfo(m_kind);
        if (hero == null)
            return;

        // 현재 레벨을 10을 맥스라고 가정
        if (hero.m_level >= 10)
            return;

        hero.m_level++;
        RefreshUI();
    }

    public void OnClickUnitBuy()
    {
        var gachaReward = Managers.Table.GetGachaHero(1000);
        if (gachaReward == null)
            return;

        Managers.User.UpsertHero(gachaReward.m_item);
        RefreshUI();

        GachaHeroParam param = new GachaHeroParam();
        param.m_hero_kind = gachaReward.m_item;

        Managers.UI.OpenWindow(WindowID.UIPopupUnitBuy, param);
    }

    public void OnClickToolTip(int in_skill_index)
    {
        // 이미 열려있는 툴팁이라면
        if (ToolTipIndex == in_skill_index)
        {
            ToolTipIndex = -1;
            Util.CloseToolTip();
            return;
        }

        Util.OpenToolTip(m_skill[in_skill_index].Contents, m_skill[in_skill_index].GetRoot);

        ToolTipIndex = in_skill_index;
    }

    public void OnClickEquip()
    {
        EquipInfoParam param = new EquipInfoParam();
        param.m_unit_kind = m_kind;

        Managers.UI.OpenWindow(WindowID.UIPopupEquipment, param);
    }
}