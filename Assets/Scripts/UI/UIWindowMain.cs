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
        Managers.UI.OpenWindow(WindowID.UIPopupGame);
    }

    public void OnClickShop(int in_tab_number)
    {
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
        Managers.UI.OpenWindow(WindowID.UIPopupTreasure);
    }

    public void OnClickUnit()
    {
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