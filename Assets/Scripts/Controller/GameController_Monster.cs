using UnityEngine;
using System.Collections;

public partial class GameController
{
    public void MonsterSpawn()
    {
        if (m_next_wave == false)
            return;

        m_next_wave = false;
        m_spawn_finish = false;
        m_monster_spawn_count = 0;
        m_monster_kill_count = 0;
        m_monster_goal_count = 0;
        StartCoroutine(CoMonsterSpawn());
    }

    private IEnumerator CoMonsterSpawn()
    {
        var waveDatas = Managers.Table.GetStageWaveDataByWave(1, m_wave_index);

        for (int i = 0; i < waveDatas.Count; i++)
        {
            var waveData = waveDatas[i];
            if (waveData == null)
                continue;

            var monsterInfoData = Managers.Table.GetMonsterInfoData(waveData.m_monster_kind);
            var monsterStatusData = Managers.Table.GetMonsterStatusData(waveData.m_monster_kind, waveData.m_monster_level);

            if (monsterInfoData == null || monsterStatusData == null)
                continue;

            m_sniffling = true;
            for (int j = 0; j < waveData.m_monster_spawn_count; j++)
            {
                var go = Managers.Resource.Instantiate(monsterInfoData.m_path);
                if (go != null)
                {
                    var lineInex = m_sniffling ? 0 : 1;
                    var monster = go.GetComponent<Monster>();

                    monster.Path.AddRange(m_pathes[lineInex].m_path);
                    monster.GetMonsterInfoData = monsterInfoData;
                    monster.GetMonsterStatusData = monsterStatusData;
                    go.transform.position = m_pathes[lineInex].m_path[0].position;
                    Monsters.Add(monster);

                    m_sniffling = !m_sniffling;

                    m_monster_spawn_count++;
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        m_spawn_finish = true;
        m_wave_index++;
    }

    public void MonsterKill()
    {
        m_monster_kill_count++;
        Energy++;

        WaveFinishCheck();
    }

    public void MonsterGoal()
    {
        m_monster_goal_count++;
        
        WaveFinishCheck();
    }

    public void WaveFinishCheck()
    {
        if (m_spawn_finish == false)
            return;

        if (m_monster_spawn_count == m_monster_kill_count + m_monster_goal_count)
        {
            m_next_wave = true;

            if (GUI != null)
                GUI.NextWaveActive();
        }
    }
}