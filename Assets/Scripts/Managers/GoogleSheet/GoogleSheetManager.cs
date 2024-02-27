using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    private const string GOOGLE_SHEET_URL = "https://docs.google.com/spreadsheets/d/1CDppVzjl5WXTeD0vy6Gsnsek16yf-6AgTettq47xFWE/export?format=tsv&range=A2:Z&gid=";
    private string sheetData;

    // GID
    // Hero_Info    846969345
    // Hero_Grade   1100293316
    // Hero_Level   703649086

    public void Init()
    {
        StartCoroutine(CoRequestGoogleSheet(846969345, (value) => Managers.Table.SetHeroInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1100293316, (value) => Managers.Table.SetHeroGradeData(value)));
        StartCoroutine(CoRequestGoogleSheet(703649086, (value) => Managers.Table.SetHeroLevelData(value)));
    }

    private IEnumerator CoRequestGoogleSheet(int in_gid, Action<string> in_call_back)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{GOOGLE_SHEET_URL}{in_gid}"))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                var sheetData = www.downloadHandler.text;
                in_call_back.Invoke(sheetData);
            }
        }
    }
}