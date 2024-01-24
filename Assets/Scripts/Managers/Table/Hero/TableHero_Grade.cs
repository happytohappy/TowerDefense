﻿using UnityEngine;
using System.Collections.Generic;

public partial class TableManager
{
    private void InitHeroGrade()
    {
        TextAsset TextFile = Resources.Load<TextAsset>("Table/Hero_Grade");
        string CSVText = TextFile.text;
        List<string> valueArray = Util.CSVSplitData(CSVText);
        for (int i = 2; i < valueArray.Count; i++)
        {
            string[] words = valueArray[i].Split(',');
            HeroGradeData HeroGradeData = new HeroGradeData();
            HeroGradeData.m_kind = int.Parse(words[0]);
            HeroGradeData.m_grade = int.Parse(words[1]);
            for (int j = 2; j <= 4; j++)
                SetSkill(ref HeroGradeData, words[j]);

            m_dic_hero_grade_data.Add((HeroGradeData.m_kind, HeroGradeData.m_grade), HeroGradeData);
        }
    }

    private void SetSkill(ref HeroGradeData in_data, string in_value)
    {
        if (int.TryParse(in_value, out int skill_id))
            in_data.m_skills.Add(skill_id);
    }
}