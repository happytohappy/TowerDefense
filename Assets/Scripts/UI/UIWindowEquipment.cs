using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public enum EquipTab
{
    Info,
    GradeUP
}

public class UIWindowEquipment : UIWindowBase
{
    private const string EQUIP_SLOT_PATH = "UI/Item/Slot_UnitEquip_Scale70";

    [Header("Goods")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("Equip Main")]
    [SerializeField] private Image m_image_equip_icon = null;
    [SerializeField] private List<GameObject> m_grade_layout = new List<GameObject>();
    [SerializeField] private Image m_image_equip_hero_type = null;
    [SerializeField] private TMP_Text m_text_equip_name = null;
    [SerializeField] private GameObject m_go_none_equip = null;

    [Header("Unit")]
    [SerializeField] private UnitIcon m_unit_slot = null;

    [Header("Stat")]
    [SerializeField] private TMP_Text m_text_atk = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private TMP_Text m_text_critical = null;
    [SerializeField] private TMP_Text m_text_critical_chance = null;

    [Header("Grade UP")]
    [SerializeField] private Slot_Equip m_equip_left = null;
    [SerializeField] private Slot_Equip m_equip_right = null;
    [SerializeField] private TMP_Text m_text_merge_per = null;
    [SerializeField] private ExtentionButton m_btn_merge = null;

    [Header("Equip List")]
    [SerializeField] private RectTransform m_rect_equip_root = null;
    [SerializeField] private Transform m_trs_equip_root = null;
    [SerializeField] private GameObject m_go_none_text = null;

    private EquipTab m_equip_tab = EquipTab.Info;
    private List<long> m_merge_equip_list = new List<long>();
    private int m_merge_kind;

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

        m_equip_tab = EquipTab.Info;

        RefreshUI();
        RefreshInfo();
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

    public void RefreshInfo()
    {
        m_equip_tab = EquipTab.Info;

        RefreshEquip();
    }

    public void RefreshGradeUP()
    {
        m_equip_tab = EquipTab.GradeUP;

        m_merge_kind = 0;
        m_merge_equip_list.Clear();

        RefreshEquipGradeUP();
    }

    private void RefreshEquip()
    {
        m_rect_equip_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_equip_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_equip_root.GetChild(i).gameObject);

        var equipList = Managers.User.GetEquipList();
        if (equipList.Count == 0)
        {
            m_go_none_text.Ex_SetActive(true);
            m_go_none_equip.Ex_SetActive(true);

            m_text_atk.Ex_SetText("0");
            m_text_speed.Ex_SetText("0");
            m_text_range.Ex_SetText("0");
            m_text_critical.Ex_SetText("0");
            m_text_critical_chance.Ex_SetText("0");
            return;
        }

        Util.EquipSort(ref equipList);

        m_go_none_text.Ex_SetActive(false);
        m_go_none_equip.Ex_SetActive(false);

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

    private void RefreshEquipGradeUP()
    {
        m_rect_equip_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_equip_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_equip_root.GetChild(i).gameObject);

        m_equip_left.gameObject.Ex_SetActive(false);
        m_equip_right.gameObject.Ex_SetActive(false);

        List<UserManager.EquipInfo> equipList = new List<UserManager.EquipInfo>();
        if (m_merge_equip_list.Count == 0)
        {
            equipList = Managers.User.GetEquipList();
        }
        else
        {
            var userEquip = Managers.User.GetEquip(m_merge_equip_list[0]);
            equipList = Managers.User.GetEquipList(userEquip.m_kind);
        }
       
        if (equipList.Count == 0)
        {
            m_go_none_text.Ex_SetActive(true);
            m_btn_merge.interactable = false;
            return;
        }

        Util.EquipSort(ref equipList);

        int mountCnt = 0;        
        m_go_none_text.Ex_SetActive(false);
        foreach (var e in equipList)
        {
            var equipSlot = Managers.Resource.Instantiate(EQUIP_SLOT_PATH, Vector3.zero, m_trs_equip_root);
            var item = equipSlot.GetComponentInChildren<Slot_Equip>();

            var select = m_merge_equip_list.Contains(e.m_unique_id);
            if (select)
            {
                if (mountCnt == 0)
                {
                    m_merge_kind = e.m_kind;
                    m_equip_left.SetData(e.m_unique_id, e.m_kind, e.m_mount, false, false);
                    m_equip_left.gameObject.Ex_SetActive(true);
                    mountCnt++;
                }
                else if (mountCnt == 1)
                {
                    m_equip_right.SetData(e.m_unique_id, e.m_kind, e.m_mount, false, false);
                    m_equip_right.gameObject.Ex_SetActive(true);
                    mountCnt++;
                }
            }
            
            item.SetData(e.m_unique_id, e.m_kind, e.m_mount, false, select);
        }

        if (mountCnt == 2)
        {
            var nextEquip = Managers.Table.GetEquipInfoData(m_merge_kind + 1);
            if (nextEquip == null)
            {
                // 최종 단계 아이템
                m_text_merge_per.Ex_SetText("0%");
                m_btn_merge.interactable = false;
            }
            else
            {
                m_text_merge_per.Ex_SetText($"{nextEquip.m_gradeup_per}%");
                m_btn_merge.interactable = true;
            }
        }
        else
        {
            m_text_merge_per.Ex_SetText("0%");
            m_btn_merge.interactable = false;
        }
    }

    public void OnClickGradeUP()
    {
        var nextEquip = Managers.Table.GetEquipInfoData(m_merge_kind + 1);
        if (nextEquip == null)
            return;

        // 성공 실패 유무 없이 아이템들은 모두 삭제
        foreach (var e in m_merge_equip_list)
            Managers.User.RemoveEquip(e);

        EquipResultParam param = new EquipResultParam();

        // 확률 통과 못하면 리턴
        var ran = UnityEngine.Random.Range(1, 101);
        if (ran <= nextEquip.m_gradeup_per)
        {
            // 성공
            Managers.User.InsertEquip(nextEquip.m_kind);

            param.m_success = true;
            param.m_equip_kind = nextEquip.m_kind;
        }
        else
        {
            // 실패
            param.m_success = false;
        }

        RefreshGradeUP();

        Managers.UI.OpenWindow(WindowID.UIWindowEquipmentResult, param);
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
                m_unit_slot.SetHaveUnit(hero.m_kind, heroInfo.m_rarity, hero.m_grade, hero.m_level, null);
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
    }

    public void SelectEquip(long in_unique, int in_kind)
    {
        switch (m_equip_tab)
        {
            case EquipTab.Info:
                SetEquipInfo(in_unique, in_kind);
                break;
            case EquipTab.GradeUP:
                if (m_merge_equip_list.Contains(in_unique))
                    m_merge_equip_list.Remove(in_unique);
                else if(m_merge_equip_list.Count < 2)
                    m_merge_equip_list.Add(in_unique);

                RefreshEquipGradeUP();
                break;
        }
    }
} 