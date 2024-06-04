public static partial class Util
{
    /// <summary>
    /// ���� ���� �����
    /// ���� ���� �� �ִ� ������ �ִ���
    /// </summary>
    /// <returns></returns>
    public static bool RedDotAttendance()
    {
        return !TodayDailyReward();
    }

    /// <summary>
    /// ���� ���� ������ �޾Ҵ���
    /// </summary>
    /// <returns></returns>
    public static bool TodayDailyReward()
    {
        var serverDate = Managers.BackEnd.ServerDateTime();

        if (Managers.User.UserData.DailyRewardDateTime.Year  == serverDate.Year &&
            Managers.User.UserData.DailyRewardDateTime.Month == serverDate.Month &&
            Managers.User.UserData.DailyRewardDateTime.Day   == serverDate.Day)
            return true;

        return false;
    }

    /// <summary>
    /// ���� ���� ���� �ޱ�
    /// </summary>
    /// <param name="in_ad"></param>
    /// <returns></returns>
    public static bool AttendanceReward(bool in_ad)
    {
        // ���� ������ �޾Ҵ��� üũ
        if (TodayDailyReward())
            return false;

        // ������ ����
        var attendanceData = Managers.Table.GetAttendanceInfoData(Managers.User.UserData.DailyRewardKIND);
        if (attendanceData == null)
            return false;

        // �Ϲ� ���� ȹ��
        Managers.User.UpsertInventoryItem(attendanceData.m_free_reward_kind, 
            in_ad ? attendanceData.m_free_reward_amount * 2 : attendanceData.m_free_reward_amount);

        // �����̾� ���� ȹ��
        if (Managers.User.UserData.DailyRewardPremium)
            Managers.User.UpsertInventoryItem(attendanceData.m_premium_reward_kind,
                in_ad ? attendanceData.m_premium_reward_amount * 2 : attendanceData.m_premium_reward_amount);

        // ���� ���� ��¥ ����
        Managers.User.UserData.DailyRewardDateTime = Managers.BackEnd.ServerDateTime();

        // ������ ���� KIND ����
        Managers.User.UserData.DailyRewardKIND++;

        // ��� ������ �޾Ҵ��� üũ
        if (Managers.User.UserData.DailyRewardKIND == 8)
            Managers.User.UserData.DailyAllReward = true;

        return true;
    }
}