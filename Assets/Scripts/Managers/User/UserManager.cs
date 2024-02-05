using UnityEngine;
using System;
using System.Collections.Generic;

public class UserManager : MonoBehaviour
{
    [Serializable]
    public class HeroInfo
    {
        public int m_kind;      // Ÿ�� ī�ε�
        public int m_level;     // Ÿ�� ����
        public int m_grade;     // Ÿ�� ���

        public HeroInfo(int in_kind, int in_level, int in_grade)
        {
            m_kind = in_kind;
            m_level = in_level;
            m_grade = in_grade;
        }
    }

    private Dictionary<int, HeroInfo> m_dic_have_hero = new Dictionary<int, HeroInfo>();
    private Dictionary<int, List<HeroInfo>> m_dic_have_hero_group_by_tier = new Dictionary<int, List<HeroInfo>>();

    // PlayerPrefab ���� ������ ��������
    public void Init()
    {
        // 1Ƽ�� �⺻ ���� Ÿ��
        m_dic_have_hero.Add(1001, new HeroInfo(1001, 1, 1));
        m_dic_have_hero.Add(1002, new HeroInfo(1002, 1, 1));
        m_dic_have_hero.Add(1003, new HeroInfo(1003, 1, 1));
        m_dic_have_hero.Add(1004, new HeroInfo(1004, 1, 1));
        m_dic_have_hero.Add(1005, new HeroInfo(1005, 1, 1));
        m_dic_have_hero_group_by_tier.Add(1, new List<HeroInfo>()
        {
            new HeroInfo(1001, 1, 1),
            new HeroInfo(1002, 1, 1),
            new HeroInfo(1003, 1, 1),
            new HeroInfo(1004, 1, 1),
            new HeroInfo(1005, 1, 1) 
        });

        // 2Ƽ�� �⺻ ���� Ÿ��                                      
        m_dic_have_hero.Add(2001, new HeroInfo(2001, 1, 1));             
        m_dic_have_hero.Add(2002, new HeroInfo(2002, 1, 1));            
        m_dic_have_hero.Add(2003, new HeroInfo(2003, 1, 1));
        m_dic_have_hero.Add(2004, new HeroInfo(2004, 1, 1));
        m_dic_have_hero.Add(2005, new HeroInfo(2005, 1, 1));
        m_dic_have_hero_group_by_tier.Add(2, new List<HeroInfo>()
        {
            new HeroInfo(2001, 1, 1),
            new HeroInfo(2002, 1, 1),
            new HeroInfo(2003, 1, 1),
            new HeroInfo(2004, 1, 1),
            new HeroInfo(2005, 1, 1)
        });

        // 3Ƽ�� �⺻ ���� Ÿ��
        m_dic_have_hero.Add(3001, new HeroInfo(3001, 1, 1));
        m_dic_have_hero.Add(3002, new HeroInfo(3002, 1, 1));
        m_dic_have_hero.Add(3003, new HeroInfo(3003, 1, 1));
        m_dic_have_hero.Add(3004, new HeroInfo(3004, 1, 1));
        m_dic_have_hero.Add(3005, new HeroInfo(3005, 1, 1));
        m_dic_have_hero_group_by_tier.Add(3, new List<HeroInfo>()
        {
            new HeroInfo(3001, 1, 1),
            new HeroInfo(3002, 1, 1),
            new HeroInfo(3003, 1, 1),
            new HeroInfo(3004, 1, 1),
            new HeroInfo(3005, 1, 1)
        });

        // 4Ƽ�� �⺻ ���� Ÿ��
        m_dic_have_hero.Add(4001, new HeroInfo(4001, 1, 1));
        m_dic_have_hero.Add(4002, new HeroInfo(4002, 1, 1));
        m_dic_have_hero.Add(4003, new HeroInfo(4003, 1, 1));
        m_dic_have_hero.Add(4004, new HeroInfo(4004, 1, 1));
        m_dic_have_hero.Add(4005, new HeroInfo(4005, 1, 1));
        m_dic_have_hero_group_by_tier.Add(4, new List<HeroInfo>()
        {
            new HeroInfo(4001, 1, 1),
            new HeroInfo(4002, 1, 1),
            new HeroInfo(4003, 1, 1),
            new HeroInfo(4004, 1, 1),
            new HeroInfo(4005, 1, 1)
        });

        // 5Ƽ�� �⺻ ���� Ÿ��
        m_dic_have_hero.Add(5001, new HeroInfo(5001, 1, 1));
        m_dic_have_hero.Add(5002, new HeroInfo(5002, 1, 1));
        m_dic_have_hero.Add(5003, new HeroInfo(5003, 1, 1));
        m_dic_have_hero.Add(5004, new HeroInfo(5004, 1, 1));
        m_dic_have_hero.Add(5005, new HeroInfo(5005, 1, 1));
        m_dic_have_hero_group_by_tier.Add(5, new List<HeroInfo>()
        {
            new HeroInfo(5001, 1, 1),
            new HeroInfo(5002, 1, 1),
            new HeroInfo(5003, 1, 1),
            new HeroInfo(5004, 1, 1),
            new HeroInfo(5005, 1, 1)
        });
    }

    public HeroInfo GetUserHeroInfo(int in_kind)
    {
        if (m_dic_have_hero.ContainsKey(in_kind))
            return m_dic_have_hero[in_kind];
        else
            return null;
    }

    public List<HeroInfo> GetUserHeroInfoGroupByTier(int in_tier)
    {
        if (m_dic_have_hero_group_by_tier.ContainsKey(in_tier))
            return m_dic_have_hero_group_by_tier[in_tier];
        else
            return null;
    }

    public void UpsertHero(int in_kind)
    {
        // �ִ°��� ������ �����ϴ°ǰ�?
        if (m_dic_have_hero.ContainsKey(in_kind))
        {
            m_dic_have_hero[in_kind].m_level++;
        }
        else
        {
            m_dic_have_hero.Add(in_kind, new HeroInfo(in_kind, 1, 1));
        }

    }

    /*
     * public static void SaveLocalData<T>(T SaveData, LocalKey Key)
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        binaryFormatter.Serialize(memoryStream, SaveData);
        PlayerPrefs.SetString(Key.ToString(), Convert.ToBase64String(memoryStream.GetBuffer()));
    }

    public static T LoadLocalData<T>(LocalKey key)
    {
        var data = PlayerPrefs.GetString(key.ToString());
        
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream(Convert.FromBase64String(data));

        // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ� ����ϱ� ���� �ٽ� ����Ʈ�� ĳ���� ���ݴϴ�.
        return (T)binaryFormatter.Deserialize(memoryStream);
    }

     if (PlayerPrefs.HasKey(LocalKey.Option.ToString()) == true)
        {
            Account.option = Utility.LoadLocalData<Option>(LocalKey.Option);
            Utility.SaveLocalData<Option>(Account.option, LocalKey.Option);
        }
    */

}
