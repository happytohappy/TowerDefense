using UnityEngine;
using System;
using System.Collections.Generic;

public class UnitTierGroup : MonoBehaviour
{
    private const string UNIT_ICON_PATH = "UI/Item/UnitIcon";

    [SerializeField] private List<GameObject> m_list_tier = new List<GameObject>();
    [SerializeField] private Transform m_trs_unit = null;

    public void SetTierUnit(int in_tier, Action<int, GameObject> in_callback)
    {
        var UnitTierList = Managers.Table.GetHeroInfoDataGroupByTier(in_tier);
        if (UnitTierList == null || UnitTierList.Count == 0)
            return;

        for (int i = 0; i < m_list_tier.Count; i++)
            m_list_tier[i].Ex_SetActive(i == in_tier - 1);

        foreach (var tierUnit in UnitTierList)
        {
            var unit = Managers.Resource.Instantiate(UNIT_ICON_PATH, Vector3.zero, m_trs_unit);
            var sc = unit.GetComponent<UnitIcon>();

            var userTower = Managers.User.GetUserHeroInfo(tierUnit.m_kind);
            if (userTower != null)
            {
                // 보유하고 있는 타워
                sc.SetHaveUnit(tierUnit.m_kind, tierUnit.m_rarity, userTower.m_grade, userTower.m_level, in_callback);
            }
            else
            {
                sc.SetNoneUnit(tierUnit.m_kind, tierUnit.m_rarity, in_callback);
            }
        }

        //in_callback.Invoke();
    }
}