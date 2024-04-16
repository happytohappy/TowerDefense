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
    None = 0,                   // 기본값
    WindowClose = 1 << 0,       // 윈도우가 열릴때 WindowClose면 이전 윈도우를 비활성화시킨다.
    WindowOverlay = 1 << 1,     // 윈도우가 열릴때 OverLay면 이전 윈도우를 비활성화시키지 않고 덮어 씌운다.
    WindowJustClose = 1 << 2,   // 닫힐때 무조건 닫히고 이전 노드에 간섭하지 않는다.
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
    ME,             //나만
    HERO_TYPE,      // 같은 히어로 타입만
    TEAM_ALL,       // 아군 전체
    ENEMY_TARGET,   // 적 타겟만
    ENEMY_ALL,      // 적 전체
    PASSIVE,        // 항상
}

public enum EBuff
{
    BUFF_INCREASE_DAMAGE,                   // 공격력 증가
    BUFF_INCREASE_SPEED,                    // 공격 속도 증가
    BUFF_INCREASE_RANGE,                    // 범위 증가
    BUFF_INCREASE_BOSS,                     // 보스 공격력 증가
    BUFF_INCREASE_CRITICAL,                 // 치명타 공격력 증가
    BUFF_INCREASE_CHANCE,                   // 치명타 확률 증가
    BUFF_DECREASE_DEF,                      // 방어력 감소
    BUFF_DECREASE_HP,                       // 체력 감소
    BUFF_DECREASE_SPEED,                    // 이동 속도 감소
    BUFF_STUN,                              // 기절
    BUFF_ENERGY_KILL,                       // 적 처치 시 에너지 추가 획득
    BUFF_MERGE_EQUIP,                       // 장비 합성 성공 확률 증가
    BUFF_REWARD_BOSS,                       // 보스 처치 보상 증가
    BUFF_ENERGY_ROUND,                      // 라운드 종료 시 에너지 추가 획득
    BUFF_MAKE_TIER_2,                       // 유닛 생성 시 2티어 유닛 생성
    BUFF_MORE_EQUIP,                        // 유닛 착용 장비 제한
    BUFF_REWARD_MISSION_STAGE,              // 스테이지 미션 보상 증가
    BUFF_REWARD_GOLD_STAGE,                 // 스테이지 종료 골드 보상 증가
    BUFF_MORE_LIFE_START_STAGE,             // 게임 시작 시 추가 라이프 획득
    BUFF_EARN_LIFE_WAVE,                    // 10 웨이브 클리어 시 마다 라이프 획득
    BUFF_MORE_ENERGY_START_STAGE,           // 게임 시작 시 추가 에너지
    BUFF_MORE_MISSION_START_STAGE,          // 스테이지 미션 개수 추가
    BUFF_GET_HERO_HIGHER,                   // 유닛 합성 시 한 단계 높은 티어 유닛 획득
    BUFF_ENEMY_DECREASE_SPEED_PLUS,         // 적 이동 속도 디버프 추가 값(보물용)
    BUFF_ENEMY_DECREASE_SPEED_TIME_PLUS,    // 적 이동 속도 디버프 시간 추가 값(보물용)
    BUFF_ENEMY_STUN_TIME_PLUS,              // 적 기절 디버프 시간 추가 값(보물용)
    BUFF_ALLY_INCREASE_BOSS_PLUS,           // 아군 전체 보스 공격력 증가 버프 추가 값(보물용)
    BUFF_ALLY_INCREASE_DAMAGE_PLUS,         // 아군 전체 공격력 증가 버프 추가 값(보물용)
    BUFF_ALLY_INCREASE_CRITICAL_PLUS,       // 아군 전체 치명타 공격력 증가 버프 추가 값(보물용)
}

public enum EUnion
{
    TYPE,           // 종족
    TIER,           // 티어
    RARITY,         // 희귀도
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
    HERO_MERGE,             // 영웅 합성
    HERO_LEVEL,             // 영웅 레벨업
    KILL_MONSTER,           // 몬스터 처치
    VILLAGE,                // 마을 보상 획득
    EQUIP_MERGE,            // 장비 합성
    GACHA_NORMAL,           // 일반 가챠
    GACHA_PREMIUM,          // 프리미엄 가챠
    CLEAR_INFINITE,         // 무한 모드 스테이지 달성
    HERO_SAME,              // 같은 영웅 보유
    HERO_DIFF,              // 서로 다른 영웅 종류 보유
    HERO_TIER,              // 특정 티어 이상 영웅 보유
    KILL_BOSS,              // 보스 몬스터 처치
}