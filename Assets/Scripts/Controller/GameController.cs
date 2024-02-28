using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    #region �̱���
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
        public int        m_land_index;     // �� �ε���
        public bool       m_build;          // Ÿ���� ��ġ �Ǿ�����
        public Transform  m_trans;          // �� ��ġ
        public Hero       m_hero;           // ����

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

    private bool m_wave          = true;
    private bool m_sniffling     = true;
    private int  m_monster_index = 1001;

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

                    // �Ϻ� UI Hide
                    var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
                    if (gui != null) gui.HideButton();

                    // Ÿ�� ���� ���� �����ֱ�
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
                        // ���� ���� �ִ� ��ġ��� ������ �� �ִ�.
                        if (EndLand.m_hero == SelectTower)
                        {
                            SelectTower.transform.position = EndLand.m_trans.position + new Vector3(0f, 1f, 0f);
                        }
                        else
                        {
                            // �ռ� ���� �����ߵ�
                            SelectTower.transform.position = EndLand.m_trans.position + new Vector3(0f, 1f, 0f);
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
            
            // Ÿ�� ��ġ ������ �ƴ� ������ ���콺�� ���� ���
            if (EndLand == null)
            {
                SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
                SelectTower.RangeEffect.Ex_SetActive(false);
                return;
            }

            // �������� ���� �̹� Ÿ���� ��ġ �Ǿ� �ִ� ���
            if (EndLand.m_build)
            {
                // ���� ���� �ִ� ��ġ�� ���� ���̱� ������ �ƹ��͵� ó���� �ʿ䰡 ����.
                if (EndLand.m_hero == SelectTower)
                {
                    SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
                    SelectTower.RangeEffect.Ex_SetActive(false);
                    return;
                }

                // �ռ��� ��� ���⼭ ó�� �ؾߵ�
                if (EndLand.m_hero.GetHeroData.m_info.m_kind == SelectTower.GetHeroData.m_info.m_kind)
                {
                    var nextTier = SelectTower.GetHeroData.m_info.m_tier + 1;
                    var towerList = Managers.User.GetUserHeroInfoGroupByTier(nextTier);
                    if (towerList == null || towerList.Count == 0)
                        return;

                    // Ÿ���� ��ġ�Ǿ� �ִ� ���� ������ �ʱ�ȭ
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

                SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
                SelectTower.RangeEffect.Ex_SetActive(false);

                SelectTower = null;
                EndLand = null;
                return;
            }

            {          
                // Ÿ���� ��ġ�Ǿ� �ִ� ���� ������ �ʱ�ȭ
                var startLand = LandInfo.Find(x => x.m_hero == SelectTower);
                startLand.m_hero = null;
                startLand.m_build = false;

                SelectTower.transform.SetParent(EndLand.m_trans.transform);
                SelectTower.transform.localPosition = new Vector3(0f, 1f, 0f);
                EndLand.m_hero = SelectTower;
                EndLand.m_build = true;

                // Ÿ�� ���� ������
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
            Debug.LogError("�� ���� ���ٿ�.");
            return false;
        }
        else
        {
            var heroList = Managers.User.GetUserHeroInfoGroupByTier(in_tier);
            if (heroList == null || heroList.Count == 0)
            {
                Debug.LogError("���� Ÿ�� ���� ���� ���ٿ�.");
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
                go.transform.localPosition = new Vector3(0f, 1f, 0f);
                go.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                rand.m_hero = hero;
                rand.m_build = true;
            }
            else
            {
                var rand = emptyLand[UnityEngine.Random.Range(0, emptyLand.Count)];
                go.transform.SetParent(rand.m_trans.transform);
                go.transform.localPosition = new Vector3(0f, 1f, 0f);
                go.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                rand.m_hero = hero;
                rand.m_build = true;
            }

            return true;
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
        if (m_monster_index > 1002)
            m_monster_index = 1001;
                
        var monsterInfoData = Managers.Table.GetMonsterInfoData(m_monster_index);
        var monsterStatusData = Managers.Table.GetMonsterStatusData(monsterInfoData.m_kind, 1);

        for (int i = 0; i < 20; i++)
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
            }

            yield return new WaitForSeconds(0.5f);
        }

        m_wave = true;
        m_monster_index++;
    }
}