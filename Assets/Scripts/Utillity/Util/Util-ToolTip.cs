using UnityEngine;

public static partial class Util
{
    private const string TOOLTIP_PATH          = "UI/Item/Tooltip_UnitSkillInfo";
    private const string TOOLTIP_TUTORIAL_PATH = "UI/Item/Tooltip_Tutorial";

    private static Transform in_tool_tip_parent = null;
    private static Tooltip_UnitSkillInfo m_tool_tip = null;
    private static Tooltip_Tutorial m_tool_tip_tutorial = null;

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
        m_tool_tip.transform.SetParent(Managers.Tutorial.TutorialBG);
    }

    public static void CloseToolTip()
    {
        if (m_tool_tip == null)
            return;

        m_tool_tip.Ex_SetActive(false);
    }

    public static void OpenTutorialToolTip(ETutorialDir in_dir, Transform in_target, Vector3 in_offset, string in_contents)
    {
        if (m_tool_tip_tutorial == null)
        {
            var toolTipTutorial = Managers.Resource.Instantiate(TOOLTIP_TUTORIAL_PATH, Vector3.zero, Managers.UICanvas.transform);
            m_tool_tip_tutorial = toolTipTutorial.GetComponent<Tooltip_Tutorial>();
        }

        m_tool_tip_tutorial.SetData(in_dir, in_contents);
        m_tool_tip_tutorial.Ex_SetActive(true);
        m_tool_tip_tutorial.transform.SetParent(in_target);
        m_tool_tip_tutorial.transform.localPosition = in_offset;
        m_tool_tip_tutorial.transform.SetParent(Managers.Tutorial.TutorialBG);

        m_tool_tip_tutorial.transform.localPosition = new Vector3(m_tool_tip_tutorial.transform.localPosition.x, m_tool_tip_tutorial.transform.localPosition.y, 0);
        m_tool_tip_tutorial.transform.localScale = Vector3.one;
    }

    public static void CloseTutorialToolTip()
    {
        if (m_tool_tip_tutorial == null)
            return;

        m_tool_tip_tutorial.Ex_SetActive(false);
    }
}