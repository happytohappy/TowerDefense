using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitGachaReward()
    {
        int totalRate = 0;
        int lastKind = 0;

        TextAsset TextFile = Resources.Load<TextAsset>("Table/Gacha_Reward");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            GachaRewardData GachaRewardData = new GachaRewardData();
            GachaRewardData.m_kind = int.Parse(words[0]);

            if (lastKind != GachaRewardData.m_kind)
                totalRate = 0;

            lastKind = GachaRewardData.m_kind;
            GachaRewardData.m_item = int.Parse(words[1]);
            GachaRewardData.m_amount = int.Parse(words[2]);
            GachaRewardData.m_rate = int.Parse(words[3]);
            GachaRewardData.m_rate_min = totalRate;
            GachaRewardData.m_rate_max = totalRate + GachaRewardData.m_rate;
            totalRate += GachaRewardData.m_rate;

            if (m_dic_gacha_reward_data.ContainsKey(GachaRewardData.m_kind))
                m_dic_gacha_reward_data[GachaRewardData.m_kind].Add(GachaRewardData);
            else
                m_dic_gacha_reward_data.Add(GachaRewardData.m_kind, new List<GachaRewardData>() { GachaRewardData });
        }
    }

    public void SetGachaRewardData(string in_sheet_data)
    {
        int totalRate = 0;
        int lastKind = 0;

        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(GachaRewardData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            GachaRewardData tableData = new GachaRewardData();

            for (int i = 0; i < columns.Length; i++)
            {
                System.Type type = fields[i].FieldType;
                if (string.IsNullOrEmpty(columns[i])) continue;

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

                if (lastKind != tableData.m_kind) totalRate = 0;
                lastKind = tableData.m_kind;

                tableData.m_rate_min = totalRate;
                tableData.m_rate_max = totalRate + tableData.m_rate;
                totalRate += tableData.m_rate;
            }

            if (m_dic_gacha_reward_data.ContainsKey(tableData.m_kind))
                m_dic_gacha_reward_data[tableData.m_kind].Add(tableData);
            else
                m_dic_gacha_reward_data.Add(tableData.m_kind, new List<GachaRewardData>() { tableData });
        }
    }
}