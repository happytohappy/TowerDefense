using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, StageInfoData> m_dic_stage_info_data = new Dictionary<int, StageInfoData>();
    private Dictionary<int, List<StageRewardData>> m_dic_stage_reward_data = new Dictionary<int, List<StageRewardData>>();
    private Dictionary<int, List<StageWaveData>> m_dic_stage_wave_data = new Dictionary<int, List<StageWaveData>>();
    private Dictionary<(int, int), List<StageWaveData>> m_dic_stage_wave_data_by_kind_wave = new Dictionary<(int, int), List<StageWaveData>>();

    private void InitStageTable()
    {
        InitStageWaveTable();
        //InitMonsterStatusTable();
    }

    public int GetStageCount()
    {
        return m_dic_stage_info_data.Count;
    }

    public List<StageRewardData> GetStageReward(int in_stage)
    {        
        if (m_dic_stage_reward_data.ContainsKey(in_stage))
            return m_dic_stage_reward_data[in_stage];
        else
            return null;
    }

    public List<StageWaveData> GetStageWaveData(int in_kind)
    {
        if (m_dic_stage_wave_data.ContainsKey(in_kind))
            return m_dic_stage_wave_data[in_kind];
        else
            return null;
    }

    public List<StageWaveData> GetStageWaveDataByWave(int in_kind, int in_wave)
    {
        var key = (in_kind, in_wave);
        if (m_dic_stage_wave_data_by_kind_wave.ContainsKey(key))
            return m_dic_stage_wave_data_by_kind_wave[key];
        else
            return null;
    }

    public int GetWaveCount(int in_kind)
    {
        int result = 0;
        if (m_dic_stage_wave_data.ContainsKey(in_kind))
        {
            int currWave = -1;
            foreach (var e in m_dic_stage_wave_data[in_kind])
            {
                if (currWave != e.m_wave)
                {
                    currWave = e.m_wave;
                    result++;
                }
            }
        }

        return result;
    }
}