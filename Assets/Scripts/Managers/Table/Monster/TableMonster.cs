using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, MonsterInfoData> m_dic_monster_info_data = new Dictionary<int, MonsterInfoData>();
    private Dictionary<(int, int), MonsterStatusData> m_dic_monster_status_data = new Dictionary<(int, int), MonsterStatusData>();

    private void InitMonsterTable()
    {
        InitMonsterInfoTable();
        InitMonsterStatusTable();
    }


    public MonsterInfoData GetMonsterInfoData(int in_kind)
    {
        if (m_dic_monster_info_data.ContainsKey(in_kind))
            return m_dic_monster_info_data[in_kind];
        else
            return null;
    }

    public MonsterStatusData GetMonsterStatusData(int in_kind, int in_level)
    {
        var key = (in_kind, in_level);
        if (m_dic_monster_status_data.ContainsKey(key))
            return m_dic_monster_status_data[key];
        else
            return null;
    }
}