using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class UIPopupEquipment : UIWindowBase
{
    private const string EQUIP_SLOT_PATH = "UI/Item/Slot_UnitEquip_Scale70";

    [Header("Equip Main")]
    [SerializeField] private Image m_image_equip_icon = null;
    [SerializeField] private List<GameObject> m_grade_layout = new List<GameObject>();
    [SerializeField] private Image m_image_equip_hero_type = null;
    [SerializeField] private TMP_Text m_text_equip_name = null;

    [Header("Unit")]
    [SerializeField] private UnitIcon m_unit_slot = null;

    [Header("Stat")]
    [SerializeField] private TMP_Text m_text_atk = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private TMP_Text m_text_critical = null;
    [SerializeField] private TMP_Text m_text_critical_chance = null;

    [Header("Equip List")]
    [SerializeField] private RectTransform m_rect_equip_root = null;
    [SerializeField] private Transform m_trs_equip_root = null;
    [SerializeField] private GameObject m_go_none_text = null;

    [Header("Button")]
    [SerializeField] private GameObject m_go_equip = null;
    [SerializeField] private GameObject m_go_unequip = null;
    [SerializeField] private ExtentionButton m_btn_equip = null;

    private int m_unit_kind;
    private long m_equip_id;

    public GameObject LastSelect { get; set; }

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupEquipment;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        if (in_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        var param = in_param as EquipInfoParam;
        if (param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        m_unit_kind = param.m_unit_kind;

        RefreshEquip();
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    private void RefreshEquip()
    {
        m_rect_equip_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_equip_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_equip_root.GetChild(i).gameObject);

        var userInfo = Managers.Table.GetHeroInfoData(m_unit_kind);
        if (userInfo == null)
            return;

        var equipList = Managers.User.GetEquipList(userInfo.m_equip_type);
        if (equipList.Count == 0)
        {
            m_go_none_text.Ex_SetActive(true);
            return;
        }

        m_go_none_text.Ex_SetActive(false);
        bool first = true;
        foreach (var e in equipList)
        {
            var equipSlot = Managers.Resource.Instantiate(EQUIP_SLOT_PATH, Vector3.zero, m_trs_equip_root);
            var item = equipSlot.GetComponentInChildren<Slot_Equip>();

            item.SetData(e.m_unique_id, e.m_kind, e.m_mount, e.m_new, first);
            if (first)
            {
                SetEquipInfo(e.m_unique_id, e.m_kind);
                first = false;
            }
        }
    }

    public void SetEquipInfo(long in_unique, int in_kind)
    {
        m_equip_id = in_unique;

        var userEquip = Managers.User.GetEquip(in_unique);
        if (userEquip == null)
            return;

        var equip = Managers.Table.GetEquipInfoData(in_kind);
        if (equip == null)
            return;

        m_unit_slot.gameObject.Ex_SetActive(false);
        if (userEquip.m_mount)
        {
            var hero = Managers.User.GetEquipMountHero(in_unique);
            if (hero != null)
            {
                var heroInfo = Managers.Table.GetHeroInfoData(hero.m_kind);
                m_unit_slot.SetHaveUnit(hero.m_kind, heroInfo.m_rarity, hero.m_grade, hero.m_level);
                m_unit_slot.gameObject.Ex_SetActive(true);
            }
        }

        // 장비 스탯
        m_text_atk.Ex_SetText($"+{equip.m_atk}");
        m_text_speed.Ex_SetText($"+{equip.m_speed}");
        m_text_range.Ex_SetText($"+{equip.m_range}");
        m_text_critical.Ex_SetText($"+{equip.m_critical}");
        m_text_critical_chance.Ex_SetText($"+{equip.m_critical_chance}");

        Util.SetEquipIcon(m_image_equip_icon, equip.m_equip_icon);
        Util.SetUnitType(m_image_equip_hero_type, equip.m_hero_type);
        m_text_equip_name.Ex_SetText(equip.m_equip_type.ToString());

        for (int i = 0; i < m_grade_layout.Count; i++)
            m_grade_layout[i].Ex_SetActive(i == (int)equip.m_equip_grade);

        if (userEquip.m_mount)
        {
            m_go_equip.Ex_SetActive(false);
            m_go_unequip.Ex_SetActive(true);
        }
        else
        {
            m_go_equip.Ex_SetActive(true);
            m_go_unequip.Ex_SetActive(false);

            var unit = Managers.User.GetUserHeroInfo(m_unit_kind);
            m_btn_equip.interactable = unit.m_equip_id == 0;
        }
    }

    public void SelectEquip(long in_unique, int in_kind)
    {
        SetEquipInfo(in_unique, in_kind);
    }

    public void OnClickEquip()
    {
        var userEquip = Managers.User.GetEquip(m_equip_id);
        if (userEquip == null)
            return;

        var unit = Managers.User.GetUserHeroInfo(m_unit_kind);
        if (unit == null)
            return;

        unit.m_equip_id = m_equip_id;
        userEquip.m_mount = true;

        RefreshEquip();

        if (Managers.UI.ActiveWindow(WindowID.UIWindowUnit))
        {
            var ui = Managers.UI.GetWindow(WindowID.UIWindowUnit, false) as UIWindowUnit;
            if (ui == null)
                return;

            ui.SetUnitInfo(m_unit_kind);
        }
    }

    public void OnClickUnEquip()
    {
        var userEquip = Managers.User.GetEquip(m_equip_id);
        if (userEquip == null)
            return;

        var unit = Managers.User.GetUserHeroInfo(m_unit_kind);
        if (unit == null)
            return;

        if (unit.m_equip_id == m_equip_id)
        {
            UnEquipProcess();
        }
        else
        {
            CommonInfoParam param = new CommonInfoParam();
            param.m_contents = "니꺼 아닌데 진짜 해제 시킬거임???";
            param.m_callback = () =>
            {
                var userEquip = Managers.User.GetEquip(m_equip_id);
                if (userEquip == null)
                    return;

                var hero = Managers.User.GetEquipMountHero(m_equip_id);
                if (hero == null)
                    return;

                hero.m_equip_id = 0;
                userEquip.m_mount = false;

                RefreshEquip();
            };

            Managers.UI.OpenWindow(WindowID.UIPopupCommon, param);
        }
    }

    private void UnEquipProcess()
    {
        var userEquip = Managers.User.GetEquip(m_equip_id);
        if (userEquip == null)
            return;

        var unit = Managers.User.GetUserHeroInfo(m_unit_kind);
        if (unit == null)
            return;

        unit.m_equip_id = 0;
        userEquip.m_mount = false;

        RefreshEquip();

        if (Managers.UI.ActiveWindow(WindowID.UIWindowUnit))
        {
            var ui = Managers.UI.GetWindow(WindowID.UIWindowUnit, false) as UIWindowUnit;
            if (ui == null)
                return;

            ui.SetUnitInfo(m_unit_kind);
        }
    }
}