using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    private const string MONSTER = "Monster";

    [SerializeField] private AudioClip m_audio_clip = null;

    private bool m_update = false;
    private Monster m_monster;
    private int m_ATK;

    private void Update()
    {
        if (m_update == false)
            return;

        if (m_monster == null || m_monster.gameObject.activeInHierarchy == false)
        {
            m_update = false;
            Managers.Resource.Destroy(this.gameObject);
            return;
        }

        this.transform.LookAt(m_monster.Pivot);
        this.transform.Translate(20.0f * Time.deltaTime * Vector3.forward);
    }

    public void SetData(Monster in_monster, int in_atk)
    {
        if (m_audio_clip != null)
            Managers.Sound.PlaySFX(m_audio_clip);

        m_monster = in_monster;
        m_ATK = in_atk;

        m_update = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(MONSTER) || other.gameObject == m_monster.gameObject)
        {
            if (m_monster == null)
            {
                Managers.Resource.Destroy(this.gameObject);
                return;
            }

            m_update = false;

            Util.CreateHudDamage(m_monster.transform.position, Util.CommaText(m_ATK));

            m_monster.OnHit(m_ATK);
            m_monster = null;

            Managers.Resource.Destroy(this.gameObject);
        }
    }
}
