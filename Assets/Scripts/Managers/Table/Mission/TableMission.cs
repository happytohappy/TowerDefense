using System.Collections.Generic;

public partial class TableManager
{
    public Dictionary<int, MissionInfoData> m_dic_mission_info_data = new Dictionary<int, MissionInfoData>();

    private void InitMissionTable()
    {
    }

    private void ClearMissionTable()
    {
        m_dic_mission_info_data.Clear();
    }

    public Dictionary<int, MissionInfoData> GetAllMissionInfoData()
    {
        return m_dic_mission_info_data;
    }

    public MissionInfoData GetMissionInfoData(int in_kind)
    {
        if (m_dic_mission_info_data.ContainsKey(in_kind))
            return m_dic_mission_info_data[in_kind];
        else
            return null;
    }
}