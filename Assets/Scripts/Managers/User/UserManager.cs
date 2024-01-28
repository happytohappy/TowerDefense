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
        m_dic_have_tower.Add(1001, new TowerInfo(1001, 1, 1));
        m_dic_have_tower.Add(1002, new TowerInfo(1002, 1, 1));
        m_dic_have_tower.Add(1003, new TowerInfo(1003, 1, 1));
        m_dic_have_tower.Add(1004, new TowerInfo(1004, 1, 1));
        m_dic_have_tower.Add(1005, new TowerInfo(1005, 1, 1));
        m_dic_have_tower_group_by_tier.Add(1, new List<TowerInfo>()
        {
            new TowerInfo(1001, 1, 1),
            new TowerInfo(1002, 1, 1),
            new TowerInfo(1003, 1, 1),
            new TowerInfo(1004, 1, 1),
            new TowerInfo(1005, 1, 1) 
        });

        // 2티어 기본 보유 타워                                      
        m_dic_have_tower.Add(2001, new TowerInfo(2001, 1, 1));             
        m_dic_have_tower.Add(2002, new TowerInfo(2002, 1, 1));            
        m_dic_have_tower.Add(2003, new TowerInfo(2003, 1, 1));
        m_dic_have_tower.Add(2004, new TowerInfo(2004, 1, 1));
        m_dic_have_tower.Add(2005, new TowerInfo(2005, 1, 1));
        m_dic_have_tower_group_by_tier.Add(2, new List<TowerInfo>()
        {
            new TowerInfo(2001, 1, 1),
            new TowerInfo(2002, 1, 1),
            new TowerInfo(2003, 1, 1),
            new TowerInfo(2004, 1, 1),
            new TowerInfo(2005, 1, 1)
        });

        // 3티어 기본 보유 타워
        m_dic_have_tower.Add(3001, new TowerInfo(3001, 1, 1));
        m_dic_have_tower.Add(3002, new TowerInfo(3002, 1, 1));
        m_dic_have_tower.Add(3003, new TowerInfo(3003, 1, 1));
        m_dic_have_tower.Add(3004, new TowerInfo(3004, 1, 1));
        m_dic_have_tower.Add(3005, new TowerInfo(3005, 1, 1));
        m_dic_have_tower_group_by_tier.Add(3, new List<TowerInfo>()
        {
            new TowerInfo(3001, 1, 1),
            new TowerInfo(3002, 1, 1),
            new TowerInfo(3003, 1, 1),
            new TowerInfo(3004, 1, 1),
            new TowerInfo(3005, 1, 1)
        });

        // 4티어 기본 보유 타워
        m_dic_have_tower.Add(4001, new TowerInfo(4001, 1, 1));
        m_dic_have_tower.Add(4002, new TowerInfo(4002, 1, 1));
        m_dic_have_tower.Add(4003, new TowerInfo(4003, 1, 1));
        m_dic_have_tower.Add(4004, new TowerInfo(4004, 1, 1));
        m_dic_have_tower.Add(4005, new TowerInfo(4005, 1, 1));
        m_dic_have_tower_group_by_tier.Add(4, new List<TowerInfo>()
        {
            new TowerInfo(4001, 1, 1),
            new TowerInfo(4002, 1, 1),
            new TowerInfo(4003, 1, 1),
            new TowerInfo(4004, 1, 1),
            new TowerInfo(4005, 1, 1)
        });

        // 5티어 기본 보유 타워
        m_dic_have_tower.Add(5001, new TowerInfo(5001, 1, 1));
        m_dic_have_tower.Add(5002, new TowerInfo(5002, 1, 1));
        m_dic_have_tower.Add(5003, new TowerInfo(5003, 1, 1));
        m_dic_have_tower.Add(5004, new TowerInfo(5004, 1, 1));
        m_dic_have_tower.Add(5005, new TowerInfo(5005, 1, 1));
        m_dic_have_tower_group_by_tier.Add(5, new List<TowerInfo>()
        {
            new TowerInfo(5001, 1, 1),
            new TowerInfo(5002, 1, 1),
            new TowerInfo(5003, 1, 1),
            new TowerInfo(5004, 1, 1),
            new TowerInfo(5005, 1, 1)
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
