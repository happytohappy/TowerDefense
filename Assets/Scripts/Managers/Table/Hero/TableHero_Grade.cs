using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitHeroGrade()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/Hero_Grade");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            HeroGradeData HeroGradeData = new HeroGradeData();
            HeroGradeData.m_kind = int.Parse(words[0]);
            HeroGradeData.m_grade = int.Parse(words[1]);
            for (int j = 2; j <= 4; j++)
                SetSkill(ref HeroGradeData, words[j]);

            m_dic_hero_grade_data.Add((HeroGradeData.m_kind, HeroGradeData.m_grade), HeroGradeData);
        }
    }

    private void SetSkill(ref HeroGradeData in_data, string in_value)
    {
        //if (int.TryParse(in_value, out int skill_id))
        //    in_data.m_skills.Add(skill_id);
    }

    public void SetHeroGradeData(string in_sheet_data)
    {
        object data = Activator.CreateInstance(typeof(HeroGradeData));

        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(HeroGradeData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                System.Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(columns[i])) continue;

                // 변수에 맞는 자료형으로 파싱해서 넣는다
                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(columns[i]));
                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(columns[i]));
                else if (type == typeof(bool))
                    fields[i].SetValue(data, bool.Parse(columns[i]));
                else if (type == typeof(string))
                    fields[i].SetValue(data, columns[i]);
                else
                    fields[i].SetValue(data, Enum.Parse(type, columns[i]));
            }

            var tableData = data as HeroGradeData;
            m_dic_hero_grade_data.Add((tableData.m_kind, tableData.m_grade), tableData);
        }
    }
}