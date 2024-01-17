using System.Collections.Generic;
using UnityEngine;

public class ParentTab : MonoBehaviour
{
    [SerializeField] private List<ChildTab> m_ChildTab = new List<ChildTab>();

    private void Start()
    {
        SelectTab(0);
    }

    public void SelectTab(int _Number)
    {
        for (int i = 0; i < m_ChildTab.Count; i++)
        {
            int _BtnNumber = m_ChildTab[i].BtnNumber;
            if (_BtnNumber.Equals(_Number))
            {
                m_ChildTab[i].NormalSprite.Ex_SetActive(false);
                m_ChildTab[i].SelectSprite.Ex_SetActive(true);
                m_ChildTab[i].ActiveObject.Ex_SetActive(true);
            }
            else
            {
                m_ChildTab[i].NormalSprite.Ex_SetActive(true);
                m_ChildTab[i].SelectSprite.Ex_SetActive(false);
                m_ChildTab[i].ActiveObject.Ex_SetActive(false);
            }
        }
    }
}
