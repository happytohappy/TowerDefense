public enum WindowID
{
    None,
    UIWindowLoading,
    UIWindowIntro,
    UIWindowMain,
    UIWindowGame,
    UIPopupGame,
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