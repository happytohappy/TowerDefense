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
    public Rarity m_rarity;
    public string m_projectile;
}

public class HeroGradeData
{
    public int m_kind;
    public int m_grade;
    public int m_skill_1;
    public int m_skill_2;
    public int m_skill_3;
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

// 구글 시트 정리 대기
public class GachaGroupData
{
    public int m_kind;
    public int m_consumption;
    public int m_reward;
    public string m_image;
    public int m_text_page;
}

public class GachaRewardData
{
    public int m_kind;
    public int m_item;
    public int m_amount;
    public int m_rate;
    public int m_rate_min;
    public int m_rate_max;
}



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

