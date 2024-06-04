using UnityEngine;
using TMPro;

public class ObserverRedDot : MonoBehaviour
{
    [SerializeField] private EContent m_content = EContent.None;
    [SerializeField] private GameObject m_go = null;
    
    private void Start()
    {
        Managers.Observer.SetObserverRedDot(m_content, m_go);   
    }
}