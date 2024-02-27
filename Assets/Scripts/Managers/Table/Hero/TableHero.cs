using System.Collections.Generic;

public partial class TableManager
{
    public Dictionary<int, HeroInfoData>         m_dic_hero_info_data  = new Dictionary<int, HeroInfoData>();
    public Dictionary<int, List<HeroInfoData>>   m_dic_hero_info_data_group_by_tier  = new Dictionary<int, List<HeroInfoData>>();
    public Dictionary<(int, int), HeroGradeData> m_dic_hero_grade_data = new Dictionary<(int, int), HeroGradeData>();
    public Dictionary<(int, int), HeroLevelData> m_dic_hero_level_data = new Dictionary<(int, int), HeroLevelData>();

    private void InitHeroTable()
    {
        InitHeroInfo();
        InitHeroGrade();
        InitHeroLevel();
    }

    private void ClearHeroTable()
    {
        m_dic_hero_info_data.Clear();
        m_dic_hero_grade_data.Clear();
        m_dic_hero_level_data.Clear();
    }

    public HeroInfoData GetHeroInfoData(int in_kind)
    {
        if (m_dic_hero_info_data.ContainsKey(in_kind))
            return m_dic_hero_info_data[in_kind];
        else
            return null;
    }

    public List<HeroInfoData> GetHeroInfoDataGroupByTier(int in_tier)
    {
        if (m_dic_hero_info_data_group_by_tier.ContainsKey(in_tier))
            return m_dic_hero_info_data_group_by_tier[in_tier];
        else
            return null;
    }

    public HeroGradeData GetHeroGradeData(int in_kind, int in_grade)
    {
        var key = (in_kind, in_grade);
        if (m_dic_hero_grade_data.ContainsKey(key))
            return m_dic_hero_grade_data[key];
        else
            return null;
    }

    public HeroLevelData GetHeroLevelData(int in_kind, int in_level)
    {
        var key = (in_kind, in_level);
        if (m_dic_hero_level_data.ContainsKey(key))
            return m_dic_hero_level_data[key];
        else
            return null;
    }
}