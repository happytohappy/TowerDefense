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
            Debug.Log("뒤끝 SDK 초기화 완료 : " + bro);

            // 커스텀 회원가임
            CustomSignUp();
        }
        else
        {
            Debug.Log("뒤끝 SDK 초기화 실패 : " + bro);
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

            // 랜덤으로 한번 섞어준다.
            result = Util.GetShuffleString(result);

            BackendReturnObject bro = Backend.BMember.CustomSignUp(result, result);
            if (bro.IsSuccess())
            {
                Debug.Log("회원가입 성공 : " + bro);

                PlayerPrefs.SetString(LocalKey.Account.ToString(), result);
            }
            else
            {
                Debug.Log("회원가입 실패 : " + bro);
            }
        }

        // 커스텀 로그인
        CustomLogin(result);
    }

    public void CustomLogin(string in_result)
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(in_result, in_result);
        if (bro.IsSuccess())
        {
            Debug.Log("로그인 성공 : " + bro);
        }
        else
        {
            PlayerPrefs.DeleteAll();
            //Managers.User.Init();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // 어플리케이션 종료
#endif

            Debug.Log("로그인 실패 : " + bro);
        }
    }

    // 가챠
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

        var bro = Backend.Probability.GetProbabilitys(selectedProbabilityFileId, 1); // 1연차;

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

    // 서버 시간
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

    // 회원 탈퇴
    public void DeleteAccount()
    {
        BackendReturnObject bro = Backend.BMember.WithdrawAccount();
    }
}