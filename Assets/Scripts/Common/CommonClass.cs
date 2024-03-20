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
    public Type m_type;
    public int m_tier;
    public ERarity m_rarity;
    public string m_projectile;
}

public class HeroGradeData
{
    public int m_kind;
    public int m_grade;
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
    public int m_resource;
    public int m_resource_amount;
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

public class StageWaveData
{
    public int m_kind;
    public int m_wave;
    public int m_sequence;
    public int m_monster_kind;
    public int m_monster_level;
    public int m_monster_spawn_count;
}

// 구글 시트 정리 대기
public class BuffInfoData
{
    public int m_kind;
    public EBuff m_buff;
    public string m_comment;
}

public class BuffLevelData
{
    public int m_kind;
    public int m_level;
    public int m_rate;
    public float m_time;
    public float m_width;
}

