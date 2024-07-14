using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIWindowTreasure : UIWindowBase
{
    private const string GROUP_TREASURE_PATH = "UI/Item/Group_Treasure";
   
    [Header("재화")]
    [SerializeField] private TMP_Text m_text_gold;
    [SerializeField] private TMP_Text m_text_ruby;
    [SerializeField] private TMP_Text m_text_diamond;

    [SerializeField] private Transform m_trs_root = null;
    [SerializeField] private RectTransform m_rect_root = null;

    [SerializeField] private List<Image> m_list_grade_star = new List<Image>();
    [SerializeField] private TMP_Text m_text_name = null;
    [SerializeField] private Image m_image_treasure = null;
    [SerializeField] private GameObject m_go_lock = null;
    [SerializeField] private TMP_Text m_text_desc = null;
    [SerializeField] private ExtentionButton m_btn_gradeup = null;
    [SerializeField] private TMP_Text m_text_gradeup = null;

    [Header("튜토리얼")]
    [SerializeField] private ExtentionButton m_go_treasure = null;
    [SerializeField] private ExtentionButton m_go_desc = null;

    public int SelectTreasure { get; set; }
    public GameObject LastSelect { get; set; }

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowTreasure;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        SelectTreasure = 1;
        RefreshUI();

        CheckTutorial();
    }

    private void CheckTutorial()
    {
        m_go_treasure.Ex_SetActive(false);
        m_go_desc.Ex_SetActive(false);

        if (Managers.User.UserData.ClearTutorial.Contains(8))
            return;

        // 보물 설명
        m_go_treasure.Ex_SetActive(true);
        Managers.Tutorial.TutorialStart(m_go_treasure.gameObject, ETutorialDir.Center, new Vector3(0f, 135f, 0f), "#NONE TEXT 보물 설명");
    }

    public void RefreshUI()
    {
        m_text_gold.Ex_SetText($"{Util.CommaText(Util.GetGoods(EGoods.Gold))}");
        m_text_ruby.Ex_SetText($"{Util.CommaText(Util.GetGoods(EGoods.Ruby))}");
        m_text_diamond.Ex_SetText($"{Util.CommaText(Util.GetGoods(EGoods.Diamond))}");

        for (int i = 0; i < m_trs_root.childCount; i++)
            Managers.Resource.Destroy(m_trs_root.GetChild(i).gameObject);

        var treasureGroup = Managers.Resource.Instantiate(GROUP_TREASURE_PATH, Vector3.zero, m_trs_root);
        var sc = treasureGroup.GetComponent<Group_Treasure>();
        sc.SetData();

        SetTreasure(SelectTreasure);
    }

    public void SetTreasure(int in_treasure)
    {
        SelectTreasure = in_treasure;

        var treasureInfo = Managers.Table.GetTreasureInfoData(in_treasure);
        var userTreasure = Managers.User.GetTreasureInfo(in_treasure);
        var treasureLevel = Managers.Table.GetTreasureLevelData(in_treasure, userTreasure);

        m_image_treasure.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Treasure, treasureInfo.m_icon));
        m_text_name.Ex_SetText(Util.SpecialString(Managers.Table.GetLanguage(treasureInfo.m_name)));
        Util.SetGradeStar(m_list_grade_star, userTreasure);
        m_text_desc.Ex_SetText(Util.SpecialString(Managers.Table.GetLanguage(treasureInfo.m_comment)));
        m_go_lock.Ex_SetActive(userTreasure == 0);

        if (userTreasure == 0 || treasureLevel == null)
        {
            m_btn_gradeup.Ex_SetActive(false);
            m_image_treasure.Ex_SetColor(Color.black);
        }
        else
        {
            m_btn_gradeup.Ex_SetActive(true);
            m_image_treasure.Ex_SetColor(Color.white);
            m_text_gradeup.Ex_SetText($"{Managers.User.GetInventoryItem(treasureLevel.m_item_kind)}/{treasureLevel.m_grade_up_piece}");
            m_btn_gradeup.interactable = Managers.User.GetInventoryItem(treasureLevel.m_item_kind) >= treasureLevel.m_grade_up_piece;
        }
    }

    public void OnClickTutoTreasure()
    {
        Managers.Tutorial.TutorialEnd();

        m_go_desc.Ex_SetActive(true);
        Managers.Tutorial.TutorialStart(m_go_desc.gameObject, ETutorialDir.Center, new Vector3(0f, 100f, 0f), "#NONE TEXT 보물 설명");
    }

    public void OnClickTutoDesc()
    {
        Managers.Tutorial.TutorialEnd();
        Managers.Tutorial.TutorialClear(8);
    }
}
