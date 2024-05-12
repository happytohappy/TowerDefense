using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIPopupTreasure : UIWindowBase
{
    private const string GROUP_TREASURE_PATH = "UI/Item/Group_Treasure";
   
    [Header("¿Á»≠")]
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

    public int SelectTreasure { get; set; }
    public GameObject LastSelect { get; set; }

    public override void Awake()
    {
        Window_ID = WindowID.UIPopupTreasure;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);

        SelectTreasure = 1;
        RefreshUI();
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
        m_text_name.Ex_SetText(treasureInfo.m_name);
        Util.SetGradeStar(m_list_grade_star, userTreasure);
        //m_text_desc.Ex_SetText(treasureInfo.m_comment);
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

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }
}
