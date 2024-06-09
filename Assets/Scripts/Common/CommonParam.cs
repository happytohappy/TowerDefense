using System;

public abstract class WindowParam { }

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

public class InGameSkillParam : WindowParam
{
    public EADRewardType m_reward_type = EADRewardType.ENERGY;
}

public class QuestParam : WindowParam
{
    public EQuestType m_quest_type = EQuestType.Stage;
}

public class UnitInfoParam : WindowParam
{
    public int m_kind = 0;
}

public class EquipInfoParam : WindowParam
{
    public int m_hero_kind = 0;
    public Action m_callback = null;
}

public class ProbabilityParam : WindowParam
{
    public string m_title = string.Empty;
    public int m_kind = 0;
}

public class CommonInfoParam : WindowParam
{
    public string m_contents = string.Empty;
    public Action m_callback = null;
}

public class WaveInfoParam : WindowParam
{
    public int m_curr_wave = 0;
    public int m_max_wave = 0;
}

public class TownParam : WindowParam
{
    public ETownType m_town_type = ETownType.None;
}


public class EquipResultParam : WindowParam
{
    public bool m_success = false;
    public int m_equip_kind = 0;
}