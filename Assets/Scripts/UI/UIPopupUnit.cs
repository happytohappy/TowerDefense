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
    [SerializeField] private Image m_Image_tower = null;
    [SerializeField] private Image m_Image_lock = null;
    [SerializeField] private Image m_Image_star = null;
    [SerializeField] private TMP_Text m_text_star = null;
    [SerializeField] private TMP_Text m_text_level = null;
    [SerializeField] private Image m_Image_equipment = null;
    [SerializeField] private TMP_Text m_text_damage = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupUnit;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

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
    }

    public void SetUnitInfo(int in_kind)
    {
        var TowerInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (TowerInfo == null)
            return;

        m_text_name.Ex_SetText(TowerInfo.m_name);
        m_text_rarity.Ex_SetText(TowerInfo.m_rarity.ToString());
        // m_text_attack

        //내가 보유한 타워의 레벨과 등급을 불러와서
        var Tower = Managers.User.GetUserHeroInfo(in_kind);
        if (Tower == null)
        {
            // 타워 보유하지 않은 셋팅 해주면 됨
            m_Image_tower.Ex_SetColor(Color.black);
            m_Image_lock.Ex_SetActive(true);
            m_Image_star.Ex_SetActive(false);
            // m_text_level
            m_Image_equipment.Ex_SetActive(false);
        }
        else
        {
            m_Image_tower.Ex_SetColor(Color.white);
            m_Image_lock.Ex_SetActive(false);
            m_Image_star.Ex_SetActive(true);
            // m_text_star
            // m_text_level
            m_Image_equipment.Ex_SetActive(true);
        }
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

// Hero Level 테이블을 조회해서 스탯 정보를 알아온다.
//var TowerLevelInfo = Managers.Table.GetHeroLevelData(in_kind, Tower.m_level);

//m_text_damage.Ex_SetText(TowerLevelInfo.m_atk.ToString());