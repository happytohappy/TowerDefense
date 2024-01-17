using UnityEngine;

public class UIPopupShop : UIWindowBase
{
    [SerializeField] private ParentTab m_parent_tab;

    public ParentTab ParentTab => m_parent_tab;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupShop;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }
}
