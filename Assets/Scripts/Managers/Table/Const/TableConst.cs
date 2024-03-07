using System.Collections.Generic;
using System.Reflection;

public partial class TableManager
{
    private Dictionary<string, int> m_dic_const_data = new Dictionary<string, int>();

    public void SetConstData(string in_sheet_data)
    {
        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(CONST).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        for (int i = 0; i < fields.Length; i++)
            m_dic_const_data.Add(fields[i].Name, 0);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            if (m_dic_const_data.ContainsKey(sheetData[0]))
                m_dic_const_data[sheetData[0]] = int.Parse(sheetData[1]);
        }
    }

    public int GetConstValue(string in_key)
    {
        if (m_dic_const_data.ContainsKey(in_key))
            return m_dic_const_data[in_key];
        else
            return 0;
    }
}