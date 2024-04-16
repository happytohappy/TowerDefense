using TMPro;
using UnityEngine;

public class UIWindowMain : UIWindowBase
{
    [SerializeField] private TMP_Text m_text_gold;
    [SerializeField] private TMP_Text m_text_ruby;
    [SerializeField] private TMP_Text m_text_diamond;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowMain;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        m_text_gold.Ex_SetText($"{Util.CommaText(Managers.User.UserData.Gold)}");
        m_text_ruby.Ex_SetText($"{Util.CommaText(Managers.User.UserData.Ruby)}");
        m_text_diamond.Ex_SetText($"{Util.CommaText(Managers.User.UserData.Diamond)}");
    }

    public void OnClickGame()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupGame);
    }

    public void OnClickShop(int in_tab_number)
    {
        var ui = Managers.UI.OpenWindow(WindowID.UIPopupShop) as UIPopupShop;
        if (ui == null)
            return;

        ui.ParentTab.SelectTab(in_tab_number);
    }

    public void OnClickReward()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupReward);
    }

    public void OnClickQuest()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupQuest);
    }

    public void OnClickSetting()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupSetting);
    }

    public void OnClickTown()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupTown);
    }

    public void OnClickEquipment()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupEquipment);
    }

    public void OnClickTreasure()
    {
        Managers.UI.OpenWindow(WindowID.UIPopupTreasure);
    }

    public void OnClickUnit()
    {
        Managers.UI.OpenWindow(WindowID.UIWindowUnit);
    }

    public void OnClickGacha()
    {
        var gachaReward = Managers.Table.GetGachaHero(1000);
        if (gachaReward == null)
            return;
    
        Managers.User.UpsertHero(gachaReward.m_item);
    }
}