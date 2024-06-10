using UnityEngine;
using System.Collections.Generic;

public class Slot_Recruit : MonoBehaviour
{
    private const string ANI_BACK_0 = "Ani_UIWindowRecruit_Open01";
    private const string ANI_BACK_1 = "Ani_UIWindowRecruit_Open02";
    private const string ANI_BACK_2 = "Ani_UIWindowRecruit_Open03";

    [SerializeField] private Animator m_ani = null;
    [SerializeField] private Slot_Equip m_slot_equip = null;
    [SerializeField] private UnitIcon m_slot_hero = null;
    [SerializeField] private List<GameObject> m_list_card_back = new List<GameObject>();

    private int m_back;
    private bool m_open;

    public void SetData(EItemType in_item_type, int in_item_kind, int in_back)
    {
        m_open = true;
        m_back = in_back;
        m_slot_equip.Ex_SetActive(false);
        m_slot_hero.Ex_SetActive(false);

        for (int i = 0; i < m_list_card_back.Count; i++)
            m_list_card_back[i].Ex_SetActive(i == in_back);

        switch (in_item_type)
        {
            case EItemType.HERO:
                m_slot_hero.Ex_SetActive(true);
                m_slot_hero.SetDataRecruit(in_item_kind);
                break;
            case EItemType.EQUIP:
                m_slot_equip.Ex_SetActive(true);
                m_slot_equip.SetData(0, in_item_kind, false, false, false, null);
                break;
        }
    }

    public void OnClickOpen()
    {
        if (m_open == false)
            return;

        m_open = false;

        switch (m_back)
        {
            case 0: m_ani.Ex_Play(ANI_BACK_0); break;
            case 1: m_ani.Ex_Play(ANI_BACK_1); break;
            case 2: m_ani.Ex_Play(ANI_BACK_2); break;
        }
    }
}