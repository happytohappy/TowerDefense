// 구글 시트 정리 완료
public class HeroData
{
    public HeroInfoData m_info = new HeroInfoData();
    public HeroGradeData m_skill = new HeroGradeData();
    public HeroLevelData m_stat = new HeroLevelData();

    public HeroData(HeroInfoData in_info, HeroGradeData in_grade, HeroLevelData in_level)
    {
        m_info = in_info;
        m_skill = in_grade;
        m_stat = in_level;
    }
}

public class HeroInfoData
{
    public int m_kind;
    public string m_path;
    public string m_name;
    public string m_desc;
    public EHeroType m_type;
    public int m_tier;
    public ERarity m_rarity;
    public string m_projectile;
}

public class HeroGradeData
{
    public int m_kind;
    public int m_grade;
    public int m_item_kind;
    public int m_grade_up_piece;
    public int m_skill_1;
    public int m_skill_1_level;
    public int m_skill_2;
    public int m_skill_2_level;
    public int m_skill_3;
    public int m_skill_3_level;
    public int m_ATK;
    public int m_speed;
    public float m_range;
    public int m_critical;
    public int m_critical_chance;
}

public class HeroLevelData
{
    public int m_kind;
    public int m_level;
    public int m_atk;
    public float m_speed;
    public float m_range;
    public int m_critical;
    public int m_critical_chance;
    public int m_item_kind;
    public int m_item_amount;
}

public class LocalizationData
{
    public string LAN_KEY;
    public string KOR;
    public string ENG;
    public string JPN;
    public string CHN_S;   // 간체
    public string CHN_T;   // 번체
}

public class MonsterInfoData
{
    public int m_kind;
    public string m_path;
    public string m_name;
    public string m_desc;
    public EMonsterType m_monster_type;
}

public class MonsterStatusData
{
    public int m_kind;
    public int m_level;
    public int m_hp;
    public int m_def;
    public float m_move_speed;
    public string m_resource;
    public int m_resource_amount;
}

public class GachaInfoData
{
    public int m_kind;
    public int m_gacha_kind;
    public string m_image;
    public int m_text_page;
    public int m_consumption;
    public int m_consumption_amount;
    public int m_gacha_count;
    public int m_reward;
    public int m_reward_bonus;
}

public class GachaRewardData
{
    public int m_kind;
    public int m_item;
    public int m_amount;
    public float m_rate;
    public float m_rate_min;
    public float m_rate_max;
}

public class StageInfoData
{
    public int m_stage;
    public string m_map_model_path;
    // 추가
    public int m_mission_group;
    public float m_monster_kill_compensation;
}

public class StageWaveData
{
    public int m_kind;
    public int m_wave;
    public int m_sequence;
    public int m_monster_kind;
    public int m_monster_level;
    public int m_monster_spawn_count;
}

public class StageRewardData
{
    public int m_stage;
    public int m_wave;
    public int m_first_clear_reward;
    public int m_first_clear_reward_amount;
    public int m_repeat_clear_reward;
    public int m_repeat_clear_reward_amount;
}

public class TreasureInfoData
{
    public int m_kind;
    public int m_buff_kind;
    public EBuff m_buff_enum;
    public string m_icon;
    public string m_name;
    public string m_comment;
}

public class TreasureLevelData
{
    public int m_kind;
    public int m_level;
    public int m_item_kind;
    public int m_grade_up_piece;
    public int m_buff_level;
    public int m_mileage;
}

public class MissionInfoData
{
    public int m_kind;
    public EMissionType m_mission_type;
    public EMissionCondition m_mission_condition;
    public int m_value_1;
    public int m_value_2;
    public int m_value_3;
    public int m_reward_item_kind;
    public int m_reward_item_amound;
    public string m_title;
    public string m_desc;
}

public class MissionStageData
{
    public int m_kind;
    public int m_index;
    public EMissionCondition m_mission_condition;
    public int m_value_1;
    public int m_value_2;
    public int m_reward_item_kind;
    public int m_reward_item_amound;
    public int m_rate;
    public string m_title;
    public string m_desc;

    public MissionStageData Clone()
    {
        var result = new MissionStageData();
        result.m_kind = this.m_kind;
        result.m_index = this.m_index;
        result.m_mission_condition = this.m_mission_condition;
        result.m_value_1 = this.m_value_1;
        result.m_value_2 = this.m_value_2;
        result.m_reward_item_kind = this.m_reward_item_kind;
        result.m_reward_item_amound = this.m_reward_item_amound;
        result.m_rate = this.m_rate;
        result.m_title = this.m_title;
        result.m_desc = this.m_desc;

        return result;
    }
}

public class MissionAchievementData
{
    public int m_kind;
    public int m_sequence;
    public EMissionCondition m_mission_condition;
    public int m_value_1;
    public int m_value_2;
    public int m_reward_item_kind;
    public int m_reward_item_amound;
    public string m_title;
    public string m_desc;
}

public class SynergyInfoData
{
    public int m_kind;
    public EHeroType m_hero_type;
    public int m_count;
    public int m_buff_kind;
    public int m_buff_level;
    public string m_icon;
    public string m_title;
    public string m_desc;
}

public class BuffInfoData
{
    public int m_kind;
    public EBuff m_buff;
    public EBuffTarget m_buff_target;
    public string m_icon;
    public string m_desc;
    public string m_comment;
}

public class BuffLevelData
{
    public int m_kind;
    public int m_level;
    public int m_rate;
    public float m_time;
    public float m_value;
    public string m_comment;
}

public class TownInfoData
{
    public int m_kind;
    public bool m_ad_reward;
    public string m_icon;
    public string m_title;
    public string m_contents;
    public string comment;
}

public class TownLevelData
{
    public int m_kind;
    public int m_level;
    public int m_timespan;
    public int m_level_up_kind;
    public int m_level_up_cost;
    public int m_reward_kind;
    public int m_reward_amount;
}