using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private GameObject m_parent;
    [SerializeField] private Image      m_fill;

    private Vector3 m_offset = Vector3.zero;

    public Transform Target { get; set; }

    private void FixedUpdate()
    {
        if (Target == null)
            return;

        var position = Managers.WorldCam.WorldToViewportPoint(Target.position);
        var view_position = Managers.UICam.ViewportToWorldPoint(position) + m_offset;
        this.transform.position = new Vector3(view_position.x, view_position.y, 0f);
    }

    public void Set(Vector3 in_offset, Vector3 in_scale, Color in_color)
    {
        m_offset = in_offset;
        m_parent.transform.localScale = in_scale;
        m_fill.color = in_color;
    }

    public void SetHP(float in_hp_ratio)
    {
        m_fill.fillAmount = in_hp_ratio;
    }

    public void HPBarActive(bool in_active)
    {
        m_parent.SetActive(in_active);
    }
}