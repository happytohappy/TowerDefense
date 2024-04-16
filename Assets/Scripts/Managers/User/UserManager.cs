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

    [Serializable]
    public class CUserData
    {
        public int Gold = 0;
        public int Ruby = 0;
        public int Diamond = 0;
        public int LastClearStage = 0;
        public int LastClearWave = 0;
        public float GameSpeed  = 1.0f;
        public Dictionary<int, HeroInfo> DicHaveHero = new Dictionary<int, HeroInfo>();
        public Dictionary<int, List<HeroInfo>> DicHaveHeroGroupByTier = new Dictionary<int, List<HeroInfo>>();
        public Dictionary<int, int> DicInventoryItem = new Dictionary<int, int>();
    }

    public CUserData UserData { get; set; } = new();

    public void Init()
    {
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey(LocalKey.UserData.ToString()) == true)
        {
            UserData = LoadLocalData<CUserData>(LocalKey.UserData);
        }
        else
        {
            // 1Ƽ�� �⺻ ���� Ÿ��
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

            // 2Ƽ�� �⺻ ���� Ÿ��                                      
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

            // 3Ƽ�� �⺻ ���� Ÿ��
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

            // 4Ƽ�� �⺻ ���� Ÿ��
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

            // 5Ƽ�� �⺻ ���� Ÿ��
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

    public void UpsertHero(int in_kind)
    {
        // �ִ°��� ������ �����ϴ°ǰ�?
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

    // ���� ����
    private void OnApplicationQuit()
    {
        SaveLocalData<CUserData>(UserData, LocalKey.UserData);
    }

    // ��׶���
    public static void SaveLocalData<T>(T SaveData, LocalKey Key)
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

     //if (PlayerPrefs.HasKey(LocalKey.Option.ToString()) == true)
     //   {
     //       Account.option = Utility.LoadLocalData<Option>(LocalKey.Option);
     //       Utility.SaveLocalData<Option>(Account.option, LocalKey.Option);
     //   }
}
