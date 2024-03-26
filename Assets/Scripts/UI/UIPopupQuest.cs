using UnityEngine;

public class UIPopupQuest : UIWindowBase
{
    private const string UNIT_TIER_GROUP_PATH = "UI/Item/UnitTierGroup";

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
        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        foreach (var e in Managers.Table.GetAllMissionInfoData())
        {

        }
    }
}