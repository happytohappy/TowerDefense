public class UIWindowMain : UIWindowBase
{
    public override void Awake()
    {
        Window_ID = WindowID.UIWindowMain;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
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
}