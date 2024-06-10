using UnityEngine;
using System.Collections;
using TMPro;

public class UIWindowRecruit : UIWindowBase
{
    private const string SLOT_RECRUIT_PATH = "UI/Item/Slot_Recruit";

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

    RecruitParam m_param = null;

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

        // 재화 갱신
        RefreshGoods();

        // 뽑기 카드
        RefreshRecruitList();
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
        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        m_go_open_all.Ex_SetActive(true);
        m_go_cancel.Ex_SetActive(false);
        m_go_recruit_1.Ex_SetActive(false);
        m_go_recruit_10.Ex_SetActive(false);
        m_ani.Ex_Play("Ani_UIWindowRecruit_Btn");

        StartCoroutine(CoRefreshRecruitList());
    }

    private IEnumerator CoRefreshRecruitList()
    {
        foreach (var e in m_param.m_recruit_list)
        {
            var slot = Managers.Resource.Instantiate(SLOT_RECRUIT_PATH, Vector3.zero, m_trs_root);
            var sc = slot.GetComponent<Slot_Recruit>();

            sc.SetData(e.Item1, e.Item2, 0);

            yield return new WaitForSeconds(0.1f);
        }

        m_go_open_all.Ex_SetActive(false);
        m_go_cancel.Ex_SetActive(true);
        m_go_recruit_1.Ex_SetActive(true);
        m_go_recruit_10.Ex_SetActive(true);
        m_ani.Ex_Play("Ani_UIWindowRecruit_Btn");
    }

    public void OnClickOpenAll()
    {
        Managers.UI.CloseLast();
    }

    public void OnClickCancel()
    {
        Managers.UI.CloseLast();
    }

    public void OnClickRecruit_1()
    {

    }

    public void OnClickRecruit_10()
    {

    }
}