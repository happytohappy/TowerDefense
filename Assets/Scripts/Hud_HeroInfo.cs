using UnityEngine;

public class Hud_HeroInfo : MonoBehaviour
{
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

    public void Set(Vector3 in_offset)
    {
        m_offset = in_offset;
    }
}
