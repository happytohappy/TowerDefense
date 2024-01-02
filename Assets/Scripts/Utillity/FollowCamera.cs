using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private float m_Speed = 10.0f;

    public Transform Target { get; set; }

    private void LateUpdate()
    {
        if (Target == null)
            return;

        this.transform.position = Vector3.Lerp(transform.position, Target.position, m_Speed * Time.smoothDeltaTime);
    }
}
