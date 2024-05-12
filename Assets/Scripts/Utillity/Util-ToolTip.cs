using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using System;

public static partial class Util
{
    private const string TOOLTIP_PATH = "UI/Item/Tooltip_UnitSkillInfo";

    private static Tooltip_UnitSkillInfo m_tool_tip = null;

    public static void OpenToolTip(string in_contents, Transform in_parent)
    {
        if (m_tool_tip == null)
        {
            var toolTip = Managers.Resource.Instantiate(TOOLTIP_PATH, Vector3.zero, Managers.UICanvas.transform);
            m_tool_tip = toolTip.GetComponent<Tooltip_UnitSkillInfo>();
        }

        m_tool_tip.SetData(in_contents);
        m_tool_tip.gameObject.Ex_SetActive(true);
        m_tool_tip.transform.SetParent(in_parent);
        m_tool_tip.transform.localPosition = Vector3.zero;
        m_tool_tip.transform.localScale = Vector3.one;
        m_tool_tip.transform.SetParent(Managers.UICanvas.transform);
    }

    public static void CloseToolTip()
    {
        if (m_tool_tip == null)
            return;

        {
            var ui = Managers.UI.GetWindow(WindowID.UIWindowUnit, false) as UIWindowUnit;
            if (ui != null)
                ui.ToolTipIndex = -1;
        }

        {
            var ui = Managers.UI.GetWindow(WindowID.UIPopupUnit, false) as UIPopupUnit;
            if (ui != null)
                ui.ToolTipIndex = -1;
        }

        m_tool_tip.gameObject.Ex_SetActive(false);
    }
}