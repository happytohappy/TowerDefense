using System.Collections.Generic;

public partial class TableManager
{
    public Dictionary<int, SynergyInfoData> m_dic_synergy_info_data  = new Dictionary<int, SynergyInfoData>();

    private void InitSynergyTable()
    {
    }

    private void ClearSynergyTable()
    {
        m_dic_synergy_info_data.Clear();
    }

    public SynergyInfoData GetSynergyInfoData(int in_kind)
    {
        if (m_dic_synergy_info_data.ContainsKey(in_kind))
            return m_dic_synergy_info_data[in_kind];
        else
            return null;
    }
}