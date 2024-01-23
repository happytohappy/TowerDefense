using UnityEngine;
using UnityEngine.UI;

public class ChildTab : MonoBehaviour
{
    [SerializeField] private Image m_NormalSprite = null;
    [SerializeField] private Image m_SelectSprite = null;
    [SerializeField] private GameObject m_ActiveObject = null;
    [SerializeField] private RectTransform m_ScrollRect = null;
    [SerializeField] private int m_BtnNumber = 0;

    public Image NormalSprite { get { return m_NormalSprite; } }
    public Image SelectSprite { get { return m_SelectSprite; } }
    public GameObject ActiveObject { get { return m_ActiveObject; } }
    public RectTransform ScrollRect { get { return m_ScrollRect; } }
    public int BtnNumber { get { return m_BtnNumber; } }
}
