using System.Collections.Generic;

public abstract class WindowParam
{

}

public class LoadingParam : WindowParam
{
    public int SceneIndex = 0;
    public WindowID NextWindow = WindowID.None;
    public WindowParam Param = null;
}

public class GachaHeroParam : WindowParam
{
    public int m_hero_kind = 0;
}

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
    public int         m_kind;
    public string      m_path;
    public string      m_name;
    public string      m_desc;
    public TowerType   m_type;
    public int         m_tier;
    public TowerRarity m_rarity;
}

public class HeroGradeData
{
    public int       m_kind;
    public int       m_grade;
    public List<int> m_skills = new List<int>();
}

public class HeroLevelData
{
    public int   m_kind;
    public int   m_level;
    public int   m_atk;
    public float m_speed;
    public float m_range;
    public int   m_critical;
    public int   m_critical_chance;
}

public class MonsterData
{
    public int    m_kind;
    public string m_path;
    public int    m_hp;
    public float  m_move_speed;
}

public class GachaGroupData
{
    public int m_kind;
    public int m_reward;
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

public class LocalizationData
{
    public string LAN_KEY;
    public string KOR;
    public string ENG;
    public string CHN_T;   // 번체
    public string CHN_S;   // 간체
    public string JPN;
}