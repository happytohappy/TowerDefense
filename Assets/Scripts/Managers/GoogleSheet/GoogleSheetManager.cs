using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    private const string GOOGLE_SHEET_URL = "https://docs.google.com/spreadsheets/d/1CDppVzjl5WXTeD0vy6Gsnsek16yf-6AgTettq47xFWE/export?format=tsv&range=A6:Z&gid=";
    private string sheetData;

    // GID
    // CONST            195141331
    // Hero_Info        846969345
    // Hero_Grade       1100293316
    // Hero_Level       703649086
    // Localization     1101510208
    // Monster_Info     1943578647
    // Monster_Status   1100538500
    // Gacha_Info       1085540121
    // Gacha_Reward     397372995
    // Stage_Wave       1189025789
    // Treasure_Level   494813664
    // Mission_Info     866950427
    // Synergy_Info     1669999714

    public void Init()
    {
        //StartCoroutine(CoRequestGoogleSheet(195141331,      (value) => Managers.Table.SetConstData(value)));  // ¾Æ´Ñµí,,;
        StartCoroutine(CoRequestGoogleSheet(846969345,      (value) => Managers.Table.SetHeroInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1100293316,     (value) => Managers.Table.SetHeroGradeData(value)));
        StartCoroutine(CoRequestGoogleSheet(703649086,      (value) => Managers.Table.SetHeroLevelData(value)));
        StartCoroutine(CoRequestGoogleSheet(1101510208,     (value) => Managers.Table.SetLocalizationData(value)));
        StartCoroutine(CoRequestGoogleSheet(1943578647,     (value) => Managers.Table.SetMonsterInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1100538500,     (value) => Managers.Table.SetMonsterStatusData(value)));
        StartCoroutine(CoRequestGoogleSheet(1085540121,     (value) => Managers.Table.SetGachaInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(397372995,      (value) => Managers.Table.SetGachaRewardData(value)));
        StartCoroutine(CoRequestGoogleSheet(1189025789,     (value) => Managers.Table.SetStageWaveData(value)));
        StartCoroutine(CoRequestGoogleSheet(494813664,      (value) => Managers.Table.SetTreasureLevelData(value)));
        StartCoroutine(CoRequestGoogleSheet(866950427,      (value) => Managers.Table.SetMissionInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1669999714,     (value) => Managers.Table.SetSynergyInfoData(value)));
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