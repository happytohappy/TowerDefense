using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitStageWaveTable()
    {    
    }

    public void SetStageWaveData(string in_sheet_data)
    {
        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(StageWaveData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            StageWaveData tableData = new StageWaveData();
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

            if (m_dic_stage_wave_data.ContainsKey(tableData.m_kind))
                m_dic_stage_wave_data[tableData.m_kind].Add(tableData);
            else
                m_dic_stage_wave_data.Add(tableData.m_kind, new List<StageWaveData>() { tableData });

            var key = (tableData.m_kind, tableData.m_wave);
            if (m_dic_stage_wave_data_by_kind_wave.ContainsKey(key))
                m_dic_stage_wave_data_by_kind_wave[key].Add(tableData);
            else
                m_dic_stage_wave_data_by_kind_wave.Add(key, new List<StageWaveData>() { tableData });
        }
    }
}