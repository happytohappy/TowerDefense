using System.Collections.Generic;

public partial class TableManager
{
    private Dictionary<int, AttendanceInfoData> m_dic_attendance_info_data = new Dictionary<int, AttendanceInfoData>();

    private void InitAttendanceTable()
    {
        InitAttendanceInfoTable();
    }

    public AttendanceInfoData GetAttendanceInfoData(int in_kind)
    {
        if (m_dic_attendance_info_data.ContainsKey(in_kind))
            return m_dic_attendance_info_data[in_kind];
        else
            return null;
    }

    public Dictionary<int, AttendanceInfoData> GetAllAttendanceInfoData()
    {
        return m_dic_attendance_info_data;
    }
}