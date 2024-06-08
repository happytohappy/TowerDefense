using UnityEngine;

public class UIPopupEquipment : UIWindowBase
{
    private const string EQUIP_SLOT_PATH = "UI/Item/Slot_UnitEquip_Scale70";

    [Header("장비 메인")]
    [SerializeField] private EquipBaseInfo m_equip_base_info = null;

    [Header("장비 리스트")]
    [SerializeField] private RectTransform m_rect_equip_root = null;
    [SerializeField] private Transform m_trs_equip_root = null;
    [SerializeField] private GameObject m_go_none_equip = null;
    [SerializeField] private GameObject m_go_none_text = null;

    [Header("장비 장착 & 해제")]
    [SerializeField] private GameObject m_go_equip = null;
    [SerializeField] private GameObject m_go_unequip = null;
    [SerializeField] private ExtentionButton m_btn_equip = null;

    EquipInfoParam m_param;
    private int m_equip_kind;
    private long m_equip_id;
    private GameObject m_last_select;

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

        m_param = in_param as EquipInfoParam;
        if (m_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        // 장비 리스트
        RefreshEquipList();
    }

    private void RefreshEquipList()
    {
        var userInfo = Managers.Table.GetHeroInfoData(m_param.m_hero_kind);
        if (userInfo == null)
            return;

        var userHero = Managers.User.GetUserHeroInfo(m_param.m_hero_kind);
        if (userHero == null)
            return;

        var equipList = Managers.User.GetEquipList(userInfo.m_equip_type);
        if (equipList.Count == 0)
        {
            m_go_none_text.Ex_SetActive(true);
            m_go_none_equip.Ex_SetActive(true);

            m_go_equip.Ex_SetActive(true);
            m_go_unequip.Ex_SetActive(false);

            m_btn_equip.interactable = false;
            return;
        }
        
        m_go_none_text.Ex_SetActive(false);
        m_go_none_equip.Ex_SetActive(false);

        // 장비 슬롯 초기화
        m_rect_equip_root.Ex_SetValue(0f);
        var cnt = m_trs_equip_root.childCount;
        for (int i = 0; i < cnt; i++)
            Managers.Resource.Destroy(m_trs_equip_root.GetChild(0).gameObject);

        // 장비 정렬
        Util.EquipSort(ref equipList, userHero.m_equip_id);
         
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
                        SetEquipInfo(equip_unique, equip_kind);

                        m_last_select = select;
                    }
                    return;
                }

                SetEquipInfo(equip_unique, equip_kind);

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

    public void SetEquipInfo(long in_equip_unique, int in_equip_kind)
    {
        var userEquip = Managers.User.GetEquip(in_equip_unique);
        if (userEquip == null)
            return;

        m_equip_id = in_equip_unique;
        m_equip_kind = in_equip_kind;

        m_equip_base_info.SetData(m_equip_id, m_equip_kind);

        if (userEquip.m_mount)
        {
            m_go_equip.Ex_SetActive(false);
            m_go_unequip.Ex_SetActive(true);
        }
        else
        {
            m_go_equip.Ex_SetActive(true);
            m_go_unequip.Ex_SetActive(false);
            m_btn_equip.interactable = true;
        }
    }

    public void OnClickEquip()
    {
        var userHero = Managers.User.GetUserHeroInfo(m_param.m_hero_kind);
        if (userHero == null)
            return;

        var userEquip = Managers.User.GetEquip(m_equip_id);
        if (userEquip == null)
            return;

        // 기존에 착용하고 있던 장비는 해제 상태로 변경
        var userPreEquip = Managers.User.GetEquip(userHero.m_equip_id);
        if (userEquip != null)
            userEquip.m_mount = false;

        userHero.m_equip_id = m_equip_id;
        userEquip.m_mount = true;

        RefreshEquipList();
        m_param.m_callback?.Invoke();
    }

    public void OnClickUnEquip()
    {
        var userHero = Managers.User.GetEquipMountHero(m_equip_id);
        if (userHero == null)
            return;

        if (userHero.m_equip_id == m_equip_id)
        {
            UnEquipProcess(userHero);
        }
        else
        {
            CommonInfoParam param = new CommonInfoParam();
            param.m_contents = "니꺼 아닌데 진짜 해제 시킬거임???";
            param.m_callback = () =>
            {
                UnEquipProcess(userHero);
            };

            Managers.UI.OpenWindow(WindowID.UIPopupCommon, param);
        }
    }

    private void UnEquipProcess(UserManager.HeroInfo in_hero)
    {
        var userEquip = Managers.User.GetEquip(m_equip_id);

        in_hero.m_equip_id = 0;
        userEquip.m_mount = false;

        RefreshEquipList();
        m_param.m_callback?.Invoke();
    }
}