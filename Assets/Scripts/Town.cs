using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [SerializeField] private ETownType m_town_type = ETownType.None;
    [SerializeField] private Vector3 m_offset = Vector3.zero;

    private Hud_TownInfo m_hud = null;
    private WaitForSeconds m_wait = new WaitForSeconds(1.0f);

    private void Start()
    {
        // 타운 인포 셋팅
        m_hud = Util.CreateHudTownInfo(m_town_type, this.transform, new Vector3(0f, 0f, 0f));

        StartCoroutine(CoRewardTime());
    }

    private IEnumerator CoRewardTime()
    {
        if (Managers.User.UserData.Town.TryGetValue(m_town_type, out var userData) == false)
            yield break;

        var townLevel = Managers.Table.GetTownLevelDataByLevel((int)m_town_type, userData.m_town_level);
        if (townLevel == null)
            yield break;

        while (true)
        {
            var nowTime = Util.UnixTimeNow();
            m_hud.RewardActive(nowTime >= userData.m_last_reward_time + townLevel.m_timespan);

            yield return m_wait;
        }
    }
}
