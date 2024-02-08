using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupUnit : UIWindowBase
{
    private const string UNIT_TIER_GROUP_PATH = "UI/Item/UnitTierGroup";

    [SerializeField] private Transform m_trs_root = null;

    [Header("유닛 상세 정보")]
    [SerializeField] private TMP_Text m_text_name = null;
    [SerializeField] private TMP_Text m_text_rarity = null;
    [SerializeField] private TMP_Text m_text_attack = null;
    [SerializeField] private Image m_Image_hero = null;
    [SerializeField] private Image m_Image_lock = null;
    [SerializeField] private Image m_Image_star = null;
    [SerializeField] private TMP_Text m_text_star = null;
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private Image m_Image_equipment = null;
    [SerializeField] private TMP_Text m_text_damage = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private GameObject m_go_level_up = null;
    [SerializeField] private GameObject m_go_active = null;

    private int m_kind;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupUnit;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        m_kind = 1001;
        RefreshUI();
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        for (int tier = 1; tier <= 6; tier++)
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

        m_text_name.Ex_SetText(Util.GetHeroName(m_kind));
        m_text_rarity.Ex_SetText(Util.GetHeroRarityToString(m_kind));
        m_Image_hero.Ex_SetImage(Util.GetHeroImage(m_kind));

        //내가 보유한 타워의 레벨과 등급을 불러와서
        var hero = Managers.User.GetUserHeroInfo(m_kind);
        if (hero == null)
        {
            // 타워 보유하지 않은 셋팅 해주면 됨
            m_Image_hero.Ex_SetColor(Color.black);
            m_Image_lock.Ex_SetActive(true);
            m_Image_star.Ex_SetActive(false);
            m_text_level.Ex_SetActive(false);
            m_Image_equipment.Ex_SetActive(false);
            m_go_level_up.Ex_SetActive(false);
            m_go_active.Ex_SetActive(false);
        }
        else
        {
            m_Image_hero.Ex_SetColor(Color.white);
            m_Image_lock.Ex_SetActive(false);
            m_Image_star.Ex_SetActive(true);
            m_text_level.Ex_SetActive(true);
            m_text_level.Ex_SetText(hero.m_level.ToString());
            m_Image_equipment.Ex_SetActive(true);
            m_go_level_up.Ex_SetActive(true);
            m_go_active.Ex_SetActive(true);

            var heroLevelInfo = Managers.Table.GetHeroLevelData(in_kind, hero.m_level);
            m_text_damage.Ex_SetText(heroLevelInfo.m_atk.ToString());
            m_text_speed.Ex_SetText(heroLevelInfo.m_speed.ToString());
            m_text_range.Ex_SetText(heroLevelInfo.m_range.ToString());
        }
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
}