using System.Collections.Generic;
using UnityEngine;

public partial class TableManager
{
    private Dictionary<int, HeroData> m_dic_hero_data = new Dictionary<int, HeroData>();

    private void InitHeroTable()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/TableHero");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            HeroData HeroData = new HeroData();
            HeroData.m_kind = int.Parse(words[0]);
            HeroData.m_path = words[1];
            HeroData.m_ATK = int.Parse(words[3]);
            HeroData.m_attack_speed = float.Parse(words[4]);
            HeroData.m_attack_range = float.Parse(words[5]);

            m_dic_hero_data.Add(HeroData.m_kind, HeroData);
        }
    }

    private void ClearHeroTable()
    {
        m_dic_hero_data.Clear();
    }

    public HeroData GetHeroData(int in_kind)
    {
        if (m_dic_hero_data.ContainsKey(in_kind))
            return m_dic_hero_data[in_kind];
        else
            return null;
    }
}