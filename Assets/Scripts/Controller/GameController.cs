using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public Tower      m_tower;          // 타워

        public LandData(int in_land_index, bool in_build, Transform in_trans, Tower in_tower)
        {
            m_land_index = in_land_index;
            m_build      = in_build;
            m_trans      = in_trans;
            m_tower      = in_tower;
        }
    }

    private const string MONSTER_PATH = "Monster";
    private const string MONSTER_NAME = "Monster_";
    private const string TOWER_PATH   = "Tower";
    private const string TOWER_NAME   = "Tower_";

    [SerializeField] private List<Path>       m_pathes    = new List<Path>();
    [SerializeField] private List<GameObject> m_build_map = new List<GameObject>();

    private bool m_wave          = true;
    private bool m_sniffling     = true;
    private int  m_monster_index = 1;

    public List<LandData> LandInfo    { get; set; } = new List<LandData>();
    public List<Monster>  Monsters    { get; set; } = new List<Monster>();
    public Tower          SelectTower { get; set; } = null;
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

    private void Start()
    {
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
                if (hit.transform.gameObject.CompareTag("Tower"))
                {
                    SelectTower = hit.transform.gameObject.GetComponent<Tower>();
                    EndLand = null;

                    // 일부 UI Hide
                    var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
                    if (gui != null) gui.HideButton();

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
                        if (EndLand.m_tower == SelectTower)
                        {
                            SelectTower.transform.position = EndLand.m_trans.position + new Vector3(0f, 1f, 0f);
                        }
                        else
                        {
                            // 합성 유무 따져야됨
                        }
                    }
                    else
                    {
                        SelectTower.transform.position = EndLand.m_trans.position + new Vector3(0f, 1f, 0f);
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
            
            // 타워 설치 구역이 아닌 곳에서 마우스를 놨을 경우
            if (EndLand == null)
            {
                SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
                SelectTower.RangeEffect.Ex_SetActive(false);
                return;
            }

            // 놓으려는 곳에 이미 타워가 설치 되어 있는 경우
            if (EndLand.m_build)
            {
                // 내가 원래 있던 위치에 놓은 것이기 때문에 아무것도 처리할 필요가 없다.
                if (EndLand.m_tower == SelectTower)
                {
                    SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
                    SelectTower.RangeEffect.Ex_SetActive(false);
                    return;
                }

                // 합성일 경우 여기서 처리 해야됨
                if (EndLand.m_tower.GetHeroData.m_info.m_kind == SelectTower.GetHeroData.m_info.m_kind)
                {
                    //Debug.LogError("합성 코드 작성 위치");
                    // 합성
                }

                SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
                SelectTower.RangeEffect.Ex_SetActive(false);

                SelectTower = null;
                EndLand = null;
                return;
            }

            // 타워가 설치되어 있던 땅의 정보는 초기화
            var startLand = LandInfo.Find(x => x.m_tower == SelectTower);
            startLand.m_tower = null;
            startLand.m_build = false;

            SelectTower.transform.SetParent(EndLand.m_trans.transform);
            SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
            EndLand.m_tower = SelectTower;
            EndLand.m_build = true;

            // 타워 범위 가리기
            SelectTower.RangeEffect.Ex_SetActive(false);

            SelectTower = null;
            EndLand = null;
        }
    }

    public void TowerSpawn()
    {
        var emptyLand = LandInfo.FindAll(x => x.m_build == false).ToList();
        if (emptyLand.Count == 0)
        {
            Debug.LogError("빈 땅이 없다요.");
        }
        else
        {
            //var ranTowerKind = UnityEngine.Random.Range(1, 3);
            var ranTowerKind = 1;
            var heroInfoData = Managers.Table.GetHeroInfoData(ranTowerKind);

            var userTowerInfo = Managers.User.GetUserTowerInfo(ranTowerKind);

            var heroLevelData = Managers.Table.GetHeroLevelData(ranTowerKind, userTowerInfo.m_level);
            var heroGradeData = Managers.Table.GetHeroGradeData(ranTowerKind, userTowerInfo.m_grade);
            HeroData heroData = new HeroData(heroInfoData, heroGradeData, heroLevelData);

            var go = Managers.Resource.Instantiate(heroInfoData.m_path);
            if (go == null)
                return;

            var tower = go.GetComponent<Tower>();
            tower.GetHeroData = heroData;

            var rand = emptyLand[UnityEngine.Random.Range(0, emptyLand.Count)];
            go.transform.SetParent(rand.m_trans.transform);
            go.transform.localPosition = new Vector3(0f, 1f, 0f);
            rand.m_tower = tower;
            rand.m_build = true;
        }
    }

    public void MonsterSpawn()
    {
        if (m_wave == false)
            return;

        m_wave = false;
        StartCoroutine(CoMonsterSpawn());
    }

    private IEnumerator CoMonsterSpawn()
    {
        if (m_monster_index > 2)
            m_monster_index = 1;
                
        var monsterData = Managers.Table.GetMonsterData(m_monster_index);

        for (int i = 0; i < 20; i++)
        {
            var go = Managers.Resource.Instantiate(monsterData.m_path);
            if (go != null)
            {
                var lineInex = m_sniffling ? 0 : 1;
                var monster = go.GetComponent<Monster>();
                    
                monster.Path.AddRange(m_pathes[lineInex].m_path);
                monster.GetMonsterData = monsterData;
                go.transform.position = m_pathes[lineInex].m_path[0].position;
                Monsters.Add(monster);

                m_sniffling = !m_sniffling;
            }

            yield return new WaitForSeconds(0.5f);
        }

        m_wave = true;
        m_monster_index++;
    }
}