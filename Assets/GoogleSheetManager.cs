using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    private const string googleSheetURL = "https://docs.google.com/spreadsheets/d/1CDppVzjl5WXTeD0vy6Gsnsek16yf-6AgTettq47xFWE/export?format=tsv&gid=1995903818&range=A2:D";
    private string sheetData;

    private IEnumerator Start()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(googleSheetURL))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
                sheetData = www.downloadHandler.text;
        }

        DisplayText();
    }

    private void DisplayText()
    {
        string[] row = sheetData.Split('\n');
        string[] columns = row[0].Split('\t');

        Debug.Log(columns[1] + "\n" + columns[2] + "\n" + columns[3]);
    }
}
