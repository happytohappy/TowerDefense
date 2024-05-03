using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    //private void InitMissionInfo()
    //{
    //    //TextAsset TextFile = Resources.Load<TextAsset>("Table/Hero_Grade");
    //    //string CSVText = TextFile.text;
    //    //List<string> valueArray = Util.CSVSplitData(CSVText);
    //    //for (int i = 2; i < valueArray.Count; i++)
    //    //{
    //    //    string[] words = valueArray[i].Split(',');
    //    //    HeroGradeData HeroGradeData = new HeroGradeData();
    //    //    HeroGradeData.m_kind = int.Parse(words[0]);
    //    //    HeroGradeData.m_grade = int.Parse(words[1]);
    //    //    for (int j = 2; j <= 4; j++)
    //    //        SetSkill(ref HeroGradeData, words[j]);

    //    //    m_dic_hero_grade_data.Add((HeroGradeData.m_kind, HeroGradeData.m_grade), HeroGradeData);
    //    //}
    //}

    public void SetMissionStageData(string in_sheet_data)
    {
        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(MissionStageData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            MissionStageData tableData = new MissionStageData();
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

            if (m_dic_mission_stage_data.ContainsKey(tableData.m_kind))
            {
                m_dic_mission_stage_data[tableData.m_kind].Add(tableData);
            }
            else
            {
                m_dic_mission_stage_data.Add(tableData.m_kind, new List<MissionStageData>() { tableData });
            }
        }
    }
}