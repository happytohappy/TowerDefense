using UnityEngine;
using System;
using System.Collections.Generic;

public class UserManager : MonoBehaviour
{
    [Serializable]
    public class TowerInfo
    {
        public int m_kind;      // 타워 카인드
        public int m_level;     // 타워 레벨
        public int m_grade;     // 타워 등급

        public TowerInfo(int in_kind, int in_level, int in_grade)
        {
            m_kind = in_kind;
            m_level = in_level;
            m_grade = in_grade;
        }
    }

    private Dictionary<int, TowerInfo> m_dic_have_tower = new Dictionary<int, TowerInfo>();
    private Dictionary<int, List<TowerInfo>> m_dic_have_tower_group_by_tier = new Dictionary<int, List<TowerInfo>>();

    // PlayerPrefab 에서 데이터 가져오기
    public void Init()
    {
        // 1티어 기본 보유 타워
        m_dic_have_tower.Add(1, new TowerInfo(1, 1, 1));
        m_dic_have_tower.Add(2, new TowerInfo(2, 1, 1));
        m_dic_have_tower.Add(3, new TowerInfo(3, 1, 1));
        m_dic_have_tower.Add(4, new TowerInfo(4, 1, 1));
        m_dic_have_tower.Add(5, new TowerInfo(5, 1, 1));
        m_dic_have_tower_group_by_tier.Add(1, new List<TowerInfo>()
        {
            new TowerInfo(1, 1, 1),
            new TowerInfo(2, 1, 1),
            new TowerInfo(3, 1, 1),
            new TowerInfo(4, 1, 1),
            new TowerInfo(5, 1, 1) 
        });

        // 2티어 기본 보유 타워                                      
        m_dic_have_tower.Add(9, new TowerInfo(9, 1, 1));             
        m_dic_have_tower.Add(10, new TowerInfo(10, 1, 1));            
        m_dic_have_tower.Add(11, new TowerInfo(11, 1, 1));
        m_dic_have_tower.Add(12, new TowerInfo(12, 1, 1));
        m_dic_have_tower.Add(13, new TowerInfo(13, 1, 1));
        m_dic_have_tower_group_by_tier.Add(2, new List<TowerInfo>()
        {
            new TowerInfo(9, 1, 1),
            new TowerInfo(10, 1, 1),
            new TowerInfo(11, 1, 1),
            new TowerInfo(12, 1, 1),
            new TowerInfo(13, 1, 1)
        });

        // 3티어 기본 보유 타워
        m_dic_have_tower.Add(17, new TowerInfo(17, 1, 1));
        m_dic_have_tower.Add(18, new TowerInfo(18, 1, 1));
        m_dic_have_tower.Add(19, new TowerInfo(19, 1, 1));
        m_dic_have_tower.Add(20, new TowerInfo(20, 1, 1));
        m_dic_have_tower.Add(21, new TowerInfo(21, 1, 1));
        m_dic_have_tower_group_by_tier.Add(3, new List<TowerInfo>()
        {
            new TowerInfo(17, 1, 1),
            new TowerInfo(18, 1, 1),
            new TowerInfo(19, 1, 1),
            new TowerInfo(20, 1, 1),
            new TowerInfo(21, 1, 1)
        });

        // 4티어 기본 보유 타워
        m_dic_have_tower.Add(25, new TowerInfo(25, 1, 1));
        m_dic_have_tower.Add(26, new TowerInfo(26, 1, 1));
        m_dic_have_tower.Add(27, new TowerInfo(27, 1, 1));
        m_dic_have_tower.Add(28, new TowerInfo(28, 1, 1));
        m_dic_have_tower.Add(29, new TowerInfo(29, 1, 1));
        m_dic_have_tower_group_by_tier.Add(4, new List<TowerInfo>()
        {
            new TowerInfo(25, 1, 1),
            new TowerInfo(26, 1, 1),
            new TowerInfo(27, 1, 1),
            new TowerInfo(28, 1, 1),
            new TowerInfo(29, 1, 1)
        });

        // 5티어 기본 보유 타워
        m_dic_have_tower.Add(33, new TowerInfo(33, 1, 1));
        m_dic_have_tower.Add(34, new TowerInfo(34, 1, 1));
        m_dic_have_tower.Add(35, new TowerInfo(35, 1, 1));
        m_dic_have_tower.Add(36, new TowerInfo(36, 1, 1));
        m_dic_have_tower.Add(37, new TowerInfo(37, 1, 1));
        m_dic_have_tower_group_by_tier.Add(5, new List<TowerInfo>()
        {
            new TowerInfo(33, 1, 1),
            new TowerInfo(34, 1, 1),
            new TowerInfo(35, 1, 1),
            new TowerInfo(36, 1, 1),
            new TowerInfo(37, 1, 1)
        });
    }

    public TowerInfo GetUserTowerInfo(int in_kind)
    {
        if (m_dic_have_tower.ContainsKey(in_kind))
            return m_dic_have_tower[in_kind];
        else
            return null;
    }

    public List<TowerInfo> GetUserTowerInfoGroupByTier(int in_tier)
    {
        if (m_dic_have_tower_group_by_tier.ContainsKey(in_tier))
            return m_dic_have_tower_group_by_tier[in_tier];
        else
            return null;
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

        // 가져온 데이터를 바이트 배열로 변환하고 사용하기 위해 다시 리스트로 캐스팅 해줍니다.
        return (T)binaryFormatter.Deserialize(memoryStream);
    }

     if (PlayerPrefs.HasKey(LocalKey.Option.ToString()) == true)
        {
            Account.option = Utility.LoadLocalData<Option>(LocalKey.Option);
            Utility.SaveLocalData<Option>(Account.option, LocalKey.Option);
        }
    */

}
