using System.Collections.Generic;

public partial class TableManager
{
    public Dictionary<int, SynergyInfoData> m_dic_synergy_info_data  = new Dictionary<int, SynergyInfoData>();
    public Dictionary<EHeroType, List<SynergyInfoData>> m_dic_synergy_info_data_by_hero_type  = new Dictionary<EHeroType, List<SynergyInfoData>>();

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

    public List<SynergyInfoData> GetSynergyInfoDataList(EHeroType in_hero_type)
    {
        if (m_dic_synergy_info_data_by_hero_type.ContainsKey(in_hero_type))
            return m_dic_synergy_info_data_by_hero_type[in_hero_type];
        else
            return null;
    }

    public SynergyInfoData GetSynergyInfoData(EHeroType in_hero_type, int in_count)
    {
        SynergyInfoData result = null;
        if (m_dic_synergy_info_data_by_hero_type.TryGetValue(in_hero_type, out var out_synergy_list))
        {
            foreach (var e in out_synergy_list)
            {
                if (in_count >= e.m_count)
                    result = e;
            }
        }

        return result;
    }

    public bool GetEnableSynergy(EHeroType in_hero_type, int in_count)
    {
        if (m_dic_synergy_info_data_by_hero_type.TryGetValue(in_hero_type, out var out_synergy_list))
        {
            foreach (var e in out_synergy_list)
            {
                if (in_count >= e.m_count)
                    return true;
            }
        }

        return false;
    }
}