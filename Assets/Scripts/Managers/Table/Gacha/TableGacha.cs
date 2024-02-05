using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, GachaGroupData> m_dic_gacha_group_data = new Dictionary<int, GachaGroupData>();
    private Dictionary<int, List<GachaRewardData>> m_dic_gacha_reward_data = new Dictionary<int, List<GachaRewardData>>();

    private void InitGachaTable()
    {
        InitGachaGroup();
        InitGachaReward();
    }

    private void ClearGachaTable()
    {
        m_dic_gacha_group_data.Clear();
        m_dic_gacha_reward_data.Clear();
    }

    public GachaGroupData GetGachaGroupData(int in_kind)
    {
        if (m_dic_gacha_group_data.ContainsKey(in_kind))
            return m_dic_gacha_group_data[in_kind];
        else
            return null;
    }

    public List<GachaRewardData> GetGachaRewardsData(int in_kind)
    {
        if (m_dic_gacha_reward_data.ContainsKey(in_kind))
            return m_dic_gacha_reward_data[in_kind];
        else
            return null;
    }


    public GachaRewardData GetGachaHero(int in_kind)
    {
        var gachaGroup = GetGachaGroupData(in_kind);
        if (gachaGroup == null)
            return null;

        var gachaRewards = GetGachaRewardsData(gachaGroup.m_reward);
        if (gachaRewards == null)
            return null;

        var gachaIndex = UnityEngine.Random.Range(0, 10000);
        foreach (var gachaReward in gachaRewards)
        {
            if (gachaIndex >= gachaReward.m_rate_min && gachaIndex < gachaReward.m_rate_max)
            {
                return gachaReward;
            }
        }

        return null;
    }
}