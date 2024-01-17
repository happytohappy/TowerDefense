using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    #region ½Ì±ÛÅæ
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
        public int        m_land_index;     // ¶¥ ÀÎµ¦½º
        public bool       m_build;          // Å¸¿ö°¡ ¼³Ä¡ µÇ¾ú´ÂÁö
        public Transform  m_trans;          // ¶¥ À§Ä¡
        public GameObject m_tower_object;   // Å¸¿ö ¿ÀºêÁ§Æ®

        public LandData(int in_land_index, bool in_build, Transform in_trans, GameObject in_tower_object)
        {
            m_land_index   = in_land_index;
            m_build        = in_build;
            m_trans        = in_trans;
            m_tower_object = in_tower_object;
        }
    }

    private const string MONSTER_PATH = "Monster";
    private const string MONSTER_NAME = "Monster_";
    private const string TOWER_PATH = "Tower";
    private const string TOWER_NAME = "Tower_";


    [SerializeField] private List<Path>       m_pathes    = new List<Path>();
    [SerializeField] private List<GameObject> m_build_map = new List<GameObject>();

    public Dictionary<int, LandData> LandInfo { get; set; } = new Dictionary<int, LandData>();

    //public Transform TowerRoot => m_tower_root; 
    //public List<Monster> Monsters { get; set; } = new List<Monster>();
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
            LandInfo.Add(i, new LandData(i, false, m_build_map[i].transform, null));

        //Gold = 50;
        //m_towers.Clear();

        //StartCoroutine(CoSpawn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var emptyLand = LandInfo.Where(x => x.Value.m_build == false).ToList();
            if (emptyLand.Count == 0)
            {
                Debug.LogError("ºó ¶¥ÀÌ ¾ø´Ù¿ä.");
            }
            else
            {
                var go = Managers.Resource.Instantiate($"{TOWER_PATH}/{TOWER_NAME}1");
                if (go == null)
                    return;

                var rand = emptyLand[UnityEngine.Random.Range(0, emptyLand.Count)];
                go.transform.SetParent(rand.Value.m_trans.transform);
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
                rand.Value.m_tower_object = go;
                rand.Value.m_build = true;
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
        //        if (gui == null)
        //            return;

        //        if (hit.transform.gameObject.CompareTag("Build"))
        //        {
        //            m_select = hit.transform.gameObject;

        //            ÀÌ¹Ì ¼³Ä¡µÈ ¶¥
        //            if (m_towers.Contains(hit.transform.gameObject))
        //            {
        //                gui.ShowFixed();
        //            }
        //            ½Å±Ô ¶¥
        //            else
        //            {
        //                gui.ShowCreate();
        //            }
        //        }
        //        else
        //        {
        //            gui.HideButton();
        //        }
        //    }
        //}
    }

    //private IEnumerator CoSpawn()
    //{
    //    yield return new WaitForSeconds(5.0f);

    //    for (int i = 0; i < 10; i++)
    //    {
    //        var go = Managers.Resource.Instantiate($"{MONSTER_PATH}/{MONSTER_NAME}1");
    //        if (go != null)
    //        {
    //            go.transform.position = m_spawn_pos.position;

    //            var sc = go.GetComponent<Monster>();
    //            sc.SetDestination(m_goal_pos.position);

    //            Monsters.Add(sc);
    //        }

    //        yield return new WaitForSeconds(0.5f);
    //    }

    //    yield return new WaitForSeconds(4.5f);
    //    for (int i = 0; i < 10; i++)
    //    {
    //        var go = Managers.Resource.Instantiate($"{MONSTER_PATH}/{MONSTER_NAME}2");
    //        if (go != null)
    //        {
    //            go.transform.position = m_spawn_pos.position;

    //            var sc = go.GetComponent<Monster>();
    //            sc.SetDestination(m_goal_pos.position);

    //            Monsters.Add(sc);
    //        }

    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

    //public void OnCreate()
    //{
    //    if (m_select == null)
    //        return;

    //    if (Gold < 50)
    //        return;

    //    var ranIndex = UnityEngine.Random.Range(1, 3);
    //    var go = Managers.Resource.Instantiate($"{TOWER_PATH}/{TOWER_NAME}{ranIndex}");
    //    if (go == null)
    //        return;

    //    go.transform.SetParent(m_select.transform);
    //    go.transform.localPosition = new Vector3(0f, 1.1f, 0f);
    //    m_towers.Add(m_select);

    //    Gold -= 50;
    //}

    //public void OnUpgrade()
    //{
    //    //if (m_select == null)
    //    //    return;
    //}

    //public void OnDelete()
    //{
    //    //if (m_select == null)
    //    //    return;
    //}
}