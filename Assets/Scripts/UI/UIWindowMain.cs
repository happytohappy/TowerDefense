using UnityEngine;
using TMPro;

public class UIWindowMain : UIWindowBase
{
    [Header("재화")]
    [SerializeField] private TMP_Text m_text_gold = null;
    [SerializeField] private TMP_Text m_text_ruby = null;
    [SerializeField] private TMP_Text m_text_diamond = null;

    [Header("레드닷")]
    [SerializeField] private GameObject m_go_attendance = null;

    [Header("튜토리얼")]
    [SerializeField] private GameObject m_go_tutorial_shop = null;
    [SerializeField] private GameObject m_go_tutorial_unit = null;
    [SerializeField] private GameObject m_go_tutorial_stage = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowMain;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        RefreshUI();

        CheckTutorial();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnClickReset();
        }

        if (Input.touchCount == 3)
        {
            OnClickReset();
        }
    }

    private void CheckTutorial()
    {
        do
        {
            if (Managers.User.UserData.ClearTutorial.Contains(1) == false)
            {
                // 상점 진입
                Managers.Tutorial.TutorialStart(m_go_tutorial_shop, ETutorialDir.Center, new Vector3(0f, 100f, 0f), "#상점 클릭");
                break;
            }

            if (Managers.User.UserData.ClearTutorial.Contains(2) == false)
            {
                // 영웅 진입
                Managers.Tutorial.TutorialStart(m_go_tutorial_unit);
                break;
            }

            if (Managers.User.UserData.ClearTutorial.Contains(3) == false)
            {
                // 영웅 진입
                Managers.Tutorial.TutorialStart(m_go_tutorial_unit);
                break;
            }

            if (Managers.User.UserData.ClearTutorial.Contains(5) == false)
            {
                // 스테이지 진입
                Managers.Tutorial.TutorialStart(m_go_tutorial_stage);
                break;
            }
        }
        while (false);
    }

    public void RefreshUI()
    {
        Util.SetGoods(EGoods.Gold, m_text_gold);
        Util.SetGoods(EGoods.Ruby, m_text_ruby);
        Util.SetGoods(EGoods.Diamond, m_text_diamond);

        m_go_attendance.Ex_SetActive(Util.RedDotAttendance());
    }

    public void OnClickGame()
    {
        Managers.Tutorial.TutorialEnd();

        Managers.UI.OpenWindow(WindowID.UIPopupGame);
    }

    public void OnClickShop(int in_tab_number)
    {
        Managers.Tutorial.TutorialEnd();

        EShopTab shopTab = (EShopTab)in_tab_number;

        var param = new ShopParam();
        param.m_tab = shopTab;

        Managers.UI.OpenWindow(WindowID.UIWindowShop, param);
    }

    public void OnClickReward()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupReward);
    }

    public void OnClickQuest()
    {
        QuestParam param = new QuestParam();
        param.m_quest_type = EQuestType.Achievement;

        Managers.UI.OpenWindow(WindowID.UIPopupQuest, param);
    }

    public void OnClickSetting()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupSetting);
    }

    public void OnClickEquipment()
    {
        Managers.UI.OpenWindow(WindowID.UIWindowEquipment);
    }

    public void OnClickTreasure()
    {
        Managers.UI.OpenWindow(WindowID.UIWindowTreasure);
    }

    public void OnClickUnit()
    {
        Managers.Tutorial.TutorialEnd();

        Managers.UI.OpenWindow(WindowID.UIWindowUnit);
    }

    // 여기부터는 테스트 버튼
    public void OnClickGachaUnit()
    {
        var gachaReward = Managers.Table.GetGachaReward(1000);
        if (gachaReward == null)
            return;
    
        Managers.User.UpsertHero(gachaReward.m_item);
    }

    public void OnClickEquip()
    {
        var tableEquip = Managers.Table.GetAllEquipInfoData();
        if (tableEquip == null)
            return;

        foreach (var e in tableEquip)
            Managers.User.InsertEquip(e.Value.m_kind);
    }

    public void OnClickReset()
    {
        Managers.User.InitUserData = true;

        Managers.BackEnd.DeleteAccount();

        PlayerPrefs.DeleteAll();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void OnClickGachaTreasure()
    {
        var result = Managers.BackEnd.GetProbabilitysTest();
        
        foreach (var e in result)
        {
            Managers.User.UpsertTreasure(int.Parse(e.itemID), 10);

        }
    }
}