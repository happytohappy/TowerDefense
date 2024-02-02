using UnityEngine;

public class Managers : MonoBehaviour
{
    #region ½Ì±ÛÅæ
    private static Managers Instance = null;
    public static Managers GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType(typeof(Managers)) as Managers;

            return Instance;
        }
    }
    #endregion

    [SerializeField] private Canvas m_UIRootCan = null;
    [SerializeField] private Transform m_Widget = null;
    [SerializeField] private Camera m_WorldCam = null;
    [SerializeField] private Camera m_UICam = null;
    [SerializeField] private UIWindowManager m_UI = null;
    [SerializeField] private TableManager m_Table = null;
    [SerializeField] private SoundManager m_Sound = null;
    [SerializeField] private PoolManager m_Pool = null;
    [SerializeField] private ResourceManager m_Resource = null;
    [SerializeField] private UserManager m_User = null;
    [SerializeField] private BackendManager m_BackEnd = null;

    public static Canvas UICanvas => GetInstance.m_UIRootCan;
    public static Transform Widget => GetInstance.m_Widget;
    public static Camera WorldCam => GetInstance.m_WorldCam;
    public static Camera UICam => GetInstance.m_UICam;
    public static UIWindowManager UI => GetInstance.m_UI;
    public static TableManager Table => GetInstance.m_Table;
    public static SoundManager Sound => GetInstance.m_Sound;
    public static PoolManager Pool => GetInstance.m_Pool;
    public static ResourceManager Resource => GetInstance.m_Resource;
    public static UserManager User => GetInstance.m_User;
    public static BackendManager BackEnd => GetInstance.m_BackEnd;

    public void Init()
    {
        Table.Init();
        Pool.Init();
        UI.Init();
        User.Init();
        BackEnd.Init();
    }

    public void Clear()
    {
        Table.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
