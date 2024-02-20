using UnityEngine;
using System.Text;
using System.Collections.Generic;

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

    public static Sprite GetHeroImage(int in_kind)
    {
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return null;

        return Managers.Resource.Load<Sprite>($"Image/Hero/Tier_{heroInfo.m_tier}/Hero_{heroInfo.m_kind}");
    }

    public static string GetHeroName(int in_kind)
    {
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return string.Empty;

        return heroInfo.m_name;
    }

    public static string GetHeroRarityToString(int in_kind)
    {
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return string.Empty;

        return heroInfo.m_rarity.ToString();
    }

    public static int GetHeroTier(int in_kind)
    {
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return 0;

        return heroInfo.m_tier;
    }
}