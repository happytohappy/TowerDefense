using UnityEngine;
using TMPro;

public class Tooltip_UnitSkillInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text m_text_contents = null;

    public void SetData(string in_contents)
    {
        m_text_contents.Ex_SetText(in_contents);
    }
}