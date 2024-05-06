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
        var result = new List<MissionStageData>();

        var stageList = GetAllMissionStageData(in_kind);
        if (stageList == null)
            return null;

        var tempAllList = new List<MissionStageData>();
        foreach (var e in stageList)
            tempAllList.Add(e.Clone());

        for (int i = 0; i < in_count; i++)
        {
            int totalRate = 0;
            List<(int, int, MissionStageData)> tempList = new List<(int, int, MissionStageData)>();
            foreach (var e in tempAllList)
            {
                tempList.Add((totalRate, totalRate + e.m_rate, e));
                totalRate += e.m_rate;
            }

            var ranIndex = UnityEngine.Random.Range(0, totalRate);
            foreach (var e in tempList)
            {
                if (ranIndex >= e.Item1 && ranIndex < e.Item2)
                {
                    tempAllList.Remove(e.Item3);
                    result.Add(e.Item3);
                }
            }
        }

        return result;
    }

    public MissionAchievementData GetAchievementData(int in_kind, int in_sequence)
    {
        if (m_dic_mission_achievement_data.ContainsKey(in_kind))
        {
            var data = m_dic_mission_achievement_data[in_kind];
            if (data.ContainsKey(in_sequence))
                return data[in_sequence];
        }

        return null;
    }
}