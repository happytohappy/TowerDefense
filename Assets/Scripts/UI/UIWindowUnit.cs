using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWindowUnit : UIWindowBase
{
    private const string UNIT_TIER_GROUP_PATH = "UI/Item/UnitTierGroup";

    [Header("재화")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("슬롯 루트")]
    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private RectTransform m_rect_root = null;

    [Header("영웅 정보")]
    [SerializeField] private HeroBaseInfo m_hero_base_info = null;

    [Header("영웅 강화")]
    [SerializeField] private ExtentionButton m_btn_gradeup = null;
    [SerializeField] private ExtentionButton m_btn_levelup = null;
    [SerializeField] private TMP_Text m_text_gradeup = null;
    [SerializeField] private TMP_Text m_text_levelup = null;
    [SerializeField] private Slider m_slider_gradeup = null;
    [SerializeField] private Image m_image_resource = null;

    [Header("튜토리얼")]
    [SerializeField] private GameObject m_go_tutorial_grade = null;
    [SerializeField] private GameObject m_go_tutorial_level = null;

    private int m_tuto_index;
    private int m_kind;
    private GameObject m_last_select;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowUnit;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        m_tuto_index = 0;
        m_kind = 1001;
        m_last_select = null;

        // 재화 갱신
        RefreshGoods();

        // 영웅 리스트
        RefreshHeroList();
    }

    public void RefreshGoods()
    {
        Util.SetGoods(EGoods.Gold, m_text_gold);
        Util.SetGoods(EGoods.Ruby, m_text_ruby);
        Util.SetGoods(EGoods.Diamond, m_text_diamond);
    }

    private void RefreshHeroList()
    {
        // 영웅 슬롯 초기화
        m_rect_root.Ex_SetValue(EScrollDir.Vertical, 0f);
        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        // 영웅 슬롯 리스트 갱신
        for (int tier = 1; tier <= 8; tier++)
        {
            var tierGroup = Managers.Resource.Instantiate(UNIT_TIER_GROUP_PATH, Vector3.zero, m_trs_root);
            var sc = tierGroup.GetComponent<UnitTierGroup>();

            sc.SetTierUnit(tier, (kind, select) =>
            {
                if (Managers.Tutorial.TutorialProgress)
                {
                    Managers.Tutorial.TutorialEnd();

                    if (Managers.User.UserData.ClearTutorial.Contains(2) == false)
                        Managers.Tutorial.TutorialStart(m_go_tutorial_grade);
                    else if (Managers.User.UserData.ClearTutorial.Contains(3) == false)
                        Managers.Tutorial.TutorialStart(m_go_tutorial_level);
                }

                if (m_kind == kind)
                {
                    if (m_last_select == null)
                    {
                        SetHeroInfo(kind, true);

                        m_last_select = select;
                    }
                    return;
                }

                SetHeroInfo(kind);

                m_last_select.Ex_SetActive(false);
                m_last_select = select;
            });
        }
    }

    public void SetHeroInfo(int in_kind, bool in_first = false)
    {
        m_kind = in_kind;

        // 영웅 정보 셋팅
        m_hero_base_info.SetData(in_kind);

        var userHero = Managers.User.GetUserHeroInfo(in_kind);
        if (userHero == null)
        {
            m_btn_gradeup.Ex_SetActive(false);
            m_btn_levelup.Ex_SetActive(false);
        }
        else
        {
            var heroGrade = Managers.Table.GetHeroGradeData(in_kind, userHero.m_grade + 1);
            if (heroGrade == null)
            {
                m_btn_gradeup.Ex_SetActive(false);
            }
            else
            {
                m_btn_gradeup.Ex_SetActive(true);
                m_text_gradeup.Ex_SetText($"{Managers.User.GetInventoryItem(heroGrade.m_item_kind)}/{heroGrade.m_grade_up_piece}");
                m_slider_gradeup.Ex_SetValue(Managers.User.GetInventoryItem(heroGrade.m_item_kind) / heroGrade.m_grade_up_piece);
                m_btn_gradeup.interactable = Managers.User.GetInventoryItem(heroGrade.m_item_kind) >= heroGrade.m_grade_up_piece;
            }

            var heroLevel = Managers.Table.GetHeroLevelData(in_kind, userHero.m_level + 1);
            if (heroLevel == null)
            {
                m_btn_levelup.Ex_SetActive(false);
            }
            else
            {
                m_btn_levelup.Ex_SetActive(true);
                m_image_resource.Ex_SetImage(Util.GetResourceImage(heroLevel.m_item_kind));
                m_text_levelup.Ex_SetText(Util.CommaText(heroLevel.m_item_amount));
                m_btn_levelup.interactable = Managers.User.GetInventoryItem(heroLevel.m_item_kind) >= heroLevel.m_item_amount;
            }
        }
    }

    public void OnClickUnitGradeUp()
    {
        var userHero = Managers.User.GetUserHeroInfo(m_kind);
        if (userHero == null)
            return;

        var heroGrade = Managers.Table.GetHeroGradeData(m_kind, userHero.m_grade + 1);
        if (heroGrade == null)
            return;

        if (Managers.User.GetInventoryItem(m_kind) < heroGrade.m_grade_up_piece)
            return;

        if (Managers.User.UserData.ClearTutorial.Contains(2) == false)
        {
            Managers.Tutorial.TutorialEnd();
            Managers.Tutorial.TutorialClear(2);

            Managers.Tutorial.TutorialStart(m_go_tutorial_level);
        }

        // 히어로 Grade 증가
        userHero.m_grade++;
        
        // 사용 재화 감소
        Managers.User.UpsertInventoryItem(heroGrade.m_item_kind, -heroGrade.m_grade_up_piece);

        // UI 갱신
        RefreshGoods();
        SetHeroInfo(m_kind);
    }

    public void OnClickUnitLevelUp()
    {
        var userHero = Managers.User.GetUserHeroInfo(m_kind);
        if (userHero == null)
            return;

        var heroLevel = Managers.Table.GetHeroLevelData(m_kind, userHero.m_level + 1);
        if (heroLevel == null)
            return;

        if (Managers.User.UserData.ClearTutorial.Contains(3) == false)
        {
            Managers.Tutorial.TutorialEnd();
            Managers.Tutorial.TutorialClear(3);
        }

        // 레벨 증가
        userHero.m_level++;

        // 사용 재화 감소
        Managers.User.UpsertInventoryItem(heroLevel.m_item_kind, -heroLevel.m_item_amount);

        // UI 갱신
        RefreshGoods();
        SetHeroInfo(m_kind);
    }

    public void OnClickEquip()
    {
        EquipInfoParam param = new EquipInfoParam();
        param.m_hero_kind = m_kind;
        param.m_callback = () =>
        {
            SetHeroInfo(m_kind);
        };

        Managers.UI.OpenWindow(WindowID.UIPopupEquipment, param);
    }
}