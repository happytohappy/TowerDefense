using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitMonsterInfoTable()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/TableMonster");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            MonsterInfoData MonsterData = new MonsterInfoData();
            MonsterData.m_kind = int.Parse(words[0]);
            MonsterData.m_path = words[1];
            //MonsterData.m_hp = int.Parse(words[3]);
            //MonsterData.m_move_speed = float.Parse(words[4]);

            m_dic_monster_info_data.Add(MonsterData.m_kind, MonsterData);
        }
    }

    public void SetMonsterInfoData(string in_sheet_data)
    {
        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(MonsterInfoData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            MonsterInfoData tableData = new MonsterInfoData();
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

            m_dic_monster_info_data.Add(tableData.m_kind, tableData);
        }
    }
}