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
    [SerializeField] private Camera m_UnitCam = null;
    [SerializeField] private Camera m_UICam = null;
    [SerializeField] private GoogleSheetManager m_GoogleSheet = null;
    [SerializeField] private UIWindowManager m_UI = null;
    [SerializeField] private TableManager m_Table = null;
    [SerializeField] private SoundManager m_Sound = null;
    [SerializeField] private PoolManager m_Pool = null;
    [SerializeField] private ResourceManager m_Resource = null;
    [SerializeField] private UserManager m_User = null;
    [SerializeField] private BackendManager m_BackEnd = null;
    [SerializeField] private ObserverManager m_Observer = null;
    [SerializeField] private SpriteManager m_Sprite = null;
    [SerializeField] private ADManager m_AD = null;
    [SerializeField] private IAPManager m_IAP = null;

    public static Canvas UICanvas => GetInstance.m_UIRootCan;
    public static Transform Widget => GetInstance.m_Widget;
    public static Camera WorldCam => GetInstance.m_WorldCam;
    public static Camera UnitCam => GetInstance.m_UnitCam;
    public static Camera UICam => GetInstance.m_UICam;
    public static GoogleSheetManager GoogleSheet => GetInstance.m_GoogleSheet;
    public static UIWindowManager UI => GetInstance.m_UI;
    public static TableManager Table => GetInstance.m_Table;
    public static SoundManager Sound => GetInstance.m_Sound;
    public static PoolManager Pool => GetInstance.m_Pool;
    public static ResourceManager Resource => GetInstance.m_Resource;
    public static UserManager User => GetInstance.m_User;
    public static BackendManager BackEnd => GetInstance.m_BackEnd;
    public static SpriteManager Sprite => GetInstance.m_Sprite;
    public static ADManager AD => GetInstance.m_AD;
    public static ObserverManager Observer => GetInstance.m_Observer;
    public static IAPManager IAP => GetInstance.m_IAP;

    public void Init()
    {
        GoogleSheet.Init(AfterInit);
    }

    private void AfterInit()
    {
        Table.Init();
        Pool.Init();
        UI.Init();
        BackEnd.Init();
        AD.Init();
        User.Init();
        Sound.Init();
        IAP.Init();
    }

    public void Clear()
    {
        Table.Clear();
        UI.Clear();
        Pool.Clear();
    }

    private void Update()
    {
        //if (Input.touchCount == 3)
        //{
        //    Debug.LogError("3°³ µ¿½Ã ÅÍÄ¡");
        //}
    }
}
