using UnityEngine;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitGachaReward()
    {
        int totalRate = 0;
        int lastKind = 0;

        TextAsset TextFile = Resources.Load<TextAsset>("Table/Gacha_Reward");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            GachaRewardData GachaRewardData = new GachaRewardData();
            GachaRewardData.m_kind = int.Parse(words[0]);

            if (lastKind != GachaRewardData.m_kind)
                totalRate = 0;

            lastKind = GachaRewardData.m_kind;
            GachaRewardData.m_item = int.Parse(words[1]);
            GachaRewardData.m_amount = int.Parse(words[2]);
            GachaRewardData.m_rate = int.Parse(words[3]);
            GachaRewardData.m_rate_min = totalRate;
            GachaRewardData.m_rate_max = totalRate + GachaRewardData.m_rate;
            totalRate += GachaRewardData.m_rate;

            if (m_dic_gacha_reward_data.ContainsKey(GachaRewardData.m_kind))
                m_dic_gacha_reward_data[GachaRewardData.m_kind].Add(GachaRewardData);
            else
                m_dic_gacha_reward_data.Add(GachaRewardData.m_kind, new List<GachaRewardData>() { GachaRewardData });
        }
    }
}