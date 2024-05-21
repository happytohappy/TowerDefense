using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public static partial class Util
{
    public static float GetBuffValue(HeroData in_hero_data, EBuff in_buff)
    {
        var buffValue = 1.0f;

        // 1. 보물

        // 2. 시너지
        var dicHeroTypeCount = GameController.GetInstance.GetHeroTypeCount();

        foreach (var e in Managers.Table.GetSynergyAllInfoDataList())
        {
            int count = 0;  // 타입별 영웅 개수
            if (dicHeroTypeCount.TryGetValue(e.Key, out count) == false)
                continue;

            SynergyInfoData synergyInfo = null;
            foreach (var synergy in e.Value)
            {
                if (count >= synergy.m_count)
                    synergyInfo = synergy;
            }

            if (synergyInfo == null)
                continue;

            // 버프 타입이 일치 하지 않으면 리턴
            var buffInfo = Managers.Table.GetBuffInfoData(synergyInfo.m_buff_kind);
            if (buffInfo == null || buffInfo.m_buff != in_buff)
                continue;

            // 버프 타겟이 일치하지 않으면 리턴
            if (buffInfo.m_buff_target == EBuffTarget.HERO_TYPE)
            {
                if (synergyInfo.m_hero_type != in_hero_data.m_info.m_type)
                    continue;
            }

            var buffLevel = Managers.Table.GetBuffLevelData(synergyInfo.m_buff_kind, synergyInfo.m_buff_level);

            // 확률 통과 못하면 리턴
            if (buffLevel.m_rate < 10000)
            {
                var ran = UnityEngine.Random.Range(0, 10001);
                if (ran > buffLevel.m_rate)
                    continue;
            }

            switch (in_buff)
            {
                case EBuff.BUFF_INCREASE_DAMAGE:
                    buffValue += buffLevel.m_value * 0.01f;
                    break;
                case EBuff.BUFF_INCREASE_SPEED:
                    buffValue += buffLevel.m_value * 0.01f;
                    break;
                case EBuff.BUFF_INCREASE_RANGE:
                    break;
                case EBuff.BUFF_INCREASE_BOSS:
                    break;
                case EBuff.BUFF_INCREASE_CRITICAL:
                    buffValue += buffLevel.m_value * 0.01f;
                    break;
                case EBuff.BUFF_INCREASE_CHANCE:
                    buffValue += buffLevel.m_value * 0.01f;
                    break;
                case EBuff.BUFF_DECREASE_DEF:
                    break;
                case EBuff.BUFF_DECREASE_HP:
                    break;
                case EBuff.BUFF_DECREASE_SPEED:
                    break;
                case EBuff.BUFF_STUN:
                    break;
                case EBuff.BUFF_ENERGY_KILL:
                    break;
                case EBuff.BUFF_MERGE_EQUIP:
                    break;
                case EBuff.BUFF_REWARD_BOSS:
                    break;
                case EBuff.BUFF_ENERGY_ROUND:
                    break;
                case EBuff.BUFF_MAKE_TIER_2:
                    break;
                case EBuff.BUFF_MORE_EQUIP:
                    break;
                case EBuff.BUFF_REWARD_MISSION_STAGE:
                    break;
                case EBuff.BUFF_REWARD_GOLD_STAGE:
                    break;
                case EBuff.BUFF_MORE_LIFE_START_STAGE:
                    break;
                case EBuff.BUFF_EARN_LIFE_WAVE:
                    break;
                case EBuff.BUFF_MORE_ENERGY_START_STAGE:
                    break;
                case EBuff.BUFF_MORE_MISSION_START_STAGE:
                    break;
                case EBuff.BUFF_GET_HERO_HIGHER:
                    break;
                case EBuff.BUFF_ENEMY_DECREASE_SPEED_PLUS:
                    break;
                case EBuff.BUFF_ENEMY_DECREASE_SPEED_TIME_PLUS:
                    break;
                case EBuff.BUFF_ENEMY_STUN_TIME_PLUS:
                    break;
                case EBuff.BUFF_ALLY_INCREASE_BOSS_PLUS:
                    break;
                case EBuff.BUFF_ALLY_INCREASE_DAMAGE_PLUS:
                    break;
                case EBuff.BUFF_ALLY_INCREASE_CRITICAL_PLUS:
                    break;
                default:
                    break;
            }
        }

        return buffValue;
    }

    public static float GetDeBuffValue(Monster in_monster_data, EBuff in_buff)
    {
        var DeBuffValue = 1.0f;

        // 1. 보물

        // 2. 시너지
        var dicHeroTypeCount = GameController.GetInstance.GetHeroTypeCount();

        foreach (var e in Managers.Table.GetSynergyAllInfoDataList())
        {
            int count = 0;  // 타입별 영웅 개수
            if (dicHeroTypeCount.TryGetValue(e.Key, out count) == false)
                continue;

            SynergyInfoData synergyInfo = null;
            foreach (var synergy in e.Value)
            {
                if (count >= synergy.m_count)
                    synergyInfo = synergy;
            }

            if (synergyInfo == null)
                continue;

            // 버프 타입이 일치 하지 않으면 리턴
            var buffInfo = Managers.Table.GetBuffInfoData(synergyInfo.m_buff_kind);
            if (buffInfo == null || buffInfo.m_buff != in_buff)
                continue;

            // 버프 타겟이 일치하지 않으면 리턴
            //if (buffInfo.m_buff_target == EBuffTarget.HERO_TYPE)
            //{
            //    if (synergyInfo.m_hero_type != in_hero_data.m_info.m_type)
            //        continue;
            //}

            var buffLevel = Managers.Table.GetBuffLevelData(synergyInfo.m_buff_kind, synergyInfo.m_buff_level);

            // 확률 통과 못하면 리턴
            if (buffLevel.m_rate < 10000)
            {
                var ran = UnityEngine.Random.Range(0, 10001);
                if (ran > buffLevel.m_rate)
                    continue;
            }

            switch (in_buff)
            {
                case EBuff.BUFF_DECREASE_DEF:
                    break;
                case EBuff.BUFF_DECREASE_HP:
                    break;
                case EBuff.BUFF_DECREASE_SPEED:
                    DeBuffValue += buffLevel.m_value * 0.01f;
                    break;
            }
        }

        return DeBuffValue;
    }
}