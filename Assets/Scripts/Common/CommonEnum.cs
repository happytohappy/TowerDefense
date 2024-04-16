public enum WindowID
{
    None,
    UIWindowCharacterTool,
    UIWindowLoading,
    UIWindowIntro,
    UIWindowMain,
    UIWindowGame,
    UIWindowUnit,
    UIWindowGameResult,
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
    UIPopupIngameSkill,
    UIPopupIngameAuto,
    UIPopupUnit,
    UIPopupSynergy
}

[System.Flags]
public enum WindowMode
{
    None = 0,                   // �⺻��
    WindowClose = 1 << 0,       // �����찡 ������ WindowClose�� ���� �����츦 ��Ȱ��ȭ��Ų��.
    WindowOverlay = 1 << 1,     // �����찡 ������ OverLay�� ���� �����츦 ��Ȱ��ȭ��Ű�� �ʰ� ���� �����.
    WindowJustClose = 1 << 2,   // ������ ������ ������ ���� ��忡 �������� �ʴ´�.
}

public enum LocalKey
{
    UserData
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

public enum EBuffTarget
{
    ME,             //����
    HERO_TYPE,      // ���� ����� Ÿ�Ը�
    TEAM_ALL,       // �Ʊ� ��ü
    ENEMY_TARGET,   // �� Ÿ�ٸ�
    ENEMY_ALL,      // �� ��ü
    PASSIVE,        // �׻�
}

public enum EBuff
{
    BUFF_INCREASE_DAMAGE,                   // ���ݷ� ����
    BUFF_INCREASE_SPEED,                    // ���� �ӵ� ����
    BUFF_INCREASE_RANGE,                    // ���� ����
    BUFF_INCREASE_BOSS,                     // ���� ���ݷ� ����
    BUFF_INCREASE_CRITICAL,                 // ġ��Ÿ ���ݷ� ����
    BUFF_INCREASE_CHANCE,                   // ġ��Ÿ Ȯ�� ����
    BUFF_DECREASE_DEF,                      // ���� ����
    BUFF_DECREASE_HP,                       // ü�� ����
    BUFF_DECREASE_SPEED,                    // �̵� �ӵ� ����
    BUFF_STUN,                              // ����
    BUFF_ENERGY_KILL,                       // �� óġ �� ������ �߰� ȹ��
    BUFF_MERGE_EQUIP,                       // ��� �ռ� ���� Ȯ�� ����
    BUFF_REWARD_BOSS,                       // ���� óġ ���� ����
    BUFF_ENERGY_ROUND,                      // ���� ���� �� ������ �߰� ȹ��
    BUFF_MAKE_TIER_2,                       // ���� ���� �� 2Ƽ�� ���� ����
    BUFF_MORE_EQUIP,                        // ���� ���� ��� ����
    BUFF_REWARD_MISSION_STAGE,              // �������� �̼� ���� ����
    BUFF_REWARD_GOLD_STAGE,                 // �������� ���� ��� ���� ����
    BUFF_MORE_LIFE_START_STAGE,             // ���� ���� �� �߰� ������ ȹ��
    BUFF_EARN_LIFE_WAVE,                    // 10 ���̺� Ŭ���� �� ���� ������ ȹ��
    BUFF_MORE_ENERGY_START_STAGE,           // ���� ���� �� �߰� ������
    BUFF_MORE_MISSION_START_STAGE,          // �������� �̼� ���� �߰�
    BUFF_GET_HERO_HIGHER,                   // ���� �ռ� �� �� �ܰ� ���� Ƽ�� ���� ȹ��
    BUFF_ENEMY_DECREASE_SPEED_PLUS,         // �� �̵� �ӵ� ����� �߰� ��(������)
    BUFF_ENEMY_DECREASE_SPEED_TIME_PLUS,    // �� �̵� �ӵ� ����� �ð� �߰� ��(������)
    BUFF_ENEMY_STUN_TIME_PLUS,              // �� ���� ����� �ð� �߰� ��(������)
    BUFF_ALLY_INCREASE_BOSS_PLUS,           // �Ʊ� ��ü ���� ���ݷ� ���� ���� �߰� ��(������)
    BUFF_ALLY_INCREASE_DAMAGE_PLUS,         // �Ʊ� ��ü ���ݷ� ���� ���� �߰� ��(������)
    BUFF_ALLY_INCREASE_CRITICAL_PLUS,       // �Ʊ� ��ü ġ��Ÿ ���ݷ� ���� ���� �߰� ��(������)
}

public enum EUnion
{
    TYPE,           // ����
    TIER,           // Ƽ��
    RARITY,         // ��͵�
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

public enum ELanguage
{
    Kor,
    Eng,
    Jpn,
    Chn_S,
    Chn_T,
}

public enum EHeroType
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

public enum EMissionType
{
    STAGE,
    ACHIEVEMENT
}

public enum EMissionCondition
{
    HERO_MERGE,             // ���� �ռ�
    HERO_LEVEL,             // ���� ������
    KILL_MONSTER,           // ���� óġ
    VILLAGE,                // ���� ���� ȹ��
    EQUIP_MERGE,            // ��� �ռ�
    GACHA_NORMAL,           // �Ϲ� ��í
    GACHA_PREMIUM,          // �����̾� ��í
    CLEAR_INFINITE,         // ���� ��� �������� �޼�
    HERO_SAME,              // ���� ���� ����
    HERO_DIFF,              // ���� �ٸ� ���� ���� ����
    HERO_TIER,              // Ư�� Ƽ�� �̻� ���� ����
    KILL_BOSS,              // ���� ���� óġ
}