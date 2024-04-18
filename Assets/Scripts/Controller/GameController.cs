using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public partial class GameController : MonoBehaviour
{
    #region 싱글톤
    private static GameController Instance = null;
    public static GameController GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType(typeof(GameController)) as GameController;

            return Instance;
        }
    }
    #endregion

    public class RewardData
    {
        public int m_reward;
        public int m_amount;
        public string m_text;
    }

    [Serializable]
    public class Path
    {
        public List<Transform> m_path = new List<Transform>();
    }

    public class LandData
    {
        public int m_land_index;     // 땅 인덱스
        public bool m_build;         // 타워가 설치 되었는지
        public Transform m_trans;    // 땅 위치
        public Hero m_hero;          // 영웅

        public LandData(int in_land_index, bool in_build, Transform in_trans, Hero in_tower)
        {
            m_land_index = in_land_index;
            m_build = in_build;
            m_trans = in_trans;
            m_hero = in_tower;
        }
    }

    [SerializeField] private List<Path> m_pathes = new List<Path>();
    [SerializeField] private List<GameObject> m_build_map = new List<GameObject>();

    private bool m_first_hero_spawn = true;
    private bool m_spawn_finish = true;
    private bool m_next_wave = true;
    private int m_wave_index = 1;
    private int m_monster_spawn_count = 0;
    private int m_monster_kill_count = 0;
    private int m_monster_goal_count = 0;
    private bool m_sniffling = true;
    private Vector3 m_hero_position = new Vector3(0f, 0.55f, 0f);
    private Vector3 m_hero_rotation = new Vector3(0f, 180f, 0f);
    private int m_wave_count = 0;
    private List<StageRewardData> m_list_stage_reward = new List<StageRewardData>();

    // AD DATA
    public bool ADAutoFlag { get; set; }
    public Dictionary<EADAutoType, bool> ADAuto = new Dictionary<EADAutoType, bool>();
    public Dictionary<EADRewardType, bool> ADReward = new Dictionary<EADRewardType, bool>();

    // REWARD DATA
    public List<RewardData> GetRewardData { get; set; } = new List<RewardData>();

    // GAME DATA
    public List<LandData> LandInfo    { get; set; } = new List<LandData>();
    public List<Monster>  Monsters    { get; set; } = new List<Monster>();
    public Hero           SelectHero  { get; set; } = null;
    public LandData       EndLand     { get; set; } = null;
    public Hud_HeroInfo   HudHeroInfo { get; set; } = null;

    private UIWindowGame m_gui = null;
    public UIWindowGame GUI
    {
        get
        {
            if (m_gui != null)
                return m_gui;

            m_gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
            return m_gui;
        }
    }

    private int m_energy;
    public int Energy
    {
        get
        {
            return m_energy;
        }
        set
        {
            m_energy = value;

            // 자동 소환 시도
            if (ADAuto[EADAutoType.UNIT_ADD])
                GetInstance.HeroSpawn(ESpawnType.Energy);

            if (GUI != null)
                GUI.SetEnergy(m_energy);
        }
    }

    private int m_life;
    public int Life
    {
        get
        {
            return m_life;
        }
        set
        {
            m_life = value;

            if (GUI != null)
                GUI.SetLifeInfo(m_life);
        }
    }

    private void Awake()
    {
        m_raycaster = Managers.UICanvas.gameObject.GetComponent<GraphicRaycaster>();
        m_pointer_event_data = new PointerEventData(EventSystem.current);        
    }

    private void Start()
    {
        m_wave_count = Managers.Table.GetWaveCount(Managers.User.SelectStage);
        m_list_stage_reward = Managers.Table.GetStageReward(Managers.User.SelectStage);

        StageInit();
    }

    private void Update()
    {
        Update_Input();
    }

    public void StageInit()
    {
        Time.timeScale = Managers.User.UserData.GameSpeed;

        StopAllCoroutines();

        AllDestory();

        m_first_hero_spawn = true;
        m_energy = 100;
        m_wave_index = 1;
        m_next_wave = true;
        Life = CONST.STAGE_LIFE;
        ADAutoFlag = false;

        LandInfo.Clear();

        for (int i = 0; i < m_build_map.Count; i++)
            LandInfo.Add(new LandData(i, false, m_build_map[i].transform, null));

        for (EADRewardType i = EADRewardType.LIFE; i <= EADRewardType.DAMAGE; i++)
            ADReward.Add(i, true);

        for (EADAutoType i = EADAutoType.UNIT_ADD; i <= EADAutoType.NEXT_WAVE; i++)
            ADAuto.Add(i, false);
    }

    public void HeroMerge()
    {
        var nextTier = SelectHero.GetHeroData.m_info.m_tier + 1;
        var towerList = Managers.User.GetUserHeroInfoGroupByTier(nextTier);
        if (towerList == null || towerList.Count == 0)
            return;

        // 타워가 설치되어 있던 땅의 정보는 초기화
        var startLand = LandInfo.Find(x => x.m_hero == SelectHero);
        Managers.Resource.Destroy(startLand.m_hero.gameObject);
        Managers.Resource.Destroy(startLand.m_hero.HudHeroInfo.gameObject);
        startLand.m_hero = null;
        startLand.m_build = false;

        Managers.Resource.Destroy(EndLand.m_hero.gameObject);
        Managers.Resource.Destroy(EndLand.m_hero.HudHeroInfo.gameObject);
        EndLand.m_hero = null;
        EndLand.m_build = false;

        HeroSpawn(ESpawnType.Merge, nextTier, EndLand);

        GUI.OnCheckHeroSynergy();
    }

    public void HeroSwap()
    {
        var tempHero = SelectHero;
        var startLand = LandInfo.Find(x => x.m_hero == SelectHero);

        startLand.m_hero = EndLand.m_hero;
        startLand.m_hero.transform.SetParent(startLand.m_trans);
        startLand.m_hero.transform.localPosition = m_hero_position;

        EndLand.m_hero = SelectHero;
        EndLand.m_hero.transform.SetParent(EndLand.m_trans);
        EndLand.m_hero.transform.localPosition = m_hero_position;
    }

    public void HeroSell()
    {
        var startLand = LandInfo.Find(x => x.m_hero == SelectHero);
        Managers.Resource.Destroy(startLand.m_hero.gameObject);
        Managers.Resource.Destroy(startLand.m_hero.HudHeroInfo.gameObject);
        startLand.m_hero = null;
        startLand.m_build = false;

        switch (SelectHero.GetHeroData.m_info.m_tier)
        {
            case 1: Energy += CONST.STAGE_ENERGY_SELL_1; break;
            case 2: Energy += CONST.STAGE_ENERGY_SELL_2; break;
            case 3: Energy += CONST.STAGE_ENERGY_SELL_3; break;
            case 4: Energy += CONST.STAGE_ENERGY_SELL_4; break;
            case 5: Energy += CONST.STAGE_ENERGY_SELL_5; break;
            case 6: Energy += CONST.STAGE_ENERGY_SELL_6; break;
            case 7: Energy += CONST.STAGE_ENERGY_SELL_7; break;
            case 8: Energy += CONST.STAGE_ENERGY_SELL_8; break;
        }

        GUI.OnCheckHeroSynergy();
    }

    public void HeroMove()
    {
        // 타워가 설치되어 있던 땅의 정보는 초기화
        var startLand = LandInfo.Find(x => x.m_hero == SelectHero);
        startLand.m_hero = null;
        startLand.m_build = false;

        SelectHero.transform.SetParent(EndLand.m_trans.transform);
        SelectHero.transform.localPosition = m_hero_position;
        EndLand.m_hero = SelectHero;
        EndLand.m_build = true;
    }

    public void HeroSpawn(ESpawnType in_type, int in_tier = 1, LandData in_land = null)
    {
        // 재화 선 체크
        switch (in_type)
        {
            case ESpawnType.Energy: 
                if (Energy < CONST.STAGE_ENERGY_BUY) 
                    return;
                break;
        }

        // 머지가 아닐 경우 빈 땅이 있는지 체크
        var emptyLand = LandInfo.FindAll(x => x.m_build == false).ToList();
        if (in_type != ESpawnType.Merge)
        {
            if (emptyLand.Count == 0)
                return;
        }

        // 해당 티어 유닛이 있는지 체크
        var heroList = Managers.User.GetUserHeroInfoGroupByTier(in_tier);
        if (heroList == null || heroList.Count == 0)
            return;

        // 여기 까지 왔다면 소환 시작
        var userHeroInfo = heroList[UnityEngine.Random.Range(0, heroList.Count)];
        var heroInfoData = Managers.Table.GetHeroInfoData(userHeroInfo.m_kind);

        var heroLevelData = Managers.Table.GetHeroLevelData(userHeroInfo.m_kind, userHeroInfo.m_level);
        var heroGradeData = Managers.Table.GetHeroGradeData(userHeroInfo.m_kind, userHeroInfo.m_grade);
        HeroData heroData = new HeroData(heroInfoData, heroGradeData, heroLevelData);

        var go = Managers.Resource.Instantiate(heroInfoData.m_path);
        var hero = go.GetComponent<Hero>();
        hero.GetHeroData = heroData;

        // 만약 지정된 위치가 있다면 해당 위치로 소환
        LandData landData = in_land != null ? in_land : emptyLand[UnityEngine.Random.Range(0, emptyLand.Count)];
        go.transform.SetParent(landData.m_trans.transform);
        go.transform.localPosition = m_hero_position;
        go.transform.eulerAngles = m_hero_rotation;
        landData.m_hero = hero;
        landData.m_build = true;

        // 비용 지불
        switch (in_type)
        {
            case ESpawnType.Energy:
                Energy -= CONST.STAGE_ENERGY_BUY;
                break;
        }

        // 첫 소환이라면 웨이브 시작 가능 상태로 변경
        if (m_first_hero_spawn)
        {
            m_first_hero_spawn = false;
            GUI.NextWaveActive();

            if (ADAuto[EADAutoType.NEXT_WAVE])
                GUI.OnClickWave();
        }

        if (ADAuto[EADAutoType.UNIT_MERGE])
            CheckAutoMerge();

        GUI.OnCheckHeroSynergy();
    }

    private void CheckAutoMerge()
    {
        Dictionary<int, int> HeroSet = new Dictionary<int, int>();

        var SpawnHeroList = LandInfo.FindAll(x => x.m_hero != null).ToList();
        foreach (var e in SpawnHeroList)
        {
            var heroInfo = e.m_hero.GetHeroData.m_info;

            // MAX TIER 는 머지를 할 수가 없다.
            var nextTier = heroInfo.m_tier + 1;
            var towerList = Managers.User.GetUserHeroInfoGroupByTier(nextTier);
            if (towerList == null || towerList.Count == 0)
                continue;
    
            if (HeroSet.ContainsKey(heroInfo.m_kind))
            {
                var firstLand = LandInfo[HeroSet[heroInfo.m_kind]];
                Managers.Resource.Destroy(firstLand.m_hero.gameObject);
                if (firstLand.m_hero.HudHeroInfo != null) 
                    Managers.Resource.Destroy(firstLand.m_hero.HudHeroInfo.gameObject);
                firstLand.m_hero = null;
                firstLand.m_build = false;

                var secondLand = LandInfo[e.m_land_index];
                Managers.Resource.Destroy(secondLand.m_hero.gameObject);
                if (secondLand.m_hero.HudHeroInfo != null) 
                    Managers.Resource.Destroy(secondLand.m_hero.HudHeroInfo.gameObject);
                secondLand.m_hero = null;
                secondLand.m_build = false;

                // 머지
                HeroSpawn(ESpawnType.Merge, heroInfo.m_tier + 1, LandInfo[e.m_land_index]);
                CheckAutoMerge();
                break;
            }
            else
            {
                HeroSet.Add(heroInfo.m_kind, e.m_land_index);
            }
        }
    }

    public Dictionary<EHeroType, int> GetHeroTypeCount()
    {
        Dictionary<EHeroType, int> result = new Dictionary<EHeroType, int>();

        var HeroLand = LandInfo.FindAll(x => x.m_build).ToList();
        foreach (var e in HeroLand)
        {
            var heroType = e.m_hero.GetHeroData.m_info.m_type;
            if (result.ContainsKey(heroType))
                result[heroType]++;
            else
                result.Add(heroType, 1);
        }

        return result;
    }

    public void AutoAllCheck()
    {
        // 유닛 소환 부터 체크
        if (ADAuto[EADAutoType.UNIT_ADD])
        {
            while (Energy > CONST.STAGE_ENERGY_BUY)
                GetInstance.HeroSpawn(ESpawnType.Energy);
        }

        // 유닛 합성 체크
        if (ADAuto[EADAutoType.UNIT_MERGE])
            CheckAutoMerge();

        // 웨이브 시작 체크
        if (ADAuto[EADAutoType.NEXT_WAVE] && m_first_hero_spawn == false)
            GUI.OnClickWave();
    }

    public void AllDestory()
    {
        for (int i = 0; i < LandInfo.Count; i++)
        {
            var landInfo = LandInfo[i];
            if (landInfo == null || landInfo.m_hero == null)
                continue;

            Managers.Resource.Destroy(landInfo.m_hero.gameObject);
            Managers.Resource.Destroy(landInfo.m_hero.HudHeroInfo.gameObject);
        }

        for (int i = 0; i < Monsters.Count; i++)
        {
            if (Monsters[i] == null || Monsters[i].gameObject == null)
                continue;

            Managers.Resource.Destroy(Monsters[i].HPBar.gameObject);
            Managers.Resource.Destroy(Monsters[i].gameObject);
        }

        LandInfo.Clear();
        Monsters.Clear();

        for (int i = 0; i < Managers.Widget.transform.childCount; i++)
        {
            var child = Managers.Widget.transform.GetChild(i);
            Managers.Resource.Destroy(child.gameObject);
        }

        GetRewardData.Clear();
        ADReward.Clear();
        ADAuto.Clear();
    }
}