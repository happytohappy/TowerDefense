using UnityEngine;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitGachaGroup()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/Gacha_Group");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            GachaGroupData GachaGroupData = new GachaGroupData();
            GachaGroupData.m_kind = int.Parse(words[0]);
            GachaGroupData.m_reward = int.Parse(words[2]);

            m_dic_gacha_group_data.Add(GachaGroupData.m_kind, GachaGroupData);
        }
    }
}