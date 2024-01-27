using UnityEngine;

public class UIPopupUnit : UIWindowBase
{
    private const string UNIT_TIER_GROUP_PATH = "UI/Item/UnitTierGroup";

    [SerializeField] private Transform m_trs_root = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupUnit;
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
        for (int tier = 1; tier <= 6; tier++)
        {
            var tierGroup = Managers.Resource.Instantiate(UNIT_TIER_GROUP_PATH, Vector3.zero, m_trs_root);
            var sc = tierGroup.GetComponent<UnitTierGroup>();

            sc.SetTierUnit(tier);
        }
    }

    public void SetUnitInfo(int in_kind)
    {
        // 내가 보유한 타워의 레벨과 등급을 불러와서
        var Tower = Managers.User.GetUserTowerInfo(in_kind);
        if (Tower == null)
        {
            // 타워 보유하지 않은 셋팅 해주면 됨
        }
        else
        {
            // Hero Level 테이블을 조회해서 스탯 정보를 알아온다.
            var TowerLevelInfo = Managers.Table.GetHeroLevelData(in_kind, Tower.m_level);
        }
    }
}