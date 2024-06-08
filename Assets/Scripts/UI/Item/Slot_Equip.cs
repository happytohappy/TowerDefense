using UnityEngine;
using UnityEngine.UI;
using System;

public class Slot_Equip : MonoBehaviour
{
    [SerializeField] private Image m_image_icon = null;
    [SerializeField] private Image m_image_bg = null;
    [SerializeField] private Image m_image_type = null;
    [SerializeField] private Image m_image_grade = null;
    [SerializeField] private GameObject m_go_mount = null;
    [SerializeField] private GameObject m_go_select = null;
    [SerializeField] private GameObject m_go_red = null;

    private long m_unique;
    private int m_kind;
    private Action<int, long, GameObject> m_callback;

    public void SetData(long in_unique, int in_kind, bool in_mount, bool in_new, bool in_select, Action<int, long, GameObject> in_callback)
    {
        var equip = Managers.Table.GetEquipInfoData(in_kind);
        if (equip == null)
            return;

        m_unique = in_unique;
        m_kind = in_kind;
        m_callback = in_callback;

        Util.SetEquipIcon(m_image_icon, equip.m_equip_icon);
        Util.SetEquipGradeBG(m_image_bg, equip.m_equip_grade);
        Util.SetUnitType(m_image_type, equip.m_hero_type);
        Util.SetEquipGradeTextImage(m_image_grade, equip.m_equip_grade);
        m_go_mount.Ex_SetActive(in_mount);
        m_go_select.Ex_SetActive(in_select);
        m_go_red.Ex_SetActive(in_new);

        if (in_select)
            m_callback?.Invoke(m_kind, m_unique, m_go_select);
    }

    public void OnClickEquip()
    {
        m_go_select.Ex_SetActive(true);

        m_callback?.Invoke(m_kind, m_unique, m_go_select);
    }
}