using UnityEngine;

public abstract class WindowParam
{

}

public class LoadingParam : WindowParam
{
    public int SceneIndex = 0;
    public WindowID NextWindow = WindowID.None;
    public WindowParam Param = null;
}

public class HeroData
{
    public int    m_kind;
    public string m_path;
    public string m_name;
    public string m_desc;
    public TowerType m_type;
    public int m_tier;
    public TowerRarity m_rarity;

    public int    m_ATK;
    public float  m_attack_speed;
    public float  m_attack_range;
}

public class MonsterData
{
    public int    m_kind;
    public string m_path;
    public int    m_hp;
    public float  m_move_speed;
}