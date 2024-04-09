using UnityEngine;
using TMPro;

public class Hud_Damage : MonoBehaviour
{
    [SerializeField] private TMP_Text m_text_damage = null;

    private float m_start_pos;
    private float m_end_pos;

    private void FixedUpdate()
    {
        var newYPos = this.transform.position.y + 0.01f;
        this.transform.position = new Vector3(this.transform.position.x, newYPos, 0f);

        if (newYPos - m_start_pos >= 0.5f)
            Managers.Resource.Destroy(this.gameObject);

        //if (Target == null)
        //    return;

        //var position = Managers.WorldCam.WorldToViewportPoint(Target.transform.position);
        //var view_position = Managers.UICam.ViewportToWorldPoint(position) + m_offset;
        //this.transform.position = new Vector3(view_position.x, view_position.y, 0f);
    }

    public void Set(Vector3 in_position, string in_damage)
    {
        var position = Managers.WorldCam.WorldToViewportPoint(in_position);
        var view_position = Managers.UICam.ViewportToWorldPoint(position);

        m_start_pos = view_position.y;
        this.transform.position = new Vector3(view_position.x, view_position.y, 0f);

        m_text_damage.text = in_damage;
    }
}
