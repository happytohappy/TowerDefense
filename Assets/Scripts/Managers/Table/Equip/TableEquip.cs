using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, EquipInfoData> m_dic_equip_info_data = new Dictionary<int, EquipInfoData>();

    private void InitEquipTable()
    {
        InitEquipInfoTable();
    }

    public EquipInfoData GetEquipInfoData(int in_kind)
    {
        if (m_dic_equip_info_data.ContainsKey(in_kind))
            return m_dic_equip_info_data[in_kind];
        else
            return null;
    }
}