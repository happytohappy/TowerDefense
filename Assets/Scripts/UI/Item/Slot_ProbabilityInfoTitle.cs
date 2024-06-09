using UnityEngine;
using TMPro;

public class Slot_ProbabilityInfoTitle : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_text_title = null;

    public void SetData(string in_text)
    {
        m_text_title.Ex_SetText(in_text);
    }
}