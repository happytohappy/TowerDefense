public static partial class Util
{
    /// <summary>
    /// 일일 보상 레드닷
    /// 오늘 받을 수 있는 보상이 있는지
    /// </summary>
    /// <returns></returns>
    public static bool RedDotAttendance()
    {
        return !TodayDailyReward();
    }

    /// <summary>
    /// 오늘 일일 보상을 받았는지
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
    /// 오늘 일일 보상 받기
    /// </summary>
    /// <param name="in_ad"></param>
    /// <returns></returns>
    public static bool AttendanceReward(bool in_ad)
    {
        // 오늘 보상을 받았는지 체크
        if (TodayDailyReward())
            return false;

        // 데이터 검증
        var attendanceData = Managers.Table.GetAttendanceInfoData(Managers.User.UserData.DailyRewardKIND);
        if (attendanceData == null)
            return false;

        // 일반 보상 획득
        Managers.User.UpsertInventoryItem(attendanceData.m_free_reward_kind, 
            in_ad ? attendanceData.m_free_reward_amount * 2 : attendanceData.m_free_reward_amount);

        // 프리미엄 보상 획득
        if (Managers.User.UserData.DailyRewardPremium)
            Managers.User.UpsertInventoryItem(attendanceData.m_premium_reward_kind,
                in_ad ? attendanceData.m_premium_reward_amount * 2 : attendanceData.m_premium_reward_amount);

        // 보상 받은 날짜 갱신
        Managers.User.UserData.DailyRewardDateTime = Managers.BackEnd.ServerDateTime();

        // 다음날 보상 KIND 셋팅
        Managers.User.UserData.DailyRewardKIND++;

        // 모든 보상을 받았는지 체크
        if (Managers.User.UserData.DailyRewardKIND == 8)
            Managers.User.UserData.DailyAllReward = true;

        return true;
    }
}