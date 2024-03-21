public enum WindowID
{
    None,
    UIWindowCharacterTool,
    UIWindowLoading,
    UIWindowIntro,
    UIWindowMain,
    UIWindowGame,
    UIWindowUnit,
    UIPopupGame,
    UIPopupShop,
    UIPopupReward,
    UIPopupQuest,
    UIPopupSetting,
    UIPopupTown,
    UIPopupPause,
    UIPopupEquipment,
    UIPopupTreasure,
    UIPopupUnitBuy,
    UIPopupIngameSkill
}

[System.Flags]
public enum WindowMode
{
    None = 0,                   // �⺻��
    WindowClose = 1 << 0,       // �����찡 ������ WindowClose�� ���� �����츦 ��Ȱ��ȭ��Ų��.
    WindowOverlay = 1 << 1,     // �����찡 ������ OverLay�� ���� �����츦 ��Ȱ��ȭ��Ű�� �ʰ� ���� �����.
    WindowJustClose = 1 << 2,   // ������ ������ ������ ���� ��忡 �������� �ʴ´�.
}

public enum DebugType
{
    Normal,
    Warning,
    Error
}

public enum FSM_STATE
{
    None,
    Idle,
    Run,
    Attack,
    Die
}

public enum EInputType
{
    None,
    Click,
    Press
}

public enum ESpawnType
{
    Free,
    Merge,
    Energy,
    Ruby,
    AD
}

public enum EBuff
{
    BUFF_ALLY_CHARGE_ENERGY,                    // ������ ����
    BUFF_ALLY_INCREASE_DAMAGE_ALL,              // �Ʊ� ��ü ���ݷ� ����
    BUFF_ALLY_INCREASE_DAMAGE_SOLO,             // �ڱ� �ڽ� ���ݷ� ����
    BUFF_ALLY_INCREASE_SPEED_ALL,               // �Ʊ� ��ü ���� �ӵ� ����
    BUFF_ALLY_INCREASE_SPEED_SOLO,              // �ڱ� �ڽ� ���� �ӵ� ����
    BUFF_ALLY_INCREASE_RANGE_ALL,               // �Ʊ� ��ü ���� ���� ����
    BUFF_ALLY_INCREASE_RANGE_SOLO,              // �ڱ� �ڽ� ���� ���� ����
    BUFF_ALLY_INCREASE_CRITICAL_ALL,            // �Ʊ� ��ü ġ��Ÿ ����
    BUFF_ALLY_INCREASE_CRITICAL_SOLO,           // �ڱ� �ڽ� ġ��Ÿ ����
    BUFF_ALLY_INCREASE_CRITCHANCE_ALL,          // �Ʊ� ��ü ġ��Ÿ�� ����
    BUFF_ALLY_INCREASE_CRITCHANCE_SOLO,         // �ڱ� �ڽ� ġ��Ÿ�� ����
    BUFF_ALLY_INCREASE_BOSSDAMAGE_ALL,          // �Ʊ� ��ü ���� ��� ���ݷ� ����
    BUFF_ALLY_INCREASE_BOSSDAMAGE_SOLO,         // �ڱ� �ڽ� ���� ��� ���ݷ� ����
    BUFF_ENEMY_DECREASE_DEFENSE_ALL,            // �� ��ü ���� ����
    BUFF_ENEMY_DECREASE_DEFENSE_SOLO,           // �ǰݴ��� �� ���� ����
    BUFF_ENEMY_EFFECT_STUN_ALL,                 // �� ��ü ���� ȿ��
    BUFF_ENEMY_EFFECT_STUN_SOLO,                // �ǰݴ��� �� ���� ȿ��
    BUFF_ENEMY_EFFECT_SLOW_ALL,                 // �� ��ü ���ο� ȿ��
    BUFF_ENEMY_EFFECT_SLOW_SOLO,                // �ǰݴ��� �� ���ο� ȿ��
}

public enum EUnion
{
    TYPE,           // ����
    TIER,           // Ƽ��
    RARITY,         // ��͵�
}

public enum EType
{
    NONE    = 0,    //
    HUMAN   = 1,    // �ΰ�
    ORC     = 2,    // ����
    ELF     = 3,    // ��ũ
}

public enum ETier
{
    NONE    = 0,    //
    TIER_1  = 1,    // 1Ƽ��
    TIER_2  = 2,    // 2Ƽ��
    TIER_3  = 3,    // 3Ƽ��
    TIER_4  = 4,    // 4Ƽ��
    TIER_5  = 5,    // 5Ƽ��
    TIER_6  = 6,    // 6Ƽ��
    TIER_7  = 7,    // 7Ƽ��
    TIER_8  = 8,    // 8Ƽ��
}

public enum ERarity
{
    NONE   = 0,    //
    RARE   = 1,    // ���
    EPIC   = 2,    // ����
    LEGEND = 3,    // ����
    MYTH   = 4,    // ��ȭ
}

public enum Language
{
    Kor,
    Eng,
    Jpn,
    Chn_S,
    Chn_T,
}

public enum Type
{
    None,
    Fighter,
    Archer,
    Warrior,
    Assassin,
    Magician,
    Lancer,
    Shooter,
    Gladiator
}