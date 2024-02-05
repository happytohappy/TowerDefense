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

        // 여기서 내가 보유한 타워 정보 불러오고, 레벨 데이터 이용해서 레벨 별 타워 정보불러오고
        // 대충 데이터 UI 에 셋팅해준다.

    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }
}
