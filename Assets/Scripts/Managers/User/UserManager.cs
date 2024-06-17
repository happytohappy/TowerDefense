using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class UserManager : MonoBehaviour
{
    [Serializable]
    public class HeroInfo
    {
        public int m_kind;      // 영웅 카인드
        public int m_level;     // 영웅 레벨
        public int m_grade;     // 영웅 등급
        public long m_equip_id;

        public HeroInfo(int in_kind, int in_level, int in_grade)
        {
            m_kind = in_kind;
            m_level = in_level;
            m_grade = in_grade;
        }
    }

    [Serializable]
    public class MissionInfo
    {
        public int m_kind;
        public int m_sequence;

        public MissionInfo(int in_kind, int in_sequence)
        {
            m_kind = in_kind;
            m_sequence = in_sequence;
        }
    }

    [Serializable]
    public class TownInfo
    {
        public int m_town_kind;
        public int m_town_level;
        public long m_last_reward_time;

        public TownInfo(int in_town_kind, int in_town_level, long in_last_reward_time)
        {
            m_town_kind = in_town_kind;
            m_town_level = in_town_level;
            m_last_reward_time = in_last_reward_time;
        }
    }

    [Serializable]
    public class EquipInfo
    {
        public long m_unique_id;
        public int m_kind;
        public bool m_mount;
        public bool m_new;
        public EEquipGrade m_grade;
        public EEquipType m_type;

        public EquipInfo(long in_unique_id, int in_kind, bool in_mount, bool in_new)
        {
            var tableEquip = Managers.Table.GetEquipInfoData(in_kind);

            m_unique_id = in_unique_id;
            m_kind = in_kind;
            m_mount = in_mount;
            m_new = in_new;
            m_grade = tableEquip.m_equip_grade;
            m_type = tableEquip.m_equip_type;
        }
    }

    [Serializable]
    public class RecruitInfo
    {
        public long m_last_reward_time;
        public int m_reward_max_count;
        public int m_reward_count;

        public RecruitInfo(long in_last_reward_time, int in_reward_max_count, int in_reward_count)
        {
            m_last_reward_time = in_last_reward_time;
            m_reward_max_count = in_reward_max_count;
            m_reward_count = in_reward_count;
        }
    }

    [Serializable]
    public class CUserData
    {
        public int LastClearStage = 0;                                                                          // 클리어한 마지막 스테이지
        public int LastClearWave = 0;                                                                           // 클리어한 마지막 웨이브
        public float GameSpeed = 1.0f;                                                                          // 게임 스피드
        public float SFXSoundVolum = 1.0f;                                                                      // 효과음 볼륨
        public float BGMSoundVolum = 1.0f;                                                                      // 배경음 볼륨
        public bool DailyRewardPremium = false;                                                                 // 일일 보상 패스 구매 유무           
        public bool DailyAllReward = false;                                                                     // 일주일 보상을 모두 받았는지
        public int DailyRewardKIND = 1;                                                                         // 며칠 보상을 받을 차례인지
        public DateTime DailyRewardDateTime;                                                                    // 마지막 보상을 받은 날짜
        public Dictionary<int, HeroInfo> HaveHero = new Dictionary<int, HeroInfo>();                            // 보유 영웅
        public Dictionary<int, List<HeroInfo>> HaveHeroGroupByTier = new Dictionary<int, List<HeroInfo>>();     // 보유 영웅 티어별 소팅
        public Dictionary<int, int> InventoryItem = new Dictionary<int, int>();                                 // 보유 아이템
        public Dictionary<int, int> Treasure = new Dictionary<int, int>();                                      // 보유 보물
        public List<MissionInfo> Mission = new List<MissionInfo>();                                             // 보유 미션
        public Dictionary<ETownType, TownInfo> Town = new Dictionary<ETownType, TownInfo>();                    // 타운
        public Dictionary<long, EquipInfo> Equip = new Dictionary<long, EquipInfo>();                           // 장비
        public Dictionary<ERecruitType, RecruitInfo> Recruit = new Dictionary<ERecruitType, RecruitInfo>();     // 가챠
    }

    public CUserData UserData { get; set; } = new CUserData();
    public int SelectStage { get; set; } = 0;

    private bool Paused;

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
            UserData.HaveHero.Add(1001, new HeroInfo(1001, 1, 1));
            UserData.HaveHero.Add(1002, new HeroInfo(1002, 1, 1));
            UserData.HaveHero.Add(1003, new HeroInfo(1003, 1, 1));
            UserData.HaveHero.Add(1004, new HeroInfo(1004, 1, 1));
            UserData.HaveHero.Add(1005, new HeroInfo(1005, 1, 1));
            UserData.HaveHeroGroupByTier.Add(1, new List<HeroInfo>()
            {
                new HeroInfo(1001, 1, 1),
                new HeroInfo(1002, 1, 1),
                new HeroInfo(1003, 1, 1),
                new HeroInfo(1004, 1, 1),
                new HeroInfo(1005, 1, 1)
            });

            // 2티어 기본 보유 타워                                      
            UserData.HaveHero.Add(2001, new HeroInfo(2001, 1, 1));
            UserData.HaveHero.Add(2002, new HeroInfo(2002, 1, 1));
            UserData.HaveHero.Add(2003, new HeroInfo(2003, 1, 1));
            UserData.HaveHero.Add(2004, new HeroInfo(2004, 1, 1));
            UserData.HaveHero.Add(2005, new HeroInfo(2005, 1, 1));
            UserData.HaveHeroGroupByTier.Add(2, new List<HeroInfo>()
            {
                new HeroInfo(2001, 1, 1),
                new HeroInfo(2002, 1, 1),
                new HeroInfo(2003, 1, 1),
                new HeroInfo(2004, 1, 1),
                new HeroInfo(2005, 1, 1)
            });

            // 3티어 기본 보유 타워
            UserData.HaveHero.Add(3001, new HeroInfo(3001, 1, 1));
            UserData.HaveHero.Add(3002, new HeroInfo(3002, 1, 1));
            UserData.HaveHero.Add(3003, new HeroInfo(3003, 1, 1));
            UserData.HaveHero.Add(3004, new HeroInfo(3004, 1, 1));
            UserData.HaveHero.Add(3005, new HeroInfo(3005, 1, 1));
            UserData.HaveHeroGroupByTier.Add(3, new List<HeroInfo>()
            {
                new HeroInfo(3001, 1, 1),
                new HeroInfo(3002, 1, 1),
                new HeroInfo(3003, 1, 1),
                new HeroInfo(3004, 1, 1),
                new HeroInfo(3005, 1, 1)
            });

            // 4티어 기본 보유 타워
            UserData.HaveHero.Add(4001, new HeroInfo(4001, 1, 1));
            UserData.HaveHero.Add(4002, new HeroInfo(4002, 1, 1));
            UserData.HaveHero.Add(4003, new HeroInfo(4003, 1, 1));
            UserData.HaveHero.Add(4004, new HeroInfo(4004, 1, 1));
            UserData.HaveHero.Add(4005, new HeroInfo(4005, 1, 1));
            UserData.HaveHeroGroupByTier.Add(4, new List<HeroInfo>()
            {
                new HeroInfo(4001, 1, 1),
                new HeroInfo(4002, 1, 1),
                new HeroInfo(4003, 1, 1),
                new HeroInfo(4004, 1, 1),
                new HeroInfo(4005, 1, 1)
            });

            // 5티어 기본 보유 타워
            UserData.HaveHero.Add(5001, new HeroInfo(5001, 1, 1));
            UserData.HaveHero.Add(5002, new HeroInfo(5002, 1, 1));
            UserData.HaveHero.Add(5003, new HeroInfo(5003, 1, 1));
            UserData.HaveHero.Add(5004, new HeroInfo(5004, 1, 1));
            UserData.HaveHero.Add(5005, new HeroInfo(5005, 1, 1));
            UserData.HaveHeroGroupByTier.Add(5, new List<HeroInfo>()
            {
                new HeroInfo(5001, 1, 1),
                new HeroInfo(5002, 1, 1),
                new HeroInfo(5003, 1, 1),
                new HeroInfo(5004, 1, 1),
                new HeroInfo(5005, 1, 1)
            });

            // 기본 미션(업적) 넣어주기
            UserData.Mission.Add(new MissionInfo(1, 1));
            UserData.Mission.Add(new MissionInfo(2, 1));
            UserData.Mission.Add(new MissionInfo(3, 1));
            UserData.Mission.Add(new MissionInfo(4, 1));
            UserData.Mission.Add(new MissionInfo(5, 1));
            UserData.Mission.Add(new MissionInfo(6, 1));
            UserData.Mission.Add(new MissionInfo(7, 1));
            UserData.Mission.Add(new MissionInfo(8, 1));

            // 기본 건물 레벨 넣어주기
            UserData.Town.Add(ETownType.Gold, new TownInfo(1, 1, Managers.BackEnd.ServerTimeGetUTCTimeStamp()));
            UserData.Town.Add(ETownType.Ruby, new TownInfo(10, 1, Managers.BackEnd.ServerTimeGetUTCTimeStamp()));
            UserData.Town.Add(ETownType.Dia, new TownInfo(20, 1, Managers.BackEnd.ServerTimeGetUTCTimeStamp()));
            UserData.Town.Add(ETownType.Unit, new TownInfo(30, 1, Managers.BackEnd.ServerTimeGetUTCTimeStamp()));
            UserData.Town.Add(ETownType.Equip, new TownInfo(40, 1, Managers.BackEnd.ServerTimeGetUTCTimeStamp()));

            // 뽑기 정보
            UserData.Recruit.Add(ERecruitType.Normal, new RecruitInfo(Managers.BackEnd.ServerTimeGetUTCTimeStamp(), CONST.AD_NORMAL_COUNT, 0));
            UserData.Recruit.Add(ERecruitType.Premium, new RecruitInfo(Managers.BackEnd.ServerTimeGetUTCTimeStamp(), CONST.AD_PREMIUM_COUNT, 0));

            // 장비 넣어주기
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100000);
            //InsertEquip(100008);
            //InsertEquip(100100);
            //InsertEquip(100100);
            //InsertEquip(100100);

            // 일일보상 넣어주기
            InitDailyReward();
        }

        if (UserData.DailyAllReward)
        {
            if (Util.TodayDailyReward() == false)
            {
                InitDailyReward();
            }
        }
    }

    public void InitDailyReward()
    {
        var serverDate = Managers.BackEnd.ServerDateTime();
        UserData.DailyAllReward = false;
        UserData.DailyRewardPremium = false;
        UserData.DailyRewardKIND = 1;
        UserData.DailyRewardDateTime = serverDate.AddDays(-1);
    }

    public long SetUniqueKey()
    {
        var strTime = Util.UnixTimeNow().ToString();
        var strRan = UnityEngine.Random.Range(10000, 99999).ToString();
        var key = long.Parse(strTime + strRan);

        return key;
    }

    public void InsertEquip(int m_kind)
    {
        var key = SetUniqueKey();
        if (UserData.Equip.ContainsKey(key))
        {
            InsertEquip(m_kind);
            return;
        }

        var equip = Managers.Table.GetEquipInfoData(m_kind);
        UserData.Equip.Add(key, new EquipInfo(key, m_kind, false, true));
    }

    public void RemoveEquip(long in_unique)
    {
        UserData.Equip.Remove(in_unique);
    }

    public HeroInfo GetEquipMountHero(long in_unique)
    {
        return UserData.HaveHero.Values.ToList().Find(x => x.m_equip_id == in_unique);
    }

    public HeroInfo GetUserHeroInfo(int in_kind)
    {
        if (UserData.HaveHero.ContainsKey(in_kind))
            return UserData.HaveHero[in_kind];
        else
            return null;
    }

    public List<HeroInfo> GetUserHeroInfoGroupByTier(int in_tier)
    {
        if (UserData.HaveHeroGroupByTier.ContainsKey(in_tier))
            return UserData.HaveHeroGroupByTier[in_tier];
        else
            return null;
    }

    public int GetInventoryItem(int in_kind)
    {
        if (UserData.InventoryItem.ContainsKey(in_kind))
            return UserData.InventoryItem[in_kind];
        else
            return 0;
    }

    public int GetTreasureInfo(int in_kind)
    {
        if (UserData.Treasure.ContainsKey(in_kind))
            return UserData.Treasure[in_kind];
        else
            return 0;
    }

    public void UpsertTreasure(int in_kind, int in_amount)
    {
        if (UserData.Treasure.ContainsKey(in_kind))
        {
            UpsertInventoryItem(in_kind, in_amount);
        }
        else
        {
            UserData.Treasure.Add(in_kind, 1);
        }
    }

    public void UpsertHero(int in_kind)
    {
        // 있는것은 레벨이 증가하는건가?
        if (UserData.HaveHero.ContainsKey(in_kind))
        {
            UpsertInventoryItem(in_kind, 1);
        }
        else
        {
            UserData.HaveHero.Add(in_kind, new HeroInfo(in_kind, 1, 1));
        }
    }

    public void UpsertInventoryItem(int in_kind, int in_amount)
    {
        if (UserData.InventoryItem.ContainsKey(in_kind))
        {
            UserData.InventoryItem[in_kind] += in_amount;
        }
        else
        {
            UserData.InventoryItem.Add(in_kind, in_amount);
        }

        // 재화라면 업데이트
        if (in_kind >= (int)EGoods.Gold && in_kind <= (int)EGoods.Diamond)
            Managers.Observer.UpdateObserverGoods((EGoods)in_kind);
    }

    public List<MissionInfo> GetMission()
    {
        return UserData.Mission;
    }

    public List<EquipInfo> GetEquipList()
    {
        return UserData.Equip.Values.ToList();
    }

    public List<EquipInfo> GetEquipList(int in_kind)
    {
        return UserData.Equip.Values.ToList().FindAll(x => x.m_kind == in_kind);
    }

    public List<EquipInfo> GetEquipList(EEquipType in_equip_type)
    {
        return UserData.Equip.Values.ToList().FindAll(x => x.m_type == in_equip_type);
    }

    public EquipInfo GetEquip(long in_unique)
    {
        if (UserData.Equip.ContainsKey(in_unique))
            return UserData.Equip[in_unique];
        else
            return null;
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
            Paused = true;
        }
        else
        {
            if (Paused)
            {
                Paused = false;

                SaveLocalData<CUserData>(UserData, LocalKey.UserData);
            }
        }
    }

    // 게임 종료
    private void OnApplicationQuit()
    {
        //SaveLocalData<CUserData>(UserData, LocalKey.UserData);

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