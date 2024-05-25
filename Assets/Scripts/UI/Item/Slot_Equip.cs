using UnityEngine;
using UnityEngine.UI;

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

    public void SetData(long in_unique, int in_kind, bool in_mount, bool in_new, bool in_select)
    {
        var equip = Managers.Table.GetEquipInfoData(in_kind);
        if (equip == null)
            return;

        m_unique = in_unique;
        m_kind = in_kind;

        Util.SetEquipIcon(m_image_icon, equip.m_equip_icon);
        Util.SetEquipGradeBG(m_image_bg, equip.m_equip_grade);
        Util.SetUnitType(m_image_type, equip.m_hero_type);
        Util.SetEquipGradeTextImage(m_image_grade, equip.m_equip_grade);
        m_go_mount.Ex_SetActive(in_mount);
        m_go_select.Ex_SetActive(in_select);
        m_go_red.Ex_SetActive(in_new);

        if (in_select)
        {
            var ui = Managers.UI.GetWindow(WindowID.UIWindowEquipment, false) as UIWindowEquipment;
            if (ui == null)
                return;

            ui.LastSelect = m_go_select;
        }
    }

    public void OnClickEquip()
    {
        var equip = Managers.User.GetEquip(m_unique);
        if (equip == null)
            return;

        var ui = Managers.UI.GetWindow(WindowID.UIWindowEquipment, false) as UIWindowEquipment;
        if (ui == null)
            return;

        equip.m_new = false;
        m_go_red.Ex_SetActive(false);

        ui.LastSelect.Ex_SetActive(false);
        ui.SelectEquip(m_unique, m_kind);
        m_go_select.Ex_SetActive(true);

        ui.LastSelect = m_go_select;
    }
}