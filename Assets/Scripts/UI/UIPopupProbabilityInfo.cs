using UnityEngine;
using TMPro;

public class UIPopupProbabilityInfo : UIWindowBase
{
    private const string TITLE_SLOT_PATH = "UI/Item/Slot_ProbabilityInfoTitle";
    private const string SLOT_PATH = "UI/Item/Slot_ProbabilityInfo";

    [SerializeField] private RectTransform m_rect_root = null;
    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private TMP_Text m_text_title = null;

    ProbabilityParam m_param = null;
         
    public override void Awake()
    {
        Window_ID = WindowID.UIPopupProbabilityInfo;
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

        m_param = in_param as ProbabilityParam;
        if (m_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        RefreshList();
    }

    private void RefreshList()
    {
        // ΩΩ∑‘ √ ±‚»≠
        m_rect_root.Ex_SetValue(EScrollDir.Vertical, 0f);
        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        var titleSlot = Managers.Resource.Instantiate(TITLE_SLOT_PATH, Vector3.zero, m_trs_root);
        titleSlot.transform.localScale = Vector3.one;

        var scTitle = titleSlot.GetComponent<Slot_ProbabilityInfoTitle>();
        scTitle.SetData("¿Ø¥÷");

        var repo = Managers.Table.GetRecruitInfoData(m_param.m_kind);
        if (repo == null)
            return;

        foreach (var e in repo)
        {
            var slot = Managers.Resource.Instantiate(SLOT_PATH, Vector3.zero, m_trs_root);
            slot.transform.localScale = Vector3.one;

            var sc = slot.GetComponent<Slot_ProbabilityInfo>();
            sc.SetData(e);
        }
    }
}