using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ObserverManager : MonoBehaviour
{
    private readonly Dictionary<EGoods,   List<TMP_Text>> dic_goods = new Dictionary<EGoods, List<TMP_Text>>();
    private readonly Dictionary<EContent, List<GameObject>> dic_red_dot = new Dictionary<EContent, List<GameObject>>();

    public void SetObserverGoods(EGoods in_goods, TMP_Text in_tmp)
    {
        if (dic_goods.ContainsKey(in_goods))
        {
            if (dic_goods[in_goods].Find(x => x == in_tmp) == false)
                dic_goods[in_goods].Add(in_tmp);
        }
        else
        {
            dic_goods.Add(in_goods, new List<TMP_Text>() { in_tmp });
        }
    }

    public void UpdateObserverGoods(EGoods in_goods)
    {
        if (dic_goods.TryGetValue(in_goods, out var value))
        {
            foreach (var e in value)
            {
                Util.SetGoods(in_goods, e);
            }
        }
    }

    public void SetObserverRedDot(EContent in_content, GameObject in_go)
    {
        if (dic_red_dot.ContainsKey(in_content))
        {
            if (dic_red_dot[in_content].Find(x => x == in_go) == false)
                dic_red_dot[in_content].Add(in_go);
        }
        else
        {
            dic_red_dot.Add(in_content, new List<GameObject>() { in_go });
        }
    }

    public void UpdateObserverRedDot(EContent in_content)
    {
        if (dic_red_dot.TryGetValue(in_content, out var value))
        {
            foreach (var e in value)
            {
                switch (in_content)
                {
                    case EContent.Attendance:
                        e.Ex_SetActive(Util.RedDotAttendance());
                        break;
                }
            }
        }
    }
}