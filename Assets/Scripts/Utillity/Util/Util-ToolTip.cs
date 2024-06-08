using UnityEngine;

public static partial class Util
{
    private const string TOOLTIP_PATH = "UI/Item/Tooltip_UnitSkillInfo";

    private static Transform in_tool_tip_parent = null;
    private static Tooltip_UnitSkillInfo m_tool_tip = null;

    public static void OpenToolTip(string in_contents, Transform in_parent)
    {
        if (m_tool_tip == null)
        {
            var toolTip = Managers.Resource.Instantiate(TOOLTIP_PATH, Vector3.zero, Managers.UICanvas.transform);
            m_tool_tip = toolTip.GetComponent<Tooltip_UnitSkillInfo>();
        }
        else
        {
            if (m_tool_tip.gameObject.activeInHierarchy)
                m_tool_tip.Ex_SetActive(false);

            if (in_tool_tip_parent == in_parent)
                return;
        }

        in_tool_tip_parent = in_parent;

        m_tool_tip.SetData(in_contents);
        m_tool_tip.Ex_SetActive(true);
        m_tool_tip.transform.SetParent(in_parent);
        m_tool_tip.transform.localPosition = Vector3.zero;
        m_tool_tip.transform.localScale = Vector3.one;
        m_tool_tip.transform.SetParent(Managers.UICanvas.transform);
    }

    public static void CloseToolTip()
    {
        if (m_tool_tip == null)
            return;

        m_tool_tip.Ex_SetActive(false);
    }
}