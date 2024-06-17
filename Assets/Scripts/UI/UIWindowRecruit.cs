using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIWindowRecruit : UIWindowBase
{
    private const string SLOT_RECRUIT_PATH = "UI/Item/Slot_Recruit";
    private const string ANI_RECRUIT_BTN   = "Ani_UIWindowRecruit_Btn";

    [Header("재화")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("슬롯 루트")]
    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private RectTransform m_rect_root = null;

    [Header("버튼")]
    [SerializeField] private Animator m_ani = null;
    [SerializeField] private GameObject m_go_open_all = null;
    [SerializeField] private GameObject m_go_cancel = null;
    [SerializeField] private GameObject m_go_recruit_1 = null;
    [SerializeField] private GameObject m_go_recruit_10 = null;

    [Header("뽑기")]
    [SerializeField] private TMP_Text m_recruit_count_1;
    [SerializeField] private Image m_recruit_goods_1;
    [SerializeField] private TMP_Text m_recruit_price_1;
    [SerializeField] private TMP_Text m_recruit_count_2;
    [SerializeField] private Image m_recruit_goods_2;
    [SerializeField] private TMP_Text m_recruit_price_2;

    private RecruitParam m_param;
    private WaitForSeconds m_wait = new WaitForSeconds(0.1f);
    private List<Slot_Recruit> m_recruit_list = new List<Slot_Recruit>();
    private int m_item_count;
    private int m_create_count;
    private int m_open_count;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowRecruit;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        if (in_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        m_param = in_param as RecruitParam;
        if (m_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        // 초기화
        Init();

        // 재화 갱신
        RefreshGoods();

        // 뽑기 카드
        RefreshRecruitList();
    }

    private void Init()
    {
        m_recruit_list.Clear();
        m_item_count = m_param.m_recruit_list.Count;
        m_open_count = 0;
        m_create_count = 0;

        var tableData_1 = Managers.Table.GetGachaInfoData(m_param.m_recruit_type, 1);
        if (tableData_1 == null)
            return;

        var tableData_2 = Managers.Table.GetGachaInfoData(m_param.m_recruit_type, 2);
        if (tableData_2 == null)
            return;

        m_recruit_count_1.Ex_SetText($"{tableData_1.m_recruit_count}회");
        m_recruit_goods_1.Ex_SetImage(Util.GetResourceImage(tableData_1.m_consumption_kind));
        m_recruit_price_1.Ex_SetText(Util.CommaText(tableData_1.m_consumption_amount));

        m_recruit_count_2.Ex_SetText($"{tableData_2.m_recruit_count}회");
        m_recruit_goods_2.Ex_SetImage(Util.GetResourceImage(tableData_2.m_consumption_kind));
        m_recruit_price_2.Ex_SetText(Util.CommaText(tableData_2.m_consumption_amount));
    }

    private void RefreshGoods()
    {
        Util.SetGoods(EGoods.Gold, m_text_gold);
        Util.SetGoods(EGoods.Ruby, m_text_ruby);
        Util.SetGoods(EGoods.Diamond, m_text_diamond);
    }

    private void RefreshRecruitList()
    {
        // 슬롯 초기화
        m_rect_root.Ex_SetValue(EScrollDir.Vertical, 0f);
        var cnt = m_trs_root.childCount;
        for (int i = 0; i < cnt; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(0).gameObject);

        foreach (var e in m_param.m_recruit_list)
        {
            var slot = Managers.Resource.Instantiate(SLOT_RECRUIT_PATH, Vector3.zero, m_trs_root);
            slot.transform.localScale = Vector3.one;
            slot.Ex_SetActive(false);

            var sc = slot.GetComponent<Slot_Recruit>();
            
            sc.SetData(e.Item1, e.Item2, 0, () =>
            {
                m_open_count++;
                if (m_item_count == m_open_count)
                {
                    m_go_open_all.Ex_SetActive(false);
                    m_go_cancel.Ex_SetActive(true);
                    m_go_recruit_1.Ex_SetActive(true);
                    m_go_recruit_10.Ex_SetActive(true);
                    m_ani.Ex_Play(ANI_RECRUIT_BTN);
                }
            });

            m_recruit_list.Add(sc);
            m_create_count++;
        }

        m_go_open_all.Ex_SetActive(true);
        m_go_cancel.Ex_SetActive(false);
        m_go_recruit_1.Ex_SetActive(false);
        m_go_recruit_10.Ex_SetActive(false);
        m_ani.Ex_Play(ANI_RECRUIT_BTN);

        StartCoroutine(CoRefreshRecruitList());
    }

    private IEnumerator CoRefreshRecruitList()
    {
        foreach (var e in m_recruit_list)
        {
            e.Ex_SetActive(true);

            yield return m_wait;
        }
    }

    public void OnClickOpenAll()
    {
        // 아이템 만들기전 이라면 리턴
        if (m_create_count == 0)
            return;

        // 아이템 만드는중 이라면 리턴
        if (m_item_count != m_create_count)
            return;

        StopAllCoroutines();
        foreach (var e in m_recruit_list)
        {
            if (e.GetOpen)
            {
                e.Ex_SetActive(true);
                e.OnClickOpen();
            }
        }

        m_go_open_all.Ex_SetActive(false);
        m_go_cancel.Ex_SetActive(true);
        m_go_recruit_1.Ex_SetActive(true);
        m_go_recruit_10.Ex_SetActive(true);
        m_ani.Ex_Play(ANI_RECRUIT_BTN);
    }

    public void OnClickCancel()
    {
        Managers.UI.CloseLast();
    }

    public void OnClickRecruit(int in_index)
    {
        var recruitData = Managers.Table.GetGachaInfoData(m_param.m_recruit_type, in_index);
        if (recruitData == null)
            return;

        var recruitList = Util.Recruit(m_param.m_recruit_type, recruitData.m_recruit_count);
        if (recruitList == null || recruitList.Count == 0)
            return;

        m_param.m_recruit_list = recruitList;

        // 초기화
        Init();

        // 재화 갱신
        RefreshGoods();

        // 뽑기 카드
        RefreshRecruitList();
    }
}