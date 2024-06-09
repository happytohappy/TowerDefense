using UnityEngine;
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

    [Header("재화")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("장비 메인")]
    [SerializeField] private EquipBaseInfo m_equip_base_info = null;

    [Header("탭")]
    [SerializeField] private ParentTab m_parent_tab = null;

    [Header("장비 리스트")]
    [SerializeField] private RectTransform m_rect_equip_root = null;
    [SerializeField] private Transform m_trs_equip_root = null;
    [SerializeField] private GameObject m_go_none_equip = null;
    [SerializeField] private GameObject m_go_none_text = null;

    [Header("장비 합성")]
    [SerializeField] private Slot_Equip m_equip_left = null;
    [SerializeField] private Slot_Equip m_equip_right = null;
    [SerializeField] private TMP_Text m_text_merge_per = null;
    [SerializeField] private ExtentionButton m_btn_merge = null;

    private EquipTab m_equip_tab = EquipTab.Info;
   
    // Info 변수
    private long m_equip_id;
    private GameObject m_last_select;

    // Merge 변수
    private int m_merge_kind;
    private List<long> m_merge_equip_list = new List<long>();
    private List<UserManager.EquipInfo> m_equip_list = new List<UserManager.EquipInfo>();

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

        OnClickUIInfo();
    }

    private void RefreshUI()
    {
        Util.SetGoods(EGoods.Gold, m_text_gold);
        Util.SetGoods(EGoods.Ruby, m_text_ruby);
        Util.SetGoods(EGoods.Diamond, m_text_diamond);
    }

    private void TabClickInit()
    {
        m_equip_id = 0;
        m_last_select = null;
        m_merge_kind = 0;
        m_merge_equip_list.Clear();
        m_equip_list.Clear();
    }

    public void OnClickUIInfo()
    {
        TabClickInit();

        m_equip_tab = EquipTab.Info;
        m_parent_tab.SelectTab((int)m_equip_tab);

        RefreshEquipList_Info();
    }

    public void OnClickUIGradeUp()
    {
        TabClickInit();

        m_equip_tab = EquipTab.GradeUP;
        m_parent_tab.SelectTab((int)m_equip_tab);

        RefreshEquipList_GradUp();
    }

    private void RefreshEquipList_Info()
    {
        var equipList = Managers.User.GetEquipList();
        if (equipList.Count == 0)
        {
            m_go_none_text.Ex_SetActive(true);
            m_go_none_equip.Ex_SetActive(true);
            return;
        }

        m_go_none_text.Ex_SetActive(false);
        m_go_none_equip.Ex_SetActive(false);

        // 장비 슬롯 초기화
        m_rect_equip_root.Ex_SetValue(EScrollDir.Vertical, 0f);
        var cnt = m_trs_equip_root.childCount;
        for (int i = 0; i < cnt; i++)
            Managers.Resource.Destroy(m_trs_equip_root.GetChild(0).gameObject);

        Util.EquipSort(ref equipList);

        bool first = true;
        foreach (var e in equipList)
        {
            var equipSlot = Managers.Resource.Instantiate(EQUIP_SLOT_PATH, Vector3.zero, m_trs_equip_root);
            equipSlot.transform.localScale = Vector3.one;

            var item = equipSlot.GetComponentInChildren<Slot_Equip>();

            item.SetData(e.m_unique_id, e.m_kind, e.m_mount, e.m_new, first, (equip_kind, equip_unique, select) =>
            {
                if (m_equip_id == equip_unique)
                {
                    if (m_last_select == null)
                    {
                        m_equip_id = equip_unique;
                        m_equip_base_info.SetData(equip_unique, equip_kind);
                        m_last_select = select;
                    }
                    return;
                }

                m_equip_id = equip_unique;
                m_equip_base_info.SetData(equip_unique, equip_kind);
                m_last_select.Ex_SetActive(false);
                m_last_select = select;
            });

            if (first)
            {
                first = false;
                item.OnClickEquip();
            }
        }
    }

    private void RefreshEquipList_GradUp()
    {
        // 장비가 하나도 없다면
        var equipList = Managers.User.GetEquipList();
        if (equipList.Count == 0)
        {
            m_go_none_text.Ex_SetActive(true);
            m_go_none_equip.Ex_SetActive(true);
            m_btn_merge.interactable = false;
            return;
        }

        m_go_none_text.Ex_SetActive(false);
        m_go_none_equip.Ex_SetActive(false);

        // 장비 슬롯 초기화
        m_rect_equip_root.Ex_SetValue(EScrollDir.Vertical, 0f);
        var cnt = m_trs_equip_root.childCount;
        for (int i = 0; i < cnt; i++)
            Managers.Resource.Destroy(m_trs_equip_root.GetChild(0).gameObject);

        // 좌우 슬롯 초기화
        m_equip_left.gameObject.Ex_SetActive(false);
        m_equip_right.gameObject.Ex_SetActive(false);

        if (m_merge_equip_list.Count == 0)
        {
            // 선택된 장비가 없다면 전체 장비
            m_equip_list.AddRange(equipList);
        }
        else
        {
            // 선택된 장비가 있다면 같은 종류의 장비만
            var userEquip = Managers.User.GetEquip(m_merge_equip_list[0]);
            m_equip_list = Managers.User.GetEquipList(userEquip.m_kind);
        }

        var firstUniqueID = m_merge_equip_list.Count > 0 ? m_merge_equip_list[0] : 0;
        Util.EquipSortMerge(ref m_equip_list, firstUniqueID);

        int mountCnt = 0;        
        foreach (var e in m_equip_list)
        {
            var equipSlot = Managers.Resource.Instantiate(EQUIP_SLOT_PATH, Vector3.zero, m_trs_equip_root);
            equipSlot.transform.localScale = Vector3.one;

            var item = equipSlot.GetComponentInChildren<Slot_Equip>();

            var select = m_merge_equip_list.Contains(e.m_unique_id);
            if (select)
            {
                if (mountCnt == 0)
                {
                    m_merge_kind = e.m_kind;
                    m_equip_left.SetData(e.m_unique_id, e.m_kind, e.m_mount, false, false, null);
                    m_equip_left.gameObject.Ex_SetActive(true);
                    mountCnt++;
                }
                else if (mountCnt == 1)
                {
                    m_equip_right.SetData(e.m_unique_id, e.m_kind, e.m_mount, false, false, null);
                    m_equip_right.gameObject.Ex_SetActive(true);
                    mountCnt++;
                }
            }
            
            item.SetData(e.m_unique_id, e.m_kind, e.m_mount, false, select, (equip_kind, equip_unique, select) =>
            {
                if (m_merge_equip_list.Contains(equip_unique))
                    m_merge_equip_list.Remove(equip_unique);
                else if (m_merge_equip_list.Count < 2)
                    m_merge_equip_list.Add(equip_unique);
                else
                {
                    item.DisableSelect();
                    return;
                }

                RefreshEquipList_GradUp();
            });
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

        OnClickUIGradeUp();

        Managers.UI.OpenWindow(WindowID.UIWindowEquipmentResult, param);
    }
} 