using System.Collections.Generic;

public partial class TableManager
{
    public Dictionary<int, MissionInfoData> m_dic_mission_info_data = new Dictionary<int, MissionInfoData>();
    public Dictionary<int, List<MissionStageData>> m_dic_mission_stage_data = new Dictionary<int, List<MissionStageData>>();
    public Dictionary<int, Dictionary<int, MissionAchievementData>> m_dic_mission_achievement_data = new Dictionary<int, Dictionary<int, MissionAchievementData>>();

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

    public List<MissionStageData> GetAllMissionStageData(int in_kind)
    {
        if (m_dic_mission_stage_data.ContainsKey(in_kind))
            return m_dic_mission_stage_data[in_kind];
        else
            return null;
    }

    public List<MissionStageData> GetMissionStageData(int in_kind, int in_count)
    {
        var stageList = GetAllMissionStageData(in_kind);
        if (stageList == null)
            return null;

        var result = new List<MissionStageData>();

        return null;
    }
}