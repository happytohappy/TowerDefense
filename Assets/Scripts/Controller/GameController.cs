using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
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

    [Serializable]
    public class Path
    {
        public List<Transform> m_path = new List<Transform>();
    }

    public class LandData
    {
        public int        m_land_index;     // 땅 인덱스
        public bool       m_build;          // 타워가 설치 되었는지
        public Transform  m_trans;          // 땅 위치
        public Hero       m_hero;           // 영웅

        public LandData(int in_land_index, bool in_build, Transform in_trans, Hero in_tower)
        {
            m_land_index = in_land_index;
            m_build      = in_build;
            m_trans      = in_trans;
            m_hero      = in_tower;
        }
    }

    private const string MONSTER_PATH = "Monster";
    private const string MONSTER_NAME = "Monster_";

    [SerializeField] private List<Path>       m_pathes    = new List<Path>();
    [SerializeField] private List<GameObject> m_build_map = new List<GameObject>();

    private bool m_spawn_finish = true;
    private bool m_next_wave = true;
    private int  m_wave_index = 1;
    private int m_monster_spawn_count = 0;
    private int m_monster_kill_count = 0;
    private int m_monster_goal_count = 0;
    private bool m_sniffling        = true;
    private Vector3 m_hero_position = new Vector3(0f, 0.55f, 0f);
    private Vector3 m_hero_rotation = new Vector3(0f, 180f, 0f);

    public List<LandData> LandInfo    { get; set; } = new List<LandData>();
    public List<Monster>  Monsters    { get; set; } = new List<Monster>();
    public Hero           SelectTower { get; set; } = null;
    public LandData       EndLand     { get; set; } = null;



    //public int Gold
    //{
    //    get { return m_gold; }
    //    set 
    //    {
    //        m_gold = value;
    //        var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
    //        if (gui != null)
    //            gui.SetGold(Gold);
    //    }
    //}

    private void Awake()
    {
        //for (int z = 0; z < 30; z++)
        //{
        //    for (int i = 1; i <= 8; i++)
        //    {
        //        var heroList = Managers.User.GetUserHeroInfoGroupByTier(i);
        //        if (heroList == null)
        //            continue;

        //        foreach (var hero in heroList)
        //        {
        //            var heroInfoData = Managers.Table.GetHeroInfoData(hero.m_kind);
        //            var go = Managers.Resource.Instantiate(heroInfoData.m_path);
        //            if (go != null)
        //                Managers.Resource.Destroy(go);
        //        }
        //    }
        //}
    }

    private void Start()
    {
        //for (int z = 0; z < 30; z++)
        //{
        //    for (int i = 1; i <= 8; i++)
        //    {
        //        var heroList = Managers.User.GetUserHeroInfoGroupByTier(i);
        //        if (heroList == null)
        //            continue;

        //        foreach (var hero in heroList)
        //        {
        //            var heroInfoData = Managers.Table.GetHeroInfoData(hero.m_kind);
        //            var go = Managers.Resource.Instantiate(heroInfoData.m_path);
        //            if (go != null)
        //                Managers.Resource.Destroy(go);
        //        }
        //    }
        //}

        LandInfo.Clear();
        for (int i = 0; i < m_build_map.Count; i++)
            LandInfo.Add(new LandData(i, false, m_build_map[i].transform, null));

        //Gold = 50;
        //m_towers.Clear();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Hero"))
                {
                    SelectTower = hit.transform.gameObject.GetComponent<Hero>();
                    EndLand = null;

                    Managers.UnitCam.gameObject.SetActive(true);

                    // 일부 UI 제어
                    var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
                    if (gui != null) gui.ActiveButton(true);

                    // 타워 공격 범위 보여주기
                    SelectTower.RangeEffect.Ex_SetActive(true);
                }
                else
                {
                    SelectTower = null;
                    EndLand = null;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (SelectTower == null)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Build")))
            {
                EndLand = LandInfo.Find(x => x.m_trans == hit.transform);
                if (EndLand != null)
                {
                    if (EndLand.m_build)
                    {
                        // 내가 원래 있던 위치라면 움직일 수 있다.
                        if (EndLand.m_hero == SelectTower)
                        {
                            SelectTower.transform.position = EndLand.m_trans.position + m_hero_position;
                        }
                        else
                        {
                            // 합성 유무 따져야됨
                            SelectTower.transform.position = EndLand.m_trans.position + m_hero_position;
                        }
                    }
                    else
                    {
                        SelectTower.transform.position = EndLand.m_trans.position + m_hero_position;
                    }
                }
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
            {
                SelectTower.transform.position = hit.point;
                EndLand = null;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (SelectTower == null)
                return;

            Managers.UnitCam.gameObject.SetActive(false);

            // 일부 UI 제어
            var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
            if (gui != null) gui.ActiveButton(false);

            /*  그래픽 레이캐스트 적용 */
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("UI")))
            //{
            //    Managers.Resource.Destroy(SelectTower.gameObject);
            //    return;
            //}

            // 타워 설치 구역이 아닌 곳에서 마우스를 놨을 경우
            if (EndLand == null)
            {
                SelectTower.transform.localPosition = m_hero_position;
                SelectTower.RangeEffect.Ex_SetActive(false);
                return;
            }

            // 놓으려는 곳에 이미 타워가 설치 되어 있는 경우
            if (EndLand.m_build)
            {
                // 내가 원래 있던 위치에 놓은 것이기 때문에 아무것도 처리할 필요가 없다.
                if (EndLand.m_hero == SelectTower)
                {
                    SelectTower.transform.localPosition = m_hero_position;
                    SelectTower.RangeEffect.Ex_SetActive(false);
                    return;
                }

                // 합성일 경우 여기서 처리 해야됨
                if (EndLand.m_hero.GetHeroData.m_info.m_kind == SelectTower.GetHeroData.m_info.m_kind)
                {
                    var nextTier = SelectTower.GetHeroData.m_info.m_tier + 1;
                    var towerList = Managers.User.GetUserHeroInfoGroupByTier(nextTier);
                    if (towerList == null || towerList.Count == 0)
                        return;

                    // 타워가 설치되어 있던 땅의 정보는 초기화
                    var startLand = LandInfo.Find(x => x.m_hero == SelectTower);
                    Managers.Resource.Destroy(startLand.m_hero.gameObject);
                    Managers.Resource.Destroy(startLand.m_hero.HudHeroInfo.gameObject);
                    startLand.m_hero = null;
                    startLand.m_build = false;

                    Managers.Resource.Destroy(EndLand.m_hero.gameObject);
                    Managers.Resource.Destroy(EndLand.m_hero.HudHeroInfo.gameObject);
                    EndLand.m_hero = null;
                    EndLand.m_build = false;

                    TowerSpawn(nextTier, EndLand);
                    return;
                }

                SelectTower.transform.localPosition = m_hero_position;
                SelectTower.RangeEffect.Ex_SetActive(false);

                SelectTower = null;
                EndLand = null;
                return;
            }

            {          
                // 타워가 설치되어 있던 땅의 정보는 초기화
                var startLand = LandInfo.Find(x => x.m_hero == SelectTower);
                startLand.m_hero = null;
                startLand.m_build = false;

                SelectTower.transform.SetParent(EndLand.m_trans.transform);
                SelectTower.transform.localPosition = m_hero_position;
                EndLand.m_hero = SelectTower;
                EndLand.m_build = true;

                // 타워 범위 가리기
                SelectTower.RangeEffect.Ex_SetActive(false);

                SelectTower = null;
                EndLand = null;
            }
        }
    }

    public bool TowerSpawn(int in_tier = 1, LandData in_land = null)
    {
        var emptyLand = LandInfo.FindAll(x => x.m_build == false).ToList();
        if (emptyLand.Count == 0)
        {
            Debug.LogError("빈 땅이 없다요.");
            return false;
        }
        else
        {
            var heroList = Managers.User.GetUserHeroInfoGroupByTier(in_tier);
            if (heroList == null || heroList.Count == 0)
            {
                Debug.LogError("상위 타워 없음 땅이 없다요.");
                return false;
            }

            var userHeroInfo = heroList[UnityEngine.Random.Range(0, heroList.Count)];
            var heroInfoData = Managers.Table.GetHeroInfoData(userHeroInfo.m_kind);
            
            var heroLevelData = Managers.Table.GetHeroLevelData(userHeroInfo.m_kind, userHeroInfo.m_level);
            var heroGradeData = Managers.Table.GetHeroGradeData(userHeroInfo.m_kind, userHeroInfo.m_grade);
            HeroData heroData = new HeroData(heroInfoData, heroGradeData, heroLevelData);

            var go = Managers.Resource.Instantiate(heroInfoData.m_path);
            if (go == null)
                return false;

            var hero = go.GetComponent<Hero>();
            hero.GetHeroData = heroData;

            if (in_land != null)
            {
                var rand = in_land;
                go.transform.SetParent(rand.m_trans.transform);
                go.transform.localPosition = m_hero_position;
                go.transform.eulerAngles = m_hero_rotation;
                rand.m_hero = hero;
                rand.m_build = true;
            }
            else
            {
                var rand = emptyLand[UnityEngine.Random.Range(0, emptyLand.Count)];
                go.transform.SetParent(rand.m_trans.transform);
                go.transform.localPosition = m_hero_position;
                go.transform.eulerAngles = m_hero_rotation;
                rand.m_hero = hero;
                rand.m_build = true;
            }

            return true;
        }
    }

    public void MonsterSpawn()
    {
        if (m_next_wave == false)
            return;

        m_next_wave = false;
        m_spawn_finish = false;
        m_monster_spawn_count = 0;
        m_monster_kill_count = 0;
        m_monster_goal_count = 0;
        StartCoroutine(CoMonsterSpawn());
    }

    private IEnumerator CoMonsterSpawn()
    {
        var waveDatas = Managers.Table.GetStageWaveDataByWave(1, m_wave_index);

        for (int i = 0; i < waveDatas.Count; i++)
        {
            var waveData = waveDatas[i];
            if (waveData == null)
                continue;

            var monsterInfoData = Managers.Table.GetMonsterInfoData(waveData.m_monster_kind);
            var monsterStatusData = Managers.Table.GetMonsterStatusData(waveData.m_monster_kind, waveData.m_monster_level);

            if (monsterInfoData == null || monsterStatusData == null)
                continue;

            m_sniffling = true;
            for (int j = 0; j < waveData.m_monster_spawn_count; j++)
            {
                var go = Managers.Resource.Instantiate(monsterInfoData.m_path);
                if (go != null)
                {
                    var lineInex = m_sniffling ? 0 : 1;
                    var monster = go.GetComponent<Monster>();

                    monster.Path.AddRange(m_pathes[lineInex].m_path);
                    monster.GetMonsterInfoData = monsterInfoData;
                    monster.GetMonsterStatusData = monsterStatusData;
                    go.transform.position = m_pathes[lineInex].m_path[0].position;
                    Monsters.Add(monster);

                    m_sniffling = !m_sniffling;

                    m_monster_spawn_count++;
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        m_spawn_finish = true;
    }

    public void MonsterKill()
    {
        m_monster_kill_count++;
        if (m_spawn_finish == false)
            return;

        if (m_monster_spawn_count != m_monster_kill_count + m_monster_goal_count)
            return;

        var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
        if (gui != null) gui.NextWaveActive();
    }

    public void MonsterGoal()
    {
        m_monster_goal_count++;
        if (m_spawn_finish == false)
            return;

        if (m_monster_spawn_count != m_monster_kill_count + m_monster_goal_count)
            return;

        var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
        if (gui != null) gui.NextWaveActive();
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

            Managers.Resource.Destroy(Monsters[i].gameObject);
        }
    }
}