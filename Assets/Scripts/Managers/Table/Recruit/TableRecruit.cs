using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, List<RecruitInfoData>> m_dic_recruit_info_data = new Dictionary<int, List<RecruitInfoData>>();

    private void InitRecruitTable()
    {
        InitRecruitInfo();
    }

    public List<RecruitInfoData> GetRecruitInfoData(int in_kind)
    {
        if (m_dic_recruit_info_data.ContainsKey(in_kind))
            return m_dic_recruit_info_data[in_kind];
        else
            return null;
    }
}