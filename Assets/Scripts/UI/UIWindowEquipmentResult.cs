using UnityEngine;
using TMPro;

public class UIWindowEquipmentResult : UIWindowBase
{
    [Header("Equip")]
    [SerializeField] private TMP_Text m_text_equip_name = null;
    [SerializeField] private Slot_Equip m_equip_slot = null;

    [Header("Result Root")]
    [SerializeField] private GameObject m_go_success = null;
    [SerializeField] private GameObject m_go_fail = null;

    [Header("Stat")]
    [SerializeField] private TMP_Text m_text_atk = null;
    [SerializeField] private TMP_Text m_text_speed = null;
    [SerializeField] private TMP_Text m_text_range = null;
    [SerializeField] private TMP_Text m_text_critical = null;
    [SerializeField] private TMP_Text m_text_critical_chance = null;

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowEquipmentResult;
        Window_Mode = WindowMode.WindowOverlay | WindowMode.WindowJustClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam in_param)
    {
        base.OpenUI(in_param);

        if (in_param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        var param = in_param as EquipResultParam;
        if (param == null)
        {
            Managers.UI.CloseLast();
            return;
        }

        if (param.m_success)
        {
            m_go_success.Ex_SetActive(true);
            m_go_fail.Ex_SetActive(false);

            SetEquipInfo(param.m_equip_kind);
        }
        else
        {
            m_go_success.Ex_SetActive(false);
            m_go_fail.Ex_SetActive(true);
        }

    }

    public void OnClickClose()
    {
        Managers.UI.CloseLast();
    }

    public void SetEquipInfo(int in_equip_kind)
    {
        var tableEquip = Managers.Table.GetEquipInfoData(in_equip_kind);
        if (tableEquip == null)
            return;

        m_text_equip_name.Ex_SetText($"{tableEquip.m_equip_grade} {Util.SpecialString(Managers.Table.GetLanguage(tableEquip.m_name))}");
        m_equip_slot.SetData(0, in_equip_kind, false, false, false, null);

        // ¿Â∫Ò Ω∫≈»
        m_text_atk.Ex_SetText($"{tableEquip.m_atk}");
        m_text_speed.Ex_SetText($"{tableEquip.m_speed}");
        m_text_range.Ex_SetText($"{tableEquip.m_range}");
        m_text_critical.Ex_SetText($"{tableEquip.m_critical}");
        m_text_critical_chance.Ex_SetText($"{tableEquip.m_critical_chance}");
    }
} 