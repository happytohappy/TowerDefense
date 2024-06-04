using UnityEngine;
using TMPro;

public class ObserverGoods : MonoBehaviour
{
    [SerializeField] private EGoods m_goods = EGoods.None;
    [SerializeField] private TMP_Text m_text = null;
    
    private void Start()
    {
        Managers.Observer.SetObserverGoods(m_goods, m_text);   
    }
}