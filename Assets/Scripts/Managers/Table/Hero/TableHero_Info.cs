using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitHeroInfo()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/Hero_Info");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            HeroInfoData HeroInfoData = new HeroInfoData();
            HeroInfoData.m_kind = int.Parse(words[0]);
            HeroInfoData.m_path = words[1];
            HeroInfoData.m_name = words[2];
            HeroInfoData.m_desc = words[3];
            HeroInfoData.m_type = (EHeroType)Enum.Parse(typeof(EHeroType), words[4]);
            HeroInfoData.m_tier = int.Parse(words[5]);
            HeroInfoData.m_rarity = (ERarity)Enum.Parse(typeof(ERarity), words[6]);

            m_dic_hero_info_data.Add(HeroInfoData.m_kind, HeroInfoData);

            // GroupBy Tier
            if (m_dic_hero_info_data_group_by_tier.ContainsKey(HeroInfoData.m_tier))
                m_dic_hero_info_data_group_by_tier[HeroInfoData.m_tier].Add(HeroInfoData);
            else
                m_dic_hero_info_data_group_by_tier.Add(HeroInfoData.m_tier, new List<HeroInfoData>() { HeroInfoData });
        }
    }

    public void SetHeroInfoData(string in_sheet_data)
    {
        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(HeroInfoData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            HeroInfoData tableData = new HeroInfoData();
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

            m_dic_hero_info_data.Add(tableData.m_kind, tableData);

            // GroupBy Tier
            if (m_dic_hero_info_data_group_by_tier.ContainsKey(tableData.m_tier))
                m_dic_hero_info_data_group_by_tier[tableData.m_tier].Add(tableData);
            else
                m_dic_hero_info_data_group_by_tier.Add(tableData.m_tier, new List<HeroInfoData>() { tableData });
        }
    }
}