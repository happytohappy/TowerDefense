using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UserManager : MonoBehaviour
{
    [Serializable]
    public class HeroInfo
    {
        public int m_kind;      // 타워 카인드
        public int m_level;     // 타워 레벨
        public int m_grade;     // 타워 등급

        public HeroInfo(int in_kind, int in_level, int in_grade)
        {
            m_kind = in_kind;
            m_level = in_level;
            m_grade = in_grade;
        }
    }

    [Serializable]
    public class CUserData
    {
        public int LastClearStage = 0;
        public int LastClearWave = 0;
        public float GameSpeed  = 1.0f;
        public Dictionary<int, HeroInfo> DicHaveHero = new Dictionary<int, HeroInfo>();
        public Dictionary<int, List<HeroInfo>> DicHaveHeroGroupByTier = new Dictionary<int, List<HeroInfo>>();
        public Dictionary<int, int> DicInventoryItem = new Dictionary<int, int>();
        public Dictionary<int, int> DicTreasure = new Dictionary<int, int>();
    }

    public CUserData UserData { get; set; } = new CUserData();
    public int SelectStage { get; set; } = 0;

    private bool m_paused;

    public void Init()
    {
        if (PlayerPrefs.HasKey(LocalKey.UserData.ToString()) == true)
        {
            UserData = LoadLocalData<CUserData>(LocalKey.UserData);
        }
        else
        {
            UserData = new CUserData();

            // 1티어 기본 보유 타워
            UserData.DicHaveHero.Add(1001, new HeroInfo(1001, 1, 1));
            UserData.DicHaveHero.Add(1002, new HeroInfo(1002, 1, 1));
            UserData.DicHaveHero.Add(1003, new HeroInfo(1003, 1, 1));
            UserData.DicHaveHero.Add(1004, new HeroInfo(1004, 1, 1));
            UserData.DicHaveHero.Add(1005, new HeroInfo(1005, 1, 1));
            UserData.DicHaveHeroGroupByTier.Add(1, new List<HeroInfo>()
            {
                new HeroInfo(1001, 1, 1),
                new HeroInfo(1002, 1, 1),
                new HeroInfo(1003, 1, 1),
                new HeroInfo(1004, 1, 1),
                new HeroInfo(1005, 1, 1)
            });

            // 2티어 기본 보유 타워                                      
            UserData.DicHaveHero.Add(2001, new HeroInfo(2001, 1, 1));
            UserData.DicHaveHero.Add(2002, new HeroInfo(2002, 1, 1));
            UserData.DicHaveHero.Add(2003, new HeroInfo(2003, 1, 1));
            UserData.DicHaveHero.Add(2004, new HeroInfo(2004, 1, 1));
            UserData.DicHaveHero.Add(2005, new HeroInfo(2005, 1, 1));
            UserData.DicHaveHeroGroupByTier.Add(2, new List<HeroInfo>()
            {
                new HeroInfo(2001, 1, 1),
                new HeroInfo(2002, 1, 1),
                new HeroInfo(2003, 1, 1),
                new HeroInfo(2004, 1, 1),
                new HeroInfo(2005, 1, 1)
            });

            // 3티어 기본 보유 타워
            UserData.DicHaveHero.Add(3001, new HeroInfo(3001, 1, 1));
            UserData.DicHaveHero.Add(3002, new HeroInfo(3002, 1, 1));
            UserData.DicHaveHero.Add(3003, new HeroInfo(3003, 1, 1));
            UserData.DicHaveHero.Add(3004, new HeroInfo(3004, 1, 1));
            UserData.DicHaveHero.Add(3005, new HeroInfo(3005, 1, 1));
            UserData.DicHaveHeroGroupByTier.Add(3, new List<HeroInfo>()
            {
                new HeroInfo(3001, 1, 1),
                new HeroInfo(3002, 1, 1),
                new HeroInfo(3003, 1, 1),
                new HeroInfo(3004, 1, 1),
                new HeroInfo(3005, 1, 1)
            });

            // 4티어 기본 보유 타워
            UserData.DicHaveHero.Add(4001, new HeroInfo(4001, 1, 1));
            UserData.DicHaveHero.Add(4002, new HeroInfo(4002, 1, 1));
            UserData.DicHaveHero.Add(4003, new HeroInfo(4003, 1, 1));
            UserData.DicHaveHero.Add(4004, new HeroInfo(4004, 1, 1));
            UserData.DicHaveHero.Add(4005, new HeroInfo(4005, 1, 1));
            UserData.DicHaveHeroGroupByTier.Add(4, new List<HeroInfo>()
            {
                new HeroInfo(4001, 1, 1),
                new HeroInfo(4002, 1, 1),
                new HeroInfo(4003, 1, 1),
                new HeroInfo(4004, 1, 1),
                new HeroInfo(4005, 1, 1)
            });

            // 5티어 기본 보유 타워
            UserData.DicHaveHero.Add(5001, new HeroInfo(5001, 1, 1));
            UserData.DicHaveHero.Add(5002, new HeroInfo(5002, 1, 1));
            UserData.DicHaveHero.Add(5003, new HeroInfo(5003, 1, 1));
            UserData.DicHaveHero.Add(5004, new HeroInfo(5004, 1, 1));
            UserData.DicHaveHero.Add(5005, new HeroInfo(5005, 1, 1));
            UserData.DicHaveHeroGroupByTier.Add(5, new List<HeroInfo>()
            {
                new HeroInfo(5001, 1, 1),
                new HeroInfo(5002, 1, 1),
                new HeroInfo(5003, 1, 1),
                new HeroInfo(5004, 1, 1),
                new HeroInfo(5005, 1, 1)
            });
        }    
    }

    public HeroInfo GetUserHeroInfo(int in_kind)
    {
        if (UserData.DicHaveHero.ContainsKey(in_kind))
            return UserData.DicHaveHero[in_kind];
        else
            return null;
    }

    public List<HeroInfo> GetUserHeroInfoGroupByTier(int in_tier)
    {
        if (UserData.DicHaveHeroGroupByTier.ContainsKey(in_tier))
            return UserData.DicHaveHeroGroupByTier[in_tier];
        else
            return null;
    }

    public int GetInventoryItem(int in_kind)
    {
        if (UserData.DicInventoryItem.ContainsKey(in_kind))
            return UserData.DicInventoryItem[in_kind];
        else
            return 0;
    }

    public int GetTreasureInfo(int in_kind)
    {
        if (UserData.DicTreasure.ContainsKey(in_kind))
            return UserData.DicTreasure[in_kind];
        else
            return 0;
    }

    public void UpsertTreasure(int in_kind, int in_amount)
    {
        if (UserData.DicTreasure.ContainsKey(in_kind))
        {
            UpsertInventoryItem(in_kind, in_amount);
        }
        else
        {
            UserData.DicTreasure.Add(in_kind, 1);
        }
    }

    public void UpsertHero(int in_kind)
    {
        // 있는것은 레벨이 증가하는건가?
        if (UserData.DicHaveHero.ContainsKey(in_kind))
        {
            UpsertInventoryItem(in_kind, 1);
        }
        else
        {
            UserData.DicHaveHero.Add(in_kind, new HeroInfo(in_kind, 1, 1));
        }
    }

    public void UpsertInventoryItem(int in_kind, int in_amount)
    {
        if (UserData.DicInventoryItem.ContainsKey(in_kind))
        {
            UserData.DicInventoryItem[in_kind] += in_amount;
        }
        else
        {
            UserData.DicInventoryItem.Add(in_kind, in_amount);
        }
    }

    public void SetGameSpeedUP()
    {
        UserData.GameSpeed += 0.5f;
        if (UserData.GameSpeed > 2.0f)
            UserData.GameSpeed = 1.0f;
    }

    // 게임 비활성화
    private void OnApplicationFocus(bool pause)
    {
        if (pause)
        {
            m_paused = true;
        }
        else
        {
            if (m_paused)
            {
                m_paused = false;

                SaveLocalData<CUserData>(UserData, LocalKey.UserData);
            }
        }
    }

    // 게임 종료
    private void OnApplicationQuit()
    {
        SaveLocalData<CUserData>(UserData, LocalKey.UserData);

        //var data = GetLocalDataString<CUserData>(LocalKey.UserData);

        //Managers.BackEnd.InsertUserData(data);
    }

    public static void SaveLocalData<T>(T SaveData, LocalKey Key)
    {
        MemoryStream memoryStream = null;
        try
        {
            var binaryFormatter = new BinaryFormatter();
            memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, SaveData);
            PlayerPrefs.SetString(Key.ToString(), Convert.ToBase64String(memoryStream.GetBuffer()));
        }
        finally
        {
            memoryStream.Close();
        }
    }

    public static T LoadLocalData<T>(LocalKey key)
    {
        MemoryStream memoryStream = null;

        try
        {
            var data = PlayerPrefs.GetString(key.ToString());

            var binaryFormatter = new BinaryFormatter();
            memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // 가져온 데이터를 바이트 배열로 변환하고 사용하기 위해 다시 리스트로 캐스팅 해줍니다.
            return (T)binaryFormatter.Deserialize(memoryStream);
        }
        finally
        {
            memoryStream.Close();
        }
    }

    public static string GetLocalDataString<T>(LocalKey key)
    {
        var data = PlayerPrefs.GetString(key.ToString());

        return data;
    }
}