public enum WindowID
{
    None,
    UIWindowLoading,
    UIWindowIntro,
    UIWindowMain,
    UIWindowGame,
    UIPopupGame,
    UIPopupShop,
    UIPopupReward,
    UIPopupQuest,
    UIPopupSetting,
    UIPopupTown,
    UIPopupPause,
    UIPopupEquipment,
    UIPopupTreasure,
    UIPopupUnit,
    UIPopupUnitBuy,
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

public enum Language
{
    Kor,
    Eng,
    Jpn,
    Chn_S,
    Chn_T,
}

public enum TowerType
{
    None,
    Human,
    Orc,
    Elf
}

public enum TowerRarity
{
    None,
    Normal,
    Rare,
    Epic,
    Legend
}