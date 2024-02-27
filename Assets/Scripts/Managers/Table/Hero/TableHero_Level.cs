using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitHeroLevel()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/Hero_Level");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            HeroLevelData HeroLevelData = new HeroLevelData();
            HeroLevelData.m_kind = int.Parse(words[0]);
            HeroLevelData.m_level = int.Parse(words[1]);
            HeroLevelData.m_atk = int.Parse(words[2]);
            HeroLevelData.m_speed = float.Parse(words[3]);
            HeroLevelData.m_range = float.Parse(words[4]);
            HeroLevelData.m_critical = int.Parse(words[5]);
            HeroLevelData.m_critical_chance = int.Parse(words[5]);

            m_dic_hero_level_data.Add((HeroLevelData.m_kind, HeroLevelData.m_level), HeroLevelData);
        }
    }

    public void SetHeroLevelData(string in_sheet_data)
    {
        object data = Activator.CreateInstance(typeof(HeroLevelData));

        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(HeroLevelData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                var sheetData = rows[row].Split('\t');

                System.Type type = fields[i].FieldType;
                if (string.IsNullOrEmpty(columns[i])) continue;

                // 변수에 맞는 자료형으로 파싱해서 넣는다
                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(sheetData[i]));
                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(sheetData[i]));
                else if (type == typeof(bool))
                    fields[i].SetValue(data, bool.Parse(sheetData[i]));
                else if (type == typeof(string))
                    fields[i].SetValue(data, sheetData[i]);
                else
                    fields[i].SetValue(data, Enum.Parse(type, sheetData[i]));
            }

            var tableData = data as HeroLevelData;
            m_dic_hero_level_data.Add((tableData.m_kind, tableData.m_level), tableData);
        }
    }
}