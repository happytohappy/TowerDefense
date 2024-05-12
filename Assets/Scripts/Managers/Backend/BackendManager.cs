using BackEnd;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    public void Init()
    {
        // �ڳ� �ʱ�ȭ
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            Debug.Log("�ڳ� SDK �ʱ�ȭ �Ϸ� : " + bro);

            // �Խ�Ʈ �α���
            GuestLogin();
        }
        else
        {
            Debug.Log("�ڳ� SDK �ʱ�ȭ ���� : " + bro);
        }
    }

    public void GuestLogin()
    {
        BackendReturnObject bro = Backend.BMember.GuestLogin("�Խ�Ʈ �α���");
        if (bro.IsSuccess())
        {
            Debug.Log("�Խ�Ʈ �α��ο� �����߽��ϴ�");
        }
        else
        {
            Backend.BMember.DeleteGuestInfo();

            PlayerPrefs.DeleteAll();
            Managers.User.Init();

//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;
//#else
//            Application.Quit(); // ���ø����̼� ����
//#endif

            Debug.Log("�Խ�Ʈ �α��� ����");
        }
    }

    public class ProbabilityItem
    {
        public string itemID;
        public string percent;

        public override string ToString()
        {
            return $"itemID : {itemID}\n" + $"percent : {percent}\n";
        }
    }

    public List<ProbabilityItem> GetProbabilitysTest()
    {
        string selectedProbabilityFileId = "10991";

        var bro = Backend.Probability.GetProbabilitys(selectedProbabilityFileId, 1); // 1����;

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return null;
        }

        LitJson.JsonData json = bro.GetFlattenJSON()["elements"];

        List<ProbabilityItem> itemList = new List<ProbabilityItem>();

        for (int i = 0; i < json.Count; i++)
        {
            ProbabilityItem item = new ProbabilityItem();

            item.itemID = json[i]["itemID"].ToString();
            item.percent = json[i]["percent"].ToString();

            itemList.Add(item);
        }

        foreach (var item in itemList)
        {
            Debug.Log(item.ToString());
        }

        return itemList;
    }

    public void InsertUserData(string in_data)
    {
        Param param = new Param();
        param.Add("Data", in_data);

        var aaa = Backend.GameData.Insert("User", param);

        Debug.LogError(aaa);
    }

    public long ServerTimeGetUTCTimeStamp()
    {
        BackendReturnObject servertime = Backend.Utils.GetServerTime();

        string time = servertime.GetReturnValuetoJSON()["utcTime"].ToString();
        DateTime parsedDate = DateTime.Parse(time);

        var timeSpan = parsedDate - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long)timeSpan.TotalSeconds;
    }

    public void ServerTime()
    {
        BackendReturnObject servertime = Backend.Utils.GetServerTime();

        string time = servertime.GetReturnValuetoJSON()["utcTime"].ToString();
        DateTime parsedDate = DateTime.Parse(time);

        var timeSpan = parsedDate - new DateTime(1970, 1, 1, 0, 0, 0);

        Debug.Log("���� �ð� : " + parsedDate);
    }
}
