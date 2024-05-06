using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, TownInfoData> m_dic_town_info_data = new Dictionary<int, TownInfoData>();
    private Dictionary<int, List<TownLevelData>> m_dic_town_level_data = new Dictionary<int, List<TownLevelData>>();
    private Dictionary<(int, int), TownLevelData> m_dic_town_level_data_by_kind_level = new Dictionary<(int, int), TownLevelData>();

    private void InitTownTable()
    {
        InitTownInfoTable();
        InitTownLevelTable();
    }

    public TownInfoData GetTownInfoData(int in_kind)
    {
        if (m_dic_town_info_data.ContainsKey(in_kind))
            return m_dic_town_info_data[in_kind];
        else
            return null;
    }

    public TownLevelData GetTownLevelDataByLevel(int in_kind, int in_level)
    {
        var key = (in_kind, in_level);
        if (m_dic_town_level_data_by_kind_level.ContainsKey(key))
            return m_dic_town_level_data_by_kind_level[key];
        else
            return null;
    }

    public int GetTownMaxLevel(int in_kind)
    {
        if (m_dic_town_level_data.ContainsKey(in_kind))
            return m_dic_town_level_data[in_kind].Count;

        return 0;
    }

    //public int GetStageCount()
    //{
    //    return m_dic_stage_info_data.Count;
    //}

    //public List<StageRewardData> GetStageReward(int in_stage)
    //{        
    //    if (m_dic_stage_reward_data.ContainsKey(in_stage))
    //        return m_dic_stage_reward_data[in_stage];
    //    else
    //        return null;
    //}

    //public List<StageWaveData> GetStageWaveData(int in_kind)
    //{
    //    if (m_dic_stage_wave_data.ContainsKey(in_kind))
    //        return m_dic_stage_wave_data[in_kind];
    //    else
    //        return null;
    //}

    //public List<StageWaveData> GetStageWaveDataByWave(int in_kind, int in_wave)
    //{
    //    var key = (in_kind, in_wave);
    //    if (m_dic_stage_wave_data_by_kind_wave.ContainsKey(key))
    //        return m_dic_stage_wave_data_by_kind_wave[key];
    //    else
    //        return null;
    //}

    //public int GetWaveCount(int in_kind)
    //{
    //    int result = 0;
    //    if (m_dic_stage_wave_data.ContainsKey(in_kind))
    //    {
    //        int currWave = -1;
    //        foreach (var e in m_dic_stage_wave_data[in_kind])
    //        {
    //            if (currWave != e.m_wave)
    //            {
    //                currWave = e.m_wave;
    //                result++;
    //            }
    //        }
    //    }

    //    return result;
    //}
}