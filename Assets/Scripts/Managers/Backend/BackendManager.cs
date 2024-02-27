using BackEnd;
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
            //GetProbabilitysTest();

            Debug.Log("�ʱ�ȭ ���� : " + bro);
        }
        else
            Debug.LogError("�ʱ�ȭ ���� : " + bro);
    }

    public class ProbabilityItem
    {
        public string itemID;
        public string itemName;
        public string hpPower;
        public string percent;
        public int num;
        public override string ToString()
        {
            return $"itemID : {itemID}\n" +
            $"itemName : {itemName}\n" +
            $"hpPower : {hpPower}\n" +
            $"percent : {percent}\n" +
            $"num : {num}\n";
        }
    }

    public void GetProbabilitysTest()
    {
        string selectedProbabilityFileId = "9635";

        var bro = Backend.Probability.GetProbabilitys(selectedProbabilityFileId, 10); // 10����;

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return;
        }

        LitJson.JsonData json = bro.GetFlattenJSON()["elements"];

        List<ProbabilityItem> itemList = new List<ProbabilityItem>();

        for (int i = 0; i < json.Count; i++)
        {
            ProbabilityItem item = new ProbabilityItem();

            item.itemID = json[i]["itemID"].ToString();
            item.itemName = json[i]["itemName"].ToString();
            item.hpPower = json[i]["hpPower"].ToString();
            item.percent = json[i]["percent"].ToString();
            item.num = int.Parse(json[i]["num"].ToString());

            itemList.Add(item);
        }

        foreach (var item in itemList)
        {
            Debug.Log(item.ToString());
        }
    }
}
