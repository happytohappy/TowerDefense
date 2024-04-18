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
    }

    public void MonsterKill()
    {
        m_monster_kill_count++;
        Energy++;

        GUI.SetUnitEnergy();
        WaveFinishCheck();
    }

    public void MonsterGoal()
    {
        m_monster_goal_count++;
        Life--;

        if (Life <= 0)
        {
            Time.timeScale = 0;

            WaveInfoParam param = new WaveInfoParam();
            param.m_curr_wave = m_wave_index;
            param.m_max_wave = m_wave_count;

            Managers.UI.OpenWindow(WindowID.UIWindowGameResult, param);
        }

        GUI.SetLifeInfo(Life);
        WaveFinishCheck();
    }

    public void WaveFinishCheck()
    {
        if (m_spawn_finish == false)
            return;

        if (m_monster_spawn_count == m_monster_kill_count + m_monster_goal_count)
        {
            m_next_wave = true;
            m_wave_index++;

            // 도전 중인 스테이지 인지 체크
            if (Managers.User.UserData.LastClearStage == Managers.User.SelectStage - 1)
            {
                // 라운드를 갱신 했는지 체크
                if (Managers.User.UserData.LastClearWave < m_wave_index - 1)
                {
                    // 새로운 라운드를 깻다고 저장 해주고
                    Managers.User.UserData.LastClearWave = m_wave_index - 1;

                    // 받을 보상이 있는지 체크한다.
                    for (int i = 0; i < m_list_stage_reward.Count; i++)
                    {
                        var reward = m_list_stage_reward[i];
                        if (reward.m_wave == Managers.User.UserData.LastClearWave)
                        {
                            RewardData rewardData = new RewardData();
                            rewardData.m_reward = reward.m_first_clear_reward;
                            rewardData.m_amount = reward.m_first_clear_reward_amount;
                            rewardData.m_text = $"{reward.m_wave}-{m_wave_count}";
                            GetRewardData.Add(rewardData);

                            Managers.User.UpsertInventoryItem(reward.m_first_clear_reward, reward.m_first_clear_reward_amount);
                        }
                    }
                }
            }

            // 최종 라운드까지 진행 했다면
            if (m_wave_index > m_wave_count)
            {
                // 이미 깬 스테이지라면 반복 보상만 받는다.
                if (Managers.User.UserData.LastClearStage >= Managers.User.SelectStage)
                {
                    for (int i = 0; i < m_list_stage_reward.Count; i++)
                    {
                        var reward = m_list_stage_reward[i];
                        if (reward.m_repeat_clear_reward == 0)
                            continue;

                        RewardData rewardData = new RewardData();
                        rewardData.m_reward = reward.m_repeat_clear_reward;
                        rewardData.m_amount = reward.m_repeat_clear_reward_amount;
                        rewardData.m_text = string.Empty;
                        GetRewardData.Add(rewardData);

                        Managers.User.UpsertInventoryItem(reward.m_repeat_clear_reward, reward.m_repeat_clear_reward_amount);
                    }
                }
                else
                {
                    // 최고 스테이지 갱신!
                    // 웨이브는 0으로 변경해준다.
                    Managers.User.UserData.LastClearStage = Managers.User.SelectStage;
                    Managers.User.UserData.LastClearWave = 0;
                }

                WaveInfoParam param = new WaveInfoParam();
                param.m_curr_wave = m_wave_index;
                param.m_max_wave = m_wave_count;

                Managers.UI.OpenWindow(WindowID.UIWindowGameResult, param);
                return;
            }

            if (GUI != null)
            {
                GUI.NextWaveActive();
                GUI.SetWaveInfo(m_wave_index);

                // 자동 넥스트 라운드 시도
                if (ADAuto[EADAutoType.NEXT_WAVE])
                    GUI.OnClickWave();
            }
        }
    }
}