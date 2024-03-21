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
    None = 0,                   // 기본값
    WindowClose = 1 << 0,       // 윈도우가 열릴때 WindowClose면 이전 윈도우를 비활성화시킨다.
    WindowOverlay = 1 << 1,     // 윈도우가 열릴때 OverLay면 이전 윈도우를 비활성화시키지 않고 덮어 씌운다.
    WindowJustClose = 1 << 2,   // 닫힐때 무조건 닫히고 이전 노드에 간섭하지 않는다.
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
    BUFF_ALLY_CHARGE_ENERGY,                    // 에너지 충전
    BUFF_ALLY_INCREASE_DAMAGE_ALL,              // 아군 전체 공격력 증가
    BUFF_ALLY_INCREASE_DAMAGE_SOLO,             // 자기 자신 공격력 증가
    BUFF_ALLY_INCREASE_SPEED_ALL,               // 아군 전체 공격 속도 증가
    BUFF_ALLY_INCREASE_SPEED_SOLO,              // 자기 자신 공격 속도 증가
    BUFF_ALLY_INCREASE_RANGE_ALL,               // 아군 전체 공격 범위 증가
    BUFF_ALLY_INCREASE_RANGE_SOLO,              // 자기 자신 공격 범위 증가
    BUFF_ALLY_INCREASE_CRITICAL_ALL,            // 아군 전체 치명타 증가
    BUFF_ALLY_INCREASE_CRITICAL_SOLO,           // 자기 자신 치명타 증가
    BUFF_ALLY_INCREASE_CRITCHANCE_ALL,          // 아군 전체 치명타율 증가
    BUFF_ALLY_INCREASE_CRITCHANCE_SOLO,         // 자기 자신 치명타율 증가
    BUFF_ALLY_INCREASE_BOSSDAMAGE_ALL,          // 아군 전체 보스 대상 공격력 증가
    BUFF_ALLY_INCREASE_BOSSDAMAGE_SOLO,         // 자기 자신 보스 대상 공격력 증가
    BUFF_ENEMY_DECREASE_DEFENSE_ALL,            // 적 전체 방어력 감소
    BUFF_ENEMY_DECREASE_DEFENSE_SOLO,           // 피격당한 적 방어력 감소
    BUFF_ENEMY_EFFECT_STUN_ALL,                 // 적 전체 기절 효과
    BUFF_ENEMY_EFFECT_STUN_SOLO,                // 피격당한 적 기절 효과
    BUFF_ENEMY_EFFECT_SLOW_ALL,                 // 적 전체 슬로우 효과
    BUFF_ENEMY_EFFECT_SLOW_SOLO,                // 피격당한 적 슬로우 효과
}

public enum EUnion
{
    TYPE,           // 종족
    TIER,           // 티어
    RARITY,         // 희귀도
}

public enum EType
{
    NONE    = 0,    //
    HUMAN   = 1,    // 인간
    ORC     = 2,    // 요정
    ELF     = 3,    // 오크
}

public enum ETier
{
    NONE    = 0,    //
    TIER_1  = 1,    // 1티어
    TIER_2  = 2,    // 2티어
    TIER_3  = 3,    // 3티어
    TIER_4  = 4,    // 4티어
    TIER_5  = 5,    // 5티어
    TIER_6  = 6,    // 6티어
    TIER_7  = 7,    // 7티어
    TIER_8  = 8,    // 8티어
}

public enum ERarity
{
    NONE   = 0,    //
    RARE   = 1,    // 희귀
    EPIC   = 2,    // 영웅
    LEGEND = 3,    // 전설
    MYTH   = 4,    // 신화
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