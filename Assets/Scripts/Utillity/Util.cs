using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

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

        return Managers.Table.GetLanguage(heroInfo.m_name);
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

    public static void ChangeLayersRecursively(Transform in_trans, string in_layer_name)
    {
        in_trans.gameObject.layer = LayerMask.NameToLayer(in_layer_name);
        foreach (Transform child in in_trans)
            ChangeLayersRecursively(child, in_layer_name);
    }

    public static void SetGradeStar(List<Image> in_star, int in_grade)
    {
        string GradeNone = "Icon_GradeStar_Off";
        string GradeOneByFive = "Icon_GradeStar_On1-5";
        string GradeSixByTen = "Icon_GradeStar_On6-10";

        for (int i = 0; i < in_star.Count; i++)
        {
            if (in_grade < 6)
            {
                if (i < in_grade)
                    in_star[i].Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, GradeOneByFive));
                else
                    in_star[i].Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, GradeNone));
            }
            else
            {
                if (i < in_grade - 5)
                    in_star[i].Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, GradeSixByTen));
                else
                    in_star[i].Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, GradeOneByFive));
            }
        }
    }

    public static Sprite GetUnitType(int in_kind)
    {
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return null;

        return GetUnitType(heroInfo.m_type);
    }

    public static Sprite GetUnitType(EHeroType in_type)
    {
        string resourceName = string.Empty;
        switch (in_type)
        {
            case EHeroType.Fighter: resourceName = "Icon_HeroType_Fighter"; break;
            case EHeroType.Archer: resourceName = "Icon_HeroType_Archer"; break;
            case EHeroType.Warrior: resourceName = "Icon_HeroType_Warrior"; break;
            case EHeroType.Assassin: resourceName = "Icon_HeroType_Assassin"; break;
            case EHeroType.Magician: resourceName = "Icon_HeroType_Magician"; break;
            case EHeroType.Lancer: resourceName = "Icon_HeroType_Lancer"; break;
            case EHeroType.Shooter: resourceName = "Icon_HeroType_Shooter"; break;
            case EHeroType.Gladiator: resourceName = "Icon_HeroType_Gladiator"; break;
        }

        return Managers.Sprite.GetSprite(Atlas.Common, resourceName);
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

    public static void SetUnitType(Image in_image, EHeroType in_type)
    {
        in_image.Ex_SetImage(GetUnitType(in_type));
    }

    public static void SetRarityBG(Image in_grade_bg, ERarity in_rarity)
    {
        string Rare = "BG_Slot_Rare";
        string Epic = "BG_Slot_Epic";
        string Legend = "BG_Slot_Legend";
        string Myth = "BG_Slot_Myth";

        switch (in_rarity)
        {
            case ERarity.RARE:   in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, Rare));      break;
            case ERarity.EPIC:   in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, Epic));      break;
            case ERarity.LEGEND: in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, Legend));    break;
            case ERarity.MYTH:   in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, Myth));      break;
        }
    }

    public static void SetEquipIcon(Image in_image, string in_name)
    {
        in_image.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Equip, in_name));
    }

    public static void SetEquipGradeBG(Image in_grade_bg, EEquipGrade in_grade)
    {
        switch (in_grade)
        {
            case EEquipGrade.F:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_F"));
                break;
            case EEquipGrade.E:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_E"));
                break;
            case EEquipGrade.D:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_D"));
                break;
            case EEquipGrade.C:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_C"));
                break;
            case EEquipGrade.B:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_B"));
                break;
            case EEquipGrade.A:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_A"));
                break;
            case EEquipGrade.SS:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_SS"));
                break;
            case EEquipGrade.SR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_SR"));
                break;
            case EEquipGrade.SSR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "BG_Slot_Equip_SSR"));
                break;
        }
    }

    public static void SetEquipGradeTextImage(Image in_grade_bg, EEquipGrade in_grade)
    {
        switch (in_grade)
        {
            case EEquipGrade.F:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_F"));
                break;
            case EEquipGrade.E:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_E"));
                break;
            case EEquipGrade.D:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_D"));
                break;
            case EEquipGrade.C:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_C"));
                break;
            case EEquipGrade.B:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_B"));
                break;
            case EEquipGrade.A:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_A"));
                break;
            case EEquipGrade.SS:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_SS"));
                break;
            case EEquipGrade.SR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_SR"));
                break;
            case EEquipGrade.SSR:
                in_grade_bg.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Common, "Icon_Slot_Equip_SSR"));
                break;
        }
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

    public static void SetSkill(List<Slot_Skill> in_skills, int in_kind)
    {
        var hero = Managers.User.GetUserHeroInfo(in_kind);
        HeroGradeData heroGradeData = null;

        if (hero == null)
            heroGradeData = Managers.Table.GetHeroGradeData(in_kind, 1);
        else
            heroGradeData = Managers.Table.GetHeroGradeData(in_kind, hero.m_grade);

        if (heroGradeData == null)
            return;

        for (int i = 0; i < in_skills.Count; i++)
        {
            if (i == 0)
                in_skills[i].SetSkill(heroGradeData.m_skill_1, 1, hero == null);
            else if (i == 1)
                in_skills[i].SetSkill(heroGradeData.m_skill_2, 1, hero == null);
            else if (i == 2)
                in_skills[i].SetSkill(heroGradeData.m_skill_3, 1, hero == null);
        }
    }
}