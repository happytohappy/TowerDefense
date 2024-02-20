using UnityEngine;
using TMPro;

public class LocalizationText : MonoBehaviour
{
    [SerializeField] private TMP_Text m_text = null;
    [SerializeField] private string m_lan_key = null;

    private void Awake()
    {
        SetLanguage();
        Managers.Table.LocalizationTextList.Add(this);
    }

    public void SetLanguage()
    {
        m_text.text = Util.SpecialString(Managers.Table.GetLanguage(this.m_lan_key));
    }
}
