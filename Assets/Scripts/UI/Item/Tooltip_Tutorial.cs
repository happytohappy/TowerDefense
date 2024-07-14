using UnityEngine;
using TMPro;

public class Tooltip_Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject m_go_left = null;
    [SerializeField] private GameObject m_go_right = null;
    [SerializeField] private GameObject m_go_center = null;
    [SerializeField] private TMP_Text m_text_left = null;
    [SerializeField] private TMP_Text m_text_right = null;
    [SerializeField] private TMP_Text m_text_center = null;

    public void SetData(ETutorialDir in_dir, string in_contents)
    {
        m_go_left.Ex_SetActive(false);
        m_go_right.Ex_SetActive(false);
        m_go_center.Ex_SetActive(false);

        switch (in_dir)
        {
            case ETutorialDir.Left:
                m_go_left.Ex_SetActive(true);
                m_text_left.Ex_SetText(in_contents);
                break;
            case ETutorialDir.Right:
                m_go_right.Ex_SetActive(true);
                m_text_right.Ex_SetText(in_contents);
                break;
            case ETutorialDir.Center:
                m_go_center.Ex_SetActive(true);
                m_text_center.Ex_SetText(in_contents);
                break;
        }
    }
}
