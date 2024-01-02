using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private const string MONSTER_PATH = "Monster";
    private const string MONSTER_NAME = "Monster_";
    private const string TOWER_PATH = "Tower";
    private const string TOWER_NAME = "Tower_";


    [SerializeField] private Transform m_spawn_pos = null;
    [SerializeField] private Transform m_goal_pos = null;
    [SerializeField] private Transform m_tower_root = null;

    private int m_gold;
    private HashSet<GameObject> m_towers = new HashSet<GameObject>();
    private GameObject m_select;

    public Transform TowerRoot => m_tower_root; 
    public List<Monster> Monsters { get; set; } = new List<Monster>();
    public int Gold
    {
        get { return m_gold; }
        set 
        {
            m_gold = value;
            var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
            if (gui != null)
                gui.SetGold(Gold);
        }
    }

    private void Start()
    {
        Gold = 50;
        m_towers.Clear();

        StartCoroutine(CoSpawn());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var gui = Managers.UI.GetWindow(WindowID.UIWindowGame, false) as UIWindowGame;
                if (gui == null)
                    return;

                if (hit.transform.gameObject.CompareTag("Build"))
                {
                    m_select = hit.transform.gameObject;

                    // ÀÌ¹Ì ¼³Ä¡µÈ ¶¥
                    if (m_towers.Contains(hit.transform.gameObject))
                    {
                        gui.ShowFixed();
                    }
                    // ½Å±Ô ¶¥
                    else
                    {
                        gui.ShowCreate();
                    }
                }
                else
                {
                    gui.HideButton();
                }
            }
        }
    }

    private IEnumerator CoSpawn()
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < 10; i++)
        {
            var go = Managers.Resource.Instantiate($"{MONSTER_PATH}/{MONSTER_NAME}1");
            if (go != null)
            {
                go.transform.position = m_spawn_pos.position;

                var sc = go.GetComponent<Monster>();
                sc.SetDestination(m_goal_pos.position);

                Monsters.Add(sc);
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(4.5f);
        for (int i = 0; i < 10; i++)
        {
            var go = Managers.Resource.Instantiate($"{MONSTER_PATH}/{MONSTER_NAME}2");
            if (go != null)
            {
                go.transform.position = m_spawn_pos.position;

                var sc = go.GetComponent<Monster>();
                sc.SetDestination(m_goal_pos.position);

                Monsters.Add(sc);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnCreate()
    {
        if (m_select == null)
            return;

        if (Gold < 50)
            return;

        var ranIndex = UnityEngine.Random.Range(1, 3);
        var go = Managers.Resource.Instantiate($"{TOWER_PATH}/{TOWER_NAME}{ranIndex}");
        if (go == null)
            return;

        go.transform.SetParent(m_select.transform);
        go.transform.localPosition = new Vector3(0f, 1.1f, 0f);
        m_towers.Add(m_select);

        Gold -= 50;
    }

    public void OnUpgrade()
    {
        //if (m_select == null)
        //    return;
    }

    public void OnDelete()
    {
        //if (m_select == null)
        //    return;
    }
}