using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, BuffInfoData> m_dic_buff_info_data = new Dictionary<int, BuffInfoData>();
    private Dictionary<(int, int), BuffLevelData> m_dic_buff_level_data = new Dictionary<(int, int), BuffLevelData>();

    private void InitBuffTable()
    {
        InitBuffInfo();
        InitBuffLevel();
    }

    private void ClearBuffTable()
    {
        m_dic_buff_info_data.Clear();
        m_dic_buff_level_data.Clear();
    }

    public BuffInfoData GetBuffInfoData(int in_kind)
    {
        if (m_dic_buff_info_data.ContainsKey(in_kind))
            return m_dic_buff_info_data[in_kind];
        else
            return null;
    }

    public BuffLevelData GetBuffLevelData(int in_kind, int in_level)
    {
        var key = (in_kind, in_level);
        if (m_dic_buff_level_data.ContainsKey(key))
            return m_dic_buff_level_data[key];
        else
            return null;
    }
}