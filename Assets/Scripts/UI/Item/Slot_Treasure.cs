using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Slot_Treasure : MonoBehaviour
{
    [SerializeField] private Image m_image_treasure = null;
    [SerializeField] private GameObject m_go_dot = null;
    [SerializeField] private GameObject m_go_select = null;
    [SerializeField] private List<Image> m_list_grade_star = new List<Image>();
    [SerializeField] private GameObject m_go_lock = null;

    private int m_treasure_kind;

    public void SetData(int in_treasure)
    {
        var uiTreasure = Managers.UI.GetWindow(WindowID.UIWindowTreasure, false) as UIWindowTreasure;
        if (uiTreasure == null)
            return;

        m_treasure_kind = in_treasure;

        var treasureInfo = Managers.Table.GetTreasureInfoData(in_treasure);
        var userTreasure = Managers.User.GetTreasureInfo(in_treasure);
        var treasureLevel = Managers.Table.GetTreasureLevelData(in_treasure, userTreasure);

        m_image_treasure.Ex_SetImage(Managers.Sprite.GetSprite(Atlas.Treasure, treasureInfo.m_icon));
        Util.SetGradeStar(m_list_grade_star, userTreasure);
        m_go_lock.Ex_SetActive(userTreasure == 0);
        m_go_select.Ex_SetActive(m_treasure_kind == uiTreasure.SelectTreasure);

        if (treasureLevel == null || treasureLevel == null)
        {
            m_image_treasure.Ex_SetColor(Color.black);
            m_go_dot.Ex_SetActive(false);
        }
        else
        {
            m_image_treasure.Ex_SetColor(Color.white);
            m_go_dot.Ex_SetActive(Managers.User.GetInventoryItem(treasureLevel.m_item_kind) >= treasureLevel.m_grade_up_piece);
        }

        if (m_treasure_kind == 1)
            uiTreasure.LastSelect = m_go_select;
    }

    public void OnClickTreasure()
    {
        var uiTreasure = Managers.UI.GetWindow(WindowID.UIWindowTreasure, false) as UIWindowTreasure;
        if (uiTreasure == null)
            return;

        uiTreasure.LastSelect.Ex_SetActive(false);
        m_go_select.Ex_SetActive(true);
        uiTreasure.LastSelect = m_go_select;

        uiTreasure.SetTreasure(m_treasure_kind);
    }
}