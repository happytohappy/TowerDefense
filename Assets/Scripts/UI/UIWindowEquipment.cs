using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIWindowEquipment : UIWindowBase
{
    private const string EQUIP_SLOT_PATH = "UI/Item/Slot_UnitEquip_Scale70";

    [Header("Goods")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("Equip")]
    [SerializeField] private Image m_image_equip_icon = null;
    [SerializeField] private List<GameObject> m_grade_layout = new List<GameObject>();
    [SerializeField] private Image m_image_equip_hero_type = null;
    [SerializeField] private TMP_Text m_text_equip_name = null;

    [Header("ETC")]
    [SerializeField] private UnitIcon m_unit_slot = null;
    [SerializeField] private GameObject m_unit_stats = null;
    [SerializeField] private RectTransform m_rect_equip_root = null;
    [SerializeField] private Transform m_trs_equip_root = null;
    [SerializeField] private GameObject m_go_none_text = null;

    public GameObject LastSelect { get; set; }

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowEquipment;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        RefreshUI();
        RefreshInfo();
        RefreshGradeUP();
        RefreshEquip();
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    private void RefreshUI()
    {
        m_text_gold.Ex_SetText($"{Util.CommaText(Util.GetGoods(EGoods.Gold))}");
        m_text_ruby.Ex_SetText($"{Util.CommaText(Util.GetGoods(EGoods.Ruby))}");
        m_text_diamond.Ex_SetText($"{Util.CommaText(Util.GetGoods(EGoods.Diamond))}");
    }

    private void RefreshInfo()
    {

    }

    private void RefreshGradeUP()
    {

    }

    private void RefreshEquip(EEquipType in_euqip_type = EEquipType.None)
    {
        m_rect_equip_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_equip_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_equip_root.GetChild(i).gameObject);

        var equipList = Managers.User.GetEquipList(in_euqip_type);
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

        Util.SetEquipIcon(m_image_equip_icon, equip.m_equip_icon);
        Util.SetUnitType(m_image_equip_hero_type, equip.m_hero_type);
        m_text_equip_name.Ex_SetText(equip.m_equip_type.ToString());

        for (int i = 0; i < m_grade_layout.Count; i++)
            m_grade_layout[i].Ex_SetActive(i == (int)equip.m_equip_grade);
    }
} 