public class UIPopupUnitBuy : UIWindowBase
{
    private GachaHeroParam m_param = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupUnitBuy;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);
        
        if (in_param == null)
            Managers.UI.CloseLast();

        m_param = in_param as GachaHeroParam;
        if (m_param == null)
            Managers.UI.CloseLast();

        RefreshUI();
    }

    private void RefreshUI()
    {
        var heroData = Managers.Table.GetHeroInfoData(m_param.m_hero_kind);

        // ���⼭ ���� ������ Ÿ�� ���� �ҷ�����, ���� ������ �̿��ؼ� ���� �� Ÿ�� �����ҷ�����
        // ���� ������ UI �� �������ش�.

    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }
}
