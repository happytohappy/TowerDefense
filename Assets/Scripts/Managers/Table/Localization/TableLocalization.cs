using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
public partial class TableManager : MonoBehaviour
{ 
    private Dictionary<string, LocalizationData> m_dic_localization_data = new Dictionary<string, LocalizationData>();
    public List<LocalizationText> LocalizationTextList { get; set; } = new List<LocalizationText>();

    private void InitLocalization()
    {
        if (m_dic_localization_data.Count > 0)
            return;

        TextAsset TextFile = Resources.Load<TextAsset>("Table/Localization");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            LocalizationData LocalizationData = new LocalizationData();
            LocalizationData.LAN_KEY = words[0];
            LocalizationData.KOR = words[1];
            LocalizationData.ENG = words[2];
            LocalizationData.JPN = words[3];
            LocalizationData.CHN_S = words[4];
            LocalizationData.CHN_T = words[5];
            m_dic_localization_data.Add(LocalizationData.LAN_KEY, LocalizationData);
        }
    }

    public string GetLanguage(string _lanKey)
    {
        string LanStr = string.Empty;
        var accountLanguage = ELanguage.Kor;

        switch (accountLanguage)
        {
            case ELanguage.Kor:
                LanStr = m_dic_localization_data[_lanKey].KOR;
                break;
            case ELanguage.Eng:
                LanStr = m_dic_localization_data[_lanKey].ENG;
                break;
            case ELanguage.Jpn:
                LanStr = m_dic_localization_data[_lanKey].JPN;
                break;
            case ELanguage.Chn_S:
                LanStr = m_dic_localization_data[_lanKey].CHN_S;
                break;
            case ELanguage.Chn_T:
                LanStr = m_dic_localization_data[_lanKey].CHN_T;
                break;
        }
        return LanStr;
    }

    public void AllLanguageUpdate()
    {
        for (int i = 0; i < LocalizationTextList.Count; i++)
        {
            LocalizationTextList[i].SetLanguage();
        }
    }

    public void SetLocalizationData(string in_sheet_data)
    {
        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(LocalizationData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        string[] rows = in_sheet_data.Split('\n');
        string[] columns = rows[0].Split('\t');
        for (int row = 0; row < rows.Length; row++)
        {
            var sheetData = rows[row].Split('\t');
            LocalizationData tableData = new LocalizationData();
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

            if (tableData.LAN_KEY == "//")
                break;

            if (m_dic_localization_data.ContainsKey(tableData.LAN_KEY))
            {
                Debug.LogError($"Key 중복 : {tableData.LAN_KEY}");
            }
            else
            {
                m_dic_localization_data.Add(tableData.LAN_KEY, tableData);
            }
        }
    }
}