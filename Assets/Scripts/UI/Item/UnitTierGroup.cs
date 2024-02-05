using TMPro;
using UnityEngine;

public class UnitTierGroup : MonoBehaviour
{
    private const string UNIT_ICON_PATH = "UI/Item/UnitIcon";

    [SerializeField] private TMP_Text m_text_tier = null;
    [SerializeField] private Transform m_trs_unit = null;

    public void SetTierUnit(int in_tier)
    {
        var UnitTierList = Managers.Table.GetHeroInfoDataGroupByTier(in_tier);
        if (UnitTierList == null || UnitTierList.Count == 0)
            return;

        m_text_tier.Ex_SetText($"클래스 {in_tier}");

        foreach (var tierUnit in UnitTierList)
        {
            var unit = Managers.Resource.Instantiate(UNIT_ICON_PATH, Vector3.zero, m_trs_unit);
            var sc = unit.GetComponent<UnitIcon>();

            var userTower = Managers.User.GetUserHeroInfo(tierUnit.m_kind);
            if (userTower != null)
            {
                // 보유하고 있는 타워
                sc.SetHaveUnit(tierUnit.m_kind, ((int)tierUnit.m_rarity).ToString());
            }
            else
            {
                sc.SetNoneUnit(tierUnit.m_kind);
            }
        }
    }
}