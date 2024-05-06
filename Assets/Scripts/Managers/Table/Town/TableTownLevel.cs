using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitTownLevelTable()
    {    
    }

    public void SetTownLevelData(string in_sheet_data)
    {
        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(TownLevelData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            TownLevelData tableData = new TownLevelData();
            for (int i = 0; i < sheetData.Length; i++)
            {
                System.Type type = fields[i].FieldType;
                sheetData[i] = sheetData[i].Replace("\r", "");
                if (string.IsNullOrEmpty(sheetData[i])) continue;

                // 변수에 맞는 자료형으로 파싱해서 넣는다
                if (type == typeof(int))
                    fields[i].SetValue(tableData, int.Parse(sheetData[i]));
                else if (type == typeof(float))
                    fields[i].SetValue(tableData, float.Parse(sheetData[i]));
                else if (type == typeof(bool))
                    fields[i].SetValue(tableData, bool.Parse(sheetData[i]));
                else if (type == typeof(string))
                    fields[i].SetValue(tableData, sheetData[i]);
                else
                    fields[i].SetValue(tableData, Enum.Parse(type, sheetData[i]));
            }

            if (m_dic_town_level_data.ContainsKey(tableData.m_kind))
                m_dic_town_level_data[tableData.m_kind].Add(tableData);
            else
                m_dic_town_level_data.Add(tableData.m_kind, new List<TownLevelData>() { tableData });

            var key = (tableData.m_kind, tableData.m_level);
            if (!m_dic_town_level_data_by_kind_level.ContainsKey(key))
                m_dic_town_level_data_by_kind_level.Add(key, tableData);
        }
    }
}