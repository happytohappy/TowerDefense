using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public static partial class Util
{
    public static void SetHeroName(TMP_Text in_label, int in_kind)
    {
        if (in_label == null)
            return;

        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return;

        in_label.Ex_SetText(Managers.Table.GetLanguage(heroInfo.m_name));
    }

    public static void SetHeroImage(Image in_image, int in_kind)
    {
        if (in_image == null)
            return;

        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return;

        in_image.Ex_SetImage(Managers.Resource.Load<Sprite>($"Image/Hero/Tier_{heroInfo.m_tier}/Hero_{heroInfo.m_kind}"));
    }

    public static Sprite GetUnitType(int in_kind)
    {
        var heroInfo = Managers.Table.GetHeroInfoData(in_kind);
        if (heroInfo == null)
            return null;

        return GetUnitType(heroInfo.m_type);
    }

    public static void SetUnitType(Image in_image, int in_kind)
    {
        if (in_image == null)
            return;

        in_image.Ex_SetImage(GetUnitType(in_kind));
    }

    public static void SetGradeStar(List<Image> in_star, int in_grade)
    {
        if (in_star == null || in_star.Count == 0)
            return;

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

    public static void SetSkill(List<Slot_Skill> in_skills, int in_kind)
    {
        var hero = Managers.User.GetUserHeroInfo(in_kind);
        int heroGrade = hero != null ? hero.m_grade : 1;

        var heroGradeData = Managers.Table.GetHeroGradeData(in_kind, heroGrade);
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