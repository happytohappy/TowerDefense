using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    private const string GOOGLE_SHEET_URL = "https://docs.google.com/spreadsheets/d/1CDppVzjl5WXTeD0vy6Gsnsek16yf-6AgTettq47xFWE/export?format=tsv&range=A6:Z&gid=";
    private string sheetData;
    private Action m_callback = null;
    private int SheetTotalCnt = 0;
    private int SheetCurrCnt = 0;
    private bool SheetCheck = false;

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
    // Stage_Info       1399009269
    // Stage_Wave       1189025789
    // Stage_Reward     715738274
    // Treasure_Level   494813664
    // Treasure_Info    1233941213
    // Mission_Info     866950427
    // Synergy_Info     1669999714
    // Buff_Info        0
    // Buff_Level       1995903818
    // Stage_Mission    1324865195
    // Achievement_Info 123634346
    // Village_Info     1419666850
    // Village_Level    1297306839
    // Equip_Info       261053514

    public void Init(Action in_callback)
    {
        m_callback = in_callback;
        SheetTotalCnt = 22;
        SheetCheck = true;

        //StartCoroutine(CoRequestGoogleSheet(195141331,      (value) => Managers.Table.SetConstData(value)));  // �ƴѵ�,,;
        StartCoroutine(CoRequestGoogleSheet(846969345,      (value) => Managers.Table.SetHeroInfoData(value)));;
        StartCoroutine(CoRequestGoogleSheet(1100293316,     (value) => Managers.Table.SetHeroGradeData(value)));
        StartCoroutine(CoRequestGoogleSheet(703649086,      (value) => Managers.Table.SetHeroLevelData(value)));
        StartCoroutine(CoRequestGoogleSheet(1101510208,     (value) => Managers.Table.SetLocalizationData(value)));
        StartCoroutine(CoRequestGoogleSheet(1943578647,     (value) => Managers.Table.SetMonsterInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1100538500,     (value) => Managers.Table.SetMonsterStatusData(value)));
        StartCoroutine(CoRequestGoogleSheet(1085540121,     (value) => Managers.Table.SetGachaInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(397372995,      (value) => Managers.Table.SetGachaRewardData(value)));
        StartCoroutine(CoRequestGoogleSheet(1399009269,     (value) => Managers.Table.SetStageInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1189025789,     (value) => Managers.Table.SetStageWaveData(value)));
        StartCoroutine(CoRequestGoogleSheet(715738274,      (value) => Managers.Table.SetStageRewardData(value)));
        StartCoroutine(CoRequestGoogleSheet(494813664,      (value) => Managers.Table.SetTreasureLevelData(value)));
        StartCoroutine(CoRequestGoogleSheet(1233941213,     (value) => Managers.Table.SetTreasureInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(866950427,      (value) => Managers.Table.SetMissionInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1669999714,     (value) => Managers.Table.SetSynergyInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(0,              (value) => Managers.Table.SetBuffInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1995903818,     (value) => Managers.Table.SetBuffLevelData(value)));
        StartCoroutine(CoRequestGoogleSheet(1324865195,     (value) => Managers.Table.SetMissionStageData(value)));
        StartCoroutine(CoRequestGoogleSheet(123634346,      (value) => Managers.Table.SetMissionAchievementData(value)));
        StartCoroutine(CoRequestGoogleSheet(1419666850,     (value) => Managers.Table.SetTownInfoData(value)));
        StartCoroutine(CoRequestGoogleSheet(1297306839,     (value) => Managers.Table.SetTownLevelData(value)));
        StartCoroutine(CoRequestGoogleSheet(261053514,      (value) => Managers.Table.SetEquipInfoData(value)));
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
                SheetCurrCnt++;
            }
        }
    }

    private void Update()
    {
        if (SheetCheck)
        {
            if (SheetTotalCnt == SheetCurrCnt)
            {
                m_callback.Invoke();
                SheetCheck = false;
            }
        }
    }
}