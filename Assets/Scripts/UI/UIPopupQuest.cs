using UnityEngine;

public class UIPopupQuest : UIWindowBase
{
    private const string SLOT_QUEST_PATH = "UI/Item/Slot_PopupQuest";

    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private RectTransform m_rect_root = null;

    [SerializeField] private GameObject m_empty = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupQuest;
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
        m_rect_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        if (Managers.Table.GetAllMissionInfoData().Count == 0)
        {
            m_empty.Ex_SetActive(true);
            return;
        }

        m_empty.Ex_SetActive(false);
        foreach (var e in Managers.Table.GetAllMissionInfoData())
        {
            var slotQuest = Managers.Resource.Instantiate(SLOT_QUEST_PATH, Vector3.zero, m_trs_root);
            var sc = slotQuest.GetComponent<Slot_PopupQuest>();

            sc.SetData();
        }
    }
}