using BackEnd;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    public void Init()
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            Debug.Log("�ڳ� SDK �ʱ�ȭ �Ϸ� : " + bro);

            // Ŀ���� ȸ������
            CustomSignUp();
        }
        else
        {
            Debug.Log("�ڳ� SDK �ʱ�ȭ ���� : " + bro);
        }
    }

    public void CustomSignUp()
    {
        string result = string.Empty;

        if (PlayerPrefs.HasKey(LocalKey.Account.ToString()))
        {
            result = PlayerPrefs.GetString(LocalKey.Account.ToString());
        }
        else
        {
            result = ServerTimeGetUTCTimeStamp().ToString();

            for (int i = 0; i < 7; i++)
            {
                var ran = UnityEngine.Random.Range(65, 91);
                result += (char)ran;
            }

            // �������� �ѹ� �����ش�.
            result = Util.GetShuffleString(result);

            BackendReturnObject bro = Backend.BMember.CustomSignUp(result, result);
            if (bro.IsSuccess())
            {
                Debug.Log("ȸ������ ���� : " + bro);

                PlayerPrefs.SetString(LocalKey.Account.ToString(), result);
            }
            else
            {
                Debug.Log("ȸ������ ���� : " + bro);
            }
        }

        // Ŀ���� �α���
        CustomLogin(result);
    }

    public void CustomLogin(string in_result)
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(in_result, in_result);
        if (bro.IsSuccess())
        {
            Debug.Log("�α��� ���� : " + bro);
        }
        else
        {
            PlayerPrefs.DeleteAll();
            //Managers.User.Init();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // ���ø����̼� ����
#endif

            Debug.Log("�α��� ���� : " + bro);
        }
    }

    // ��í
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

    // ���� �ð�
    public long ServerTimeGetUTCTimeStamp()
    {
        BackendReturnObject servertime = Backend.Utils.GetServerTime();

        string time = servertime.GetReturnValuetoJSON()["utcTime"].ToString();
        DateTime parsedDate = DateTime.Parse(time);

        var timeSpan = parsedDate - new DateTime(1970, 1, 1, 0, 0, 0);
        return (long)timeSpan.TotalSeconds;
    }

    public DateTime ServerDateTime()
    {
        BackendReturnObject servertime = Backend.Utils.GetServerTime();

        string time = servertime.GetReturnValuetoJSON()["utcTime"].ToString();
        DateTime parsedDate = DateTime.Parse(time);

        return parsedDate;
    }

    // ȸ�� Ż��
    public void DeleteAccount()
    {
        BackendReturnObject bro = Backend.BMember.WithdrawAccount();
    }
}