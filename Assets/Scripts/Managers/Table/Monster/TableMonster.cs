using System.Collections.Generic;
using UnityEngine;

public partial class TableManager
{
    private Dictionary<int, MonsterData> m_dic_monster_data = new Dictionary<int, MonsterData>();

    private void InitMonsterTable()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/TableMonster");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            MonsterData MonsterData = new MonsterData();
            MonsterData.m_kind = int.Parse(words[0]);
            MonsterData.m_path = words[1];
            MonsterData.m_hp = int.Parse(words[3]);
            MonsterData.m_move_speed = float.Parse(words[4]);

            m_dic_monster_data.Add(MonsterData.m_kind, MonsterData);
        }
    }

    private void ClearMonsterTable()
    {
        m_dic_monster_data.Clear();
    }

    public MonsterData GetMonsterData(int in_kind)
    {
        if (m_dic_monster_data.ContainsKey(in_kind))
            return m_dic_monster_data[in_kind];
        else
            return null;
    }
}