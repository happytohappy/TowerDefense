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
        // ���� ������ Ÿ���� ������ ����� �ҷ��ͼ�
        var Tower = Managers.User.GetUserTowerInfo(in_kind);
        if (Tower == null)
        {
            // Ÿ�� �������� ���� ���� ���ָ� ��
        }
        else
        {
            // Hero Level ���̺��� ��ȸ�ؼ� ���� ������ �˾ƿ´�.
            var TowerLevelInfo = Managers.Table.GetHeroLevelData(in_kind, Tower.m_level);
        }
    }
}