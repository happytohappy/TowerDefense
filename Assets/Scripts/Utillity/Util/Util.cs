using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public static partial class Util
{
    public static GameObject LoadPrefab(string path, GameObject parent = null)
    {
        GameObject prefabs = Resources.Load<GameObject>(string.Format("UI/{0}", path));
        if (prefabs == null)
        {
            DebugLog($"{path} 경로를 찾을 수 없습니다.", DebugType.Error);
            return null;
        }

        GameObject go = GameObject.Instantiate(prefabs) as GameObject;

        SetParent(go, Managers.UICanvas.gameObject);

        var item = go.GetComponent<RectTransform>();
        item.offsetMin = Vector2.zero;
        item.offsetMax = Vector2.zero;

        return go;
    }

    public static void SetParent(GameObject obj, GameObject parent)
    {
        obj.transform.SetParent(parent.transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }

    public static T ReadJsonData<T>(byte[] buf)
    {
        var strByte = Encoding.Default.GetString(buf);
        return JsonUtility.FromJson<T>(strByte);
    }

    public static byte[] DataToJsonData<T>(T obj)
    {
        var jsonData = JsonUtility.ToJson(obj);
        return Encoding.UTF8.GetBytes(jsonData);
    }

    public static List<string> CSVSplitData(string t)
    {
        t = t.Replace("\r\n", "\n");
        string[] lineTemp = t.Split("\n"[0]); return new List<string>(lineTemp);
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static string GetShuffleString(string in_str)
    {
        List<char> array = new List<char>();
        for (int i = 0; i < in_str.Length; i++)
            array.Add(in_str[i]);

        System.Random rand = new System.Random();
        var shuffled = array.OrderBy(_ => rand.Next()).ToList();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < shuffled.Count; i++)
            sb.Append(shuffled[i]);

        return sb.ToString();
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static string CommaText(int in_value)
    {
        if (in_value == 0)
            return "0";

        return string.Format("{0:#,###}", in_value); 
    }

    public static string SpecialString(string in_str)
    {
        in_str = in_str.Replace('^', ',');

        string[] tempStr = in_str.Split('|');
        string Result = tempStr[0];

        for (int i = 1; i < tempStr.Length; i++)
            Result += "\n" + tempStr[i];

        return Result;
    }

    public static void DebugLog(string logMsg, DebugType debugLogType = DebugType.Normal)
    {
#if UNITY_EDITOR
        switch (debugLogType)
        {
            case DebugType.Normal:
                Debug.Log(logMsg);
                break;
            case DebugType.Warning:
                Debug.LogWarning(logMsg);
                break;
            case DebugType.Error:
                Debug.LogError(logMsg);
                break;
        }
#endif
    }

    /// <summary>
    /// 재화 셋팅
    /// </summary>
    /// <param name="in_goods"></param>
    /// <param name="in_text"></param>
    public static void SetGoods(EGoods in_goods, TMP_Text in_text)
    {
        in_text.Ex_SetText($"{CommaText(GetGoods(in_goods))}");
    }

    public static HPBar CreateHP(Transform in_transform, float in_curr_hp, float in_max_hp, Vector3 in_scale, Vector3 in_offset, bool in_active = false)
    {
        var go = Managers.Resource.Instantiate("Item/HPBar", new Vector3(9999f, 9999f, 9999f), Managers.Widget);
        var hpBar = go.GetComponent<HPBar>();
        if (hpBar == null)
            return null;

        hpBar.Target = in_transform;
        hpBar.Set(in_offset, in_scale, Color.red);
        hpBar.SetHP(in_curr_hp / in_max_hp);
        hpBar.HPBarActive(in_active);

        return hpBar;
    }

    public static Hud_HeroInfo CreateHudHeroInfo(Hero in_hero, Vector3 in_offset)
    {
        var go = Managers.Resource.Instantiate("Item/Hud_HeroInfo", new Vector3(9999f, 9999f, 9999f), Managers.Widget);
        var hudHeroInfo = go.GetComponent<Hud_HeroInfo>();
        if (hudHeroInfo == null)
            return null;

        hudHeroInfo.Target = in_hero;
        hudHeroInfo.Set(in_offset);

        return hudHeroInfo;
    }

    public static Hud_TownInfo CreateHudTownInfo(ETownType in_town_type, Transform in_town, Vector3 in_offset)
    {
        string prefabName = string.Empty;
        switch (in_town_type)
        {
            case ETownType.Gold:
                prefabName = "Hud_Town_Gold";
                break;
            case ETownType.Ruby:
                prefabName = "Hud_Town_Ruby";
                break;
            case ETownType.Dia:
                prefabName = "Hud_Town_Dia";
                break;
            case ETownType.Unit:
                prefabName = "Hud_Town_Unit";
                break;
            case ETownType.Equip:
                prefabName = "Hud_Town_Equip";
                break;
        }

        var go = Managers.Resource.Instantiate($"Item/{prefabName}", new Vector3(9999f, 9999f, 9999f), Managers.Widget);
        var hudTownInfo = go.GetComponent<Hud_TownInfo>();
        if (hudTownInfo == null)
            return null;

        hudTownInfo.Target = in_town;
        hudTownInfo.Set(in_town_type, in_offset);

        return hudTownInfo;
    }

    public static Hud_Damage CreateHudDamage(Vector3 in_position, string in_damage)
    {
        var go = Managers.Resource.Instantiate("Item/Hud_Damage", new Vector3(9999f, 9999f, 9999f), Managers.Widget);
        var hudDamage = go.GetComponent<Hud_Damage>();
        if (hudDamage == null)
            return null;

        hudDamage.Set(in_position, in_damage);

        return hudDamage;
    }

    public static void ChangeLayersRecursively(Transform in_trans, string in_layer_name)
    {
        in_trans.gameObject.layer = LayerMask.NameToLayer(in_layer_name);
        foreach (Transform child in in_trans)
            ChangeLayersRecursively(child, in_layer_name);
    }

    public static Sprite GetResourceImage(int in_kind)
    {
        string resourceName = string.Empty;
        switch (in_kind)
        {
            case 1: resourceName = "Icon_Common_Gold"; break;
            case 2: resourceName = "Icon_Common_Ruby"; break;
            case 3: resourceName = "Icon_Common_Dia"; break;
            case 4: resourceName = "Icon_Common_Energy"; break;
        }

        return Managers.Sprite.GetSprite(Atlas.Common, resourceName);
    }   

    public static int GetGoods(EGoods in_goods)
    {
        return Managers.User.GetInventoryItem((int)in_goods);
    }

    public static long UnixTimeNow() 
    {
        var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)); return (long)timeSpan.TotalSeconds;
    }

    public static string RemainingToDate(long in_time)
    {
        StringBuilder result = new StringBuilder();

        if (in_time >= 3600)
        {
            result.Append($"{string.Format("{0:D2}", in_time / 3600)}:");
            in_time %= 3600;
        }
        else
        {
            result.Append($"00:");
        }

        if (in_time >= 60)
        {
            result.Append($"{string.Format("{0:D2}", in_time / 60)}:");
            in_time %= 60;
        }
        else
        {
            result.Append($"00:");
        }

        if (in_time > 0)
        {
            result.Append($"{string.Format("{0:D2}", in_time)}");
        }
        else
        {
            result.Append($"00");
        }

        return result.ToString();
    }

    public static string GetItemTypeLocal(EEquipType in_equip_type)
    {
        var str = string.Empty;
        switch (in_equip_type)
        {
            case EEquipType.Glove:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_GLOVE"));
                break;
            case EEquipType.Bow:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_BOW"));
                break;
            case EEquipType.Sword:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_SWORD"));
                break;
            case EEquipType.Dualsword:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_DUAL"));
                break;
            case EEquipType.Wand:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_WAND"));
                break;
            case EEquipType.Lance:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_LANCE"));
                break;
            case EEquipType.Gun:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_GUN"));
                break;
            case EEquipType.Shield:
                str = SpecialString(Managers.Table.GetLanguage("UI_EQUIP_CATEGORY_SHIELD"));
                break;
        }

        return str;
    }
}