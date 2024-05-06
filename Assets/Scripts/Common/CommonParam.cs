using System.Collections.Generic;

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

public class WaveInfoParam : WindowParam
{
    public int m_curr_wave = 0;
    public int m_max_wave = 0;
}

public class TownParam : WindowParam
{
    public ETownType m_town_type = ETownType.None;
}