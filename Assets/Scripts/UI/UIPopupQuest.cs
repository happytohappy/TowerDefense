using UnityEngine;

public class UIPopupQuest : UIWindowBase
{
    private const string SLOT_QUEST_PATH = "UI/Item/Slot_PopupQuest";

    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private RectTransform m_rect_root = null;

    [SerializeField] private GameObject m_empty = null;

    private QuestParam m_param = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupQuest;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        m_param = in_param as QuestParam;
        if (m_param == null)
            Managers.UI.CloseLast();

        switch (m_param.m_quest_type)
        {
            case EQuestType.Stage:
                RefreshUI_Stage();
                break;
            case EQuestType.Achievement:
                RefreshUI_Achievement();
                break;
        }
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void RefreshUI_Stage()
    {
        var missionCnt = 6;

        m_rect_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        var questList = GameController.GetInstance.StageMission;
        if (questList == null || questList.Count == 0)
        {
            m_empty.Ex_SetActive(true);
            return;
        }

        if (GameController.GetInstance.ClearMission.Count == missionCnt)
        {
            m_empty.Ex_SetActive(true);
            return;
        }

        m_empty.Ex_SetActive(false);
        foreach (var e in questList)
        {
            // 클리어하고 보상까지 받았으면 목록에서 삭제
            if (GameController.GetInstance.ClearMission.Contains(e.m_index))
                continue;

            var slotQuest = Managers.Resource.Instantiate(SLOT_QUEST_PATH, Vector3.zero, m_trs_root);
            var sc = slotQuest.GetComponent<Slot_PopupQuest>();

            sc.SetData(e);
        }
    }

    public void RefreshUI_Achievement()
    {
        m_rect_root.Ex_SetValue(0f);

        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        foreach (var e in Managers.User.GetMission())
        {
            var data = Managers.Table.GetAchievementData(e.m_kind, e.m_sequence);

            var slotQuest = Managers.Resource.Instantiate(SLOT_QUEST_PATH, Vector3.zero, m_trs_root);
            var sc = slotQuest.GetComponent<Slot_PopupQuest>();

            sc.SetData(data);
        }
    }
}