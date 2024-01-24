using UnityEngine;
using System;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitHeroLevel()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/Hero_Level");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            HeroLevelData HeroLevelData = new HeroLevelData();
            HeroLevelData.m_kind = int.Parse(words[0]);
            HeroLevelData.m_level = int.Parse(words[1]);
            HeroLevelData.m_atk = int.Parse(words[2]);
            HeroLevelData.m_speed = float.Parse(words[3]);
            HeroLevelData.m_range = float.Parse(words[4]);
            HeroLevelData.m_critical = int.Parse(words[5]);
            HeroLevelData.m_critical_chance = int.Parse(words[5]);

            m_dic_hero_level_data.Add((HeroLevelData.m_kind, HeroLevelData.m_level), HeroLevelData);
        }
    }
}