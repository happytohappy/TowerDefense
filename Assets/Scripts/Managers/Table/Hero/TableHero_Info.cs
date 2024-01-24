using UnityEngine;
using System;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitHeroInfo()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/Hero_Info");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            HeroInfoData HeroInfoData = new HeroInfoData();
            HeroInfoData.m_kind = int.Parse(words[0]);
            HeroInfoData.m_path = words[1];
            HeroInfoData.m_name = words[2];
            HeroInfoData.m_desc = words[3];
            HeroInfoData.m_type = (TowerType)Enum.Parse(typeof(TowerType), words[4]);
            HeroInfoData.m_tier = int.Parse(words[5]);
            HeroInfoData.m_rarity = (TowerRarity)Enum.Parse(typeof(TowerRarity), words[6]);

            m_dic_hero_info_data.Add(HeroInfoData.m_kind, HeroInfoData);
        }
    }
}