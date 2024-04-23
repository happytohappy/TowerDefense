using System.Collections.Generic;

public partial class TableManager
{
    public Dictionary<int, TreasureInfoData> m_dic_treasure_info_data = new Dictionary<int, TreasureInfoData>();
    public Dictionary<(int, int), TreasureLevelData> m_dic_treasure_level_data = new Dictionary<(int, int), TreasureLevelData>();

    private void InitTreasureTable()
    {
    }

    private void ClearTreasureTable()
    {
        m_dic_treasure_level_data.Clear();
    }

    public TreasureLevelData GetTreasureLevelData(int in_kind, int in_level)
    {
        var key = (in_kind, in_level);
        if (m_dic_treasure_level_data.ContainsKey(key))
            return m_dic_treasure_level_data[key];
        else
            return null;
    }
}