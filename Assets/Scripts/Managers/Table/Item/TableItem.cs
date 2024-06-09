using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, ItemInfoData> m_dic_item_info_data = new Dictionary<int, ItemInfoData>();

    private void InitItemTable()
    {
        InitItemInfo();
    }

    public ItemInfoData GetItemInfoData(int in_kind)
    {
        if (m_dic_item_info_data.ContainsKey(in_kind))
            return m_dic_item_info_data[in_kind];
        else
            return null;
    }
}