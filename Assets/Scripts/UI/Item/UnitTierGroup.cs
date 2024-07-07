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
        var heroTierList = Managers.Table.GetHeroInfoDataGroupByTier(in_tier);
        if (heroTierList == null || heroTierList.Count == 0)
            return;

        for (int i = 0; i < m_list_tier.Count; i++)
            m_list_tier[i].Ex_SetActive(i == in_tier - 1);

        foreach (var tierUnit in heroTierList)
        {
            var hero = Managers.Resource.Instantiate(UNIT_ICON_PATH, Vector3.zero, m_trs_unit);

            var sc = hero.GetComponent<UnitIcon>();
            sc.SetData(tierUnit.m_kind);
            sc.SetDataInfo(in_callback);

            if (Managers.User.UserData.ClearTutorial.Contains(2) == false && tierUnit.m_kind == CONST.TUTORIAL_RECRUIT_HERO)
                Managers.Tutorial.TutorialStart(hero);
            else if (Managers.User.UserData.ClearTutorial.Contains(3) == false && tierUnit.m_kind == CONST.TUTORIAL_RECRUIT_HERO)
                Managers.Tutorial.TutorialStart(hero);
        }
    }
}